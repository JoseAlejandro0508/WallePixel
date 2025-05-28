class WhileBucle:Statement{
    public Statement Body;
    public BasicValue<bool> Condition;
    public Token WhileID;

    public WhileBucle(Compiling CR,Statement Body,BasicValue<bool> Condition,Token WhileID) :base(CR){

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

        while(Condition.Value){
            Body.Execute();
            
            if(!Condition.CheckSemantic(CompilatorRef.CE)){
                CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional while", WhileID.Position));
                return;
            };
        }
    }
}