namespace SoegeN;


public class Soege
{
    Gemme gem = new Gemme();
    private string _wordToLookup { get; set; }

    public string WordToLookup()
    {
        string input = Console.ReadLine();
        return input;
    }

    public Resultat<string> TextToSearch(string wordToLookup)
    {

        string textToSearch = Console.ReadLine();
        _wordToLookup = wordToLookup;
        Regex r = new Regex(wordToLookup, RegexOptions.IgnoreCase);
        int hits = r.Matches(textToSearch).Count;
        string hitOutput = "Søgeord '" + wordToLookup + "' blev fundet " + hits.ToString();
        bool gemFil = false;


        if (hits == 0)
        {
            hitOutput = "Søgeord '" + wordToLookup + "' blev ikke fundet i teksten";
        }
        else if (hits == 1)
        {
            hitOutput = hitOutput + " gang.";
        }
        else if (hits > 1)
        {
            hitOutput = hitOutput + " gange.";
            if (hits > 10)
            {
                gemFil = true;
            }
        }

        if (gemFil)
        {
            try
            {

                return new Resultat<string>(true, textToSearch, "Teksten blev gemt som txt fil i den valgte folder", hitOutput);
            }
            catch
            {
                return new Resultat<string>(false, textToSearch, "Teksten blev ikke gemt som txt fil på grund af en fejl", hitOutput);
            }
        }
        return new Resultat<string>(false, textToSearch, "Teksten blev ikke gemt som txt fil på grund af lavt antal hits", hitOutput);


    }
    public void Draw()
    {
        Gemme gem = new Gemme();

        string[] linesToWrite =
        {
            "Hvilken ord vil du gerne søge efter?",
            ""

        };
        Menu menu1 = new Menu();
        menu1.drawCenteredProgram(linesToWrite, "Tekst søgeværktøj");
        string ord = WordToLookup();

        linesToWrite[0] = "Hvilken tekst vil du gerne søge i?";
        menu1.drawCenteredProgram(linesToWrite, "Tekst søgeværktøj");
        Resultat<string> resultat = TextToSearch(ord);

        if (resultat.IsSucces)
        {
            gem.SkrivFil(resultat.Value);
        }
        linesToWrite[0] = resultat.Hits;
        linesToWrite[1] = resultat.ResultatBesked;
        menu1.drawCenteredProgram(linesToWrite, "Tekst søgeværktøj");

        Console.ReadKey();
    }
}
public class Resultat<T>
{
    public bool IsSucces { get; }
    public T Value { get; }
    public string ResultatBesked { get; }
    public string Hits { get; }
    public Resultat(bool isSucces, T value, string resultatBesked, string hits)
    {
        IsSucces = isSucces;
        Value = value;
        ResultatBesked = resultatBesked;
        Hits = hits;
    }
}
