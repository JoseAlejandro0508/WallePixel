public class TokenStream{
    public List<Error>  CE;
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
    public TokenStream(List<Token> Tokens,List<Error>  CE)
    {
        this.CE= CE;
        this.Tokens = Tokens;

    }

    public void Next(){
        if (EOF)return;
        position++;

    }
    public void Syncronize(){
        while(!EOL){
            Next();
        }
        /*while(!EOF && EOL){
            Next();

        }*/
       
    }
    public bool Match(List<TokenIDS>TokenID){
        foreach (TokenIDS tokenID in TokenID){
            if(!LexicDates.Operators.ContainsKey(Current.Value))return false;
            if (tokenID==LexicDates.Operators[Current.Value].tokenIDS){
                return true;
            }
        }
        return false;
    }
    public bool Consume(TokenIDS tokenID,string MessageError){
        Next();
        if(!LexicDates.Operators.ContainsKey(Current.Value)){
            CE.Add(new Error("Operador no reconocido",Current.Position));
            return false;
        }
        if (tokenID==LexicDates.Operators[Current.Value].tokenIDS){
                return true;
            

        }
        CE.Add(new Error("MessageError",Current.Position));
        Syncronize();
        return false;
    }

        
       
    


    

}