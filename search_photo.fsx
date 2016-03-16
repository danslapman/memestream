#load "imports.fsx"
#load "vk.fsx"

open System
open FSharp.Date
open VK
open VK.Domain

type Date = DateProvider<epoch=2010>
type Time = TimeProvider

let moscowArea: Area = { Latitude = 55.7522200; Longitude = 37.6155600; Radius = 6000 }
let day = Date.``2016``.``03``.``16``
let sd = day + Time.``17``.``49``.``00``
let ed = day + Time.``17``.``51``.``00``

let slice = searchPhoto moscowArea sd ed

let found =
    slice.Data.Items
    |> List.exists (fun ph -> ph.Text = "Это Семушка")
    
printfn "%b" found