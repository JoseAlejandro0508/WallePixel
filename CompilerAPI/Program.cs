using System;

public abstract class MyClase<T>
{
    // Propiedades y métodos de la clase
}

public class MyClase1<T> : MyClase<T>
{
    // Implementación de MyClase1
}

public class Program
{
    public static void Main()
    {
        MyClase<int> instancia1 = new MyClase1<int>();
        MyClase<string> instancia2 = new MyClase1<string>();
        object instancia3 = new object();

        Console.WriteLine(IsMyClase(instancia1)); // True
        Console.WriteLine(IsMyClase(instancia2)); // True
        Console.WriteLine(IsMyClase(instancia3)); // False
    }

    public static bool IsMyClase(object obj)
    {
        // Verifica si el objeto es nulo
        if (obj == null) return false;

        // Obtiene el tipo del objeto
        var type = obj.GetType();

        // Comprueba si el tipo es generico
        if (type.IsGenericType)
        {
            // Comprueba si el tipo genérico base es MyClase<>
            return type.GetGenericTypeDefinition().IsAssignableFrom(typeof(MyClase<>));
        }

        // Verifica si el objeto es una subclase de MyClase<>
        return typeof(MyClase<>).IsAssignableFrom(type.BaseType);
    }
}

/*
string text = File.ReadAllText("code");  
LexicalAnalizer lex=new LexicalAnalizer(text,"a");
lex.GetTokens(CompilationsError);
List<Token> t=lex.Tokens;
int a=1;*/