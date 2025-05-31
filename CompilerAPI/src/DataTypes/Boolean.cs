
public class Boolean:PrimitiveDate{
    public Boolean(Token token):base(token) {

    }
    public override void GetValue(List<Error> CE)
    {

        Value=bool.Parse(TokenValue.Value);
        
    }


}