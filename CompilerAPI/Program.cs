string text = File.ReadAllText("code");  

List<Error> CE = new List<Error>();
Environment Env=new Environment(CE);
LexicalAnalizer lex=new LexicalAnalizer(text,"a");
lex.GetTokens(CE);
TokenStream stream=new TokenStream(lex.Tokens,CE);
Parser parser=new Parser(stream,CE,Env);

Compiling Compiler=new Compiling(Env,CE,parser);
Compiler.GetAllDeclarations();
Compiler.Interprete();
int a =1;

