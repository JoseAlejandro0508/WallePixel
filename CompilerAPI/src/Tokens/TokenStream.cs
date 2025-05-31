public class TokenStream{
    public LexicDates_ LexicDates=new LexicDates_();
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
            //if (EOF)throw new InvalidOperationException("La lista de tokens llego a su fin");
            return (Tokens[position]);
        }
    }
    public TokenStream(List<Token> Tokens,List<Error>  CE)
    {
        LexicDates.InitLexic();
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

            if(!LexicDates.Operators.ContainsKey(Current.Value)&&!LexicDates.Keywords.ContainsKey(Current.Value)&&!LexicDates.Bucles.ContainsKey(Current.Value))return false;
            
            if (tokenID==LexicDates.Operators[Current.Value].tokenIDS || tokenID==LexicDates.Keywords[Current.Value].tokenIDS || tokenID==LexicDates.Bucles[Current.Value].tokenIDS  ){
                return true;
            }
        }
        return false;
    }
    public bool Consume(TokenIDS tokenID,string MessageError,bool initialAdvance=true){
        if(initialAdvance)Next();
        
        if(!LexicDates.Operators.ContainsKey(Current.Value)&&!LexicDates.Keywords.ContainsKey(Current.Value)&&!LexicDates.Bucles.ContainsKey(Current.Value))return false;

        if (tokenID==LexicDates.Operators[Current.Value].tokenIDS || tokenID==LexicDates.Keywords[Current.Value].tokenIDS || tokenID==LexicDates.Bucles[Current.Value].tokenIDS  ){
                Next();
                return true;
        }
        CE.Add(new Error(MessageError,Current.Position));
        Syncronize();
        return false;
    }
    public bool Consume(List<TokenIDS> tokensIDs ,List<string> MessagesError,bool initialAdvance=true){

        if(initialAdvance)Next();
        for(int i=0;i<tokensIDs.Count;i++){
            TokenIDS tokenID=tokensIDs[i];
            if(!LexicDates.Operators.ContainsKey(Current.Value)&&!LexicDates.Keywords.ContainsKey(Current.Value)&&!LexicDates.Bucles.ContainsKey(Current.Value))return false;

            if (!(tokenID==LexicDates.Operators[Current.Value].tokenIDS || tokenID==LexicDates.Keywords[Current.Value].tokenIDS || tokenID==LexicDates.Bucles[Current.Value].tokenIDS  )){
                CE.Add(new Error(MessagesError[i],Current.Position));
                Syncronize();
                return false;
            }
            Next();

            
        }

        return true;
    }
    public void GoToTag(Token Tag){
        for (int i = 0;i<Tokens.Count;i++){
            if(TokenType.Tag==Tokens[i].Type && Tag.Value==Tokens[i].Value){
                position=i;
            }

        }
        position++;
        while(!EOF && EOL){
            position++;
        }

    }
    public int GetPos(){
        return position;
    }

        
       
    


    

}