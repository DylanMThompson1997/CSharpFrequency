using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

public class Read {
    public static void Main(String[] args) {
        try {
            var fname = args[0];
			int results; //= Convert.ToInt32(args[1]);
			if (args.Length == 2){
				results = Convert.ToInt32(args[1]);
			} else {
				results = 3;
			}
			if (results <= 0) {
				Console.WriteLine($"*** Error: Value is negative");
				Environment.ExitCode = 1;
				return;
			}
            var text = File.ReadAllText (fname);
            Regex rgx = new Regex ("[^A-Za-z]");
            var words = rgx.Replace(text, " ").Trim().Split(' ');
			Frequencies(words,results).ToList().ForEach(x => {Console.WriteLine ("{0} {1}", x.Item1, x.Item2);}  );
        } catch (Exception ex) {
            Console.WriteLine ($"*** Error: {ex.Message}");
            Environment.ExitCode = 1;
        } finally {
            ;
        }
	
		IEnumerable<(int, string)> Frequencies (string[] words, int results) {
			var frs = (words
				.GroupBy (s => s.ToUpper())
				.Select (g => (g.Count(), g.Key))
				.Where(x => x.Item2 != "")
				.OrderByDescending (kc => kc.Item1)
				.ThenBy (kc => kc.Item2)
				.ToArray()
				) .Take(results);
			return frs;
		}
	}	
}