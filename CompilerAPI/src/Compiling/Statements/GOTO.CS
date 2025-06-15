class GOTOBucle:Statement{
    public BasicValue Condition;
    public Token GOTOID;
    public Token Tag;

    public GOTOBucle(Compiling CR,BasicValue Condition,Token GOTOID,Token Tag) :base(CR){

        this.GOTOID=GOTOID;
        this.Condition=Condition;
        this.Tag=Tag;

       
        
        

    }
    public override void Execute()
    {
        
        if(!Condition.CheckSemantic(CompilatorRef.CE)){
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del GoTo", GOTOID.Position,ErrorType.RuntimeError));
            return;
        };
        if(!(Condition.Value is bool)){
            CompilatorRef.CE.Add(new Error($"La declaracion GoTo solo acepta condicionales booleanos", GOTOID.Position,ErrorType.SemanticError));
            return;
        };

        if((bool)Condition.Value){
            CompilatorRef.BucleCount++;
            if(CompilatorRef.BucleCount>GlobalDates.MaxBucleable){
                

                CompilatorRef.CE.Add(new Error("Hay un bucle GoTo que es potencialmente infinito ,por razones de eficiencia no lo seguiremos ejecutando", GOTOID.Position,ErrorType.RuntimeError));
            }
            int Objective;
            if(!CompilatorRef.ProgramEnvironment.GetTagPos(Tag, out Objective)){
                CompilatorRef.CE.Add(new Error("La etiqueta del GoTo no ha sido declarada", GOTOID.Position,ErrorType.RuntimeError));
                return;
            }

            CompilatorRef.PosInterp=Objective;
        }
    }
}