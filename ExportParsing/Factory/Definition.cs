using LeanSharp.LeanObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanSharp.ExportParsing.Factory
{
    /// <summary>
    /// Factory methods for parsing definitions such as inductive types, axioms, functions and quotients.
    /// </summary>
    internal static class Definition
    {
        /// <summary>
        /// Parse an inductive definition
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        public static LeanObject.Inductive Inductive(string[] command,ParseState parseState)
        {
            var numberOfParameters = int.Parse(command[1]);
            var name = parseState.GetName(command[2]);
            var type = parseState.GetExpression(command[3]);
            var declaration = new Declaration(name, type);

            List<Declaration> intros = new List<Declaration>();
            var introNum = int.Parse(command[4]);
            for (int k = 0; k < introNum; k++)
            {
                var introName = parseState.GetName(command[5 + 2 * k]);
                var introType = parseState.GetExpression(command[6 + 2 * k]);
                intros.Add(new LeanObject.Declaration(introName, introType));
            }
            List<ILevel.Param> universesParam = new List<ILevel.Param>();
            for (int k = (5 + introNum * 2); k < command.Length; k++)
            {
                var univerName = parseState.GetName(command[k]);
                universesParam.Add(new LeanObject.ILevel.Param(univerName));
            }
            return new Inductive
            (
                Name: declaration.Name,
                Type: declaration.Type,
                UniverseParameters: universesParam,
                Intros: intros,
                ParametersNumber: numberOfParameters
            );
        }
        /// <summary>
        /// Parse an axiom
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static Axiom Axiom(string[] command, ParseState parseState)
        {
            var levelParams = new List<ILevel.Param>();
            for(int k = 3;k< command.Length;k++)
            {
                levelParams.Add(new(parseState.GetName(command[k])));
            }
            return new
            (
                Name: parseState.GetName(command[1]),
               Type: parseState.GetExpression(command[2]),
                UniverseParameters: levelParams
            );
        }
        /// <summary>
        /// Parse a function
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>

        internal static Function Function(string[] command, ParseState parseState)
        {
            var levelParams = new List<ILevel.Param>();
            for (int k = 4; k < command.Length; k++)
            {
                levelParams.Add(new(parseState.GetName(command[k])));
            }
            return new(
                Name: parseState.GetName(command[1]),
                Type: parseState.GetExpression(command[2]),
                UniverseParameters: levelParams,
                Value: parseState.GetExpression(command[3])
                );
        }
        /// <summary>
        /// Parse a quotient. Currently not implemented.
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static void Quotient(string[] command, ParseState parseState)
        {
            Console.WriteLine("Quotient are currently not implemented.");
        }
    }
}
