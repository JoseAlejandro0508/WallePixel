public class DrawRectangle : Calleable
{
    public DrawRectangle(Token Caller_, Compiling CR_) : base(Caller_, 5, CR_)
    {

        TypeReturn = FunctionType.Void;
    }

    protected override bool CheckType(List<object> Arguments)
    {
        if (Arguments[0] is not int)
        {
            CompilatorRef.CE.Add(new Error($"El primer argumento de la funcion {Caller.Value} debe ser un entero", Caller.Position,ErrorType.SemanticError));
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

    protected override void call(List<object> Arguments)
    {
        int x = (int)Arguments[0];
        int y = (int)Arguments[1]; 
        int distance = (int)Arguments[2]-1;
        int radius= (int)Arguments[2];
        int width= (int)Arguments[3];
        int heigth= (int)Arguments[4];
        void GetCenter()
        {
            

            List<int> ValidDir = [1, -1, 0];
            if (!ValidDir.Contains(x) || !ValidDir.Contains(y))
            {

                CompilatorRef.CE.Add(new Error($"La direccion pasada a  {Caller.Value} es invalida", Caller.Position,ErrorType.RuntimeError));
                return;
            }
            if (distance <= 0)
            {

                CompilatorRef.CE.Add(new Error($"La distancia pasada a {Caller.Value} es invalida", Caller.Position,ErrorType.RuntimeError));
                return;
            }
            if (width <= 0 || heigth<=0)
            {

                CompilatorRef.CE.Add(new Error($"La altura y el ancho pasados a {Caller.Value} es invalida", Caller.Position,ErrorType.RuntimeError));
                return;
            }
            int m = -1 * x * y;
            int dir = x;
            if (x == 0)
            {
                dir = y;

            }
            if (x == 0)
            {
                for (int y_ = 0; y_ <= distance; y_++)
                {
                    int X = GlobalDates.ActualPos.x;
                    int Y = (y_ * dir) + GlobalDates.ActualPos.y;
                    if (X < 0 || X > GlobalDates.BoardSize.x || Y < 0 || Y > GlobalDates.BoardSize.x)
                    {
                        return;

                    }
                    if (y_ == distance)
                    {
                        GlobalDates.ActualPos = (X, Y);
                        continue;

                    }
                   


                }


            }
            else
            {
                for (int x_ = 0; x_ <= distance; x_++)
                {
                    int X = (GlobalDates.ActualPos.x + x_ * dir);
                    int Y = -1 * m * (x_ * dir) + GlobalDates.ActualPos.y;
                    if (X < 0 || X > GlobalDates.BoardSize.x || Y < 0 || Y > GlobalDates.BoardSize.x)
                    {
                        return;

                    }
                    if (x_ == distance)
                    {
                        GlobalDates.ActualPos = (X, Y);
                        continue;

                    }
                    
                }


            }
        }
        GetCenter();
        for(int x_=0; x_<=GlobalDates.BoardSize.x;x_++){
            for(int y_=0; y_<=GlobalDates.BoardSize.x;y_++){
                if((Math.Abs(GlobalDates.ActualPos.x-x_+1)==width&&Math.Abs(GlobalDates.ActualPos.y-y_+1)<=heigth) || Math.Abs(GlobalDates.ActualPos.x-x_+1)<=width&&Math.Abs(GlobalDates.ActualPos.y-y_+1)==heigth){
                    GlobalDates.BoardInstance.SetPixel(x_,y_,GlobalDates.BrushColor);

                }
            

            }


        }





    }


}