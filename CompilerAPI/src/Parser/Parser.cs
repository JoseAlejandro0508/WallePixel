using System.Text.RegularExpressions;

public class Parser{
    public List<Error>  CE;
    public TokenStream Stream;
    public Environment ProgramEnvironment;
    public Parser(TokenStream Stream,List<Error> CE,Environment env){
        ProgramEnvironment = env;
        this.Stream = Stream;
        this.CE = CE;

    }


    public  IValue ParseExpression(){

        return ParseLogic();

    }

    public  IValue ParseLogic(){
        IValue expr=ParseOR();
        while(!Stream.EOL && Stream.Match([TokenIDS.AndOper])){

            if(! (expr is BasicValue<bool>)|| expr is null){
                CE.Add(new Error("Operacion logica no valida,solo es posible utilizar el operador and entre 2 booleanos",Stream.Current.Position));
                throw new Exception("Operacion logica invalida");
            }
            Token OP=Stream.Current;
            Stream.Next();
            IValue right=ParseOR();
            if( !(right is BasicValue<bool>) || right is null){
                throw new Exception("Operacion logica invalida");
            }
            expr=new And(expr as BasicValue<bool>,right as BasicValue<bool>,OP);

        }
        
        return expr;

    }
    public  IValue ParseOR(){
        IValue expr=ParseComparison();
        while(!Stream.EOL && Stream.Match([TokenIDS.OrOper])){

            if(! (expr is BasicValue<bool>)|| expr is null){
                CE.Add(new Error("Operacion logica no valida,solo es posible utilizar el operador or entre 2 booleanos",Stream.Current.Position));
                throw new Exception("Operacion logica invalida");
            }
            Token OP=Stream.Current;
            Stream.Next();
            IValue right=ParseComparison();
            if( !(right is BasicValue<bool>) || right is null){
                throw new Exception("Operacion logica invalida");
            }
            expr=new Or(expr as BasicValue<bool>,right as BasicValue<bool>,OP);

        }
        
        return expr;

    }





    public IValue ParseComparison(){
        BasicValue<int> left=ParseTerm();
 

        if(!Stream.EOL && Stream.Match([TokenIDS.MoreEqualOper,TokenIDS.LessEqualOper,TokenIDS.LessOper,TokenIDS.MoreOper ,TokenIDS.EqualOper])){
            Token OP=Stream.Current;
            if(Stream.Match([TokenIDS.MoreEqualOper] )){
                Stream.Next();
                BasicValue<int> right=ParseTerm();
                return new MoreEqual(left,right,OP);
                
            }
            if(Stream.Match([TokenIDS.LessEqualOper] )){
                Stream.Next();
                BasicValue<int> right=ParseFactor();
                return new LessEqual(left,right,OP);
                
            }
            if(Stream.Match([TokenIDS.LessOper] )){
                Stream.Next();
                BasicValue<int> right=ParseFactor();
                return new Less(left,right,OP);
                

            }
            if(Stream.Match([TokenIDS.MoreOper] )){
                Stream.Next();
                BasicValue<int> right=ParseFactor();
                return new More(left,right,OP);
                
            }
            if(Stream.Match([TokenIDS.EqualOper] )){
                Stream.Next();
                BasicValue<int> right=ParseFactor();
                return new Equal(left,right,OP);
                
            }



       }
       return left;


    }

    public BasicValue<int> ParseTerm(){

        BasicValue<int> expr=ParseFactor();
        while(!Stream.EOL && Stream.Match([TokenIDS.AddOper,TokenIDS.RestOper] )){
            
            if(Stream.Match([TokenIDS.AddOper] )){
                Token OP=Stream.Current;
                Stream.Next();

                BasicValue<int> right=ParseFactor();
                
                expr=new Add(expr,right,OP);
                
                
            }
            if(Stream.Match([TokenIDS.RestOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                
                BasicValue<int> right=ParseFactor();
               
                expr=new Rest(expr,right,OP);
                
            }



       }
       return expr;


    }
    
    public BasicValue<int> ParseFactor(){
        BasicValue<int> expr=ParsePow();
        while(!Stream.EOL && Stream.Match([TokenIDS.MultOper,TokenIDS.DivOper,TokenIDS.ModOper] )){
            
            if(Stream.Match([TokenIDS.MultOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                BasicValue<int> right=ParsePow();

                expr=new Mult(expr,right,OP);
                
            }
            if(Stream.Match([TokenIDS.DivOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                BasicValue<int> right=ParsePow();
                expr=new Div(expr,right,OP);
                
            }
            if(Stream.Match([TokenIDS.ModOper] )){
                Token OP=Stream.Current;
                Stream.Next();
                BasicValue<int> right=ParsePow();
                
                expr=new Mod(expr,right,OP);
                
            }


       }
       return expr;


    }
    public BasicValue<int> ParsePow(){
        
       BasicValue<int> expr=ParseParenteses();

       while(!Stream.EOL && Stream.Match([TokenIDS.PowOper] )){
        Token OP=Stream.Current;
        Stream.Next();
        
        BasicValue<int> right=ParseParenteses();
        
        expr=new Pow(expr,right,OP);


       }
       return expr;

    }
    public BasicValue<int> ParseParenteses(){
        BasicValue<int> expr;
        if(Stream.Match([TokenIDS.OpenParenteses] )){
            Token OP=Stream.Current;
            Stream.Next();
            IValue expr_=ParseExpression();
            if(!(expr_ is BasicValue<int>)){
                CE.Add(new Error("El parentizado solo esta permitido para operaciones numericas",Stream.Current.Position));
                throw new Exception("Solo se permite el parentizado en operaciones numericas");
            }
            expr=ParseExpression() as BasicValue<int>;

            if(!Stream.Match([TokenIDS.CloseParenteses] )){
                CE.Add(new Error("Se esperaba un cierre de parentesis )",Stream.Current.Position));
                throw new Exception("Parentesis no cerrado");

            }
            Stream.Next();

       }else{
        expr=ParseNumber();
       }

       
       return expr;

    }

    public BasicValue<int> ParseUnary(){
        if(Stream.EOL){
            CE.Add(new Error("Se esperaba un token de formato numero",Stream.Current.Position));
            
            throw new Exception("Se esperaba un Numero");

        }
        if(Stream.Current.Type==TokenType.Variable){
            object Value;
            if(!ProgramEnvironment.Get(Stream.Current,out Value)){
                CE.Add(new Error($"La variable {Stream.Current.Value} no ha sido asignada",Stream.Current.Position));
                throw new Exception("Variable no asignada");
            }
            if(!(Value is BasicValue<int>)){
                CE.Add(new Error($"La variable {Stream.Current.Value} no es numerica",Stream.Current.Position));
                throw new Exception("Variable no numerica");
            }
            Stream.Next();
            return Value as BasicValue<int>;

        }
        Number number=new Number(Stream.Current);
       
        Stream.Next();

        return number;
    }
    


}