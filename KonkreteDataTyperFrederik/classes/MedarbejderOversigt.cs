using System.Dynamic;

namespace MedarbejderOversigtN;

public class MedarbejderProgram
{
    Menu menu1 = new Menu();
    public void MedarbejderMenu(ConsoleKeyInfo keyInfo)
    {
        int stage = 0;
        bool running = true;
        string json = "";
        string titel = "Medarbejderoversigt";
        List<string> oplysninger = new List<string>();
        while (running)
        {
            oplysninger.Clear();
            string[] linesToWrite = {
            "Listevisning",
            "Søgefunktion",
            "Opret ny medarbejder",
            "Masse opret random medarbejdere",
            "Afslut"
        };
            linesToWrite[stage] = "| " + linesToWrite[stage] + " |";
            Console.Clear();
            menu1.DrawBorders(titel);
            menu1.drawCenteredProgram(linesToWrite, titel);
            keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (stage > 0)
                    {
                        stage--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (stage < linesToWrite.Length - 1)
                    {
                        stage++;
                    }
                    break;
                case ConsoleKey.Enter:
                    switch (stage)
                    {
                        case 0://Vis liste over medarbejdere
                            int sorteringsvalg = 0;
                            string[] sorteringsvalgliste =
                           {
                                "Oprettelsesdato",
                                "Alder",
                                "Køn",
                                "Fornavn",
                                "Efternavn",
                                "Stilling",
                                "Afdeling"
                            };
                            bool vaelgerSortering = true;
                            while (vaelgerSortering)
                            {
                                string[] sorteringsvalglisteHighLight = (string[])sorteringsvalgliste.Clone();
                                sorteringsvalglisteHighLight[sorteringsvalg] = "| " + sorteringsvalglisteHighLight[sorteringsvalg] + " |";
                                menu1.drawCenteredProgram(sorteringsvalglisteHighLight, "Sorteringsvalg");
                                ConsoleKeyInfo keyInfo2 = Console.ReadKey();

                                switch (keyInfo2.Key)
                                {
                                    case ConsoleKey.UpArrow:
                                        if (sorteringsvalg > 0)
                                        {
                                            sorteringsvalg--;
                                        }
                                        break;
                                    case ConsoleKey.DownArrow:
                                        if (sorteringsvalg < sorteringsvalgliste.Length - 1)
                                        {
                                            sorteringsvalg++;
                                        }
                                        break;
                                    case ConsoleKey.Escape:
                                        vaelgerSortering = false;
                                        break;
                                    case ConsoleKey.Enter:
                                        vaelgerSortering = false;
                                        listevisning(sorteringsvalgliste[sorteringsvalg], "Sorterer efter:" + sorteringsvalgliste[sorteringsvalg], keyInfo);
                                        break;
                                }
                            }

                            break;
                        case 1: //Søg efter medarbejder
                            oplysninger.Add("Indtast søgeord");
                            oplysninger.Add("Afslut med enter");
                            Console.Clear();
                            menu1.DrawBorders("Søg efter medarbejder");
                            DrawStep(oplysninger, "", 5);
                            string soegeord = Console.ReadLine();
                            listevisning("Søgeord", soegeord, keyInfo);
                            break;
                        case 2: //Opret medarbejder

                            int[] placeholderDatoIntArray = { 1, 1, 1920 };
                            DateOnly placeholderDatoDateOnly = DateOnly.Parse(placeholderDatoIntArray[1] + "/" + placeholderDatoIntArray[0] + "/" + placeholderDatoIntArray[2]);
                            string maNummer = GenererMANummer();
                            MedarbejderOplysninger.Medarbejder medarbejder = RedigeringMedarbejder(keyInfo, "", "", "", "", MedarbejderOplysninger.Koen.M, maNummer, placeholderDatoIntArray, true, MedarbejderOplysninger.Afdeling.SoftwareUdvikling);
                            if (medarbejder != null)
                            {
                                List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();
                                medarbejdere.Add(medarbejder);
                                GemMedarbejdere(medarbejdere);
                            }
                            break;
                        case 3:
                            oplysninger.Add("Hvor mange vil du oprette?");
                            oplysninger.Add("Afslut med enter");
                            Console.Clear();
                            menu1.DrawBorders("Masseoprettelse");
                            DrawStep(oplysninger, "test", 5);
                            string antal = Console.ReadLine();
                            randomizeMedarbejdere(Int32.Parse(antal));

                            break;
                        case 4://Afslut
                            running = false;
                            break;
                    }
                    break;
            }



        }

    }
    public DateOnly PensionsDatoUdregner(DateOnly foedselsdato)
    {
        DateOnly nu = DateOnly.FromDateTime(DateTime.Now);
        DateOnly pensionsdato = foedselsdato;
        if (foedselsdato <= DateOnly.FromDateTime(new DateTime(1954, 12, 31)))
        {
            pensionsdato.AddYears(66);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1955, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1955, 6, 30)))
        {
            pensionsdato.AddYears(66).AddMonths(6);

        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1955, 7, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1962, 12, 31)))
        {
            pensionsdato.AddYears(67);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1963, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1966, 12, 31)))
        {
            pensionsdato.AddYears(68);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1967, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1970, 12, 31)))
        {
            pensionsdato.AddYears(69);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1971, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1974, 12, 31)))
        {
            pensionsdato.AddYears(70);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1975, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1978, 12, 31)))
        {
            pensionsdato.AddYears(71);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1979, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1982, 12, 31)))
        {
            pensionsdato.AddYears(72);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1983, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1987, 6, 30)))
        {
            pensionsdato.AddYears(72).AddMonths(6);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1987, 7, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1991, 12, 31)))
        {
            pensionsdato.AddYears(73);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1992, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1996, 6, 30)))
        {
            pensionsdato.AddYears(73).AddMonths(6);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1996, 7, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(2002, 12, 31)))
        {
            pensionsdato.AddYears(74);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(2003, 1, 1)))
        {
            pensionsdato.AddYears(74).AddMonths(6);
        }

        return pensionsdato;
    }
    public void listevisning(string sorteringsvalg, string soegeord, ConsoleKeyInfo keyInfo)
    {
        List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();
        switch (sorteringsvalg)
        {
            case "Oprettelsesdato":
                medarbejdere = medarbejdere
                    .OrderBy(m => m.Oprettelsesdato)
                    .ToList();
                break;

            case "Alder":
                medarbejdere = medarbejdere
                    .OrderBy(m => BeregnAlder(m.Foedselsdato))
                    .ToList();
                break;

            case "Køn":
                medarbejdere = medarbejdere
                    .OrderBy(m => m.Koen)
                    .ToList();
                break;

            case "Fornavn":
                medarbejdere = medarbejdere
                    .OrderBy(m => m.Navn.Fornavn)
                    .ToList();
                break;

            case "Efternavn":
                medarbejdere = medarbejdere
                    .OrderBy(m => m.Navn.Efternavn)
                    .ToList();
                break;
            case "Stilling":
                medarbejdere = medarbejdere.OrderBy(m => m.Stilling).ToList();
                break;
            case "Afdeling":
                medarbejdere = medarbejdere.OrderBy(m => m.Afdeling).ToList();
                break;
            case "Søgeord":
                medarbejdere = medarbejdere
                    .Where(m =>
                        m.Navn.Fornavn.Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
                        m.Navn.Efternavn.Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
                        m.Stilling.Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
                        m.Foedselsdato.Year.ToString().Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
                        (DateOnly.FromDateTime(DateTime.Now).Year - m.Foedselsdato.Year).ToString().Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
                        m.Koen.ToString().Contains(soegeord, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
                break;
        }
        string[] linesToWrite = new string[medarbejdere.Count];
        for (int i = 0; i < medarbejdere.Count; i++)
        {
            var m = medarbejdere[i];
            MedarbejderOplysninger.Alder alder = BeregnAlder(m.Foedselsdato);
            string afdeling = "";
            switch (m.Afdeling)
            {
                case MedarbejderOplysninger.Afdeling.SoftwareUdvikling:
                    afdeling = "Software Udvikling";
                break;
                case MedarbejderOplysninger.Afdeling.Administration:
                    afdeling = "Administration";
                break;
                case MedarbejderOplysninger.Afdeling.ServiceOgSupport:
                    afdeling = "Service og support";
                break;
            }
            linesToWrite[i] = m.Navn.Fornavn + " " + m.Navn.Efternavn + ", " + m.Koen + ", " + alder.AlderM.ToString() + " år, " + m.Stilling + ", " + afdeling;
        }
        bool gennemgaarListe = true;
        string titel = "Medarbejderoversigt";
        int sidenummer = 0;
        int stage = 0;
        int linjerPerSide = 10;
        while (gennemgaarListe)
        {
            string[] linesToWriteWithHighligt = (string[])linesToWrite.Clone();
            linesToWriteWithHighligt[stage] = "| " + linesToWriteWithHighligt[stage] + " |";
            menu1.DrawBorders(titel);
            menu1.drawCenteredListe(linesToWriteWithHighligt, titel, sidenummer, linjerPerSide);

            keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow://Gå op af listen
                    if (stage > 0)
                    {
                        stage--;
                    }
                    break;
                case ConsoleKey.DownArrow://Gå ned af listen
                    if (stage < medarbejdere.Count - 1)
                    {
                        stage++;
                    }
                    break;
                case ConsoleKey.RightArrow://Bladre til højre
                    if (sidenummer < (int)Math.Ceiling(linesToWrite.Length / (double)linjerPerSide) - 1)
                    {
                        sidenummer++;
                        stage = sidenummer * linjerPerSide;
                    }
                    break;
                case ConsoleKey.LeftArrow://Bladre til venstre
                    if (sidenummer > 0)
                    {
                        sidenummer--;
                        stage = sidenummer * linjerPerSide;
                    }
                    break;
                case ConsoleKey.Enter://Vælg en medarbejder at kigge på
                    var valgtMedarbejder = medarbejdere[stage];
                    break;

                case ConsoleKey.Escape://Går ud af programmet
                    gennemgaarListe = false;
                    break;
            }

            //Udregner om man er indenfor sidetal
            int sidsteSide = Math.Max(0, (int)Math.Ceiling(linesToWrite.Length / (double)linjerPerSide) - 1);
            if (sidenummer < 0)
            {
                sidenummer = 0;
            }
            else if (sidenummer > sidsteSide)
            {
                sidenummer = sidsteSide;
            }
        }
    }
    public void randomizeMedarbejdere(int antal)
    {
        string[] fornavne = new string[20];
        string[] efternavne =
        {
            "Chi",
            "Abdul",
            "Hansen",
            "Jensen",
            "Kirkegaard",
            "McDonald",
            "Tuk Tuk",
            "Neingescheis",
            "Actung",
            "Black",
            "Westphal",
            "Yung",
            "Ali",
            "Johnson",
            "Frederiksen",
            "Henriksne",
            "Petersen",
            "Bager",
            "Humle",
            "Paulsen"
        };
        string maNummer = "";
        MedarbejderOplysninger.Koen[] koen =
        {
            MedarbejderOplysninger.Koen.M,
            MedarbejderOplysninger.Koen.F
        };
        MedarbejderOplysninger.Afdeling[] afdelinger =
            {
                MedarbejderOplysninger.Afdeling.SoftwareUdvikling,
                MedarbejderOplysninger.Afdeling.Administration,
                MedarbejderOplysninger.Afdeling.ServiceOgSupport
            };
        DateOnly foedselsdato = DateOnly.FromDateTime(DateTime.Now);

        string[] stillinger =
        {
            "Stenhugger",
            "Udvikler",
            "Testperson",
            "Kantinemedarbejder",
            "Ingengør",
            "Webdesigner",
            "Grafisk designer",
            "Hundepasser",
            "Pedagog",
            "Servicemedarbejder",
            "Mellemleder"
        };
        DateTime oprettelsesdato = DateTime.Now;
        List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();
        for (int i = 1; i < antal; i++)
        {
            maNummer = GenererMANummer();
            MedarbejderOplysninger.Koen valgtKoen = koen[random.Next(0, 2)];
            switch (valgtKoen)
            {
                case MedarbejderOplysninger.Koen.M:
                    fornavne[0] = "Erik";
                    fornavne[1] = "Hans";
                    fornavne[2] = "Muhammed";
                    fornavne[3] = "Bruce";
                    fornavne[4] = "Henrik";
                    fornavne[5] = "Frederik";
                    fornavne[6] = "Yang";
                    fornavne[7] = "Niels";
                    fornavne[8] = "Philip";
                    fornavne[9] = "Christian";
                    fornavne[10] = "Ronald";
                    fornavne[11] = "Jon";
                    fornavne[12] = "Toke";
                    fornavne[13] = "Kalle";
                    fornavne[14] = "Johan";
                    fornavne[15] = "Emil";
                    fornavne[16] = "Tobias";
                    fornavne[17] = "Søren";
                    fornavne[18] = "Carl";
                    fornavne[19] = "Lee";
                    break;
                case MedarbejderOplysninger.Koen.F:
                    fornavne[0] = "Elisabeth";
                    fornavne[1] = "Lilje";
                    fornavne[2] = "Lise";
                    fornavne[3] = "Lotte";
                    fornavne[4] = "Lone";
                    fornavne[5] = "Frederikke";
                    fornavne[6] = "Amalie";
                    fornavne[7] = "Maria";
                    fornavne[8] = "Julie";
                    fornavne[9] = "Jane";
                    fornavne[10] = "Nora";
                    fornavne[11] = "Thyra";
                    fornavne[12] = "Erika";
                    fornavne[13] = "Sanne";
                    fornavne[14] = "Simone";
                    fornavne[15] = "Fillipa";
                    fornavne[16] = "Alberte";
                    fornavne[17] = "Andersine";
                    fornavne[18] = "Pauline";
                    fornavne[19] = "Henriette";
                    break;
            }
            string fornavn = fornavne[random.Next(0, 20)];
            string efternavn = efternavne[random.Next(0, efternavne.Length)];
            string brugernavn = fornavn.Substring(0, 2).ToLower() + efternavn.Substring(0, 2).ToLower() + random.Next(0, 10).ToString() + random.Next(0, 10).ToString();
            foedselsdato = DateOnly.FromDateTime(RandomDate());


            MedarbejderOplysninger.Afdeling afdeling = afdelinger[random.Next(0, 3)];
            var medarbejder = new MedarbejderOplysninger.Medarbejder
            {
                MANummer = maNummer,
                Navn = new MedarbejderOplysninger.Fuldtnavn(fornavn, efternavn),
                Brugernavn = brugernavn,
                Stilling = stillinger[random.Next(0, stillinger.Length)],
                Afdeling = afdeling,
                Koen = valgtKoen,
                Foedselsdato = foedselsdato,
                Oprettelsesdato = DateTime.Now
            };
            medarbejdere.Add(medarbejder);
        }
        GemMedarbejdere(medarbejdere);
    }
    public DateTime RandomDate()
    {

        DateTime start = new DateTime(1945, 1, 1);
        DateTime slut = new DateTime(2006, 12, 31);

        int range = (slut - start).Days;

        return start.AddDays(random.Next(range));
    }
    public MedarbejderOplysninger.Alder BeregnAlder(DateOnly foedselsdato)
    {
        DateOnly nu = DateOnly.FromDateTime(DateTime.Now);
        MedarbejderOplysninger.Alder alder = new MedarbejderOplysninger.Alder();
        int alderInt = nu.Year - foedselsdato.Year;

        if (nu < foedselsdato.AddYears(alderInt))
        {
            alderInt--;
        }
        alder.AlderM = alderInt;
        return alder;
    }
    public MedarbejderOplysninger.Medarbejder? RedigeringMedarbejder(ConsoleKeyInfo keyInfo, string fornavn, string efternavn, string brugernavn, string stilling, MedarbejderOplysninger.Koen koen, string maNummer, int[] foedselsdato, bool opretter, MedarbejderOplysninger.Afdeling afdeling)
    {
        const int fornavnStep = 10;
        const int efternavnStep = 11;
        const int koenStep = 12;
        const int foedselsdatoStep = 13;
        const int stillingStep = 14;
        const int afdelingStep = 15;
        const int gemOgAfslutStep = 17;
        int step = fornavnStep;
        bool redigerer = true;
        string brugernavnTal = random.Next(0, 10).ToString() + random.Next(0, 10).ToString();
        string titel = "Redigere medarbejder: " + brugernavn;
        if (opretter)
        {
            titel = "Opret ny medarbejder";
        }

        int valgtTidsType = 0;
        Console.Clear();
        Menu menu1 = new Menu();


        while (redigerer)
        {
            Console.Clear();

            MedarbejderOplysninger.Alder alder = BeregnAlder(DateOnly.Parse(foedselsdato[1] + "/" + foedselsdato[0] + "/" + foedselsdato[2]));
            string valgtAfdeling = "";
            switch (afdeling)
            {
                case MedarbejderOplysninger.Afdeling.SoftwareUdvikling:
                    valgtAfdeling = "Software Udvikling";
                break;
                case MedarbejderOplysninger.Afdeling.Administration:
                    valgtAfdeling = "Administration";
                break;
                case MedarbejderOplysninger.Afdeling.ServiceOgSupport:
                    valgtAfdeling = "Service og support";
                break;
            }
            string[] linesToWrite =
            {
              "Tryk enter for at bekræfte oplysning. Tryk escape for at gå tilbage", //0
              "", //1
              "", //2
              "-Oplysninger-", //3
              "MA-Nummer: " + maNummer, //4
              "Brugernavn: " + brugernavn, //5
              "Fulde navn: " + fornavn + " " + efternavn, //6
              "Alder: " + alder.AlderM.ToString() + " år", // 7 
              "",  //8
              "-Indtast manglende oplysniger-", // 9
              "Fornavn: " + fornavn, //10
              "Efternavn: " + efternavn,  //11
              "Køn: " + koen.ToString(), //12
              "Fødsesldato: " + foedselsdato[0] + "/" + foedselsdato[1] + "/" + foedselsdato[2], //13
              "Stilling: " + stilling, //14
              "Afdeling: " + valgtAfdeling, //15
              "", //16
              "Gem og afslut", //17
              "",
              step.ToString(),
              keyInfo.Key.ToString()
            };
            switch (step)
            {
                case fornavnStep://Fornavn
                case efternavnStep://Efternavn
                case stillingStep://Stilling
                    linesToWrite[1] = "Indtast oplysning med tastaturet";
                    break;
                case koenStep://Køn
                    if (koen == MedarbejderOplysninger.Koen.M)
                    {
                        linesToWrite[koenStep] = "Køn:   " + koen.ToString() + " > ";
                    }
                    else
                    {
                        linesToWrite[koenStep] = "Køn: < " + koen.ToString() + "   ";
                    }
                    break;
                case afdelingStep:
                    if (afdeling == MedarbejderOplysninger.Afdeling.SoftwareUdvikling)
                    {
                        linesToWrite[afdelingStep] = "Afdeling:   " + valgtAfdeling + " > ";
                    }
                    else if (afdeling == MedarbejderOplysninger.Afdeling.Administration)
                    {
                        linesToWrite[afdelingStep] = "Afdeling: < " + valgtAfdeling + " > ";
                    }
                    else if (afdeling == MedarbejderOplysninger.Afdeling.ServiceOgSupport)
                    {
                        linesToWrite[afdelingStep] = "Afdeling: < " + valgtAfdeling + "   ";
                    }
                    break;
                case foedselsdatoStep://Fødselsdato
                    linesToWrite[1] = "Brug højre og venstre piltast til at vælge datotype. Brug op og ned for at ændre datotype.";
                    switch (valgtTidsType)
                    {
                        case 0:
                            linesToWrite[13] = "Fødsesldato: [" + foedselsdato[0] + "]/" + foedselsdato[1] + "/" + foedselsdato[2];
                            break;
                        case 1:
                            linesToWrite[13] = "Fødsesldato: " + foedselsdato[0] + "/[" + foedselsdato[1] + "]/" + foedselsdato[2];
                            break;
                        case 2:
                            linesToWrite[13] = "Fødsesldato: " + foedselsdato[0] + "/" + foedselsdato[1] + "/[" + foedselsdato[2] + "]";
                            break;
                    }
                    break;
                case gemOgAfslutStep://Afslut
                    linesToWrite[0] = "Tryk enter for at gemme og afslutte. Tryk escape for at gå tilbage";
                    linesToWrite[1] = "Tryk backspace for at anullere og afslutte uden at gemme";
                    break;
            }
            linesToWrite[step] = "| " + linesToWrite[step] + " |";
            menu1.DrawBorders(titel);
            menu1.drawCenteredProgram(linesToWrite, titel);

            if (opretter && fornavn.Length >= 2 && efternavn.Length >= 2 && step == efternavnStep)
            {
                brugernavn = fornavn.Substring(0, 2).ToLower() + efternavn.Substring(0, 2).ToLower() + brugernavnTal;
            }
            keyInfo = Console.ReadKey();
            switch (step)
            {
                case fornavnStep: //Fornavn
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (fornavn == "" || fornavn == null)
                        {
                            fornavn = "";
                        }
                        else
                        {
                            fornavn = fornavn.Remove(fornavn.Length - 1);
                        }
                    }
                    else if ((keyInfo.KeyChar >= 'a' && keyInfo.KeyChar <= 'z') || (keyInfo.KeyChar >= 'A' && keyInfo.KeyChar <= 'Z') || keyInfo.KeyChar == '-' || keyInfo.KeyChar == '.' || keyInfo.KeyChar == ' ')
                    {
                        fornavn += keyInfo.KeyChar;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter && fornavn.Length >= 2)
                    {
                        step++;
                    }
                    break;
                case efternavnStep: //Efternavn
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (efternavn == "" || efternavn == null)
                        {
                            efternavn = "";
                        }
                        else
                        {
                            efternavn = efternavn.Remove(efternavn.Length - 1);
                        }
                    }
                    else if ((keyInfo.KeyChar >= 'a' && keyInfo.KeyChar <= 'z') || (keyInfo.KeyChar >= 'A' && keyInfo.KeyChar <= 'Z') || keyInfo.KeyChar == '-' || keyInfo.KeyChar == '.' || keyInfo.KeyChar == ' ')
                    {
                        efternavn += keyInfo.KeyChar;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter && efternavn.Length >= 2)
                    {
                        step++;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        step--;
                    }
                    break;
                case koenStep: //Køn
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        koen = MedarbejderOplysninger.Koen.M;
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        koen = MedarbejderOplysninger.Koen.F;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        step--;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        step++;
                    }
                    break;
                case foedselsdatoStep: //Fødsesldato
                    AlderOpgave alderOpgave = new AlderOpgave();
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (valgtTidsType > 0)
                        {
                            valgtTidsType--;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        if (valgtTidsType < 2)
                        {
                            valgtTidsType++;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        alderOpgave.DatoModifier(valgtTidsType, 1, foedselsdato);
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        alderOpgave.DatoModifier(valgtTidsType, -1, foedselsdato);
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        step--;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {

                        step++;
                    }
                    break;
                case stillingStep: //Stilling
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (stilling == "" || stilling == null)
                        {
                            stilling = "";
                        }
                        else
                        {
                            stilling = stilling.Remove(stilling.Length - 1);
                        }
                    }
                    else if ((keyInfo.KeyChar >= 'a' && keyInfo.KeyChar <= 'z') || (keyInfo.KeyChar >= 'A' && keyInfo.KeyChar <= 'Z') || keyInfo.KeyChar == '-' || keyInfo.KeyChar == '.' || keyInfo.KeyChar == ' ' || (keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9'))
                    {
                        stilling += keyInfo.KeyChar;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        step--;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter && stilling.Length >= 2)
                    {
                        step++;
                    }
                    break;
                case afdelingStep:
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (afdeling == MedarbejderOplysninger.Afdeling.Administration)
                        {
                            afdeling = MedarbejderOplysninger.Afdeling.SoftwareUdvikling;
                        }
                        else if (afdeling == MedarbejderOplysninger.Afdeling.ServiceOgSupport)
                        {
                            afdeling = MedarbejderOplysninger.Afdeling.Administration;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        if (afdeling == MedarbejderOplysninger.Afdeling.SoftwareUdvikling)
                        {
                            afdeling = MedarbejderOplysninger.Afdeling.Administration;
                        }
                        else if (afdeling == MedarbejderOplysninger.Afdeling.Administration)
                        {
                            afdeling = MedarbejderOplysninger.Afdeling.ServiceOgSupport;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        step--;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        step += 2;
                    }
                    break;
                case gemOgAfslutStep: //Gem og afslut
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        step -= 2;
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        redigerer = false;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter && stilling.Length >= 2)
                    {
                        var medarbejder = new MedarbejderOplysninger.Medarbejder
                        {
                            MANummer = maNummer,
                            Navn = new MedarbejderOplysninger.Fuldtnavn(fornavn, efternavn),
                            Brugernavn = brugernavn,
                            Stilling = stilling,
                            Afdeling = afdeling,
                            Koen = koen,
                            Foedselsdato = new DateOnly(foedselsdato[2], foedselsdato[1], foedselsdato[0]),
                            Oprettelsesdato = DateTime.Now
                        };
                        return medarbejder;

                    }
                    break;
            }

        }
        return null;

    }

    public void DrawStep(List<string> oplysninger, string stepName, int yposStart)
    {
        int ypos = yposStart;
        Console.SetCursorPosition(Console.WindowWidth / 2 - ("Oplysninger".Length / 2), ypos);
        ypos += 2;
        foreach (string oplysning in oplysninger)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - (oplysning.Length / 2), ypos);
            Console.Write(oplysning);
            ypos++;
        }
        Console.SetCursorPosition(Console.WindowWidth / 2 - (stepName.Length / 2), ypos);
        Console.Write(stepName);
        Console.SetCursorPosition(Console.WindowWidth / 2, ypos + 2);
    }
    public List<MedarbejderOplysninger.Medarbejder> IndlaesGemteMedarbejdere()
    {
        string filnavn = "medarbejdere.json";
        if (!File.Exists(filnavn))
        {
            return new List<MedarbejderOplysninger.Medarbejder>();
        }
        string json = File.ReadAllText(filnavn);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<MedarbejderOplysninger.Medarbejder>();
        }

        var medarbejdere = JsonConvert.DeserializeObject<List<MedarbejderOplysninger.Medarbejder>>(json);
        return medarbejdere ?? new List<MedarbejderOplysninger.Medarbejder>();
    }

    public void GemMedarbejdere(List<MedarbejderOplysninger.Medarbejder> medarbejdere)
    {
        string filnavn = "medarbejdere.json";
        string json = JsonConvert.SerializeObject(medarbejdere, Formatting.Indented);
        File.WriteAllText(filnavn, json);
    }
    private static Random random = new Random(); //Undgår at generer samme tal hvis den her er uden for metoden
    public string GenererMANummer() //Generere et 10 cifret MA nummer som string da int er 10 cifret, men kun op til 2147483647
    {
        string nummerString = "";
        for (int i = 0; i < 10; i++)
        {
            nummerString += random.Next(0, 10);
        }
        return nummerString;
    }


}

public class MedarbejderOplysninger
{
    public record Medarbejder
    {
        public string MANummer { get; init; } = ""; //Medarbejder nummer
        public Fuldtnavn Navn { get; set; } = new("", "");//Fulde navn (Fornavn + Efternavn)
        public string Brugernavn { get; init; } = "";//Brugernavn
        public Koen Koen { get; set; } //Køn
        public DateOnly Foedselsdato { get; set; } //Fødselsdato
        public string Stilling { get; set; } = "";//Stilling
        public Afdeling Afdeling { get; set; } //Afdeling
        public DateTime Oprettelsesdato { get; init; } //Oprettelsedato af medarbejder profilen

        public PensionsDato PensionsDato { get; set; } //Pensionsdato
    }
    public struct Fuldtnavn(string fornavn, string efternavn)
    {
        public string Fornavn { get; set; } = fornavn;
        public string Efternavn { get; set; } = efternavn;

    }
    public enum Koen
    {
        M,
        F
    }
    public enum Afdeling
    {
        SoftwareUdvikling,
        Administration,
        ServiceOgSupport
    };
    public struct PensionsDato(DateOnly dato)
    {
        public DateOnly Dato { get; set; } = dato;
    }
    public struct Alder(int alder)
    {
        public int AlderM { get; set; } = alder;
    }
}
