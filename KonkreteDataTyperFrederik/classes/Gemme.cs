namespace GemmeN;

public class Gemme
{
    public void SkrivFil (string tekst)
    {  
        bool valg = true;
        string path = "";
        while (valg)
        {
            Console.WriteLine("Hvor vil du gemme din fil?\n1. User folder\n2. Bin/Output folder\n");
            char input = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (input)
            {
                case '1':
                    valg = false;
                    gemfil(tekst);
                    break;
                case '2':
                    path = "output";
                    valg = false;
                    gemfil(tekst, path);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    break;
            }
        }
        
    }
    public void gemfil (string tekst, string path)
    {
        string filNavn = "tekst.txt";
        using (var streamWriter = new StreamWriter(Path.Combine(path, filNavn))){
            streamWriter.WriteLine(tekst);
            streamWriter.Flush();
        }
    }
     public void gemfil (string tekst)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filNavn = "tekst.txt";
        using (var streamWriter = new StreamWriter(Path.Combine(path, filNavn))){
            streamWriter.WriteLine(tekst);
            streamWriter.Flush();
        }
    }
}