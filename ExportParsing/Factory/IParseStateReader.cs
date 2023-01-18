using LeanSharp.LeanObject;

namespace LeanSharp.ExportParsing.Factory
{
    /// <summary>
    /// A read-only version of the parse state for the factory methods.
    /// </summary>
    internal interface IParseStateReader
    {
        public IName GetName(string keyString);
        public ILevel GetUniverse(string keyString);
        public IExpression GetExpression(string keyString);
    }
}
