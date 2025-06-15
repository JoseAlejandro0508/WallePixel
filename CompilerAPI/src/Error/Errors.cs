public class Error{
    public string Info;
    public Location? ErrorLocation;
    public ErrorType Type;
    
    public Error(string Info, Location? ErrorLocation,ErrorType Type=ErrorType.Unknown){
        this.Info = Info;
        this.ErrorLocation = ErrorLocation;
        this.Type = Type;

    }
    public override string ToString() {

        if(ErrorLocation != null){
            return $"[ {Type} ] (Line:{(ErrorLocation as dynamic).Row},Column:{(ErrorLocation as dynamic).Column}) {Info}\n";

        }
         return $"[ {Type} ] {Info}\n";
        
    }

}
public enum ErrorType{
    Unknown,
    LexicError,
    SemanticError,
    SintacticError,
    RuntimeError,
}