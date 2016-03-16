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

let users = slice.Data.Users |> List.map (fun u -> u.Id, u) |> Map.ofList
let groups = slice.Data.Groups |> List.map (fun g -> g.Id, g) |> Map.ofList

let mutable results: NewsfeedEntry list = []
for photo in slice.Data.Items do
    if (photo.Attachments.IsSome) then
        if (photo.OwnerId < 0L) then
            let group = groups.[abs photo.OwnerId |> uint64]
            if (group.Place.IsSome || group.City.IsSome) then
                results <- photo :: results
        else
            let user = users.[photo.OwnerId |> uint64]
            if (user.City.IsSome) then
                results <- photo :: results
                
            
printfn "%i/%i" results.Length slice.Data.Count