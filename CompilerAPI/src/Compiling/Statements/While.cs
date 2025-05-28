class WhileBucle:Statement{
    public Statement Body;
    public BasicValue Condition;
    public Token WhileID;

    public WhileBucle(Compiling CR,Statement Body,BasicValue Condition,Token WhileID) :base(CR){

        this.Body=Body;
        this.WhileID=WhileID;
        this.Condition=Condition;
        
        

    }
    public override void Execute()
    {
        
        if(!Condition.CheckSemantic(CompilatorRef.CE)){
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional while", WhileID.Position));
            return;
        };
        if(!(Condition.Value is bool)){
            CompilatorRef.CE.Add(new Error($"La declaracion while solo acepta condicionales booleanos", WhileID.Position));
            return;
        };

        while((bool)Condition.Value){
            Body.Execute();
            
            if(!Condition.CheckSemantic(CompilatorRef.CE)){
                CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional while", WhileID.Position));
                return;
            };
        }
    }
}