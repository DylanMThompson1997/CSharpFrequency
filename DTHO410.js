"use strict"

var fs = require('fs')
var Enumerable = require('linq-es2015')

function Read(args) {
    let fname = args[2]
    let text = fs.readFileSync(fname, 'utf8') //.toString()
    let words = text.replace(/[^A-Za-z]/g, " ").trim().split(' ')
	return words
}

function Frequencies (words, args) {
	if (Number.isInteger(parseInt(args[3])) == false) {
		console.log ('*** Error: Value is of the wrong type')
	}
	if (parseInt(args[3]) <= 0) {
		console.log ('*** Error: Value is zero or negative')
	}
	
    let frs = Enumerable.asEnumerable(words)
        .GroupBy (s => s.toUpperCase()) 
        .Select (g => [g.length, g.key])
        .OrderByDescending (kc => kc[1])		
        .OrderByDescending (kc => kc[0])
		.Take(args[3])
        .ToArray ()
    return frs
}

function Table(words, args) {
    let frs = Frequencies(words, args)
    frs.forEach(kc => console.log (kc[0].toString(), kc[1]))
}

function Main(args) {
	try {
		var words = Read(args)
		Table(words, args)
	} catch (ex) {
		console.log ('*** Error: ' + ex.message)
	} finally {
		;
	}
}

let args = process.argv
if (args.length == 3) {
	args.push('3');
}
Main(args)