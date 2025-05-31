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
        calleable.Execute(Arguments);
    }
}