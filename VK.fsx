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

let searchPhoto (area: Area) st et =
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
        | Some(nid) -> ("start_id", nid) :: defParameters
    
    parameters
    |> List.map (fun el -> sprintf "%s=%s" (fst el) (snd el)) |> String.concat "&"
    |> (fun s -> sprintf "https://api.vk.com/method/newsfeed.search?%s" s) 
    |> Http.RequestString 
    |> (fun s -> JsonConvert.DeserializeObject<NewsfeedVkResponse<VkCollection<NewsfeedEntry>>>(s, jsonConfig))
   