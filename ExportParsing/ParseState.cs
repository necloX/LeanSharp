using LeanSharp.ExportParsing.Factory;
using LeanSharp.LeanObject;

namespace LeanSharp.ExportParsing
{
    /// <summary>
    /// The current state of the parsing process.
    /// </summary>
    internal class ParseState : IParseStateReader
    {
        /// <summary>
        /// The root of the name tree node
        /// </summary>
        IName.Root NameTreeNodeRoot { get; set; }
        /// <summary>
        /// The names indexed by their ID
        /// </summary>
        Dictionary<long, IName> Names { get; set; }
        /// <summary>
        /// The universes indexed by their ID
        /// </summary>
        Dictionary<long, ILevel> Universes { get; set; }
        /// <summary>
        /// The expressions indexed by their ID
        /// </summary>
        Dictionary<long, IExpression> Expressions { get; set; }
        /// <summary>
        /// The current line number
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Increment the current line number
        /// </summary>
        public void IncrementLine() => Line++;

        public ParseState()
        {
            Names = new Dictionary<long, IName>();
            Universes = new Dictionary<long, ILevel>();
            Expressions = new Dictionary<long, IExpression>();
            NameTreeNodeRoot = new IName.Root();
            Names.Add(0, NameTreeNodeRoot);
            Universes.Add(0, new LeanObject.ILevel.Zero());
        }
        
        void Add<T>(long key,T value)
        {
            if(value is IName name)
            {
                try
                {
                    Names.Add(key, name);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"A name with Key = {key} already exists in the name index.");
                }
            }
            else if (value is ILevel universe)
            {
                try
                {
                    Universes.Add(key, universe);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"An universe with Key = {key} already exists in the universe index.");
                }
            }
            else if (value is IExpression expression)
            {
                try
                {
                    Expressions.Add(key, expression);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"An expression with Key = {key} already exists in the expression index.");
                }
            }
        }
        /// <summary>
        /// Adds a value to the parse state
        /// </summary>
        /// <typeparam name="T">The type of the value being added</typeparam>
        /// <param name="keyString">The string representation of the id</param>
        /// <param name="value">The value being added</param>
        public void Add<T>(string keyString,T value)
        {
            try
            {
                long key = long.Parse(keyString);
                Add(key, value);
            }
            catch(FormatException f)
            {
                Console.WriteLine(f.Message);
            }
        }
        IName GetName(long key)
        {
            try
            {
                return Names[key];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"There is no name with key = {key} at line {Line}");
            }
        }
        /// <summary>
        /// Gets the name with the specified id as a string
        /// </summary>
        /// <param name="keyString">The string representation of the id of the value</param>
        /// <returns>The name with the specified id</returns>
        public IName GetName(string keyString)
        {
            try
            {
                long key = long.Parse(keyString);
                return GetName(key);
            }
            catch (FormatException f)
            {
                throw new FormatException($"Expected an integer at line {Line}");
            }
        }
        ILevel GetUniverse(long key)
        {
            try
            {
                return Universes[key];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"There is no universe with key = {key} at line {Line}");
            }
        }
        public ILevel GetUniverse(string keyString)
        {
            try
            {
                long key = long.Parse(keyString);
                return GetUniverse(key);
            }
            catch (FormatException f)
            {
                throw new FormatException($"Expected an integer at line {Line}");
            }
        }
        IExpression GetExpression(long key)
        {
            try
            {
                return Expressions[key];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"There is no expression with key = {key} at line {Line}");
            }
        }
        /// <summary>
        /// Gets the expression with the specified id as a string
        /// </summary>
        /// <param name="keyString">The string id of the expression</param>
        /// <returns>The expression with the specified id</returns>
        public IExpression GetExpression(string keyString)
        {
            try
            {
                long key = long.Parse(keyString);
                return GetExpression(key);
            }
            catch (FormatException f)
            {
                throw new FormatException($"Expected an integer at line {Line}");
            }
        }
    }
}
