namespace SoegeN;


public class Soege
{
    Gemme gem = new Gemme();
    private string _wordToLookup { get; set;}
    
    public string WordToLookup()
    {
        Console.WriteLine("Hvilken ord vil du gerne søge efter?");
        string input = Console.ReadLine();
        return input;
    }

    public string TextToSearch(string wordToLookup)
    {
        Console.WriteLine("Hvilken tekst vil du gerne søge i?");
        string textToSearch = Console.ReadLine();
        _wordToLookup = wordToLookup;
        Regex r = new Regex(wordToLookup, RegexOptions.IgnoreCase);
        int hits = r.Matches(textToSearch).Count;
        string hitOutput = "Søgeord '" + wordToLookup + "' blev fundet " + hits.ToString();
        bool gemFil = false;
        

        if (hits == 0)
        {
            hitOutput = "Søgeord : " + wordToLookup + " blev ikke fundet i teksten";
        }
        else if(hits == 1)
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
            try{
                gem.SkrivFil(textToSearch);
                hitOutput = hitOutput + " Teksten blev gemt som txt fil i din user folder";
            }
            catch
            {
                hitOutput = hitOutput + " Teksten blev ikke gemt som txt fil på grund af en fejl";
            }
            

        }
        return hitOutput;
    }

}