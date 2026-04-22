using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace AlderOpgaveN;

internal class AlderOpgave
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
        bool running = true;
        Borger b = new Borger(DateOnly.FromDateTime(DateTime.Now), "", "", "");
        string json = "";
        while (running)
        {
            Console.Clear();
            Draw(stage, datoTyper);
            keyInfo = Console.ReadKey();
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
                    datoTyper = DatoModifier(stage, -1, datoTyper);
                    break;
                case ConsoleKey.Escape:
                    running = false;
                    break;
                case ConsoleKey.Backspace:
                    json = File.ReadAllText("borger.json");

                    b = JsonConvert.DeserializeObject<Borger>(json);

                    datoTyper[0] = b.FoedselsDato.Day;
                    datoTyper[1] = b.FoedselsDato.Month;
                    datoTyper[2] = b.FoedselsDato.Year;
                    break;
                case ConsoleKey.Enter:

                    b = pensionsAlder(datoTyper);

                    json = JsonConvert.SerializeObject(b, Formatting.Indented);

                    File.WriteAllText("borger.json", json);
                    break;
                default:
                    break;
            }



        }

    }
    public static readonly string[] datoNavne =
        {
            "Dag",
            "Måned",
            "År"
        };
    public static readonly string[] datoPileHoejre =
        {
            " >>",
            " >>",
            "   "
        };
    public static readonly string[] datoPileVenstre =
   {
            "   ",
            "<< ",
            "<< "
        };
    public void Draw(int stage, int[] datoTyper)
    {
        Borger pensionsDatoer = pensionsAlder(datoTyper);
        string[] linesToWrite =
        {
            "Brug venstre og højre piletaster til at vælge datotype",
            "Brug op og ned piletasterne til at ændre værdien.",
            "Tryk på enter for at gemme alder",
            "Tryk på backspace for a loade sidste gemte alder",
            datoPileVenstre[stage] + datoNavne[stage] + datoPileHoejre[stage],
            "Fødselsdato: " + datoTyper[0] + "/" + datoTyper[1] + "/" + datoTyper[2],
            "Er det skudår? " + skudAarsTjekker(datoTyper),
            "Pensionsalder: " + pensionsDatoer.Pensionsalder,
            "Pensionsdato: " + pensionsDatoer.PensionsDato.Dato,
            "Dagens dato: " + DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy"),
            "Tid til pension: " + pensionsDatoer.YearsTilPension.Years,
            "",
            "Tryk på escape for at afslutte og returnere til hovedmenuen."
            
        };
        Menu menu1 = new Menu();
        menu1.drawCenteredProgram(linesToWrite, "Pensionsalderudregning");
    }
    public int[] DatoModifier(int stage, int tilfoejelse, int[] dato)
    {
        int[] targetDatoer =
        {
            31, //Januar
            28, //Februar
            31, //Marts
            30, //April
            31, //Maj
            30, //Juni
            31, //Juli
            31, //August
            30, //September
            31, //Oktober
            30, //November
            31 //December
        };
        //Alle månedslister
        int[] uligeMaaneder =
        {
            1,
            3,
            5,
            7,
            8,
            10,
            12
        };

        dato[stage] += tilfoejelse;

        if (!uligeMaaneder.Contains(dato[1]))
        {
            if (dato[1] == 2)
            {
                if (skudAarsTjekker(dato))
                {
                    targetDatoer[1] = 29;
                }
            }
        }
        int targetDato = 31;
        if (tilfoejelse > 0)
        {
            if (dato[1] >= 12)
            {
            }
            else
            {
                targetDato = targetDatoer[dato[1] - 1];
            }
            
        }
        else if (tilfoejelse < 0)
        {
            if (dato[1] <= 1)
            {
            }
            else if (dato[1] == 3)
            {
                if (skudAarsTjekker(dato))
                {
                    targetDatoer[2] = 29;
                }

            }
            else
            {
                if (dato[1] < 1)
                {
                    dato[1] = 12;
                    dato[2] --;
                    targetDato = targetDatoer[dato[1] - 2];
                }
                else if (dato[1] > 12)
                {
                    dato[1] = 1;
                    dato[2] ++;

                }
                
            }
        }

        //Retter dage
        if (dato[0] < 1)
        {
            dato[1]--;
            dato[0] = targetDato;
        }
        else if (dato[0] > targetDato)
        {
            dato[0] = 1;
            dato[1]++;
        }


        //Retter måned
        if (dato[1] < 1)
        {
            dato[2] --;
            dato[1] = 12;
        }
        else if (dato[1] > 12)
        {
            dato[2] ++;
            dato[1] = 1;
        }

        //Retter år
        if (dato[2] < 1920)
        {
            dato[2] = 1920;
        }
        else if (dato[2] > 2026)
        {
            dato[2] = 2026;
        }
        if (dato[1] == 2 && dato[0] > 28)
        {
            if (tilfoejelse < 0)
            {
                if (skudAarsTjekker(dato))
                {
                    if (dato[0] > 29)
                    {
                        dato[0] = 29;
                    }

                }
                else
                {
                    if (dato[0] > 28)
                    {
                        dato[0] = 28;
                    }

                }
            }
            else if (tilfoejelse > 0)
            {
                if (skudAarsTjekker(dato))
                {
                    if (dato[0] > 29)
                    {
                        dato[0] = 1;

                    }

                }
                else
                {
                    if (dato[0] > 28)
                    {
                        dato[0] = 1;
                    }
                }


            }
        }
        return dato;
    }

    public bool skudAarsTjekker(int[] dato)
    {
        bool skudaar = false;

        if (dato[2] % 4 == 0 && (dato[2] % 100 != 0 || dato[2] % 400 == 0)) //tjekker for skud år
        {
            skudaar = true;
        }

        return skudaar;
    }

    public Borger pensionsAlder(int[] dato)
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
        return new Borger(foedselsDato, pensionsDatoer[0], pensionsDatoer[1], pensionsDatoer[2]);
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
    public class Borger
    {
        public DateOnly FoedselsDato { get; set; }
        public string Pensionsalder { get; set; }
        public PensionsDato PensionsDato { get; set; }
        public YearsTilPension YearsTilPension { get; set; }

        public Borger()
        { }
        public Borger(DateOnly foedselsDato, string pensionsalder, string pensionsDato, string yearsTilPension)
        {
            FoedselsDato = foedselsDato;
            Pensionsalder = pensionsalder;
            PensionsDato = new PensionsDato(pensionsDato);
            YearsTilPension = new YearsTilPension(yearsTilPension);

        }
    }
    public struct PensionsDato(string dato)
    {
        public string Dato { get; set; } = dato;
    }
    public struct YearsTilPension(string yearsTilPension)
    {
        public string Years { get; set; } = yearsTilPension;
    }
}