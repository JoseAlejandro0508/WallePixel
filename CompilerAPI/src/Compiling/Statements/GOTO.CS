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
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del GoTo", GOTOID.Position));
            return;
        };
        if(!(Condition.Value is bool)){
            CompilatorRef.CE.Add(new Error($"La declaracion GoTo solo acepta condicionales booleanos", GOTOID.Position));
            return;
        };

        if((bool)Condition.Value){
            int Objective;
            if(!CompilatorRef.ProgramEnvironment.GetTagPos(Tag, out Objective)){
                CompilatorRef.CE.Add(new Error("La etiqueta del GoTo no ha sido declarada", GOTOID.Position));
                return;
            }

            CompilatorRef.PosInterp=Objective;
        }
    }
}