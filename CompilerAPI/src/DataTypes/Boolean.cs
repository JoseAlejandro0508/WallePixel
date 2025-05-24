
public class Boolean:PrimitiveDate<bool>{
    public Boolean(Token token):base(token) {

    }
    public override void GetValue()
    {
        Value=bool.Parse(TokenValue.Value);
        
    }


}