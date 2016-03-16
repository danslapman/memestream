#load "imports.fsx"
#load "vk.fsx"

open System
open System.IO
open FSharp.Date
open VK
open VK.Domain

type Date = DateProvider<epoch=2010>
type Time = TimeProvider

let day = Date.``2016``.``03``.``16``
let sd = day + Time.``17``.``49``.``00``
let ed = day + Time.``17``.``51``.``00``

let slice = searchFeedAggr sd ed
    
printfn "%i" slice.Data.Count    
   
slice.Data.Items |> List.exists (fun u -> u.Text.Contains("Это Семушка")) |> printfn "%b"

(*
using (File.CreateText("data.json")) (fun writer ->
    for item in slice.Data.Items do
        fprintfn writer "%A"  item)*)

(*
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
                
            
printfn "%i/%i" results.Length slice.Data.Count*)