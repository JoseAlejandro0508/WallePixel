class WhileBucle:Statement{
    public Block Body;
    public BasicValue Condition;
    public Token WhileID;

    public WhileBucle(Compiling CR,Block Body,BasicValue Condition,Token WhileID) :base(CR){

        this.Body=Body;
        this.WhileID=WhileID;
        this.Condition=Condition;
        
        

    }
    public override void Execute()
    {
        int count=0;
        
        if(!Condition.CheckSemantic(CompilatorRef.CE)){
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional while", WhileID.Position,ErrorType.RuntimeError));
            return;
        };
        if(!(Condition.Value is bool)){
            CompilatorRef.CE.Add(new Error($"La declaracion while solo acepta condicionales booleanos", WhileID.Position,ErrorType.SemanticError));
            return;
        };

        while((bool)Condition.Value){
            Body.Execute();

            if(!Condition.CheckSemantic(CompilatorRef.CE)){
                CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional while", WhileID.Position,ErrorType.RuntimeError));
                return;
            };
            if(count>GlobalDates.MaxBucleable){
                CompilatorRef.CE.Add(new Error("El bucle declarado es potencialmente infinito ,por razones de eficiencia no lo seguiremos ejecutando", WhileID.Position,ErrorType.RuntimeError));
                return;
            }
            if(CompilatorRef.CE.Count!=0){
                return;

            }
            count++;
        }
    }
}