
public class String:PrimitiveDate<string>{
    public String(Token token):base(token) {

    }
    public override void GetValue()
    {
        Value=TokenValue.Value;
        
    }


}