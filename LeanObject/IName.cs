using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LeanSharp.LeanObject
{
    [JsonDerivedType(typeof(IName.String), "String")]
    [JsonDerivedType(typeof(IName.Number), "Number")]
    [JsonDerivedType(typeof(IName.Root), "Root")]
    public interface IName : ILeanObject
    {
        public bool GetParent(out IName parent);
        public record String(string Value,IName Parent) : IName
        {
            public bool GetParent(out IName parent)
            {
                if (Parent != null)
                {
                    parent = Parent;
                    return true;
                }
                else
                {
                    parent = Parent;
                    return false;
                }
            }
            public override string ToString() => Value;
        }
        public record Number(long Value,IName Parent) : IName
        {
            public bool GetParent(out IName parent)
            {
                parent = Parent;
                return true;
            }
            public override string ToString() => Value.ToString();
        }
        public struct Root : IName
        {
            public bool GetParent(out IName parent)
            {
                parent = null;
                return false;
            }
        }
    }
    
}
