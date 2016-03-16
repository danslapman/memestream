#load "imports.fsx"
#load "vk.fsx"

open System
open VK
open VK.Domain

let moscowArea: Area = { Latitude = 55.7522200; Longitude = 37.6155600; Radius = 6000 }

let slice = searchPhoto moscowArea (DateTime(2016, 3, 16, 13, 3, 0)) (DateTime(2016, 3, 16, 13, 4, 0)) 

printfn "%i" slice.Data.Count

for photo in slice.Data.Items do
    printfn "%A" photo
