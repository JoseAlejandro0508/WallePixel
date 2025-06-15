public abstract class Calleable:Statement{
    public Token Caller;
    public Block? Body;
    public  int MaxArguments;
    public FunctionType TypeReturn;
    public object? Value=null;
    protected List<Token>? ArgumentsID;


    protected abstract void call(List<object> Arguments);
    protected abstract bool CheckType(List<object> Arguments_);
    public bool CheckSemantic(List<BasicValue> Arguments_,out List<object> Arguments){

        Arguments = new List<object>();
        

        if(Arguments_.Count!=MaxArguments){
            CompilatorRef.CE.Add(new Error($"La funcion {Caller.Value} esperaba {MaxArguments} argumentos y recibio {Arguments_.Count}",Caller.Position,ErrorType.RuntimeError));
            return false;
        }
        foreach(BasicValue arg in Arguments_){
            if(!arg.CheckSemantic(CompilatorRef.CE)){
                return false;
            }
            if(arg.Value==null)return false;
            Arguments.Add(arg.Value);
        }


        return CheckType(Arguments);
        
    }
    public Calleable(Token Caller_,int MaxArguments_,Compiling CR_,Block? Body_=null):base(CR_){
        Body=Body_;
        Caller=Caller_;
        MaxArguments=MaxArguments_;

    
    }
    public override void Execute()
    {
        throw new NotImplementedException();
    }
    public void Execute(List<BasicValue> Arguments_){
        Value=null;
        List<object>Arguments;
        if(CheckSemantic(Arguments_,out Arguments)){
            call(Arguments);
        }

    }


}



public enum FunctionType{
    Void,
    Return,

    
}