public class Move:Calleable{
    public Move(Token Caller_,Compiling CR_):base(Caller_,2,CR_){

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
        if(Target.x>GlobalDates.BoardInstance.BoardSize || Target.x<0 || Target.y>GlobalDates.BoardInstance.BoardSize|| Target.y<0){
            CompilatorRef.CE.Add(new Error($"Cooredenadas invalidas ,deben encontrtarse entre 0 y {GlobalDates.BoardSize.x}",Caller.Position,ErrorType.RuntimeError));
            return;
        }
        GlobalDates.ActualPos=Target;

        

    }


}