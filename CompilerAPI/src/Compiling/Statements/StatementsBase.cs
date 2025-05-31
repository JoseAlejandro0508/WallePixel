public interface IStatement{
    public void Execute();
}
public abstract class Statement:IStatement{
    public Compiling CompilatorRef;
    public Statement(Compiling compiling){
        CompilatorRef = compiling;
    }
    abstract public void Execute();

}