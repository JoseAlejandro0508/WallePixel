class FunctionCall:Statement{
    public Token Caller;
    public List<BasicValue> Arguments;

    public FunctionCall(Compiling CR,Token Caller,List<BasicValue> Arguments) :base(CR){
        this.Arguments = Arguments;
        this.Caller = Caller;

    }
    public override void Execute()
    {
        Calleable? calleable=CompilatorRef.ProgramEnvironment.GetFunc(Caller);

        if(calleable==null)return;
        try{
            calleable.Execute(Arguments);
        }
        catch(ReturnException e){
            //CompilatorRef.ProgramEnvironment.CloseEnclosing();
        }
      
    }
}
class FunctionCallExpr:BasicValue{
    public Token Caller;
    public List<BasicValue> Arguments;
    public Environment env;
    public override bool CheckSemantic(List<Error> CE)
    {
        try{
            GetValue(CE);
            if(Value!=null)return true;
            CE.Add(new Error($"La funcion {Caller.Value} retorna un valor nulo",Caller.Position,ErrorType.SemanticError));
            return false;
            
        }catch{
            return false;

        }
    }
    public FunctionCallExpr(Environment env,Token Caller,List<BasicValue> Arguments) {
        this.env=env;
        this.Arguments = Arguments;
        this.Caller = Caller;

    }
    public override void GetValue(List<Error> CE)
    {
        Calleable? calleable=env.GetFunc(Caller);
        if(calleable==null)return;
        try{
            calleable.Execute(Arguments);
            Value=calleable.Value;
        }
        catch(ReturnException e){
            Value=e.Value;
            //env.CloseEnclosing();
        }
        
    }
}