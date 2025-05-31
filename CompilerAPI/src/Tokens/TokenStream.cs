using System.Security.Cryptography.X509Certificates;

public class TokenStream
{

    public List<Error> CE;
    public List<Token> Tokens;
    private int position;
    public bool EOL
    {
        get
        {
            return EOF || (Tokens[position].Type == TokenType.EOL);
        }
    }
    public bool EOF
    {
        get
        {
            return (Tokens[position].Type == TokenType.EOF) || Tokens.Count == position - 1;
        }
    }
    public Token Current
    {
        get
        {
            //if (EOF)throw new InvalidOperationException("La lista de tokens llego a su fin");
            return (Tokens[position]);
        }
    }
    public TokenStream(List<Token> Tokens, List<Error> CE)
    {

        this.CE = CE;
        this.Tokens = Tokens;

    }

    public void Next()
    {
        if (EOF) return;
        position++;

    }
    public void Syncronize()
    {
        while (!EOL)
        {
            Next();
        }
        /*while(!EOF && EOL){
            Next();

        }*/

    }
    public bool Match(List<TokenIDS> TokenID)
    {
        foreach (TokenIDS tokenID in TokenID)
        {
            TokenInfo? check1 = null, check2 = null, check3 = null;
            TokenInfo check = null;
            if (!(LexicDates.Operators.TryGetValue(Current.Value, out check1) || LexicDates.Bucles.TryGetValue(Current.Value, out check2) || LexicDates.Keywords.TryGetValue(Current.Value, out check3)))
            {
                return false;
            }
            if (check1 is not null) check = check1;
            if (check2 is not null) check = check2;
            if (check3 is not null) check = check3;



            if (tokenID == check!.tokenIDS)
            {
                return true;
            }

        }
        return false;
    }
    public bool Consume(TokenIDS tokenID, string MessageError, bool initialAdvance = true)
    {
        if (initialAdvance) Next();

        TokenInfo? check1 = null, check2 = null, check3 = null;
        TokenInfo check = null;
        if (!(LexicDates.Operators.TryGetValue(Current.Value, out check1) || LexicDates.Bucles.TryGetValue(Current.Value, out check2) || LexicDates.Keywords.TryGetValue(Current.Value, out check3)))
        {
            CE.Add(new Error(MessageError, Current.Position));
            Syncronize();
            return false;
        }
        if (check1 is not null) check = check1;
        if (check2 is not null) check = check2;
        if (check3 is not null) check = check3;



        if (tokenID == check!.tokenIDS)
        {
            Next();
            return true;
        }
        CE.Add(new Error(MessageError, Current.Position));
        Syncronize();
        return false;
    }
    public bool Consume(List<TokenIDS> tokensIDs, List<string> MessagesError, bool initialAdvance = true)
    {

        if (initialAdvance) Next();
        for (int i = 0; i < tokensIDs.Count; i++)
        {
            TokenIDS tokenID = tokensIDs[i];
            TokenInfo? check1 = null, check2 = null, check3 = null;
            TokenInfo check = null;
            if (!(LexicDates.Operators.TryGetValue(Current.Value, out check1) || LexicDates.Bucles.TryGetValue(Current.Value, out check2) || LexicDates.Keywords.TryGetValue(Current.Value, out check3)))
            {

                continue;
            }
            if (check1 is not null) check = check1;
            if (check2 is not null) check = check2;
            if (check3 is not null) check = check3;



            if (tokenID != check!.tokenIDS)
            {
                CE.Add(new Error(MessagesError[i], Current.Position));
                Syncronize();
                return false;
            }

            Next();


        }

        return true;
    }
    public void GoToTag(Token Tag)
    {
        for (int i = 0; i < Tokens.Count; i++)
        {
            if (TokenType.Tag == Tokens[i].Type && Tag.Value == Tokens[i].Value)
            {
                position = i;
            }

        }
        position++;
        while (!EOF && EOL)
        {
            position++;
        }

    }
    public int GetPos()
    {
        return position;
    }








}