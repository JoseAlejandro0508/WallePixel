public class Parser{
    public List<Error>  CE;
    public TokenStream Stream;
    public Parser(TokenStream Stream,List<Error> CE){
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
            Token OP=Stream.Current;
            if(Stream.Match([TokenIDS.AddOper] )){
                Stream.Next();
                BasicValue<int> right=ParseFactor();
                expr=new Add(expr,right,OP);
                
            }
            if(Stream.Match([TokenIDS.RestOper] )){
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
            Token OP=Stream.Current;
            if(Stream.Match([TokenIDS.MultOper] )){
                Stream.Next();
                BasicValue<int> right=ParsePow();
                expr=new Mult(expr,right,OP);
                
            }
            if(Stream.Match([TokenIDS.DivOper] )){
                Stream.Next();
                BasicValue<int> right=ParsePow();
                expr=new Div(expr,right,OP);
                
            }
            if(Stream.Match([TokenIDS.ModOper] )){
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
                throw new Exception("Solo se permite el parentizado en operaciones numericas");
            }
            expr=ParseExpression() as BasicValue<int>;

            if(!Stream.Match([TokenIDS.CloseParenteses] )){
                throw new Exception("Parentesis no cerrado");

            }
            Stream.Next();

       }else{
        expr=ParseNumber();
       }

       
       return expr;

    }

    public Number ParseNumber(){
        if(Stream.EOL){
            
            throw new Exception("Se esperaba un Numero");

        }
        Number number=new Number(Stream.Current);
       
        Stream.Next();

        return number;
    }
    


}