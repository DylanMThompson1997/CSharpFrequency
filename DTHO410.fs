open System
open System.IO
open System.Text.RegularExpressions

let Frequencies (words: list<string>) =
    words
        |> Seq.groupBy (fun w -> w.ToUpper())           
        |> Seq.map (fun (k, s) -> (Seq.length s, k))    
        |> Seq.sortByDescending (fun (c, k) -> c)        

try
    let fname = "test1.txt"
    let text = File.ReadAllText fname
    let rgx = Regex @"[^A-Z a-z]"
    let words = rgx.Replace(text, " ").Trim().Split(' ')
    let data2 = words |> Array.toList
    let frs = Frequencies data2
    frs |> Seq.iter (fun w -> printf "%i %s \n" <|| w)
  
with ex ->
    printfn "*** Error: %s" ex.Message
    Environment.ExitCode <- 1