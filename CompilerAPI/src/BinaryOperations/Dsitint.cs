public class Distint:BinaryExpression{
    public Distint(BasicValue left_,BasicValue right_,Token Operator):base(left_,right_,Operator)
    {
        IDOperator="!=";

        Location_=Operator.Position;

    }
    public override void GetValue(List<Error> CE)
    {
        if((left.Value is int) && (right.Value is int)){
            Value=(int)left.Value != (int)right.Value;
            return;

        }
        if((left.Value is bool) && (right.Value is bool)){
            Value=(bool)left.Value != (bool)right.Value;
            return;

        }
        if((left.Value is string) && (right.Value is string)){
            Value=(string)left.Value != (string)right.Value;
            return;

        }
                 CE.Add(new Error($"Operacion no valida,solo es posible utilizar el operador {Operator.Value} and entre 2 valores del mismo tipo",Operator.Position));
        throw new Exception("Tipos de datos invalidos");

    
        
    }



}
