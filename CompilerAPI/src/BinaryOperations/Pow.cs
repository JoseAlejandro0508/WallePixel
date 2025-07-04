public class Pow:BinaryExpression{
    public Pow(BasicValue left_,BasicValue right_,Token Operator):base(left_,right_,Operator)
    {
        IDOperator="**";

        Location_=Operator.Position;

    }
    public override void GetValue(List<Error> CE)
    {
        if(!(left.Value is int) || !(right.Value is int)){
             CE.Add(new Error($"Operacion no valida,solo es posible utilizar el operador {Operator.Value}  entre 2 enteros",Operator.Position,ErrorType.SemanticError));
            throw new Exception("Tipos de datos invalidos");

        }

        Value=(int)Math.Pow((int)left.Value,(int)right.Value);
    }


}