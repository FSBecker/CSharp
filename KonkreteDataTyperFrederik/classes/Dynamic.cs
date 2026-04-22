namespace DynamicN;

public class Dynamic
{
    public dynamic DynoCheck(dynamic dyno)
    {
        bool parsed = int.TryParse(dyno.ToString(), out int tal);
        if (parsed)
        {
            return tal + 100;
        }
        else
        {
            if (dyno is string)
            {
                dyno = "Modtaget følgende tekst: " + dyno;
            }
            else
            {
                dyno = false;
            }
        }


        return dyno;
    }
    public void Draw()
    {
        
        string[] linesToWrite =
        {
            "Indtast bogstav(er) eller tal: ",
            ""
            
        };

        Menu menu1 = new Menu();
        menu1.drawCenteredProgram(linesToWrite, "Type checker");

        dynamic dyno = Console.ReadLine();
        dyno = DynoCheck(dyno);
        linesToWrite[0] = dyno.ToString();
        linesToWrite[1] = "Type: " + dyno.GetType();
        menu1.drawCenteredProgram(linesToWrite, "Type checker");
        Console.ReadKey();
    }
}