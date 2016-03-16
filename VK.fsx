module VK
#load "imports.fsx"
#load "vk.domain.fsx"
#load "utils.fsx"

open System
open FSharp.Data
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
            sprintf "https://api.vk.com/method/photos.search?v=5.50&q=%s&sort=0&count=1000&start_time=%i&end_time=%i" query startTs endTs
        )
    JsonConvert.DeserializeObject<VkResponse<VkCollection<Photo>>>(data)
   