class IF:Statement{
    public Statement IfThen;
    public Statement ElseThen;
    public BasicValue Condition;
    Token IFID;

    public IF(Compiling CR,Statement IfThen,Statement ElseThen,BasicValue Condition,Token IFID) :base(CR){
        this.IfThen=IfThen;
        this.ElseThen=ElseThen;
        this.Condition=Condition;
        this.IFID=IFID;
        

    }
    public override void Execute()
    {
        if(!Condition.CheckSemantic(CompilatorRef.CE)){
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional if", IFID.Position));
            return;
        };
        if(!(Condition.Value is bool)){
            CompilatorRef.CE.Add(new Error($"La declaracion if solo acepta condicionales booleanos", IFID.Position));
            return;
        };
        if((bool)Condition.Value ){
            IfThen.Execute();
        }else{
            if(ElseThen!=null)
            {
                ElseThen.Execute();
            }
            
        }
    }
}