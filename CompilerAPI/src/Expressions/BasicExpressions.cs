using System.Numerics;

public interface IValue
{

    public abstract void GetValue(List<Error> CE);
}
public abstract class BasicValue : IValue
{
    public object? Value;
    public Location Location_;
    public abstract void GetValue(List<Error> CE);
    public abstract bool CheckSemantic(List<Error> ComplierErrors);
}
public abstract class PrimitiveDate : BasicValue
{
    public Token TokenValue { get; private set; }

    public PrimitiveDate(Token token)
    {
        TokenValue = token;
        Location_ = token.Position;

    }
    override public bool CheckSemantic(List<Error> CE)
    {

        try
        {
            GetValue(CE);


        }
        catch
        {
            CE.Add(new Error($"Error,el valor {TokenValue.Value} no se corresponde con el formato de un numero", Location_));
            return false;

        }
        return true;

    }



}
public abstract class BinaryExpression : BasicValue
{
    public BasicValue left;
    public BasicValue right;
    public Token Operator;
    public string IDOperator;

    public BinaryExpression(BasicValue left_, BasicValue right_, Token Operator_)
    {
        Operator = Operator_;
        left = left_;
        right = right_;

    }
    public override bool CheckSemantic(List<Error> CE)
    {

        if (IDOperator != Operator.Value)
        {
            throw new Exception("El Identificador de la operacion no se corresponde con el operador");
        }
        if (!left.CheckSemantic(CE))
        {
            CE.Add(new Error("Hay un error al obtener el valor de un operando", left.Location_));
            return false;
        }
        if (!right.CheckSemantic(CE))
        {
            CE.Add(new Error("Hay un error al obtener el valor de un operando", right.Location_));
            return false;
        }

        try
        {
            GetValue(CE);


        }
        catch
        {
            CE.Add(new Error($"Error al ejecutar la operacion {Operator.Value}", Operator.Position));
            return false;

        }
        return true;

    }


}

public class Variable : BasicValue
{
    public Token ID;

    public object PrimitiveValue;
    public Environment Env;

    public Variable(Token ID_, Environment Env)
    {

        ID = ID_;
        Location_ = ID.Position;
        this.Env = Env;


    }
    public override void GetValue(List<Error> CE)
    {

        if (PrimitiveValue is ValueType)
        {
            this.Value = PrimitiveValue;
            return;
        }
        this.Value = (PrimitiveValue as BasicValue).Value;



    }
    public override bool CheckSemantic(List<Error> CE)
    {
        BasicValue? var_ = null;
        if (Env.Check(ID, out var_))
        {
            bool result = var_.CheckSemantic(CE);
            if (!result) return false;
            this.PrimitiveValue = var_;
            try
            {
                GetValue(CE);
                return true;
            }
            catch
            {
                CE.Add(new Error($"Error al obtener el valor de la variable {ID}", Location_));
                return false;
            }


        }

        CE.Add(new Error($"La variable{ID.Value} no se encuetra en memoria", ID.Position));


        return false;


    }

}
