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
let sd = day + Time.``13``.``03``.``00``
let ed = day + Time.``13``.``04``.``00``

//let slice = searchPhoto moscowArea sd ed
let slice = searchFeed sd ed None

printfn "%i" slice.Data.Count

//for photo in slice.Data.Items do
    //printfn "%A" photo
