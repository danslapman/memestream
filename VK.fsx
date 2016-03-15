module VK
#load "imports.fsx"
#load "vk.domain.fsx"

open FSharp.Data
open Newtonsoft.Json
open VK.Domain

let russiaCid = "1"
let moscowCid = "1"

let searchPhoto q =
    let data = 
        Http.RequestString(
            sprintf "https://api.vk.com/method/photos.search?v=5.50&q=%s&sort=0&start_time=" q
        )
    JsonConvert.DeserializeObject<VkResponse<VkCollection<Photo>>>(data)
   