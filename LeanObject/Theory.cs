

namespace LeanSharp.LeanObject
{

    public class Theory
    {
        public List<Inductive> Inductives { get; }
        public List<Function> Functions { get; }
        public List<Axiom> Axioms { get; }
        public Theory() 
        {
            Inductives = new List<Inductive>();
            Functions = new List<Function>();
            Axioms = new List<Axiom>();
        }
    }
}
