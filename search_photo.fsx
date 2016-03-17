#load "imports.fsx"
#load "vk.fsx"

open System
open FSharp.Date
open VK
open VK.Domain

type Date = DateProvider<epoch=2010>
type Time = TimeProvider

let moscowArea: Area = { Latitude = 55.7522200; Longitude = 37.6155600; Radius = 6000 }
let day = Date.``2016``.``03``.``17``
let sd = day + Time.``11``.``49``.``00``
let ed = day + Time.``11``.``53``.``00``

let slice = searchPhotoInArea moscowArea sd ed

printfn "%i" slice.Data.Count

let found =
    slice.Data.Items
    |> List.exists (fun ph -> ph.Text.Contains "vgtt")
    
printfn "%b" found

for item in slice.Data.Items do
    printfn "%A" item