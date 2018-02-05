module NpgsqlConnectionSamples

open FSharp.Data.Npgsql
open Connection
open System

type DvdRental = NpgsqlConnection<dvdRental>

let ``Basic query``() = 
    use cmd = DvdRental.CreateCommand<"SELECT title, release_year FROM public.film LIMIT 3">(dvdRental)

    for x in cmd.Execute() do   
        printfn "Movie '%s' released in %i." x.title x.release_year.Value

let ``Parameterized query``() = 
    use cmd = DvdRental.CreateCommand<"SELECT title FROM public.film WHERE length > @longer_than">(dvdRental)

    let longerThan = TimeSpan.FromHours(3.)

    cmd.Execute(longer_than = int16 longerThan.TotalMinutes) 
    |> Seq.toList 
    |> printfn "Movies longer than %A:\n%A" longerThan
