class TagAsignament:Statement{
    Token TagID;

    public TagAsignament(Compiling CR,Token TagID) :base(CR){
        this.TagID=TagID;
        

    }
    public override void Execute()
    {
        CompilatorRef.ProgramEnvironment.AddTag(TagID,CompilatorRef.PosInterp);
    }
}