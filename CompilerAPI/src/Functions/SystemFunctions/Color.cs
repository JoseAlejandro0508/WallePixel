public class BrushColor:Calleable{
    public BrushColor(Token Caller_,Compiling CR_):base(Caller_,1,CR_){

        TypeReturn=FunctionType.Void;
    }

    protected override bool CheckType(List<object> Arguments){
        if(Arguments[0] is not string){
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un string",Caller.Position,ErrorType.SemanticError));
            return false;
        }
        if(!GlobalDates.ColorsDictionary.ContainsKey((string)Arguments[0])){
            CompilatorRef.CE.Add(new Error($"El color pasado como argumento a la funcion {Caller.Value} no esta definido",Caller.Position,ErrorType.RuntimeError));
            return false;
        }
        return true;

    }
    protected override void call(List<object> Arguments){

        GlobalDates.BrushColor=(string)Arguments[0];

    
    }


}