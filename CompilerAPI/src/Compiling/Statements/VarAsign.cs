using System.Runtime.Intrinsics.Arm;

class VariableAssign:Statement{
    public Variable Variable;

    public VariableAssign(Compiling CR,Variable variable) :base(CR){
        Variable=variable;
        

    }
    public override void Execute()
    {
    
        CompilatorRef.ProgramEnvironment.Assign(Variable.ID,Variable);
    }
}