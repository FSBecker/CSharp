

using Microsoft.VisualBasic;

namespace MedarbejderOversigtN;

public class MedarbejderProgram
{
    Menu menu1 = new Menu(); //Kalder på menuclassen så menuer kan blive lavet 

    public void MedarbejderMenu(ConsoleKeyInfo keyInfo)
    {
        int stage = 0;
        bool running = true;
        string titel = "Medarbejderoversigt";
        List<string> oplysninger = new List<string>();
        while (running)
        {
            oplysninger.Clear();
            string[] linesToWrite = {
            "Listevisning",
            "Søgefunktion",
            "Tabel over gemmensnits løn- og pensionsinfo",
            "Ændre basisløn og lønmultipliers",
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
                                "Pensionsaldato",
                                "Vis kun pensionsdato inden for 5 år",
                                "Køn",
                                "Vis kun mænd",
                                "Vis kun kvinder",
                                "Fornavn",
                                "Efternavn",
                                "Stilling",
                                "Afdeling",
                                "Løn"
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
                        case 2: //Tabel over pensioninfo
                            Console.Clear();
                            menu1.DrawBorders("Tabel over gemmensnits løn- og pensionsinfo");
                            string[,] tabel2d = BygLoenOgPensionsTabel2D();
                            menu1.draw2Dtabel(tabel2d, "Tabel over gemmensnits løn- og pensionsinfo");
                            Console.ReadKey();
                            break;
                        case 3://Ændre basisløn og lønmultipliers
                            VisAendreLoenMenu(keyInfo);
                            break;
                        case 4: //Opret medarbejder

                            int[] placeholderDatoIntArray = { 1, 1, 1955 };
                            DateOnly placeholderDatoDateOnly = DateOnly.Parse(placeholderDatoIntArray[1] + "/" + placeholderDatoIntArray[0] + "/" + placeholderDatoIntArray[2]);
                            string maNummer = GenererMANummer();
                            MedarbejderOplysninger.Medarbejder medarbejder = RedigeringMedarbejder(keyInfo, "", "", "", MedarbejderOplysninger.Stilling.Stenhugger, MedarbejderOplysninger.Koen.M, maNummer, placeholderDatoIntArray, true, MedarbejderOplysninger.Afdeling.SoftwareUdvikling);
                            if (medarbejder != null)
                            {
                                List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();
                                medarbejdere.Add(medarbejder);
                                GemMedarbejdere(medarbejdere);
                            }
                            break;
                        case 5:
                            oplysninger.Add("Hvor mange vil du oprette?");
                            oplysninger.Add("Afslut med enter");
                            Console.Clear();
                            menu1.DrawBorders("Masseoprettelse");
                            DrawStep(oplysninger, "", 5);
                            string antal = Console.ReadLine();
                            randomizeMedarbejdere(Int32.Parse(antal));

                            break;
                        case 6://Afslut
                            running = false;
                            break;
                    }
                    break;
            }

        }

    }
    public void VisAendreLoenMenu(ConsoleKeyInfo keyInfo)
    {
        bool aendrer = true;
        bool vaelgeraendring = true;
        bool basisloenAendring = false;
        bool multiplierAendring = false;
        int valgindex = 0;
        int koenEllerAfdelingIndex = 0;
        string valg = "";

        while (aendrer)
        {
            while (vaelgeraendring)
            {
                string[] aendringsValg =
                {
                "Basisløn",
                "Multipliers",
                "Se tabel over gemmensnits løn- og pensionsinfo",
                "Afslut",
            };
                aendringsValg[valgindex] = "| " + aendringsValg[valgindex] + " |";
                menu1.drawCenteredProgram(aendringsValg, "Vælg hvad du vil ændre");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (valgindex > 0)
                        {
                            valgindex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (valgindex < aendringsValg.Length - 1)
                        {
                            valgindex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (valgindex)
                        {
                            case 0:
                                basisloenAendring = true;
                                vaelgeraendring = false;
                                valgindex = 0;
                                break;
                            case 1:
                                koenEllerAfdelingIndex = 0;
                                bool vaelgerKoenEllerAfdeling = true;
                                while (vaelgerKoenEllerAfdeling)
                                {
                                    string[] koenEllerAfdeling =
                                    {
                                    "Køn",
                                    "Afdelinger"
                                };
                                    koenEllerAfdeling[koenEllerAfdelingIndex] = "| " + koenEllerAfdeling[koenEllerAfdelingIndex] + " |";
                                    menu1.drawCenteredProgram(koenEllerAfdeling, "Tabel over gemmensnits løn- og pensionsinfo");
                                    keyInfo = Console.ReadKey();
                                    switch (keyInfo.Key)
                                    {
                                        case ConsoleKey.UpArrow:
                                            if (koenEllerAfdelingIndex > 0)
                                            {
                                                koenEllerAfdelingIndex--;
                                            }
                                            break;
                                        case ConsoleKey.DownArrow:
                                            if (koenEllerAfdelingIndex < koenEllerAfdeling.Length - 1)
                                            {
                                                koenEllerAfdelingIndex++;
                                            }
                                            break;
                                        case ConsoleKey.Enter:
                                            vaelgerKoenEllerAfdeling = false;
                                            break;

                                    }
                                }
                                multiplierAendring = true;
                                vaelgeraendring = false;
                                valgindex = 0;
                                break;
                            case 2:
                                Console.Clear();
                                menu1.DrawBorders("Tabel over gemmensnits løn- og pensionsinfo");
                                string[,] tabel2d = BygLoenOgPensionsTabel2D();
                                menu1.draw2Dtabel(tabel2d, "Tabel over gemmensnits løn- og pensionsinfo");
                                Console.ReadKey();
                                break;
                            case 3:
                                vaelgeraendring = false;
                                aendrer = false;
                                break;
                        }
                        break;
                    case ConsoleKey.Escape:
                        vaelgeraendring = false;
                        aendrer = false;
                        break;
                }
            }
            List<string> stillingerList = new List<string>();
            List<int> basisloenne = new List<int>();
            List<string> afdelingerList = new List<string>();
            List<double> afdelingMultipliers = new List<double>();
            List<string> koenList = new List<string>();
            List<double> koenMultipliers = new List<double>();
            foreach (var stilling in stillinger)
            {
                stillingerList.Add(EnumNameAttributeHenter(stilling));
                basisloenne.Add(EnumBasisloenAttributeHenter(stilling));
            }
            foreach (var afdeling in afdelinger)
            {
                afdelingerList.Add(EnumNameAttributeHenter(afdeling));
                afdelingMultipliers.Add(EnumMultiplierAttributeHenter(afdeling));
            }
            foreach (var koen in koen)
            {
                koenList.Add(EnumNameAttributeHenter(koen));
                koenMultipliers.Add(EnumMultiplierAttributeHenter(koen));
            }
            if (basisloenAendring)
            {
                while (basisloenAendring)
                {
                    string[] aendringsValg = stringPlusIntListTilStringArray(stillingerList, basisloenne);
                    valg = basisloenne[valgindex].ToString();
                    aendringsValg[valgindex] = "| " + aendringsValg[valgindex] + " |";
                    menu1.drawCenteredProgram(aendringsValg, "Vælg hvad du vil ændre");
                    keyInfo = Console.ReadKey();
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (valgindex > 0)
                            {
                                valgindex--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (valgindex < aendringsValg.Length - 1)
                            {
                                valgindex++;
                            }
                            break;
                        case ConsoleKey.Enter:
                            for (int i = 0; i < stillinger.Length; i++)
                            {
                                EnumBasisloenAttributeGemmer(stillinger[i], basisloenne[i]);
                            }
                            basisloenAendring = false;
                            vaelgeraendring = true;
                            valgindex = 0;
                            break;
                        case ConsoleKey.Escape:
                            basisloenne.Clear();
                            foreach (var stilling in stillinger)
                            {
                                basisloenne.Add(EnumBasisloenAttributeHenter(stilling));
                            }
                            basisloenAendring = false;
                            vaelgeraendring = true;
                            valgindex = 0;
                            break;
                        case ConsoleKey.Backspace:
                            valg = valg.Remove(valg.Length - 1);
                            if (valg == "" || valg == null || valg == "0")
                            {
                                valg = "0";
                            }
                            basisloenne[valgindex] = Int32.Parse(valg);
                            break;
                        default:
                            if (keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9')
                            {
                                valg += keyInfo.KeyChar;
                                basisloenne[valgindex] = Int32.Parse(valg);
                            }
                            break;
                    }
                }
            }
            else if (multiplierAendring)
            {
                bool skalHavePunktum = false;
                while (multiplierAendring)
                {

                    string[] aendringsValg;
                    if (koenEllerAfdelingIndex == 0)
                    {
                        aendringsValg = stringPlusMultiplierListTilStringArray(koenList, koenMultipliers);
                        valg = koenMultipliers[valgindex].ToString();
                        aendringsValg[valgindex] = "| " + aendringsValg[valgindex] + " |";
                        menu1.drawCenteredProgram(aendringsValg, "Vælg hvad du vil ændre");
                    }
                    else
                    {
                        aendringsValg = stringPlusMultiplierListTilStringArray(afdelingerList, afdelingMultipliers);
                        valg = afdelingMultipliers[valgindex].ToString();
                        aendringsValg[valgindex] = "| " + aendringsValg[valgindex] + " |";
                        menu1.drawCenteredProgram(aendringsValg, "Vælg hvad du vil ændre");
                    }
                    Console.Write("\n" + valg);
                    keyInfo = Console.ReadKey();
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (valgindex > 0)
                            {
                                valgindex--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (valgindex < aendringsValg.Length - 1)
                            {
                                valgindex++;
                            }
                            break;
                        case ConsoleKey.Enter:
                            for (int i = 0; i < koen.Length; i++)
                            {
                                EnumKoenMultiplierAttributeGemmer(koen[i], koenMultipliers[i]);
                            }
                            for (int i = 0; i < afdelinger.Length; i++)
                            {
                                EnumAfdelingMultiplierAttributeGemmer(afdelinger[i], afdelingMultipliers[i]);
                            }
                            multiplierAendring = false;
                            vaelgeraendring = true;
                            valgindex = 0;
                            break;
                        case ConsoleKey.Escape:
                            koenMultipliers.Clear();
                            afdelingMultipliers.Clear();
                            foreach (var koen in koen)
                            {
                                koenMultipliers.Add(EnumMultiplierAttributeHenter(koen));
                            }
                            foreach (var afdeling in afdelinger)
                            {
                                afdelingMultipliers.Add(EnumMultiplierAttributeHenter(afdeling));
                            }
                            multiplierAendring = false;
                            vaelgeraendring = true;
                            valgindex = 0;
                            break;
                        case ConsoleKey.Backspace:
                            valg = valg.Remove(valg.Length - 1);
                            if (valg == "" || valg == null || valg == "0")
                            {
                                valg = "0";
                            }
                            if (koenEllerAfdelingIndex == 0)
                            {
                                koenMultipliers[valgindex] = Double.Parse(valg);
                            }
                            else
                            {
                                afdelingMultipliers[valgindex] = Double.Parse(valg);
                            }

                            break;
                        default:
                            if ((keyInfo.KeyChar == '.' && !valg.Contains('.')) || keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9')
                            {
                                if (skalHavePunktum)
                                {
                                    valg = valg + '.' + keyInfo.KeyChar;
                                    skalHavePunktum = false;
                                }
                                else
                                {
                                    if (keyInfo.KeyChar == '.')
                                    {
                                        skalHavePunktum = true;
                                    }
                                    else
                                    {
                                        valg += keyInfo.KeyChar;
                                        skalHavePunktum = false;
                                    }
                                }
                                if (koenEllerAfdelingIndex == 0)
                                {
                                    koenMultipliers[valgindex] = Convert.ToDouble(valg);
                                }
                                else
                                {
                                    afdelingMultipliers[valgindex] = Convert.ToDouble(valg);
                                }
                            }


                            break;
                    }
                }
            }
        }
    }
    public string[] stringPlusIntListTilStringArray(List<string> strings, List<int> ints)
    {
        List<string> faellesListe = new List<string>();
        for (int i = 0; i < strings.Count; i++)
        {
            faellesListe.Add(strings[i] + ": " + ints[i]);
        }
        return faellesListe.ToArray();
    }
    public string[] stringPlusMultiplierListTilStringArray(List<string> strings, List<double> doubles)
    {
        List<string> faellesListe = new List<string>();
        for (int i = 0; i < strings.Count; i++)
        {
            faellesListe.Add(strings[i] + ": " + doubles[i]);
        }
        return faellesListe.ToArray();
    }

    public float AarTilPension(DateOnly foedselsdato, MedarbejderOplysninger.Alder alder)
    {
        DateOnly pensionsDato = PensionsDatoUdregner(foedselsdato);
        DateOnly nu = DateOnly.FromDateTime(DateTime.Now);
        int years = pensionsDato.Year - nu.Year;
        int months = pensionsDato.Month - nu.Month;
        if (months < 0)
        {
            years--;
            months += 12;
        }
        float result = years + (months / 12f);

        return result;
    }
    public DateOnly PensionsDatoUdregner(DateOnly foedselsdato)
    {
        DateOnly pensionsdato = foedselsdato;
        if (foedselsdato <= DateOnly.FromDateTime(new DateTime(1954, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(66);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1955, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1955, 6, 30)))
        {
            pensionsdato = pensionsdato.AddYears(66).AddMonths(6);

        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1955, 7, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1962, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(67);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1963, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1966, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(68);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1967, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1970, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(69);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1971, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1974, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(70);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1975, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1978, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(71);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1979, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1982, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(72);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1983, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1987, 6, 30)))
        {
            pensionsdato = pensionsdato.AddYears(72).AddMonths(6);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1987, 7, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1991, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(73);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1992, 1, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(1996, 6, 30)))
        {
            pensionsdato = pensionsdato.AddYears(73).AddMonths(6);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(1996, 7, 1)) && foedselsdato <= DateOnly.FromDateTime(new DateTime(2002, 12, 31)))
        {
            pensionsdato = pensionsdato.AddYears(74);
        }
        else if (foedselsdato >= DateOnly.FromDateTime(new DateTime(2003, 1, 1)))
        {
            pensionsdato = pensionsdato.AddYears(74).AddMonths(6);
        }

        return pensionsdato;
    }
    public void listevisning(string sorteringsvalg, string soegeord, ConsoleKeyInfo keyInfo)
    {
        List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();
        Dictionary<(MedarbejderOplysninger.Afdeling, MedarbejderOplysninger.Koen), int> gennemsnitsloenPerGruppe = BeregnGennemsnitsloenPerAfdelingOgKoen(medarbejdere);
        bool visPensionsdato = false;
        bool visLoen = false;
        switch (sorteringsvalg)
        {
            case "Oprettelsesdato":
                medarbejdere = medarbejdere
                    .OrderBy(m => m.Oprettelsesdato)
                    .ToList();
                break;

            case "Alder":
                medarbejdere = medarbejdere
                    .OrderBy(m => m.Alder.AlderM)
                    .ToList();
                break;
            case "Pensionsaldato":
                visPensionsdato = true;
                medarbejdere = medarbejdere
                    .OrderBy(m => PensionsDatoUdregner(m.Foedselsdato))
                    .ToList();
                break;
            case "Vis kun pensionsdato inden for 5 år":
                visPensionsdato = true;
                medarbejdere = medarbejdere
                    .Where(m =>
                    {
                        float aarTilPension = AarTilPension(m.Foedselsdato, m.Alder);
                        return aarTilPension >= 0 && aarTilPension <= 5;
                    })
                    .OrderBy(m => PensionsDatoUdregner(m.Foedselsdato))
                    .ToList();
                break;
            case "Køn":
                medarbejdere = medarbejdere
                    .OrderBy(m => m.Koen)
                    .ToList();
                break;
            case "Vis kun mænd":
                medarbejdere = medarbejdere
                    .Where(m => m.Koen == MedarbejderOplysninger.Koen.M)
                    .ToList();
                break;
            case "Vis kun kvinder":
                medarbejdere = medarbejdere
                    .Where(m => m.Koen == MedarbejderOplysninger.Koen.F)
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
                medarbejdere = medarbejdere.OrderBy(m => EnumNameAttributeHenter(m.Stilling)).ToList();
                break;
            case "Afdeling":
                medarbejdere = medarbejdere.OrderBy(m => m.Afdeling).ToList();
                break;
            case "Løn":
                visLoen = true;
                medarbejdere = medarbejdere.OrderBy(m => LoenUdregner(m.Stilling, m.Koen, m.Afdeling)).ToList();
                medarbejdere.Reverse();
                break;
            case "Søgeord":
                medarbejdere = medarbejdere
                    .Where(m =>
                        m.Navn.Fornavn.Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
                        m.Navn.Efternavn.Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
                        EnumNameAttributeHenter(m.Stilling).Contains(soegeord, StringComparison.OrdinalIgnoreCase) ||
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
            string afdeling = EnumNameAttributeHenter(m.Afdeling);
            string koen = EnumNameAttributeHenter(m.Koen);
            string stilling = EnumNameAttributeHenter(m.Stilling);
            string pensionsInfo = "";
            float aarTilPension = AarTilPension(m.Foedselsdato, m.Alder);
            string aarTilPensionString = aarTilPension.ToString("0.0") + " år til pension";
            if (aarTilPension < 0)
            {
                aarTilPensionString = "Pensionsalder allerede opnået";
            }
            if (visPensionsdato)
            {
                int gennemsnitsloen = HentGennemsnitsloenForAfdelingOgKoen(gennemsnitsloenPerGruppe, m.Afdeling, m.Koen);
                int bonus = PensionsBonusUdregner(gennemsnitsloen, m.Koen);
                pensionsInfo = "Pensionsdato: " + PensionsDatoUdregner(m.Foedselsdato).ToString("dd/MM/yyyy") + ", Pensionsbonus: " + bonus.ToString();

                linesToWrite[i] = m.Navn.Fornavn + " " + m.Navn.Efternavn + ", " + koen + ", " + m.Alder.AlderM.ToString() + " år(" + aarTilPensionString + "), " + pensionsInfo + ", " + stilling + ", " + afdeling;
            }
            else if (visLoen)
            {
                linesToWrite[i] = m.Navn.Fornavn + " " + m.Navn.Efternavn + ", " + koen + ", " + m.Alder.AlderM.ToString() + " år(" + aarTilPensionString + "), " + stilling + ", " + afdeling + ", Månedsløn: " + LoenUdregner(m.Stilling, m.Koen, m.Afdeling);
            }
            else
            {
                linesToWrite[i] = m.Navn.Fornavn + " " + m.Navn.Efternavn + ", " + koen + ", " + m.Alder.AlderM.ToString() + " år(" + aarTilPensionString + "), " + stilling + ", " + afdeling;
            }
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
            menu1.drawCenteredListe(linesToWriteWithHighligt, titel, sidenummer, linjerPerSide, medarbejdere.Count);

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
    public string[,] BygLoenOgPensionsTabel2D()
    {
        List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();
        Dictionary<(MedarbejderOplysninger.Afdeling, MedarbejderOplysninger.Koen), int> gennemsnitsloenPerGruppe = BeregnGennemsnitsloenPerAfdelingOgKoen(medarbejdere);
        MedarbejderOplysninger.Afdeling[] afdelinger =
        {
            MedarbejderOplysninger.Afdeling.SoftwareUdvikling,
            MedarbejderOplysninger.Afdeling.Administration,
            MedarbejderOplysninger.Afdeling.ServiceOgSupport
        };

        string[,] tabel = new string[afdelinger.Length + 1, 5];
        tabel[0, 0] = "Afdeling";
        tabel[0, 1] = "Gns. løn mænd";
        tabel[0, 2] = "Gns. løn kvinder";
        tabel[0, 3] = "Bonus mænd";
        tabel[0, 4] = "Bonus kvinder";

        for (int i = 0; i < afdelinger.Length; i++)
        {
            MedarbejderOplysninger.Afdeling afdeling = afdelinger[i];
            int gennemsnitMaend = HentGennemsnitsloenForAfdelingOgKoen(gennemsnitsloenPerGruppe, afdeling, MedarbejderOplysninger.Koen.M);
            int gennemsnitKvinder = HentGennemsnitsloenForAfdelingOgKoen(gennemsnitsloenPerGruppe, afdeling, MedarbejderOplysninger.Koen.F);

            tabel[i + 1, 0] = EnumNameAttributeHenter(afdeling);
            tabel[i + 1, 1] = gennemsnitMaend.ToString();
            tabel[i + 1, 2] = gennemsnitKvinder.ToString();
            tabel[i + 1, 3] = PensionsBonusUdregner(gennemsnitMaend, MedarbejderOplysninger.Koen.M).ToString();
            tabel[i + 1, 4] = PensionsBonusUdregner(gennemsnitKvinder, MedarbejderOplysninger.Koen.F).ToString();
        }

        return tabel;
    }
    public string[,,] BygLoenOgPensionsTabel3D() //Bliver ikke brugt, er her kun for at vise at jeg kan
    {
        List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();

        Dictionary<(MedarbejderOplysninger.Afdeling, MedarbejderOplysninger.Koen), int> gennemsnitsloenPerGruppe =
            BeregnGennemsnitsloenPerAfdelingOgKoen(medarbejdere);


        string[,,] tabel = new string[3, 2, 2];

        // 1. dimension = Afdeling
        // [0, *, *] = SoftwareUdvikling
        // [1, *, *] = Administration
        // [2, *, *] = ServiceOgSupport
        //
        // 2. dimension = Køn
        // [*, 0, *] = Mænd
        // [*, 1, *] = Kvinder
        //
        // 3. dimension = Løntype
        // [*, *, 0] = Gennemsnitsløn
        // [*, *, 1] = Fratrædelsesbonus

        for (int afdelingIndex = 0; afdelingIndex < afdelinger.Length; afdelingIndex++)
        {
            MedarbejderOplysninger.Afdeling afdeling = afdelinger[afdelingIndex];

            int gennemsnitMaend = HentGennemsnitsloenForAfdelingOgKoen(
                gennemsnitsloenPerGruppe,
                afdeling,
                MedarbejderOplysninger.Koen.M);

            int gennemsnitKvinder = HentGennemsnitsloenForAfdelingOgKoen(
                gennemsnitsloenPerGruppe,
                afdeling,
                MedarbejderOplysninger.Koen.F);

            // Mænd
            tabel[afdelingIndex, 0, 0] = gennemsnitMaend.ToString();
            tabel[afdelingIndex, 0, 1] = PensionsBonusUdregner(
                gennemsnitMaend,
                MedarbejderOplysninger.Koen.M).ToString();

            // Kvinder
            tabel[afdelingIndex, 1, 0] = gennemsnitKvinder.ToString();
            tabel[afdelingIndex, 1, 1] = PensionsBonusUdregner(
                gennemsnitKvinder,
                MedarbejderOplysninger.Koen.F).ToString();
        }

        return tabel;
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

        DateOnly foedselsdato = DateOnly.FromDateTime(DateTime.Now);
        DateTime oprettelsesdato = DateTime.Now;
        List<MedarbejderOplysninger.Medarbejder> medarbejdere = IndlaesGemteMedarbejdere();
        for (int i = 1; i < antal + 1; i++)
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

        DateTime start = new DateTime(1955, 1, 1);
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

    public int LoenUdregner(MedarbejderOplysninger.Stilling stilling, MedarbejderOplysninger.Koen koen, MedarbejderOplysninger.Afdeling afdeling)
    {
        double loen = EnumBasisloenAttributeHenter(stilling);
        loen = Math.Floor(loen * EnumMultiplierAttributeHenter(koen) * EnumMultiplierAttributeHenter(afdeling));
        int gennemsnitsloen = (int)loen;
        return gennemsnitsloen;
    }

    public int PensionsBonusUdregner(int gennemsnitsloen, MedarbejderOplysninger.Koen koen)
    {
        double bonus = gennemsnitsloen;
        switch (koen)
        {
            case MedarbejderOplysninger.Koen.M:
                bonus = Math.Floor(bonus * 2);
                break;
            case MedarbejderOplysninger.Koen.F:
                bonus = Math.Floor(bonus * 1.25);
                break;
        }
        return (int)bonus;
    }
    public Dictionary<(MedarbejderOplysninger.Afdeling, MedarbejderOplysninger.Koen), int> BeregnGennemsnitsloenPerAfdelingOgKoen(List<MedarbejderOplysninger.Medarbejder> medarbejdere)
    {
        return medarbejdere
            .GroupBy(m => (m.Afdeling, m.Koen))
            .ToDictionary(
                gruppe => gruppe.Key,
                gruppe => (int)Math.Floor(gruppe.Average(m => LoenUdregner(m.Stilling, m.Koen, m.Afdeling)))
            );
    }

    public int HentGennemsnitsloenForAfdelingOgKoen(Dictionary<(MedarbejderOplysninger.Afdeling, MedarbejderOplysninger.Koen), int> gennemsnitsloenPerGruppe, MedarbejderOplysninger.Afdeling afdeling, MedarbejderOplysninger.Koen koen)
    {
        if (gennemsnitsloenPerGruppe.TryGetValue((afdeling, koen), out int gennemsnitsloen))
        {
            return gennemsnitsloen;
        }

        return 0;
    }


    public MedarbejderOplysninger.Medarbejder? RedigeringMedarbejder(ConsoleKeyInfo keyInfo, string fornavn, string efternavn, string brugernavn, MedarbejderOplysninger.Stilling stilling, MedarbejderOplysninger.Koen koen, string maNummer, int[] foedselsdato, bool opretter, MedarbejderOplysninger.Afdeling afdeling)
    {

        const int fornavnStep = 12;
        const int efternavnStep = 13;
        const int koenStep = 14;
        const int foedselsdatoStep = 15;
        const int stillingStep = 16;
        const int afdelingStep = 17;
        const int gemOgAfslutStep = 19;
        int step = fornavnStep;
        bool redigerer = true;
        string brugernavnTal = random.Next(0, 10).ToString() + random.Next(0, 10).ToString();
        string titel = "Redigere medarbejder: " + brugernavn;
        if (opretter)
        {
            titel = "Opret ny medarbejder";
        }

        int valgtTidsType = 0;
        int valgtStillingIndex = Array.IndexOf(stillinger, stilling);
        if (valgtStillingIndex < 0)
        {
            valgtStillingIndex = 0;
            stilling = stillinger[valgtStillingIndex];
        }
        Console.Clear();
        Menu menu1 = new Menu();


        while (redigerer)
        {
            Console.Clear();
            DateOnly foedselsdatoDateOnly = DateOnly.Parse(foedselsdato[1] + "/" + foedselsdato[0] + "/" + foedselsdato[2]);
            DateOnly nu = DateOnly.FromDateTime(DateTime.Now);
            
            string pensionsDato = PensionsDatoUdregner(foedselsdatoDateOnly).ToString("dd/MM/yyyy");
            MedarbejderOplysninger.Alder alder = BeregnAlder(foedselsdatoDateOnly);
            float aarTilPension = AarTilPension(foedselsdatoDateOnly, alder);
            string valgtAfdeling = EnumNameAttributeHenter(afdeling);
            string valgtKoen = EnumNameAttributeHenter(koen);
            string valgtStilling = EnumNameAttributeHenter(stilling);
            int basisloen = EnumBasisloenAttributeHenter(stilling);
            int reelLoen = LoenUdregner(stilling, koen, afdeling);
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
              "Basisløn: " + basisloen + ", Reél månedsløn: " + reelLoen, //8
              "Pensionsdato: " + pensionsDato + " (" + aarTilPension + " år til pension) ", //9
              "",  //10
              "-Indtast manglende oplysniger-", // 11
              "Fornavn: " + fornavn, //12
              "Efternavn: " + efternavn,  //13
              "Køn: " + valgtKoen, //14
              "Fødsesldato: " + foedselsdato[0] + "/" + foedselsdato[1] + "/" + foedselsdato[2], //15
              "Stilling: " + valgtStilling, //16
              "Afdeling: " + valgtAfdeling, //17
              "", //18
              "Gem og afslut", //19
              "" //20
            };
            switch (step)
            {
                case fornavnStep://Fornavn
                case efternavnStep://Efternavn
                    linesToWrite[1] = "Indtast oplysning med tastaturet";
                    break;
                case koenStep://Køn
                    if (koen == MedarbejderOplysninger.Koen.M)
                    {
                        linesToWrite[koenStep] = "Køn:   " + valgtKoen + " > ";
                    }
                    else
                    {
                        linesToWrite[koenStep] = "Køn: < " + valgtKoen + "   ";
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
                case stillingStep://Stilling
                    linesToWrite[1] = "Brug venstre og højre piltast til at vælge stilling.";
                    if (valgtStillingIndex == 0)
                    {
                        linesToWrite[stillingStep] = "Stilling:   " + valgtStilling + " > ";
                    }
                    else if (valgtStillingIndex == stillinger.Length - 1)
                    {
                        linesToWrite[stillingStep] = "Stilling: < " + valgtStilling + "   ";
                    }
                    else
                    {
                        linesToWrite[stillingStep] = "Stilling: < " + valgtStilling + " > ";
                    }
                    break;
                case foedselsdatoStep://Fødselsdato
                    linesToWrite[1] = "Brug højre og venstre piltast til at vælge datotype. Brug op og ned for at ændre datotype.";
                    switch (valgtTidsType)
                    {
                        case 0:
                            linesToWrite[foedselsdatoStep] = "Fødsesldato: [" + foedselsdato[0] + "]/" + foedselsdato[1] + "/" + foedselsdato[2];
                            break;
                        case 1:
                            linesToWrite[foedselsdatoStep] = "Fødsesldato: " + foedselsdato[0] + "/[" + foedselsdato[1] + "]/" + foedselsdato[2];
                            break;
                        case 2:
                            linesToWrite[foedselsdatoStep] = "Fødsesldato: " + foedselsdato[0] + "/" + foedselsdato[1] + "/[" + foedselsdato[2] + "]";
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
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (valgtStillingIndex > 0)
                        {
                            valgtStillingIndex--;
                            stilling = stillinger[valgtStillingIndex];
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        if (valgtStillingIndex < stillinger.Length - 1)
                        {
                            valgtStillingIndex++;
                            stilling = stillinger[valgtStillingIndex];
                        }
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
                    else if (keyInfo.Key == ConsoleKey.Enter)
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
    public MedarbejderOplysninger.Stilling[] stillinger = Enum.GetValues<MedarbejderOplysninger.Stilling>();
    public MedarbejderOplysninger.Afdeling[] afdelinger = Enum.GetValues<MedarbejderOplysninger.Afdeling>();
    public MedarbejderOplysninger.Koen[] koen = Enum.GetValues<MedarbejderOplysninger.Koen>();

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

        JArray? jsonArray = JsonConvert.DeserializeObject<JArray>(json);
        if (jsonArray == null)
        {
            return new List<MedarbejderOplysninger.Medarbejder>();
        }

        var medarbejdere = jsonArray.ToObject<List<MedarbejderOplysninger.Medarbejder>>();
        if (medarbejdere != null)
        {
            GemMedarbejdere(medarbejdere);
        }

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

    public string EnumNameAttributeHenter(Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());

        if (field == null)
            return value.ToString();

        NavnOgLoenMultiplierAttribute? multiplierAttribute =
            field.GetCustomAttribute<NavnOgLoenMultiplierAttribute>();

        if (multiplierAttribute != null)
            return multiplierAttribute.Name;

        StillingOgBasisloenAttribute? stillingAttribute =
            field.GetCustomAttribute<StillingOgBasisloenAttribute>();

        if (stillingAttribute != null)
            return stillingAttribute.Name;

        return value.ToString();
    }

    public double EnumMultiplierAttributeHenter(Enum value)
    {
        if (value is MedarbejderOplysninger.Koen koenValue)
        {
            if (koenMultiplierOverrides.TryGetValue(koenValue, out double gemtMultiplier))
            {
                return gemtMultiplier;
            }
        }

        if (value is MedarbejderOplysninger.Afdeling afdelingValue)
        {
            if (afdelingMultiplierOverrides.TryGetValue(afdelingValue, out double gemtMultiplier))
            {
                return gemtMultiplier;
            }
        }

        FieldInfo? field = value.GetType().GetField(value.ToString());

        if (field == null)
            return 0;

        NavnOgLoenMultiplierAttribute? attribute =
            field.GetCustomAttribute<NavnOgLoenMultiplierAttribute>();

        return attribute?.LoenMultiplier ?? 0;
    }
    private Dictionary<MedarbejderOplysninger.Koen, double> koenMultiplierOverrides
    = new();
    private Dictionary<MedarbejderOplysninger.Afdeling, double> afdelingMultiplierOverrides
    = new();
    public void EnumKoenMultiplierAttributeGemmer(MedarbejderOplysninger.Koen value, double nyMultiplier)
    {
        koenMultiplierOverrides[value] = nyMultiplier;
    }
    public void EnumAfdelingMultiplierAttributeGemmer(MedarbejderOplysninger.Afdeling value, double nyMultiplier)
    {
        afdelingMultiplierOverrides[value] = nyMultiplier;
    }
    private Dictionary<MedarbejderOplysninger.Stilling, int> basisloenOverrides
    = new();
    public int EnumBasisloenAttributeHenter(MedarbejderOplysninger.Stilling value)
    {
        if (basisloenOverrides.TryGetValue(value, out int gemtMultiplier))
            return gemtMultiplier;

        FieldInfo? field = value.GetType().GetField(value.ToString());

        if (field == null)
            return 0;

        StillingOgBasisloenAttribute? attribute =
            field.GetCustomAttribute<StillingOgBasisloenAttribute>();

        return attribute?.Basisloen ?? 0;
    }
    public void EnumBasisloenAttributeGemmer(MedarbejderOplysninger.Stilling stilling, int nyBasisloen)
    {
        basisloenOverrides[stilling] = nyBasisloen;
    }
}

public class MedarbejderOplysninger
{
    public MedarbejderProgram mProgram = new MedarbejderProgram();
    public record Medarbejder
    {
        public string MANummer { get; init; } = ""; //Medarbejder nummer
        public Fuldtnavn Navn { get; set; } = new("", "");//Fulde navn (Fornavn + Efternavn)
        public string Brugernavn { get; init; } = "";//Brugernavn
        public Koen Koen { get; set; } //Køn
        public DateOnly Foedselsdato { get; set; } //Fødselsdato
        public Alder Alder
        {
            get
            {
                DateOnly nu = DateOnly.FromDateTime(DateTime.Now);
                MedarbejderOplysninger.Alder alder = new MedarbejderOplysninger.Alder();
                int alderInt = nu.Year - Foedselsdato.Year;

                if (nu < Foedselsdato.AddYears(alderInt))
                {
                    alderInt--;
                }
                alder.AlderM = alderInt;
                return alder;
            }
            set;
        }
        public Stilling Stilling { get; set; } //Stilling
        public Afdeling Afdeling { get; set; } //Afdeling
        public DateTime Oprettelsesdato { get; init; } //Oprettelsedato af medarbejder profilen
    }
    public struct Fuldtnavn(string fornavn, string efternavn)
    {
        public string Fornavn { get; set; } = fornavn;
        public string Efternavn { get; set; } = efternavn;

    }
    public enum Koen
    {
        [NavnOgLoenMultiplier("Mand", 1.2)]
        M,
        [NavnOgLoenMultiplier("Kvinde", 0.8)]
        F
    }
    public enum Afdeling
    {
        [NavnOgLoenMultiplier("Software udvikling", 1.2)]
        SoftwareUdvikling,
        [NavnOgLoenMultiplier("Administration", 1.5)]
        Administration,
        [NavnOgLoenMultiplier("Service og support", 0.8)]
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
    public enum Stilling
    {
        [StillingOgBasisloen("Stenhugger", 31000)]
        Stenhugger,
        [StillingOgBasisloen("Udvikler", 52000)]
        Udvikler,
        [StillingOgBasisloen("Testperson", 31500)]
        Testperson,
        [StillingOgBasisloen("Kantinemedarbejder", 23000)]
        Kantinemedarbejder,
        [StillingOgBasisloen("Ingengør", 64000)]
        Ingengør,
        [StillingOgBasisloen("Webdesigner", 50000)]
        Webdesigner,
        [StillingOgBasisloen("GrafiskDesigner", 55000)]
        GrafiskDesigner,
        [StillingOgBasisloen("Hundepasser", 13000)]
        Hundepasser,
        [StillingOgBasisloen("Pedagog", 33000)]
        Pedagog,
        [StillingOgBasisloen("Servicemedarbejder", 28000)]
        Servicemedarbejder,
        [StillingOgBasisloen("Mellemleder", 65000)]
        Mellemleder
    }
}
public class StillingOgBasisloenAttribute : Attribute
{
    public string Name { get; set; }
    public int Basisloen { get; set; }

    public StillingOgBasisloenAttribute(string name, int basisloen)
    {
        Name = name;
        Basisloen = basisloen;
    }
}
public class NavnOgLoenMultiplierAttribute : Attribute
{
    public string Name { get; set; }
    public double LoenMultiplier { get; set; }
    public NavnOgLoenMultiplierAttribute(string name, double loenMultiplier)
    {
        Name = name;
        LoenMultiplier = loenMultiplier;
    }
}

