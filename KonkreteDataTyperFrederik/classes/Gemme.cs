namespace GemmeN;

public class Gemme
{
        public void SkrivFil (string tekst)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filNavn = "tekst.txt";
        using (var streamWriter = new StreamWriter(path + filNavn)){
            streamWriter.WriteLine(tekst);
            streamWriter.Flush();
        }
        
    }
}