using System.ComponentModel.DataAnnotations;

public class Compiling
{

    public Environment ProgramEnvironment;
    public List<Error> CE;
    public List<string> Logs;
    private List<Statement> Declarations = new List<Statement>();
    public int PosInterp = 0;
    public int  BucleCount=0;
    public Parser parser;
    public Compiling(Environment env, List<Error> CE,List<string> Logs, Parser parser)
    {
        ProgramEnvironment = env;
        this.CE = CE;
        this.parser = parser;
        this.Logs = Logs;
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "Spawn"),
            new Spawn(new Token(null, TokenType.Function, "Spawn"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "DrawLine"),
            new DrawLine(new Token(null, TokenType.Function, "DrawLine"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "Color"),
            new BrushColor(new Token(null, TokenType.Function, "Color"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "DrawCircle"),
            new DrawCircle(new Token(null, TokenType.Function, "DrawCircle"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "Size"),
            new BrushSize(new Token(null, TokenType.Function, "Size"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "DrawRectangle"),
            new DrawRectangle(new Token(null, TokenType.Function, "DrawRectangle"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "Fill"),
            new Fill(new Token(null, TokenType.Function, "Fill"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "GetActualX"),
            new GetActualX(new Token(null, TokenType.Function, "GetActualX"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "GetActualY"),
            new GetActualY(new Token(null, TokenType.Function, "GetActualY"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "GetCanvasSize"),
            new GetCanvasSize(new Token(null, TokenType.Function, "GetCanvasSize"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "GetColorCount"),
            new GetColorCount(new Token(null, TokenType.Function, "GetColorCount"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "IsBrushColor"),
            new IsBrushColor(new Token(null, TokenType.Function, "IsBrushColor"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "IsBrushSize"),
            new IsBrushSize(new Token(null, TokenType.Function, "IsBrushSize"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "IsCanvasColor"),
            new IsColor(new Token(null, TokenType.Function, "IsCanvasColor"), this)
        );
        ProgramEnvironment.DefineFunc(new Token(null, TokenType.Function, "print"),
            new Print(new Token(null, TokenType.Function, "print"), this)
        );


    }
    public void Interprete(bool Autoexecute=false)
    {
        
        SpawnValidCheck();
        ExecuteTags();
        if (CE.Count != 0)
        {
            CE.Add(new Error("Hay errores no es posible compilar", null,ErrorType.RuntimeError));
            return;
        }
        if(!CheckBucle(Autoexecute)){
                GlobalDates.BoardInstance.CreateBoard();
        }

        while (PosInterp < Declarations.Count)
        {
            if(Autoexecute&& (Declarations[PosInterp] is WhileBucle || Declarations[PosInterp] is GOTOBucle )){
                Logs.Add("En el modo de auto ejecucion los bucles no se ejecutan por razones de rendimiento");
                PosInterp++;
                continue;

            }

            Declarations[PosInterp].Execute();
            if (CE.Count != 0)
            {
                return;

            }
            PosInterp++;
        }

    }
    public bool CheckBucle(bool Autoexecute){
        for(int i = 0;i<Declarations.Count;i++){
            if(Autoexecute&& (Declarations[i] is WhileBucle || Declarations[i] is GOTOBucle )){

               return true;

            }
        }
        return false;
    }
    public bool SpawnValidCheck(){
        if(Declarations.Count==0){
            CE.Add(new Error("El codigo es invalido.Debe comenzar con un comando Spawn",null,ErrorType.RuntimeError));
            return false;
        }
        if(Declarations[0]  is not FunctionCall){
            CE.Add(new Error("El codigo es invalido.Debe comenzar con un comando Spawn",null,ErrorType.RuntimeError));
            return false;
        }
        if((Declarations[0] as FunctionCall)!.Caller.Value!="Spawn"){
            CE.Add(new Error("El codigo es invalido.Debe comenzar con un comando Spawn",null,ErrorType.RuntimeError));
            return false;
        }
        for (int i = 1;i<Declarations.Count;i++){
                if(Declarations[i] is FunctionCall){
                    if((Declarations[i] as FunctionCall)!.Caller.Value=="Spawn"){
                        CE.Add(new Error("El codigo es invalido.Debe contener solo un comando Spawn",(Declarations[i] as FunctionCall)!.Caller.Position,ErrorType.RuntimeError));
                        return false;

                    }

                }
        }
        

        return true;
    }
    public void ExecuteTags(){
        bool found = true;
        while (found){
            found=false;
            for (int i = 0;i<Declarations.Count;i++){
                if(Declarations[i] is TagAsignament){
                    PosInterp=i;
                    found=true;
                    Declarations[i].Execute();
                    Declarations.RemoveAt(i);
                    break;
                }
        }
        PosInterp =0;


        }


    }
    public void GetAllDeclarations()
    {
        while (!parser.Stream.EOF)
        {
            Statement? Dec = Declaration();
            if (Dec is null) continue;
            Declarations.Add(Dec);

        }

    }
    public Statement? Declaration()
    {
        int ActualPos = parser.Stream.GetPos();
        Statement? Declaration = null;
        if (TagParse(out Declaration))
        {
            return Declaration!;
        }
        if (VariableAssignParse(out Declaration))
        {
            return Declaration!;
        }
        if (IFParse(out Declaration))
        {
            return Declaration!;
        }
        if (WhileParse(out Declaration))
        {
            return Declaration!;
        }
        if (FunctionParse(out Declaration))
        {
            return Declaration!;
        }
        if (FunDeclarationParse(out Declaration))
        {
            return Declaration!;
        }
        if (GoToParse(out Declaration))
        {
            return Declaration!;
        }
        if (ReturnParse(out Declaration))
        {
            return Declaration!;
        }




        if (!parser.Stream.EOL)
        {
            CE.Add(new Error($"Declaracion no reconocida", parser.Stream.Current.Position,ErrorType.SintacticError));
            parser.Stream.Syncronize();

        }
        if (!parser.Stream.EOF)
        {
            parser.Stream.Next();

        }



        return Declaration;


    }
    public bool TagParse(out Statement? Declaration)
    {
        Declaration = null;
        if (parser.Stream.Current.Type != TokenType.Tag)
        {
            return false;
        }
        Token TagID = parser.Stream.Current;
        parser.Stream.Next();
        if (!parser.Stream.EOL)
        {
            CE.Add(new Error($"Sintaxis de asignacion de etiqueta invalida", parser.Stream.Current.Position,ErrorType.SintacticError));
            parser.Stream.Syncronize();
            return false;

        }
        Declaration = new TagAsignament(this, TagID);
        parser.Stream.Next();
        return true;


    }

    public bool VariableAssignParse(out Statement? Declaration_)
    {
        Declaration_ = null;
        if (parser.Stream.Current.Type != TokenType.Variable)
        {
            return false;
        }
        Token ID = parser.Stream.Current;

        parser.Stream.Next();

        
        Token OP = parser.Stream.Current;
        if (!parser.Stream.Consume(TokenIDS.AssignatorOper, "Se esperaba el operador de asignacion", initialAdvance: false))
        {
            parser.Stream.Syncronize();
            return false;

        }

        BasicValue value;
        try
        {
            value = parser.ParseExpression();

        }
        catch
        {
            parser.Stream.Syncronize();
            return false;
        }

        if (!parser.Stream.EOL)
        {
            CE.Add(new Error($"Sintaxis de asignacion invalida de declaracion", parser.Stream.Current.Position,ErrorType.SintacticError));
            parser.Stream.Syncronize();
            return false;

        }

        Declaration_ = new VariableAssign(this, value, ID, OP);
        parser.Stream.Next();
        return true;




    }
    public bool BlockParse(out Statement? Declaration_)
    {
        Declaration_ = null;
        List<Statement?> statements = new List<Statement?>();
        if (!parser.Stream.EOF && parser.Stream.EOL) parser.Stream.Next();
        if (!parser.Stream.Match([TokenIDS.OpenBlock]))
        {
            return false;
        }
        parser.Stream.Next();
        if (!parser.Stream.EOF && parser.Stream.EOL) parser.Stream.Next();
        while (!parser.Stream.EOF && !parser.Stream.Match([TokenIDS.CloseBlock]))
        {
            statements.Add(Declaration());


        }
        if (!parser.Stream.Match([TokenIDS.CloseBlock]))
        {
            CE.Add(new Error("Se esperaba el cierre del bloque }", parser.Stream.Current.Position,ErrorType.SintacticError));
            return false;
        }
        parser.Stream.Next();
        if (parser.Stream.EOL)
        {
            Declaration_ = new Block(this, statements);
            if (!parser.Stream.EOF)
            {
                parser.Stream.Next();

            }

            return true;
        }
        CE.Add(new Error($"No debe escribir nada a continuacion del cierre del bloque", parser.Stream.Current.Position,ErrorType.SintacticError));
        parser.Stream.Syncronize();
        return false;


    }
    public bool IFParse(out Statement? Declaration_)
    {
        Declaration_ = null;


        if (!parser.Stream.Match([TokenIDS.IF]))
        {
            return false;
        }
        Token OP = parser.Stream.Current;
        if (!parser.Stream.Consume(TokenIDS.OpenParenteses, "Sintaxis del condicional if invalida se esperaba ("))
        {
            parser.Stream.Syncronize();
            return false;
        }

        BasicValue Condition;
        try
        {
            Condition = parser.ParseExpression();
        }
        catch
        {
            parser.Stream.Syncronize();
            return false;
        }

        if (!parser.Stream.Consume(TokenIDS.CloseParenteses, "Sintaxis del condicional if invalida se esperaba )", initialAdvance: false))
        {
            parser.Stream.Syncronize();
            return false;
        }

        Statement? IFBlockDeclaration;
        if (!BlockParse(out IFBlockDeclaration))
        {
            CE.Add(new Error("Error de sintaxis en la declaracion del bloque del condicional if", parser.Stream.Current.Position,ErrorType.SintacticError));
            return false;
        }
        Statement? ELSEBlockDeclaration = null;

        if (parser.Stream.Match([TokenIDS.ELSE]))
        {
            parser.Stream.Next();
            if (!BlockParse(out ELSEBlockDeclaration))
            {
                CE.Add(new Error("Error de sintaxis en la declaracion del bloque del condicional else", parser.Stream.Current.Position,ErrorType.SintacticError));
                return false;
            }

        }


        Declaration_ = new IF(this, IFBlockDeclaration!, ELSEBlockDeclaration, Condition, OP);
        return true;


    }
    public bool WhileParse(out Statement? Declaration_)
    {
        Declaration_ = null;


        if (!parser.Stream.Match([TokenIDS.WHILEBulce]))
        {
            return false;
        }

        Token OP = parser.Stream.Current;

        if (!parser.Stream.Consume(TokenIDS.OpenParenteses, "Sintaxis del bucle while invalida se esperaba ("))
        {
            parser.Stream.Syncronize();
            return false;
        }
        BasicValue Condition;
        try
        {
            Condition = parser.ParseExpression();

        }
        catch
        {
            parser.Stream.Syncronize();
            return false;

        }

        if (!parser.Stream.Consume(TokenIDS.CloseParenteses, "Sintaxis del bucle while invalida se esperaba )", initialAdvance: false))
        {
            parser.Stream.Syncronize();
            return false;
        }

        Statement? Body = null;
        if (!BlockParse(out Body))
        {
            CE.Add(new Error("Error de sintaxis en la declaracion del bloque while", parser.Stream.Current.Position,ErrorType.SintacticError));
            return false;
        }


        Declaration_ = new WhileBucle(this, Body! as Block, Condition, OP);
        return true;


    }

    public bool GoToParse(out Statement? Declaration_)
    {

        Declaration_ = null;


        if (!parser.Stream.Match([TokenIDS.GoToBucle]))
        {
            return false;
        }

        Token OP = parser.Stream.Current;

        if (!parser.Stream.Consume(TokenIDS.OpenCorchete, "Sintaxis del bucle GoTo invalida se esperaba ["))
        {
            parser.Stream.Syncronize();
            return false;
        }


        if (parser.Stream.Current.Type != TokenType.Tag)
        {
            CE.Add(new Error("Se esperaba una etiqueta", parser.Stream.Current.Position));
        }
        Token TagID = parser.Stream.Current;
        if (!parser.Stream.Consume([TokenIDS.CloseCorchete, TokenIDS.OpenParenteses], ["Sintaxis del bucle GoTo invalida se esperaba ]", "Sintaxis del bucle GoTo invalida se esperaba ("]))
        {
            parser.Stream.Syncronize();
            return false;
        }


        BasicValue Condition = parser.ParseExpression();

        if (!parser.Stream.Consume(TokenIDS.CloseParenteses, "Sintaxis del bucle GoTo invalida se esperaba )", initialAdvance: false))
        {
            parser.Stream.Syncronize();
            return false;
        }




        if (parser.Stream.EOL)
        {
            Declaration_ = new GOTOBucle(this, Condition, OP, TagID);
            return true;
        }
        CE.Add(new Error($"No debe escribir nada a continuacion del cierre del GoTo", parser.Stream.Current.Position,ErrorType.SintacticError));
        parser.Stream.Syncronize();
        return false;


    }
    public bool FunctionParse(out Statement? Declaration_)
    {
        Declaration_ = null;


        if (parser.Stream.Current.Type != TokenType.Function)
        {
            return false;
        }

        Token Caller = parser.Stream.Current;

        if (!parser.Stream.Consume(TokenIDS.OpenParenteses, "Sintaxis del llamado invalido se esperaba ("))
        {
            parser.Stream.Syncronize();
            return false;
        }
        List<BasicValue> Arguments = new List<BasicValue>();
        if (!parser.Stream.Match([TokenIDS.CloseParenteses]))
        {




            BasicValue arg;
            try
            {
                arg = parser.ParseExpression();

            }
            catch
            {

                parser.Stream.Syncronize();
                return false;

            }
            Arguments.Add(arg);
            while (parser.Stream.Match([TokenIDS.SeparatorParam]))
            {
                parser.Stream.Next();

                try
                {
                    arg = parser.ParseExpression();
                    Arguments.Add(arg);
                }
                catch
                {
                    parser.Stream.Syncronize();
                    return false;
                }
            }

            if (!parser.Stream.Consume(TokenIDS.CloseParenteses, "Sintaxis del cierre de llamada invalido se esperaba )", initialAdvance: false))
            {
                parser.Stream.Syncronize();
                return false;
            }
        }else{
            parser.Stream.Next();
        }


        if (parser.Stream.EOL)
        {
            Declaration_ = new FunctionCall(this, Caller, Arguments);
            return true;
        }
        CE.Add(new Error($"No debe escribir nada a continuacion del cierre del bloque ", parser.Stream.Current.Position,ErrorType.SintacticError));
        parser.Stream.Syncronize();
        return false;


    }
    public bool FunDeclarationParse(out Statement? Declaration_)
    {
        Declaration_ = null;
        if (!parser.Stream.Match([TokenIDS.FUNASIGN]))
        {
            return false;
        }
        Token Asign = parser.Stream.Current;
        parser.Stream.Next();

        if (parser.Stream.Current.Type != TokenType.Function)
        {
            return false;
        }

        Token Caller = parser.Stream.Current;

        if (!parser.Stream.Consume(TokenIDS.OpenParenteses, "Sintaxis de declaracion invalida se esperaba ("))
        {
            parser.Stream.Syncronize();
            return false;
        }
        List<Token> Arguments = new List<Token>();
        if (!parser.Stream.Match([TokenIDS.CloseParenteses]))
        {

            Token arg;

            try
            {
                arg = parser.Stream.Current;

            }
            catch
            {
                parser.Stream.Syncronize();
                return false;

            }

            Arguments.Add(arg);
            parser.Stream.Next();
            while (parser.Stream.Match([TokenIDS.SeparatorParam]))
            {
                parser.Stream.Next();

                try
                {
                    arg = parser.Stream.Current;
                }
                catch
                {
                    parser.Stream.Syncronize();
                    return false;
                }
                parser.Stream.Next();
            }

            if (!parser.Stream.Consume(TokenIDS.CloseParenteses, "Sintaxis de declaracion de funcion invalida se esperaba )", initialAdvance: false))
            {
                parser.Stream.Syncronize();
                return false;
            }
        }else{
            parser.Stream.Next();
        }
        Statement? Body = null;
        if (!BlockParse(out Body))
        {
            CE.Add(new Error("Error de sintaxis en la declaracion de la funcion", parser.Stream.Current.Position,ErrorType.SintacticError));
            return false;
        }
        DefinedFunc func = new DefinedFunc(Caller, this, (Body as Block)!, Arguments);

        Declaration_ = new FunctionAsign(this, Caller, Asign, func);
        return true;


    }

    public bool ReturnParse(out Statement? Declaration_)
    {
        Declaration_ = null;
        if (!parser.Stream.Match([TokenIDS.Return]))
        {
            return false;
        }
        Token ID = parser.Stream.Current;
        parser.Stream.Next();
        BasicValue value;
        try
        {
            value = parser.ParseExpression();

        }
        catch
        {
            parser.Stream.Syncronize();
            return false;
        }

        if (!parser.Stream.EOL)
        {
            CE.Add(new Error($"Sintaxis de retorno invalida", parser.Stream.Current.Position,ErrorType.SintacticError));
            parser.Stream.Syncronize();
            return false;

        }

        Declaration_ = new Return(this,  ID, value);
        parser.Stream.Next();
        return true;




    }




}