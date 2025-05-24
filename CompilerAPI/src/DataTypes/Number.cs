
public class Number:PrimitiveDate<int>{
    public Number(Token token):base(token) {

    }
    public override void GetValue()
    {
        Value=int.Parse(TokenValue.Value);
        
    }


}