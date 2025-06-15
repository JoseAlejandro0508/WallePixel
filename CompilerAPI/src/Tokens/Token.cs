public class Token{
    public Location? Position{ get; private set; }
    public TokenType Type{ get; private set; }
    public string Value{ get; private set; }
    public Token(Location? position, TokenType type, string value){

        Position = position;
        Type = type;
        Value = value.ToString();
    }


}
public struct Location{
    
    public int Column;
    public int Row;
    public string Filename;


}
public enum TokenType{
    Uknown,
    Bool,
    Number,
    String,
    Variable,
    Tag,
    Bucle,
    Keyword,
    Symbol,
    EOL,
    EOF,
    Function

    
}