public class DefinedFunc:Calleable{
    
    public DefinedFunc(Token Caller_,Compiling CR_,Block Body,List<Token> ArgumentsID):base(Caller_,ArgumentsID.Count,CR_){
        this.ArgumentsID = ArgumentsID;
        this.Body = Body;

        
    }

    protected override bool CheckType(List<object> Arguments){

        return true;

    }
    protected override void call(List<object> Arguments){
        CompilatorRef.ProgramEnvironment.AddEnclosing();

        for(int i=0;i<ArgumentsID!.Count;i++){
                Token ID=ArgumentsID[i];
                object Value=Arguments[i];
                CompilatorRef.ProgramEnvironment.Assign(ID,new UknownValue(Value));
        }
        

        Body!.Execute();
        CompilatorRef.ProgramEnvironment.CloseEnclosing();

        

    }

}