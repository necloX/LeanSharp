# LeanSharp

LeanSharp is a work-in-progress library for interacting with the Lean theorem prover ecosystem. The library contain a mostly complete parser for the Lean export format, which can be used to extract information from Lean files and create a C# object representation of the data. Additionally, it can serialize the objects into a JSON format.

## Usage
The following code snippet shows how to use the parser to extract information from a Lean export file and print it in JSON format:
```
// Read the lines of the export file
var Stream = File.ReadLines(@"export.out");

// Create a new parser with the file's lines as input
Parser parser = new Parser(Stream);

// Parse the file
parser.Parse();

// Create options for the JSON serializer
var options = new JsonSerializerOptions();
options.WriteIndented = true;

// Serialize the theory's inductive types to JSON
string jsonTheory = JsonSerializer.Serialize(parser.Theory.Inductives, options);

// Print the JSON representation
Console.WriteLine("Inductive types:");
Console.WriteLine(jsonTheory);
```
The `Parser` class takes a stream of the source file as input. After parsing the input, it creates a `Theory` object which holds all the Inductive definitions, functions, axioms and quotient.

## Contribution

If you want to contribute to the project, you can do it by submitting a pull request.

## License

LeanSharp is released under the MIT License. See LICENSE for details.
