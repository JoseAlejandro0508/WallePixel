public abstract class Statement{
    public Compiling CompilatorRef;
    public Statement(Compiling compiling){
        CompilatorRef = compiling;
    }
    abstract public void Execute();

}