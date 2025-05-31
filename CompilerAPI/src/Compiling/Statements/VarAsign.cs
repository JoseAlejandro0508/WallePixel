using System.Runtime.Intrinsics.Arm;

class VariableAssign:Statement{
    public Variable Variable;

    public VariableAssign(Compiling CR,Variable variable) :base(CR){
        Variable=variable;
        

    }
    public override void Execute()
    {
        if(!Variable.CheckSemantic(CompilatorRef.CE)){
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional while", Variable.Location_));
            return;
        };
    
        CompilatorRef.ProgramEnvironment.Assign(Variable.ID,Variable);
    }
}