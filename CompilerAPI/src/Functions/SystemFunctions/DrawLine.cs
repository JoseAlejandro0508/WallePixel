public class DrawLine:Calleable{
    public DrawLine(Token Caller_,Compiling CR_):base(Caller_,3,CR_){

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
        if(Arguments[2] is not int){
            CompilatorRef.CE.Add(new Error($"El tercer argumento de la funcion {Caller.Value} debe ser un entero",Caller.Position,ErrorType.SemanticError));
            return false;
        }

        return true;

    }

    protected override void call(List<object> Arguments){
        int x=(int)Arguments[0];int y=(int)Arguments[1];int distance=(int)Arguments[2];

        List<int>ValidDir=[1,-1,0];
        if(!ValidDir.Contains(x) || !ValidDir.Contains(y)){

            CompilatorRef.CE.Add(new Error($"La direccion pasada a  {Caller.Value} es invalida",Caller.Position,ErrorType.RuntimeError));
            return;
        }
        if(distance<=0){

            CompilatorRef.CE.Add(new Error($"La distancia pasada a {Caller.Value} es invalida",Caller.Position,ErrorType.RuntimeError));
            return;
        }
        int m=-1*x*y;
        int dir=x;
        if(x==0){
            dir=y;

        }
        if(x==0){
            int X1=GlobalDates.ActualPos.x;
            int Y1=GlobalDates.ActualPos.y;
            for(int y_=0;y_<=distance;y_++){
                int X=X1;
                int Y=(y_*dir)+Y1;
                if(X<0 || X>GlobalDates.BoardSize.x || Y<0 || Y>GlobalDates.BoardSize.x){
                    return;

                }
                if(y_!=distance){
                    GlobalDates.BoardInstance.SetPixel(X,Y,GlobalDates.BrushColor);
   

                }
                
                GlobalDates.ActualPos=(X,Y);
               

            }
            

        }else{
            int X1=GlobalDates.ActualPos.x;
            int Y1=GlobalDates.ActualPos.y;
            for(int x_=0;x_<=distance;x_++){
                int X=(X1+x_*dir);
                int Y=-1*m*(x_*dir)+Y1;
                if(X<0 || X>GlobalDates.BoardSize.x || Y<0 || Y>GlobalDates.BoardSize.x){
                    return;

                }
                if(x_!=distance){
                   GlobalDates.BoardInstance.SetPixel(X,Y,GlobalDates.BrushColor);

                }
                
                GlobalDates.ActualPos=(X,Y);
            }
           

        }

        
         

    
    }


}