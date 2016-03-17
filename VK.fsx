module VK
#load "imports.fsx"
#load "vk.domain.fsx"
#load "utils.fsx"
#load "json.fsx"

open System
open FSharp.Data
open Json
open Newtonsoft.Json
open VK.Domain
open Utils

let russiaCid = "1"
let moscowCid = "1"

let searchPhoto query st et =
    let startTs = toUnixTime st |> int64
    let endTs = toUnixTime et |> int64
    let data = 
        Http.RequestString(
            sprintf "https://api.vk.com/method/photos.search?v=5.50&sort=0&q=%s&count=1000&start_time=%i&end_time=%i" query startTs endTs
        )
    JsonConvert.DeserializeObject<VkResponse<VkCollection<Photo>>>(data, jsonConfig)

let searchPhotoInArea (area: Area) st et =
    let startTs = toUnixTime st |> int64
    let endTs = toUnixTime et |> int64
    let data = 
        Http.RequestString(
            sprintf "https://api.vk.com/method/photos.search?v=5.50&sort=0&count=1000&lat=%f&long=%f&radius=%i&start_time=%i&end_time=%i" area.Latitude area.Longitude area.Radius startTs endTs
        )
    JsonConvert.DeserializeObject<VkResponse<VkCollection<Photo>>>(data, jsonConfig)
    
let searchFeed st et (next: string option) =
    let startTs = toUnixTime st |> int64
    let endTs = toUnixTime et |> int64
    
    let defParameters = [("v", "5.50"); ("extended", "1"); ("count", "200"); ("start_time", sprintf "%i" startTs); ("end_time", sprintf "%i" endTs); ("fields", "place,city,country")]
    
    let parameters =
        match next with
        | None -> defParameters
        | Some(nid) -> ("start_from", nid) :: defParameters
    
    parameters
    |> List.map (fun el -> sprintf "%s=%s" (fst el) (snd el)) |> String.concat "&"
    |> (fun s -> sprintf "https://api.vk.com/method/newsfeed.search?%s" s) 
    //|> (fun s -> printfn "%s" s; s)
    |> Http.RequestString 
    |> (fun s -> JsonConvert.DeserializeObject<VkResponse<ExtendedVkCollection<NewsfeedEntry>>>(s, jsonConfig))
   
let searchFeedAggr st et =
    let mutable res = searchFeed st et None
    
    while not (String.IsNullOrEmpty(res.Data.NextFrom)) do
        let resPart = searchFeed st et (Some(res.Data.NextFrom))
        res <- {
            Data = 
            {
                Items = List.append res.Data.Items resPart.Data.Items
                Count = resPart.Data.Count
                Users = List.append res.Data.Users resPart.Data.Users
                Groups = List.append res.Data.Groups resPart.Data.Groups
                NextFrom = resPart.Data.NextFrom
            }
        }     
    res