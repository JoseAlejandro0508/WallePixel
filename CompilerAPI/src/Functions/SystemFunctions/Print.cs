public class Print:Calleable{
    public Print(Token Caller_,Compiling CR_):base(Caller_,1,CR_){

        TypeReturn=FunctionType.Void;
    }

    protected override bool CheckType(List<object> Arguments){
        try{
            Arguments[0].ToString();
        }catch{
             CompilatorRef.CE.Add(new Error($"El valor pasado a {Caller.Value} no es imprimible",Caller.Position,ErrorType.RuntimeError));
            return false;
        }


        return true;

    }
    protected override void call(List<object> Arguments){
        

        CompilatorRef.Logs.Add($"{Arguments[0]}\n");
        

    }


}