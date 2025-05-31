
public class Number:PrimitiveDate{
    public Number(Token token):base(token) {

    }
    public override void GetValue(List<Error> CE)
    {
        Value=int.Parse(TokenValue.Value);
        
    }


}