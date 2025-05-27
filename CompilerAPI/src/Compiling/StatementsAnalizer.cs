using System.ComponentModel.DataAnnotations;

public class Compiling
{

    public Environment ProgramEnvironment;
    public List<Error> CE;
    public Parser parser;
    public Compiling(Environment env, List<Error> CE, Parser parser)
    {
        ProgramEnvironment = env;
        this.CE = CE;
        this.parser = parser;
    }
    public Statement Declaration()
    {
        Statement Declaration=null;
        if (VariableAssign(out Declaration))
        {
            return Declaration;
        }



        if(!parser.Stream.EOL){
            CE.Add(new Error($"Declaracion no reconocida", parser.Stream.Current.Position));
            parser.Stream.Syncronize();
           
        }
        if(!parser.Stream.EOF){
            parser.Stream.Next();
                
        }


        return Declaration;


    }


    public bool VariableAssign(out Statement Declaration)
    {
        Declaration = null;
        if (parser.Stream.Current.Type != TokenType.Variable)
        {
            return false;
        }
        Token ID = parser.Stream.Current;
        if (!parser.Stream.Consume(TokenIDS.AssignatorOper, "Se esperaba el operador de asignacion")) {
            parser.Stream.Syncronize();
            return false;
            
        }
        Token OP = parser.Stream.Current;
        IValue value = parser.ParseExpression();
        if(!parser.Stream.EOL){
            CE.Add(new Error($"Sintaxis de asignacion invalida de declaracion", parser.Stream.Current.Position));
            parser.Stream.Syncronize();
            return false;

        }
        if (value is BasicValue<int>)
        {

            Variable<int> var_ = new Variable<int>(ID, value as BasicValue<int>, OP);
            VariableAssign asignament = new VariableAssign(this, var_);
            
            return true;


        }
        CE.Add(new Error($"Se le esta asingando un tipo no permitido a la variable {ID.Value}", parser.Stream.Current.Position));
        parser.Stream.Syncronize();
        return false;


    }
    public bool BlockParse(out Statement Declaration_)
    {
        Declaration_=null;
        List<Statement>statements = new List<Statement>();

        if(!parser.Stream.Match([TokenIDS.OpenBlock])){
            return false;
        }
        parser.Stream.Next();
        while(!parser.Stream.EOF && !parser.Stream.Match([TokenIDS.OpenBlock])){
            statements.Add(Declaration());

        }
        if(!parser.Stream.Match([TokenIDS.CloseBlock])){
            CE.Add(new Error("Se esperaba el cierre del bloque }", parser.Stream.Current.Position));
            return false;
        }
        parser.Stream.Next();
        if(parser.Stream.EOL){
            Declaration_=new Block(this,statements);
            if(!parser.Stream.EOF){
                parser.Stream.Next();

            }
           
            return true;
        }
        CE.Add(new Error($"No debe escribir nada a continuacion del cierre del bloque }", parser.Stream.Current.Position));
        parser.Stream.Syncronize();
        return false;


    }
    public bool IFParse(out Statement Declaration_)
    {
        Declaration_=null;


        if(!parser.Stream.Match([TokenIDS.IF])){
            return false;
        }
        Token OP=parser.Stream.Current;
        if(!parser.Stream.Consume(TokenIDS.OpenParenteses,"Sintaxis del condicional if invalida se esperaba (")){
            parser.Stream.Syncronize();
            return false;
        }
        IValue Condition = parser.ParseExpression();
        if(!(Condition is BasicValue<bool>)){
            CE.Add(new Error("El condicional if solo acepta parametros de tipo booleanos", parser.Stream.Current.Position));
            parser.Stream.Syncronize();
            return false;
        }
        if(!(Condition as BasicValue<bool>).CheckSemantic(CE)){
            CE.Add(new Error("Error en la condicion del condicional", parser.Stream.Current.Position));
            return false;
        }
        if(!parser.Stream.Consume(TokenIDS.CloseParenteses,"Sintaxis del condicional if invalida se esperaba (")){
            parser.Stream.Syncronize();
            return false;
        }
        
        Statement IFBlockDeclaration=null;
        if(!BlockParse(out IFBlockDeclaration)){
            CE.Add(new Error("Error de sintaxis en la declaracion del bloque del condicional if", parser.Stream.Current.Position));
            return false;
        }
        Statement ELSEBlockDeclaration=null;
        
        if(parser.Stream.Match([TokenIDS.ELSE])){
            parser.Stream.Next();
            if(!BlockParse(out ELSEBlockDeclaration)){
                CE.Add(new Error("Error de sintaxis en la declaracion del bloque del condicional if", parser.Stream.Current.Position));
                return false;
            }

        }

        if(parser.Stream.EOL){
            Declaration_=new IF(this,IFBlockDeclaration,ELSEBlockDeclaration,Condition as BasicValue<bool>,OP);
            return true;
        }
        CE.Add(new Error($"No debe escribir nada a continuacion del cierre del bloque }", parser.Stream.Current.Position));
        parser.Stream.Syncronize();
        return false;


    }


}