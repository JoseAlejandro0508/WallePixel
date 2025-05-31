public class Error{
    private string Info;
    private Location? ErrorLocation;

    
    public Error(string Info, Location? ErrorLocation){
        this.Info = Info;
        this.ErrorLocation = ErrorLocation;

    }

}