using System.Runtime.Intrinsics.Arm;

class Block:Statement{
    public List<Statement> Declarations;
    

    public Block(Compiling CR,List<Statement>  Declarations) :base(CR){
        this.Declarations=Declarations;
        

    }
    public override void Execute()
    {
        //CompilatorRef.ProgramEnvironment.AddEnclosing();
        foreach (Statement s in Declarations){
            s.Execute();

        }
    }
}