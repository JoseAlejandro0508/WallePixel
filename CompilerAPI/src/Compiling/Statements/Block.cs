using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;

public class Block:Statement{
    public List<Statement?> Declarations;
    

    public Block(Compiling CR,List<Statement?>  Declarations) :base(CR){
        this.Declarations=Declarations;
        

    }
    public override void Execute()
    {
        //CompilatorRef.ProgramEnvironment.AddEnclosing();
        foreach (Statement? s in Declarations)
        {
            if(s is null)continue;
            
            s.Execute();
            if(CompilatorRef.CE.Count!=0){
                return;

            }
            

        }
    }
}