class FunctionAsign:Statement{
    public Token Caller;
    public DefinedFunc Func;
    public Token OP;
    public FunctionAsign(Compiling CR,Token Caller,Token OP,DefinedFunc Func) :base(CR){
        this.Func = Func;
        this.Caller = Caller;
        this.OP = OP;
    }
    public override void Execute()
    {
        CompilatorRef.ProgramEnvironment.DefineFunc(Caller,Func);

    }
}