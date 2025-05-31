public class Environment{
    public Dictionary <string,BasicValue> Memory=new Dictionary <string, BasicValue> ();
    public Dictionary <string,Calleable> GlobalFunctions=new Dictionary <string, Calleable> ();
    public Dictionary<string,int> Tags=new Dictionary<string,int> ();
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
    public void Assign(Token ID, BasicValue value){
        if(Enclosing!=null && !Memory.ContainsKey(ID.Value)){

            Enclosing.Assign (ID, value);

        }else{
            Memory[ID.Value]=value;

        }
        
        
        

    }
    public bool Get(Token ID,out BasicValue? Value){
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
    public bool Check(Token ID,out BasicValue? Value){
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

        return Tags.ContainsKey(ID.Value);

    }
    public bool AddTag(Token ID,int pos){
        if(CheckTag(ID)){
            CE.Add(new Error("La etiqueta ya existe en memoria",ID.Position));
            return false;
        }
        Tags[ID.Value]=pos;
        return true;

    }
    public bool GetTagPos(Token ID, out int Pos){
        Pos=0;
        if(!CheckTag(ID)){
            CE.Add(new Error("La etiqueta NO existe en memoria",ID.Position));
            return false;
        }
        Pos=Tags[ID.Value];
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