module VK.Domain
#load "imports.fsx"

open Newtonsoft.Json

type VkResponse<'t> = {
    [<JsonProperty("response")>]
    Data: 't
}

type VkCollection<'t> = {
    [<JsonProperty("count")>] Count: int64
    [<JsonProperty("items")>] Items: ResizeArray<'t>
}

type Photo = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("album_id")>] AlbumId: int64
    [<JsonProperty("owner_id")>] OwnerId: int64
    [<JsonProperty("user_id")>] UserId: uint64
    [<JsonProperty("text")>] Text: string
    [<JsonProperty("date")>] Date: int64
    [<JsonProperty("photo_75")>] Photo75: string
    [<JsonProperty("photo_130")>] Photo130: string
    [<JsonProperty("photo_604")>] Photo604: string
    [<JsonProperty("photo_807")>] Photo807: string
    [<JsonProperty("photo_1280")>] Photo1280: string
    [<JsonProperty("photo_2560")>] Photo2560: string
    [<JsonProperty("width")>] Width: int32
    [<JsonProperty("height")>] Height: int32
}

type GeoUnit = {
    [<JsonProperty("id")>] Id: uint16
    [<JsonProperty("title")>] Title: string
}

type User = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("first_name")>] FirstName: string
    [<JsonProperty("last_name")>] LastName: string
    [<JsonProperty("deactivated")>] Deactivated: bool
    [<JsonProperty("city")>] City: GeoUnit
    [<JsonProperty("country")>] Country: GeoUnit
}

type Place = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("title")>] Title: string
    [<JsonProperty("latitude")>] Latitude: int16
    [<JsonProperty("longtitude")>] Longtitude: int16
    [<JsonProperty("type")>] Type: string
    [<JsonProperty("country")>] Country: uint16
    [<JsonProperty("city")>] City: uint16
    [<JsonProperty("address")>] Address: string
}

type Group = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("name")>] Name: string
    [<JsonProperty("deactivated")>] Deactivated: bool
    [<JsonProperty("city")>] City: uint16
    [<JsonProperty("country")>] Country: uint16
    [<JsonProperty("place")>] Place: Place
}

type GeoPlace = {
    [<JsonProperty("pid")>] Id: uint64
    [<JsonProperty("title")>] Title: string
    [<JsonProperty("latitude")>] Latitude: int16
    [<JsonProperty("longtitude")>] Longtitude: int16
    [<JsonProperty("created")>] Created: uint64
    [<JsonProperty("icon")>] Icon: string
    [<JsonProperty("type")>] Type: string
    [<JsonProperty("address")>] Address: string
    [<JsonProperty("country")>] Country: uint16
    [<JsonProperty("city")>] City: uint16
}

type GeoMark = {
    [<JsonProperty("type")>] Type: string
    [<JsonProperty("coordinates")>] Coordinates: string //TODO: check coordinate format
    [<JsonProperty("place")>] Place: GeoPlace
    [<JsonProperty("showmap")>] Showmap: bool
}

type FeedEntry = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("owner_id")>] OwnerId: int64
    [<JsonProperty("from_id")>] FromId: uint64
    [<JsonProperty("date")>] Date: uint64
    [<JsonProperty("text")>] Text: string
}