class IF:Statement{
    public Statement IfThen;
    public Statement ElseThen;
    public BasicValue<bool> Condition;
    Token IFID;

    public IF(Compiling CR,Statement IfThen,Statement ElseThen,BasicValue<bool> Condition,Token IFID) :base(CR){
        this.IfThen=IfThen;
        this.ElseThen=ElseThen;
        this.Condition=Condition;
        this.IFID=IFID;
        

    }
    public override void Execute()
    {
        try{
            Condition.CheckSemantic(CompilatorRef.CE);
        }catch{
            CompilatorRef.CE.Add(new Error($"Error al obtener el valor de la condicion del condicional if", IFID.Position));
            throw new Exception();
        }
        if(Condition.Value){
            IfThen.Execute();
        }else{
            if(ElseThen!=null)
            {
                ElseThen.Execute();
            }
            
        }
    }
}