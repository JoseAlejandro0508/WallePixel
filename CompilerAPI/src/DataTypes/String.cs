
public class String:PrimitiveDate{
    public String(Token token):base(token) {

    }
    public override void GetValue(List<Error> CE)
    {
        Value=TokenValue.Value;
        
    }


}