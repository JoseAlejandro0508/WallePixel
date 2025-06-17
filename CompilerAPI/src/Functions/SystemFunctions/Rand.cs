public class Rand:Calleable{
    public Rand(Token Caller_,Compiling CR_):base(Caller_,2,CR_){

        TypeReturn=FunctionType.Void;
    }

    protected override bool CheckType(List<object> Arguments){
        if(Arguments[0] is not int){
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un entero",Caller.Position,ErrorType.SemanticError));
            return false;
        }
        if(Arguments[1] is not int){
            CompilatorRef.CE.Add(new Error($"El segundo argumento de la funcion {Caller.Value} debe ser un entero",Caller.Position,ErrorType.SemanticError));
            return false;
        }
        return true;

    }
    protected override void call(List<object> Arguments){

        (int x,int y)Target=((int)Arguments[0],(int)Arguments[1]);

        Random random = new Random();
        Value=random.Next(Target.x,Target.y+1);
        

    }


}