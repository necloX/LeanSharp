using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LeanSharp.ExportParsing;

namespace LeanSharp.LeanObject
{
    [JsonDerivedType(typeof(ILevel.Zero), "Zero")]
    [JsonDerivedType(typeof(ILevel.Succ), "Succ")]
    [JsonDerivedType(typeof(ILevel.Max), "Max")]
    [JsonDerivedType(typeof(ILevel.IMax), "IMax")]
    [JsonDerivedType(typeof(ILevel.Param), "Param")]
    public interface ILevel : ILeanObject
    {
        public record Param(IName Name) : ILevel;
        record IMax(ILevel a,ILevel b) : ILevel;
        struct Zero : ILevel { }
        record Succ(ILevel level) : ILevel;
        record Max(ILevel a, ILevel b) : ILevel;
    }
    

}
