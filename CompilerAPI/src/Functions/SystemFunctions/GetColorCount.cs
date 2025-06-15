public class GetColorCount:Calleable{
    public GetColorCount(Token Caller_,Compiling CR_):base(Caller_,5,CR_){

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
            CompilatorRef.CE.Add(new Error($"El tercer argumento de la funcion {Caller.Value} debe ser un entero", Caller.Position,ErrorType.LexicError));
            return false;
        }
        if (Arguments[3] is not int)
        {
            CompilatorRef.CE.Add(new Error($"El cuarto argumento de la funcion {Caller.Value} debe ser un entero", Caller.Position,ErrorType.SemanticError));
            return false;
        }
        if (Arguments[4] is not int)
        {
            CompilatorRef.CE.Add(new Error($"El quinto argumento de la funcion {Caller.Value} debe ser un entero", Caller.Position,ErrorType.SemanticError));
            return false;
        }

        return true;
        
    }
    protected override void call(List<object> Arguments){
        string color=(string)Arguments[0];
        int x1 = (int)Arguments[1];
        int y1 = (int)Arguments[2];
        int x2 = (int)Arguments[3];
        int y2 = (int)Arguments[4]; 
        
        if(!GlobalDates.IsValidPos([(x1,y1),(x2,y2)])){
            Value=0;
            return;
        }
        int Value_=0;
        for(int x_=0;x_<GlobalDates.BoardSize.x;x_++){
           for(int y_=0;y_<GlobalDates.BoardSize.x;y_++){
                if(x1<x_ &&x_<x2 && y1<y_ && y_<y2){
                    if(GlobalDates.BoardMatrix[x_,y_]== color){
                        Value_++;
                    }
                }
            
        }   
        }  
        Value=Value_;    

    }


}
