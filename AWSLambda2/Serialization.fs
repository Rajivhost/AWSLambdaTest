[<AutoOpen>]
module AWSLambda2.Serialization

open FsCodec.NewtonsoftJson
open Newtonsoft.Json
open Giraffe.Serialization

let converters = [| Fable.JsonConverter() :> JsonConverter |]
let settings = Settings.Create(converters)
//let jsonSerializer = settings |> NewtonsoftJsonSerializer

let serialize<'data> (data: 'data) = Serdes.Serialize(data, settings)
let deserialize<'data> (data: string): 'data = Serdes.Deserialize(data, settings)

let inline ser data = Serdes.Serialize(data, settings)
let inline des<'data> data = Serdes.Deserialize<'data>(data, settings)

type FableJsonSerializer() =
    inherit Amazon.Lambda.Serialization.Json.JsonSerializer(converters)
