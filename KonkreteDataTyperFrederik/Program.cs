Soege soege = new Soege();
Gemme gem = new Gemme();
Dynamic dynamicR = new Dynamic();
string ord = soege.WordToLookup();
Resultat<string> resultat = soege.TextToSearch(ord);
if (resultat.IsSucces)
{
    gem.SkrivFil(resultat.Value);
}
Console.WriteLine(resultat.ResultatBesked);
Console.WriteLine("Indtast bogstav(er) eller tal:\n");
dynamic dyno = Console.ReadLine();
dynamicR.DynamicRun(dyno);
Console.WriteLine(dyno);
Console.WriteLine("Type: " + dyno.GetType());