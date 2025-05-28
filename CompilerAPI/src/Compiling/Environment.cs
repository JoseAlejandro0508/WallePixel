public class Environment{
    public Dictionary <string,Variable> Memory=new Dictionary <string, Variable> ();
    public List<Token> Tags=new List<Token>();
    public List<Error> CE;
    public Environment Enclosing;

    public Environment (){
        Enclosing=null;
    }
    public Environment (Environment enclosing){
        Enclosing=enclosing;
    }
    public void AddEnclosing(){
        if(Enclosing==null){
            Enclosing=new Environment ();
        }else{
            Enclosing.Enclosing=new Environment();
        }

    }
    public void Assign(Token ID, Variable value){
        if(Enclosing!=null){
            Enclosing.Assign (ID, value);
        }
        Memory[ID.Value]=value;

    }
    public bool Get(Token ID,out Variable Value){
        if(Enclosing!=null){
            if(Enclosing.Get(ID,out Value)){
                return true;
            }
        }
        Value=null;
        if(!Memory.ContainsKey(ID.Value)){
            CE.Add(new Error("La variable no existe en memoria",ID.Position));
            return false;
        }
        Value=Memory[ID.Value];
        return true;

    }
    public bool Check(Token ID,out Variable Value){
        if(Enclosing!=null){
            if(Enclosing.Get(ID,out Value)){
                return true;
            }
        }
        Value=null;
        if(!Memory.ContainsKey(ID.Value)){

            return false;
        }
        Value=Memory[ID.Value];
        return true;

    }
    public bool CheckTag(Token ID){
        foreach(Token Tag in Tags){
            if(ID.Value==Tag.Value){
                return true;
            }
        }
        return false;

    }
    public bool AddTag(Token ID){
        if(CheckTag(ID)){
            CE.Add(new Error("La etiqueta ya existe en memoria",ID.Position));
            return false;
        }
        Tags.Add(ID);
        return true;

    }

}