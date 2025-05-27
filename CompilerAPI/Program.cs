/*string text = File.ReadAllText("code");  
List<Error> CE = new List<Error>();
LexicalAnalizer lex=new LexicalAnalizer(text,"a");
lex.GetTokens(CE);
TokenStream stream=new TokenStream(lex.Tokens);
Parser parser=new Parser(stream,CE);
IValue result=parser.ParseExpression();
if (result is BasicValue<int>){
    BasicValue<int> res_=result as BasicValue<int>;
    res_.CheckSemantic(CE);
    int res=res_.Value;
    if (res==0){}
}
if (result is BasicValue<bool>){
    BasicValue<bool> res_=result as BasicValue<bool>;
    res_.CheckSemantic(CE);
    bool res=res_.Value;
    if (res is true){}
}*/
List<int>a=new List<int>();
V dec=new V();
dec.c=a;
dec.add(1);
dec.add(2);
public class V{
    public List<int>c;
    public void add(int b){
        c.Add(b);
    }
}
