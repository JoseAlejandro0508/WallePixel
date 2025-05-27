using System.Runtime.Intrinsics.Arm;

class VariableAssign:Statement{
    public dynamic Variable;

    public VariableAssign(Compiling CR,object variable) :base(CR){
        Variable=variable;
        

    }
    public override void Execute()
    {
    
        CompilatorRef.ProgramEnvironment.Assign(Variable.ID,Variable);
    }
}