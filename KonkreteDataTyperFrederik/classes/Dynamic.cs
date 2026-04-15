namespace DynamicN;

public class Dynamic
{
    public dynamic DynamicRun(dynamic dyno)
    {
        
        
        if(dyno.TryParse(dyno))
        {
            dyno += 100;
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
    
}