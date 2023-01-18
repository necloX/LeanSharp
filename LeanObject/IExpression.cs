using LeanSharp.ExportParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LeanSharp.LeanObject
{
    [JsonDerivedType(typeof(IExpression.Variable),"Variable")]
    [JsonDerivedType(typeof(IExpression.Sort), "Sort")]
    [JsonDerivedType(typeof(IExpression.Constant), "Constant")]
    [JsonDerivedType(typeof(IExpression.Application), "Application")]
    [JsonDerivedType(typeof(IExpression.Lambda), "Lambda")]
    [JsonDerivedType(typeof(IExpression.Pi), "Pi")]
    [JsonDerivedType(typeof(IExpression.Let), "Let")]
    public interface IExpression : ILeanObject
    {
        public record Variable(int Index) : IExpression
        {
            public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf) => variablef(this);
        }

        public record Sort(ILevel Universe) : IExpression
        {
            public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf) => sortf(this);

        }

        public record Constant(IName Name,List<ILevel> Universes) : IExpression
        {
            public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf) => constantf(this);

        }

        public record Application(IExpression Function,IExpression Argument) : IExpression
        {
            public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf) => applicationf(this);

        }

        public record Lambda(IName BinderName,IExpression Domain, IExpression Body) : IExpression
        {
            public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf) => lambdaf(this);

        }
        record Pi(IName BinderName,IExpression Domain,IExpression Codomain) : IExpression
        {
            public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf) => pif(this);

        }
        public record Let(IName BinderName,IExpression Domain,IExpression Value,IExpression Body) : IExpression
        {
            public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf) => letf(this);

        }

        public T Match<T>(Func<Variable, T> variablef, Func<Sort, T> sortf, Func<Constant, T> constantf, Func<Application, T> applicationf, Func<Lambda, T> lambdaf, Func<Pi, T> pif, Func<Let, T> letf);
        public void Match(Action<Variable> variablef, Action<Sort> sortf, Action<Constant> constantf, Action<Application> applicationf, Action<Lambda> lambdaf, Action<Pi> pif, Action<Let> letf)
        {
            Match<bool>(
                v => { variablef(v); return true; },
                v => { sortf(v); return true; },
                v => { constantf(v); return true; },
                v => { applicationf(v); return true; },
                v => { lambdaf(v); return true; },
                v => { pif(v); return true; },
                v => { letf(v); return true; });
        }
    }
    

}
