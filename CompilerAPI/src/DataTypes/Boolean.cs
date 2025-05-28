
public class Boolean:PrimitiveDate{
    public Boolean(Token token):base(token) {

    }
    public override void GetValue()
    {

        Value=bool.Parse(TokenValue.Value);
        
    }


}