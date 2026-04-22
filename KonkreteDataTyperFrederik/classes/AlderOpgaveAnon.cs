using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace AlderOpgaveAnonN;

internal class AlderOpgaveAnon
{
    public void Alder()
    {
        int stage = 0;
        int[] datoTyper =
        {
            1,
            1,
            1920 
        };
        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        
            Menu(stage, keyInfo, datoTyper);
            
    }

    public void Menu(int stage, ConsoleKeyInfo keyInfo, int[] datoTyper)
    {
        while (true)
        {
            Console.Clear();
            
            
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (stage > 0)
                    {
                        stage--;
                    }
                break;
                case ConsoleKey.RightArrow:
                    if (stage < 2)
                    {
                        stage++;
                    }
                break;
                case ConsoleKey.UpArrow:
                    datoTyper = DatoModifier(stage, 1, datoTyper);
                break;
                case ConsoleKey.DownArrow:
                    datoTyper= DatoModifier(stage, -1, datoTyper);
                break;
                default:
                break;
            }
            Draw(stage, datoTyper);
            keyInfo = Console.ReadKey();
        }

    }
    public static readonly string[] datoNavne =
        {
            "Dag",
            "Måned",
            "År"
        };
    public void Draw(int stage, int[] datoTyper)
    {
        
        Console.WriteLine("Brug venstre og højre piletaster til at vælge datotype, og brug op og ned piletasterne til at ændre værdien.");
        Console.WriteLine(datoNavne[stage] + ": " + datoTyper[stage]);
        Console.WriteLine("Dato: " + datoTyper[0] + "/" + datoTyper[1] + "/" + datoTyper[2]);
        var pensionsDatoer = pensionsAlder(datoTyper);
        Console.WriteLine("Pensionsalder: " + pensionsDatoer.Pensionsalder);
        Console.WriteLine("Pensionsdato: " + pensionsDatoer.PensionsDato);
        Console.WriteLine("Dato: " + DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy"));
        Console.WriteLine("Tid til pension: " + pensionsDatoer.YearsTilPension);
    }
        

    public int[] DatoModifier (int stage,int tilfoejelse, int[] dato)
    {
        switch (stage)
        {
            case 0:
                if ((dato[0] <= 1 && tilfoejelse == -1))
                {
                    dato[0] = 1;
                }
                else if(dato[0] >= 31 && tilfoejelse == 1)
                {
                    

                        dato[0] = 31;

                }
                else
                {
                    dato[0] += tilfoejelse;
                }
            break;
                case 1:
                if ((dato[1] <= 1 && tilfoejelse == -1))
                {
                    dato[1] = 1;
                }
                else if (dato[1] >= 12 && tilfoejelse == 1)
                {
                    dato[1] = 12;
                }
                else
                {
                    dato[1] += tilfoejelse;
                }
            break;
            case 2:
                if ((dato[2] <= 1920 && tilfoejelse == -1))
                {
                    dato[2] = 1920;
                }
                else if (dato[2] >= 2026 && tilfoejelse == 1)
                {
                    dato[2] = 2026;
                }
                else
                {
                    dato[2] += tilfoejelse;
                }
            break;
            default:
            break;
        }
        if ((dato[0] == 31 || dato[0] == 30) && dato[1] == 2)
        {
            if (dato[2] % 4 == 0 && (dato[2] % 100 != 0 || dato[2] % 400 == 0))
            {
                dato[0] = 29;
            }
            else
            {
                dato[0] = 28;
            } 
        }
        else if (dato[0] == 31 && (dato[1] == 4 || dato[1] == 6 || dato[1] == 9 || dato[1] == 11))
        {
            dato[0] = 30;
        }
        return dato;
    }

    public dynamic pensionsAlder(int[] dato)
    {
        string[] pensionsDatoer = new string[3];
        DateOnly foedselsDato = new DateOnly(dato[2], dato[1], dato[0]);
        DateOnly nu = DateOnly.FromDateTime(DateTime.Now);
        if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1955, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1955, 6, 30)))
        {
            pensionsDatoer[0] = "66,5 år";
            pensionsDatoer[1] = foedselsDato.AddYears(66).AddMonths(6).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(66).AddMonths(6));

        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1955, 7, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1962, 12, 31)))
        {
            pensionsDatoer[0] = "67 år";
            pensionsDatoer[1] = foedselsDato.AddYears(67).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(67));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1963, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1966, 12, 31)))
        {
            pensionsDatoer[0] = "68 år";
            pensionsDatoer[1] = foedselsDato.AddYears(68).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(68));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1967, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1970, 12, 31)))
        {
            pensionsDatoer[0] = "69 år";
            pensionsDatoer[1] = foedselsDato.AddYears(69).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(69));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1971, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1974, 12, 31)))
        {
            pensionsDatoer[0] = "70 år";
            pensionsDatoer[1] = foedselsDato.AddYears(70).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(70));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1975, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1978, 12, 31)))
        {
            pensionsDatoer[0] = "71 år";
            pensionsDatoer[1] = foedselsDato.AddYears(71).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(71));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1979, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1982, 12, 31)))
        {
            pensionsDatoer[0] = "72 år";
            pensionsDatoer[1] = foedselsDato.AddYears(72).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(72));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1983, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1987, 6, 30)))
        {
            pensionsDatoer[0] = "72,5 år";
            pensionsDatoer[1] = foedselsDato.AddYears(72).AddMonths(6).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(72).AddMonths(6));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1987, 7, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1991, 12, 31)))
        {
            pensionsDatoer[0] = "73 år";
            pensionsDatoer[1] = foedselsDato.AddYears(73).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(73));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1992, 1, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(1996, 6, 30)))
        {
            pensionsDatoer[0] = "73.5 år";
            pensionsDatoer[1] = foedselsDato.AddYears(73).AddMonths(6).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(73).AddMonths(6));
        }
        else if (foedselsDato >= DateOnly.FromDateTime(new DateTime(1996, 7, 1)) && foedselsDato <= DateOnly.FromDateTime(new DateTime(2002, 12, 31)))
        {
            pensionsDatoer[0] = "74 år";
            pensionsDatoer[1] = foedselsDato.AddYears(74).ToString("dd/MM/yyyy");
            pensionsDatoer[2] = tidTilPension(foedselsDato, foedselsDato.AddYears(74));
        }
        else
        {
            pensionsDatoer[0] = "Ukendt";
            pensionsDatoer[1] = "Ukendt";
            pensionsDatoer[2] = "Ukendt";
        }
        return new { FoedselsDato = foedselsDato, Pensionsalder = pensionsDatoer[0], PensionsDato = pensionsDatoer[1], YearsTilPension = pensionsDatoer[2] };
    }

    public string tidTilPension(DateOnly foedselsDato, DateOnly pensionsDato)
    {
        DateOnly nu = DateOnly.FromDateTime(DateTime.Now);
        int years = pensionsDato.Year - nu.Year;
        int months = pensionsDato.Month - nu.Month;
        if (months < 0)
        {
            years--;
            months += 12;
        }
        return years.ToString() + " år, " + months.ToString() + " måneder";

    }
}