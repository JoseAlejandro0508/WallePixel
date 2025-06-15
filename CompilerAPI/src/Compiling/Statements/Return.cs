class Return:Statement{
    public Token Caller;
    public BasicValue? Value=null;

    public Return(Compiling CR,Token Caller,BasicValue Value) :base(CR){
        this.Value = Value;
        this.Caller = Caller;

    }
    public override void Execute()
    {
        if(Value is null || !Value.CheckSemantic(CompilatorRef.CE)){
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de retorno", Value.Location_,ErrorType.RuntimeError));
            throw new ReturnException(null);
        };
        throw new ReturnException(Value.Value);
        
    }
}

public class ReturnException:Exception{
    public Object? Value;
    public ReturnException(Object? Value) : base("Retorno"){
        this.Value = Value;
    }
}