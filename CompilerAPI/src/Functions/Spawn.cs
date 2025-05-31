public class Spawn:Calleable{
    public Spawn(Token Caller_,Compiling CR_):base(Caller_,2,CR_){

        TypeReturn=FunctionType.Void;
    }

    protected override bool CheckType(List<object> Arguments){
        if(Arguments[0] is not int){
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un entero",Caller.Position));
            return false;
        }
        if(Arguments[1] is not int){
            CompilatorRef.CE.Add(new Error($"El segundo argumento de la funcion {Caller.Value} debe ser un entero",Caller.Position));
            return false;
        }
        return true;

    }
    protected override void call(List<object> Arguments){
        


    }

}