public class Environment{
    public Dictionary <string,Variable> Memory=new Dictionary <string, Variable> ();
    public Dictionary <string,Calleable> GlobalFunctions=new Dictionary <string, Calleable> ();
    public List<Token> Tags=new List<Token>();
    public List<Error> CE;
    public Environment? Enclosing;

    public Environment(List<Error> CE){
        this.CE= CE;
        Enclosing=null;
    }
    public Environment (Environment enclosing,List<Error> CE){
        this.CE= CE;
        Enclosing=enclosing;
    }
    public void AddEnclosing(){
        if(Enclosing==null){
            Enclosing=new Environment (CE);
        }else{
            Enclosing.Enclosing=new Environment(CE);
        }

    }
    public void CloseEnclosing(){
        if(Enclosing!=null){
            Enclosing.Enclosing=null;

        }else{
            Enclosing=null;
        }

    }
    public void Assign(Token ID, Variable value){
        if(Enclosing!=null && !Memory.ContainsKey(ID.Value)){

            Enclosing.Assign (ID, value);

        }else{
            Memory[ID.Value]=value;

        }
        
        
        

    }
    public bool Get(Token ID,out Variable? Value){
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
    public bool Check(Token ID,out Variable? Value){
        if(Enclosing!=null){
            if(Enclosing.Check(ID,out Value)){
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
    public bool DefineFunc(Token ID,Calleable Func){
        if(Enclosing!=null){
            CE.Add(new Error("No se pueden definir funciones en un bloque ",ID.Position));
            return false;
        }
        if(GlobalFunctions.ContainsKey(ID.Value)){
            CE.Add(new Error("Ya existe una definicion para esa funcion",ID.Position));
            return false;
        }
        GlobalFunctions[ID.Value]=Func;
        return true;

    }
    public Calleable? GetFunc(Token ID){

        if(!GlobalFunctions.ContainsKey(ID.Value)){
            CE.Add(new Error($"No existe se ha definido la funcion {ID.Value}",ID.Position));
            return null;
        }
        return GlobalFunctions[ID.Value];
    

    }

}