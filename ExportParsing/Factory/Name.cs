using LeanSharp.LeanObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanSharp.ExportParsing.Factory
{
    /// <summary>
    /// Factory methods for parsing Lean hierarchal names
    /// </summary>
    internal static class Name
    {
        /// <summary>
        /// Parse a number name
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        public static IName Number(string[] command, IParseStateReader parseState)
        {
            try
            {
                var numberName = long.Parse(command[3]);
                var name = new IName.Number(numberName, parseState.GetName(command[2]));
                return name;
            }
            catch(OverflowException of)
            {
                throw new OverflowException(of.Message+$"Tried parsing {command[3]} as an int32.");
            }
        }
        /// <summary>
        /// Parse a string name
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        public static IName String(string[] command, IParseStateReader parseState)
        {
            var stringName = command[3];
            var name = new IName.String(stringName, parseState.GetName(command[2]));
            return name;
        }
    }
}
