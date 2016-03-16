module VK.Domain
#load "imports.fsx"

open Newtonsoft.Json

type VkResponse<'t> = {
    [<JsonProperty("response")>]
    Data: 't
}

type VkCollection<'t> = {
    [<JsonProperty("count")>] Count: int64
    [<JsonProperty("items")>] Items: List<'t>
}

type VkCounter = {
    [<JsonProperty("count")>] Count: int64
}

type Area = {
    Latitude: double
    Longitude: double
    Radius: int32
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

type PostedPhoto = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("owner_id")>] OwnerId: int64
    [<JsonProperty("photo_130")>] Photo130: string
    [<JsonProperty("photo_604")>] Photo604: string
}

type GeoUnit = {
    [<JsonProperty("id")>] Id: uint16
    [<JsonProperty("title")>] Title: string
}

type User = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("first_name")>] FirstName: string
    [<JsonProperty("last_name")>] LastName: string
    [<JsonProperty("city")>] City: GeoUnit
    [<JsonProperty("country")>] Country: GeoUnit
}

type GeoPlace = {
    [<JsonProperty("pid")>] Id: uint64
    [<JsonProperty("title")>] Title: string
    [<JsonProperty("latitude")>] Latitude: int16
    [<JsonProperty("longitude")>] Longitude: int16
    [<JsonProperty("created")>] Created: uint64
    [<JsonProperty("icon")>] Icon: string
    [<JsonProperty("type")>] Type: string
    [<JsonProperty("address")>] Address: string
    [<JsonProperty("country")>] Country: uint16
    [<JsonProperty("city")>] City: uint16
}

type Group = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("name")>] Name: string
    [<JsonProperty("screen_name")>] ScreenName: string
    [<JsonProperty("city")>] City: uint16
    [<JsonProperty("country")>] Country: uint16
    [<JsonProperty("place")>] Place: GeoPlace
}

type GeoMark = {
    [<JsonProperty("type")>] Type: string
    /// "lat lon"
    [<JsonProperty("coordinates")>] Coordinates: string
    [<JsonProperty("place")>] Place: GeoPlace
    [<JsonProperty("showmap")>] Showmap: bool
}

type MediaAttachment = {
    [<JsonProperty("type")>] Type: string
    [<JsonProperty("photo")>] Photo: Photo
    [<JsonProperty("posted_photo")>] PostedPhoto: PostedPhoto
}

type NewsfeedEntry = {
    [<JsonProperty("id")>] Id: uint64
    [<JsonProperty("owner_id")>] OwnerId: int64
    [<JsonProperty("from_id")>] FromId: int64
    [<JsonProperty("date")>] Date: uint64
    [<JsonProperty("text")>] Text: string
    [<JsonProperty("comments")>] Comments: VkCounter
    [<JsonProperty("likes")>] Likes: VkCounter
    [<JsonProperty("reposts")>] Reposts: VkCounter
    [<JsonProperty("attachment")>] Attachment: MediaAttachment
    [<JsonProperty("attachments")>] Attachments: List<MediaAttachment>
}

type NewsfeedVkResponse<'t> = {
    [<JsonProperty("response")>]
    Data: 't
    [<JsonProperty("profiles")>]
    User: List<User>
    [<JsonProperty("groups")>]
    Groups: List<Group>
    [<JsonProperty("new_from")>]
    NewFrom: string
}