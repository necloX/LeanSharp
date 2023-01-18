using LeanSharp.ExportParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanSharp.LeanObject
{
    public record Declaration(IName Name, IExpression Type);
    public record Function(IName Name, IExpression Type, List<ILevel.Param> UniverseParameters, IExpression Value ) : Declaration(Name,Type), ILeanObject;
    public record Inductive(IName Name, IExpression Type, List<ILevel.Param> UniverseParameters, List<Declaration> Intros, int ParametersNumber) : Declaration(Name, Type), ILeanObject;
    public record Axiom(IName Name, IExpression Type, List<ILevel.Param> UniverseParameters) : Declaration(Name, Type), ILeanObject;
}
