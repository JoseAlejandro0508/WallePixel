public class BrushSize:Calleable{
    public BrushSize(Token Caller_,Compiling CR_):base(Caller_,1,CR_){

        TypeReturn=FunctionType.Void;
    }

    protected override bool CheckType(List<object> Arguments){
        if(Arguments[0] is not int){
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un entero",Caller.Position,ErrorType.SemanticError));
            return false;
        }

        return true;

    }

    protected override void call(List<object> Arguments){
        int size=(int)Arguments[0];
        if(size%2==0){
            size--;

        }
        if(size<=0){
            
            CompilatorRef.CE.Add(new Error($"El tamaño de la brocha debe ser un entero positivo",Caller.Position));
            return ;
        }
        
        GlobalDates.BrushSize=size;


    
    }


}