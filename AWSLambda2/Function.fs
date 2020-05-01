namespace AWSLambda2


open Amazon.Lambda.Core

open System
open Serilog
open AWS.Logger
open AWS.Logger.SeriLog
open Serilog.Formatting.Compact

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
//[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>)>]
[<assembly: LambdaSerializer(typeof<FableJsonSerializer>)>]
()

type CreateCustomerInput = { Name: string }

//[<CLIMutable>]
type Payload = { Field : string; Arguments : string; Source : string option }

//[<CLIMutable>]
type AppSyncEvent = { Version : string; Operation : string; Payload : Payload }

//[<CLIMutable>]
type Customer = { Id: string; Name: string }

type Function() =
    let awsLogConfig = new AWSLoggerConfig("TestLambda")
    
    let logFormatter = RenderedCompactJsonFormatter()
    let logger = LoggerConfiguration()
                        // add native destructuring
                        .Destructure.FSharpTypes()
                        // from Serilog.Sinks.Console
                        .WriteTo.Console()
                        .WriteTo.AWSSeriLog(awsLogConfig, textFormatter=logFormatter)
                        .CreateLogger();

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    member __.FunctionHandler (event: AppSyncEvent) (_: ILambdaContext) =

        let message = sprintf "%A" event

        logger.Information(message)

        //{ Id = Guid.NewGuid().ToString(); Name = "Jane Doe" }

        //match input.Payload with
        //| null -> { Id = Guid.Empty.ToString(); Name = "null" }
        //| _ -> 
        //    { Id = Guid.NewGuid().ToString(); Name = input.Payload }

        //()

        Guid.NewGuid()
            