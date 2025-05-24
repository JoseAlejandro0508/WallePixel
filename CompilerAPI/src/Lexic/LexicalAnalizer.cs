public class LexicalAnalizer{
    public List<Token>Tokens=new List<Token>();
    public TokenReader Reader;
    public LexicalAnalizer(string filename,string code){
        Reader = new TokenReader(filename,code);
        LexicDates.InitLexic();

    }
    public void GetTokens(List<Error>CompilationsError){
        while(!Reader.EOF){
            Reader.WhiteSpaceMove();
            if(Reader.EOL){
                Tokens.Add(new Token(Reader.CodeLocation,TokenType.EOL,"\n"));
                Reader.MovePointer();
                continue;
            }
           
            string Value_="";
            if(Reader.ReadId(out Value_)){
                if(LexicDates.Keywords.ContainsKey(Value_)){
                    Tokens.Add(new Token(Reader.CodeLocation,TokenType.Keyword,Value_));
                    continue;
                }
                if(LexicDates.Bucles.ContainsKey(Value_)){
                    Tokens.Add(new Token(Reader.CodeLocation,TokenType.Bucle,Value_));
                    continue;
                }

                Reader.WhiteSpaceMove();
                if(Reader.EOL && Reader.TokensInLine==1){
                    Tokens.Add(new Token(Reader.CodeLocation,TokenType.Tag,Value_));
                    continue;
                }
                Tokens.Add(new Token(Reader.CodeLocation,TokenType.Variable,Value_));
                
                continue;
            }
            if(Reader.ReadNumber(out Value_)){
                Tokens.Add(new Token(Reader.CodeLocation,TokenType.Number,Value_));
                continue;
            }
            if(Reader.ReadString(out Value_,CompilationsError)){
                Tokens.Add(new Token(Reader.CodeLocation,TokenType.String,Value_));
                continue;
            }else if(CompilationsError.Count>0)return;
            bool Matched=false;
            foreach(string oper in  LexicDates.Operators.Keys.OrderByDescending(k=>k.Length)){

                
                if(Reader.Match(oper)){
                    Tokens.Add(new Token(Reader.CodeLocation,TokenType.Symbol,oper)); 
                    Matched=true;
                    continue;
                }

            }
            if(Matched)continue;
            
            CompilationsError.Add(new Error($"No se reconoce el caracter {Reader.PointerValue} ",Reader.CodeLocation));
            return;
            

        }
        Tokens.Add(new Token(Reader.CodeLocation,TokenType.EOF,"EOF"));



    }


    public class TokenReader{
        private int pos;
        private int line;
        private int posLastLine;
        private string filename;
        private string code;
        private bool initWord=true;
        public int TokensInLine=0;
        public Location CodeLocation{
            get{
                Location CodeL;
                CodeL.Row=line;
                CodeL.Column=pos-posLastLine;
                CodeL.Filename=filename;
                return CodeL;   
            }
            
        }
        public bool EOF{
            get{
                return pos>code.Length-1||pos<0;
            }
        }
        public bool EOL{
            get{
                
                return PointerValue=='\n';
            }
        }
        public char PointerValue{
            get{
                if(EOF)throw new Exception("File is ended");
                return code[pos];
            }
        }
        public TokenReader(string code,string filename){
            this.filename=filename;
            this.code=code;
            this.line=0;
            this.pos=0;
            this.posLastLine=0;
            WhiteSpaceMove();

        }
        public bool WhiteSpaceMove(){
            bool WhiteSpace=false;
            while(!EOF && !EOL&&char.IsWhiteSpace(PointerValue) ){
                pos++;
                WhiteSpace=true;
                initWord=true;
            }
            return WhiteSpace;
        }
        public void MovePointer(){
            if(EOF)return;
            bool inEOL=EOL;
            while(!EOF && EOL){

                    line++;
                    pos++;
                    TokensInLine=0;
                    posLastLine=pos;
                    while(!EOF && WhiteSpaceMove()){
                        initWord=true;

                        
                    } 
            }
            if(!inEOL) pos++;
            initWord=false;

        }
        public bool IsValidVarChar(){
            return initWord?char.IsLetter(PointerValue):char.IsLetterOrDigit(PointerValue)||PointerValue=='_';

        }
        public bool ReadId(out string  Value_){
            Value_="";
            WhiteSpaceMove();
            while(!EOF&&IsValidVarChar()){
                Value_+=PointerValue;
                MovePointer();
            }
            if(Value_.Length>0)TokensInLine++;
            return  Value_.Length>0;
        }
        public bool ReadNumber(out string Number){
            Number="";
            WhiteSpaceMove();
            while(!EOF&&char.IsDigit(PointerValue)){
                Number+=PointerValue;
                MovePointer();
            }
            if(Number.Length>0)TokensInLine++;
            return Number.Length>0;
        }
        public bool ReadString(out string StringVal,List<Error>CompilationErrors){
            
            StringVal="";
            WhiteSpaceMove();
            if(EOF)return false;
            if(PointerValue!='"')return false;
            MovePointer();

            while(!EOF && !EOL && PointerValue!='"'){
                if( !char.IsLetterOrDigit(PointerValue)){
                    CompilationErrors.Add(new Error($"Invalid string char {PointerValue}",CodeLocation));
                    return false;
                }
                StringVal+=PointerValue;
                MovePointer();
            }

            
            if(EOF||PointerValue!='"'){
                CompilationErrors.Add(new Error("Invalid string definition, \" unclosed",CodeLocation));
                return false;
            }
            if(StringVal.Length==0){
                CompilationErrors.Add(new Error("Null string value",CodeLocation));
                return false;
            }
            MovePointer();
            TokensInLine++;
           
            return true;
        }
        public bool Match(string Match_){
            int it=0;
            foreach(char ch in Match_){
                if(pos+it>code.Length-1){
                    return false;
                }
                if(code[pos+it]!=ch)return false;
                it++;
                
            }
            pos+=it;
            TokensInLine++;
            return true;
        }
    }
}