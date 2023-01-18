using LeanSharp.ExportParsing.Factory;
using LeanSharp.LeanObject;

namespace LeanSharp.ExportParsing
{
    /// <summary>
    /// The Lean export file parser
    /// </summary>
    public class Parser
    {
        public IEnumerable<string> Stream { get; init; }
        public Theory Theory;
        public Parser(IEnumerable<string> stream) { Stream = stream; }
        /// <summary>
        /// Parses the source file as a stream and constructs a theory from the data.
        /// </summary>
        public void Parse()
        {
            
            var parseState = new ParseState();
            int i = 0;
            Theory = new Theory();
            foreach (var line in Stream)
            {
                i++;
                string[] command = line.Split(' ');
                if (command == null) ;
                else if (command.Length == 0) ;
                else if (command[0].Length == 0) ;
                else if (command[0][0] == '#')
                {
                    /// <summary>
                    /// Check the command type and call appropriate factory method in the case of a definition
                    /// </summary>
                    switch (command[0])
                    {
                        case "#IND":
                            Theory.Inductives.Add(Factory.Definition.Inductive(command, parseState));
                            break;
                        case "#DEF":
                            Theory.Functions.Add(Factory.Definition.Function(command, parseState));
                            break;
                        case "#AX":
                            Theory.Axioms.Add(Factory.Definition.Axiom(command, parseState));
                            break;
                        case "#QUOT":
                            Factory.Definition.Quotient(command, parseState);
                            break;
                    }
                }
                else if (command[1][0] == '#')
                {
                    /// <summary>
                    /// Check the command type and call appropriate factory method for introducing a name, a universe or an expression
                    /// </summary>
                    Func<string[], IParseStateReader, ILeanObject>? factory =
                    command[1] switch
                    {
                        "#NS" => Factory.Name.String,
                        "#NI" => Factory.Name.Number,
                        "#US" => Factory.Universe.Succ,
                        "#UM" => Factory.Universe.Maximum,
                        "#UIM" => Factory.Universe.ImpredicativeMaximum,
                        "#UP" => Factory.Universe.Parametric,
                        "#EV" => Factory.Expression.Variable,
                        "#ES" => Factory.Expression.Sort,
                        "#EC" => Factory.Expression.Constant,
                        "#EA" => Factory.Expression.Application,
                        "#EL" => Factory.Expression.Lambda,
                        "#EP" => Factory.Expression.Pi,
                        "#EZ" => Factory.Expression.Let,
                        _ => null
                    };
                    if (factory != null) parseState.Add(command[0], factory(command, parseState));
                }
                parseState.IncrementLine();
            }
        }
    }
}
