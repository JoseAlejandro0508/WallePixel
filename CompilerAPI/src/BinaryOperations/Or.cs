public class Or:BinaryExpression{
    public Or(BasicValue left_,BasicValue right_,Token Operator):base(left_,right_,Operator)
    {
        IDOperator="||";

        Location_=Operator.Position;

    }
    public override void GetValue(List<Error> CE)
    {
        if(!(left.Value is bool) || !(right.Value is bool)){
             CE.Add(new Error($"Operacion no valida,solo es posible utilizar el operador {Operator.Value} and entre 2 booleanos",Operator.Position,ErrorType.SemanticError));
            throw new Exception("Tipos de datos invalidos");

        }

        Value=(bool)left.Value || (bool)right.Value;
    }



}
