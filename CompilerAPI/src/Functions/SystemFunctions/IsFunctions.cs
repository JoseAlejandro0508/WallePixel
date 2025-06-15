public class IsBrushColor:Calleable{
    public IsBrushColor(Token Caller_,Compiling CR_):base(Caller_,1,CR_){

        TypeReturn=FunctionType.Return;
    }

    protected override bool CheckType(List<object> Arguments){
        if (Arguments[0] is not string)
        {
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un string", Caller.Position));
            return false;
        }

        return true;
        
    }
    protected override void call(List<object> Arguments){
        string color=(string)Arguments[0];

        Value=GlobalDates.BrushColor==color?1:0;    

    }


}
public class IsBrushSize:Calleable{
    public IsBrushSize(Token Caller_,Compiling CR_):base(Caller_,1,CR_){

        TypeReturn=FunctionType.Return;
    }

    protected override bool CheckType(List<object> Arguments){
        if (Arguments[0] is not int)
        {
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un entero", Caller.Position));
            return false;
        }

        return true;
        
    }
    protected override void call(List<object> Arguments){
        int size=(int)Arguments[0];

        Value=GlobalDates.BrushSize==size?1:0;    

    }


}
public class IsColor:Calleable{
    public IsColor(Token Caller_,Compiling CR_):base(Caller_,3,CR_){

        TypeReturn=FunctionType.Return;
    }

    protected override bool CheckType(List<object> Arguments){
        if (Arguments[0] is not string)
        {
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un string", Caller.Position,ErrorType.SemanticError));
            return false;
        }
        if (Arguments[1] is not int)
        {
            CompilatorRef.CE.Add(new Error($"El segundo argumento de la funcion {Caller.Value} debe ser un entero", Caller.Position,ErrorType.SemanticError));
            return false;
        }
        if (Arguments[2] is not int)
        {
            CompilatorRef.CE.Add(new Error($"El tercer argumento de la funcion {Caller.Value} debe ser un entero", Caller.Position,ErrorType.SemanticError));
            return false;
        }


        return true;
        
    }
    protected override void call(List<object> Arguments){

        string color=(string)Arguments[0];
        int X=(int)Arguments[1];
        int Y=(int)Arguments[2];

        Value=GlobalDates.BoardMatrix[GlobalDates.ActualPos.x+X,GlobalDates.ActualPos.y+Y]==color?1:0;    

    }


}