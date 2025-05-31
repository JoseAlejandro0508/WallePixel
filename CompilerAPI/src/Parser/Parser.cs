using System.Text.RegularExpressions;

public class Parser{
    public List<Error>  CE;
    public Compiling CR;
    public TokenStream Stream;
    public Environment ProgramEnvironment;
    public Parser(TokenStream Stream,List<Error> CE,Environment env){
        ProgramEnvironment = env;
        this.Stream = Stream;
        this.CE = CE;
    

    }


    public  BasicValue ParseExpression(){

        return ParseLogic();

    }

    public  BasicValue ParseLogic(){
        BasicValue expr=ParseOR();
        while(!Stream.EOL && Stream.Match([TokenIDS.AndOper])){

            Token OP=Stream.Current;
            Stream.Next();
            BasicValue right=ParseOR();

            expr=new And(expr,right,OP);

        }
        
        return expr;

    }
    public  BasicValue ParseOR(){
        BasicValue expr=ParseComparison();
        while(!Stream.EOL && Stream.Match([TokenIDS.OrOper])){


            Token OP=Stream.Current;
            Stream.Next();
            BasicValue right=ParseComparison();

            expr=new Or(expr,right,OP);

        }
        
        return expr;

    }





    public BasicValue ParseComparison(){
        BasicValue left=ParseTerm();
 

        if(!Stream.EOL && Stream.Match([TokenIDS.MoreEqualOper,TokenIDS.LessEqualOper,TokenIDS.LessOper,TokenIDS.MoreOper ,TokenIDS.EqualOper])){
            Token OP=Stream.Current;
            if(Stream.Match([TokenIDS.MoreEqualOper] )){
                Stream.Next();
                BasicValue right=ParseTerm();
                return new MoreEqual(left,right,OP);
                
            }
            if(Stream.Match([TokenIDS.LessEqualOper] )){
                Stream.Next();
                BasicValue right=ParseFactor();
                return new LessEqual(left,right,OP);
                
            }
            if(Stream.Match([TokenIDS.LessOper] )){
                Stream.Next();
                BasicValue right=ParseFactor();
                return new Less(left,right,OP);
                

            }
            if(Stream.Match([TokenIDS.MoreOper] )){
                Stream.Next();
                BasicValue right=ParseFactor();
                return new More(left,right,OP);
                
            }
            if(Stream.Match([TokenIDS.EqualOper] )){
                Stream.Next();
                BasicValue right=ParseFactor();
                return new Equal(left,right,OP);
                
            }



       }
       return left;


    }

    public BasicValue ParseTerm(){

        BasicValue expr=ParseFactor();
        while(!Stream.EOL && Stream.Match([TokenIDS.AddOper,TokenIDS.RestOper] )){
            
            if(Stream.Match([TokenIDS.AddOper] )){
                Token OP=Stream.Current;
                Stream.Next();

                BasicValue right=ParseFactor();
                
                expr=new Add(expr,right,OP);
                
                
            }
            if(Stream.Match([TokenIDS.RestOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                
                BasicValue right=ParseFactor();
               
                expr=new Rest(expr,right,OP);
                
            }



       }
       return expr;


    }
    
    public BasicValue ParseFactor(){
        BasicValue expr=ParsePow();
        while(!Stream.EOL && Stream.Match([TokenIDS.MultOper,TokenIDS.DivOper,TokenIDS.ModOper] )){
            
            if(Stream.Match([TokenIDS.MultOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                BasicValue right=ParsePow();

                expr=new Mult(expr,right,OP);
                
            }
            if(Stream.Match([TokenIDS.DivOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                BasicValue right=ParsePow();
                expr=new Div(expr,right,OP);
                
            }
            if(Stream.Match([TokenIDS.ModOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                BasicValue right=ParsePow();
                
                expr=new Mod(expr,right,OP);
                
            }


       }
       return expr;


    }
    public BasicValue ParsePow(){
        
       BasicValue expr=ParseParenteses();

       while(!Stream.EOL && Stream.Match([TokenIDS.PowOper] )){
        Token OP=Stream.Current;
        Stream.Next();
        
        BasicValue right=ParseParenteses();
        
        expr=new Pow(expr,right,OP);


       }
       return expr;

    }
    public BasicValue ParseParenteses(){
        BasicValue expr;
        if(Stream.Match([TokenIDS.OpenParenteses] )){
            Token OP=Stream.Current;
            Stream.Next();
            expr=ParseExpression();
    
            if(!Stream.Match([TokenIDS.CloseParenteses] )){
                CE.Add(new Error("Se esperaba un cierre de parentesis )",Stream.Current.Position));
                throw new Exception("Se esperaba un parentesis");

            }
            Stream.Next();

       }else{
        expr=ParseUnary();
       }

       
       return expr;

    }

    public BasicValue ParseUnary(){
        if(Stream.EOL){
            CE.Add(new Error("Se esperaba un token de formato numero",Stream.Current.Position));
            throw new Exception("Se esperaba un Numero");

        }
        if(Stream.Current.Type==TokenType.Variable){
            Variable Value=new Variable(Stream.Current,ProgramEnvironment);
            Stream.Next();
            return Value ;

        }
        if(Stream.Current.Type==TokenType.Function){
            return ParseFunction();
           
        }
        BasicValue? Unary=null;
        if(Stream.Current.Type==TokenType.Number){
            Unary=new Number(Stream.Current);
        }
        if(Stream.Current.Type==TokenType.Bool){
            Unary=new Boolean(Stream.Current);
        }
        if(Stream.Current.Type==TokenType.String){
            Unary=new String(Stream.Current);
        }
        if(Unary ==null){
            CE.Add(new Error("Se esperaba un valor",Stream.Current.Position));
            throw new Exception("Se esperaba un valor");
        }
        Stream.Next();

        return Unary;
    }
    public BasicValue ParseFunction(){
        Token Caller=Stream.Current;
        Stream.Next();

        if(!Stream.Consume(TokenIDS.OpenParenteses,"Sintaxis del llamado invalido se esperaba (")){
            throw new Exception("Se esperaba un (");
        }
        List<BasicValue>Arguments=new List<BasicValue>();
        BasicValue arg;
        try{
            arg= ParseExpression();

        }catch{
            throw new Exception("Error al obtener argumento");
 
        }
        Arguments.Add(arg);
        while(Stream.Match([TokenIDS.SeparatorParam])){
            Stream.Next();

            try{
                arg= ParseExpression();
            }catch{
                throw new Exception("Error al obtener argumento");
            }
        }

        if(!Stream.Consume(TokenIDS.CloseParenteses,"Sintaxis del bucle while invalida se esperaba )",initialAdvance:false)){
            throw new Exception("Se esperaba un )");

        }
        Calleable? calleable=CR.ProgramEnvironment.GetFunc(Caller);
        if(calleable==null){
            throw new Exception("Funcion no definida");
        }
        calleable.Execute(Arguments);
        if(calleable.Value==null){
            CE.Add(new Error($"Se esperaba un valor devuelto por la funcion {Caller.Value}",Stream.Current.Position));
            throw new Exception("Se esperaba un valor devuelto por la funcion");
        }

       
        Stream.Next();

        return new UknownValue(calleable.Value);
    }
    


}