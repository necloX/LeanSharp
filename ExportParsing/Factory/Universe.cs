using LeanSharp.LeanObject;

namespace LeanSharp.ExportParsing.Factory
{
    /// <summary>
    /// Factory methods for parsing universe levels
    /// </summary>
    internal static class Universe
    {
        /// <summary>
        /// Parse a paramteric universe level
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>
        public static ILevel.Param Parametric(string[] command, IParseStateReader parseState) => new(Name: parseState.GetName(command[2]));
        /// <summary>
        /// Parse the impredicative maximum of two previously defined universe levels
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>

        internal static ILevel.IMax ImpredicativeMaximum(string[] command, IParseStateReader parseState) => new(parseState.GetUniverse(command[2]), parseState.GetUniverse(command[3]));
        /// <summary>
        /// Parse the maximum of two previously defined universe levels
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>

        internal static ILevel.Max Maximum(string[] command, IParseStateReader parseState) => new(parseState.GetUniverse(command[2]), parseState.GetUniverse(command[3]));
        /// <summary>
        /// Parse an explicit universe level as succesor of a previously defined universe level
        /// </summary>
        /// <param name="command">The line where the name is introduced, splited by blank spaces </param>
        /// <param name="parseState">The current parse state</param>

        internal static ILevel.Succ Succ(string[] command, IParseStateReader parseState) => new(parseState.GetUniverse(command[2]));
    }
}
