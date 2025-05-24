public class TokenStream{
    public List<Token> Tokens;
    private int position;
    public bool EOL{
        get{
            return EOF||(Tokens[position].Type == TokenType.EOL);
        }
    }
    public bool EOF{
        get{
            return (Tokens[position].Type == TokenType.EOF)||Tokens.Count==position-1;
        }
    }
    public Token Current{
        get{
            if (EOF)throw new InvalidOperationException("La lista de tokens llego a su fin");
            return (Tokens[position]);
        }
    }
    TokenStream(List<Token> Tokens)
    {
        this.Tokens = Tokens;

    }
    public void Next(){
        if (EOF)return;
        position++;

    }
    public bool Match(List<TokenIDS>TokenID){
        foreach (TokenIDS tokenID in TokenID){
            if (tokenID==LexicDates.Operators[Current.Value].tokenIDS){
                return true;
            }
        }
        return false;
    }
    

}