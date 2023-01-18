using LeanSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeanSharp.LeanObject;

namespace LeanSharp.ExportParsing.Factory
{
    /// <summary>
    /// Factory methods for parsing expressions
    /// </summary>
    internal static class Expression
    {
        /// <summary>
        /// Parse a sort expression
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        public static IExpression.Sort Sort(string[] command, IParseStateReader parseState)
        => new IExpression.Sort(Universe: parseState.GetUniverse(command[2]));
        /// <summary>
        /// Parse a variable expression
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static IExpression.Variable Variable(string[] command, IParseStateReader parseState)
        {
            var index = int.Parse(command[2]);
            return new(Index: index);
        }
        /// <summary>
        /// Parse a constant expression
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static IExpression.Constant Constant(string[] command, IParseStateReader parseState)
        {
            var name = parseState.GetName(command[2]);
            List<ILevel> levels = new List<ILevel>();
            for (int k = 3; k < command.Length; k++)
            {
                levels.Add(parseState.GetUniverse(command[k]));
            }
            return new(Name: name, Universes: levels);
        }
        /// <summary>
        /// Parse an application expression
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static IExpression.Application Application(string[] command, IParseStateReader parseState)
        => new
            (
                Function: parseState.GetExpression(command[2]),
                Argument: parseState.GetExpression(command[3])
            );
        /// <summary>
        /// Parse a lambda expression
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static IExpression.Lambda Lambda(string[] command, IParseStateReader parseState)
        => new
            (
                BinderName: parseState.GetName(command[3]),
                Domain: parseState.GetExpression(command[4]),
                Body: parseState.GetExpression(command[5])
            );
        /// <summary>
        /// Parse a pi expression
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static IExpression.Pi Pi(string[] command, IParseStateReader parseState)
        => new(
            BinderName: parseState.GetName(command[3]),
            Domain: parseState.GetExpression(command[4]),
            Codomain: parseState.GetExpression(command[5]));
        /// <summary>
        /// Parse a let expression
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        internal static IExpression.Let Let(string[] command, IParseStateReader parseState)
        => new(
            BinderName: parseState.GetName(command[2]),
                Domain: parseState.GetExpression(command[3]),
                Value: parseState.GetExpression(command[4]),
                Body: parseState.GetExpression(command[5])
            );
    }
}

