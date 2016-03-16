open System

let fromUnixTime ts = DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc).AddSeconds(ts).ToLocalTime()
let toUnixTime dt = (TimeZoneInfo.ConvertTimeToUtc(dt) - DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds