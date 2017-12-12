namespace FeedDevs.Controllers

open System;
open Microsoft.AspNetCore.Mvc;

type WeatherForecast = {
    DateFormatted : string
    TemperatureC: int
    Summary: string
} with 
    member this.TemperatureF with
        get() = 32 + int ((float this.TemperatureC) / 0.5556)


[<Route("api/[controller]")>]
type SampleDataController () = 
    inherit Controller()

    static let Summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

    [<HttpGet("[action]")>]
    member this.WeatherForecasts() = 
        let rng = new Random()

        seq {0 .. 5}
        |> Seq.map (fun index -> {
            DateFormatted = DateTime.Now.AddDays(float index).ToString()
            TemperatureC = rng.Next(-20, 55)
            Summary = Summaries.[rng.Next(Summaries.Length)]
        }) 