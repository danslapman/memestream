#load "imports.fsx"
#load "vk.fsx"

open System
open VK

let slice = searchPhoto "Москва" (DateTime(2016, 3, 16, 13, 3, 0)) (DateTime(2016, 3, 16, 13, 4, 0)) 

printfn "%i" slice.Data.Count

