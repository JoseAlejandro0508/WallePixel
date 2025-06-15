
class VariableAssign:Statement{
    public BasicValue VariableValue;
    public Token ID;
    public Token Asign;

    public VariableAssign(Compiling CR,BasicValue variable,Token ID,Token Asign) :base(CR){
        this.VariableValue = variable;
        this.ID = ID;
        this.Asign = Asign;
        

    }
    public override void Execute()
    {
        if(!VariableValue.CheckSemantic(CompilatorRef.CE)){
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor que se le esta asignando a la variable {ID.Value}", ID.Position,ErrorType.RuntimeError));
            return;
        };
    
        CompilatorRef.ProgramEnvironment.Assign(ID,new UknownValue(VariableValue.Value));
    }
}