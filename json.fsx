#load "imports.fsx"

open Newtonsoft.Json
open FSharp.Reflection
open System

type OptionConverter() =
    inherit JsonConverter()
    override self.CanConvert(objectType: System.Type) : bool =
        objectType.IsGenericType && objectType.GetGenericTypeDefinition() = typedefof<option<_>>
    override self.WriteJson(writer: JsonWriter, value: obj, serializer: JsonSerializer) : unit =
        let value = 
            if value = null then null
            else 
                let _,fields = FSharpValue.GetUnionFields(value, value.GetType())
                fields.[0]  
        serializer.Serialize(writer, value)
    override self.ReadJson(reader: JsonReader, objectType: System.Type, existingValue: obj, serializer: JsonSerializer) : obj =
        let innerType = objectType.GetGenericArguments().[0]
        let innerType = 
            if innerType.IsValueType then (typedefof<Nullable<_>>).MakeGenericType([|innerType|])
            else innerType        
        let value = serializer.Deserialize(reader, innerType)
        let cases = FSharpType.GetUnionCases(objectType)
        if value = null then FSharpValue.MakeUnion(cases.[0], [||])
        else FSharpValue.MakeUnion(cases.[1], [|value|])
        
let jsonConfig = 
    let cfg = JsonSerializerSettings()
    cfg.Converters.Add(OptionConverter())
    cfg