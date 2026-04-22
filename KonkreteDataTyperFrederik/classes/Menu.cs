namespace MenuN;

class Menu
{
    public void Program()
    {
        int stage = 0;
        bool usingProgram = true;
        int selectedOption = 0;
        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        while (usingProgram)
        {

            selectedOption = MainMenu(stage, keyInfo);
            switch (selectedOption)
            {
                case 0: //Søge
                    selectedOption = resetMenu(selectedOption);
                    Soege soege = new Soege();
                    soege.Draw();
                    break;
                case 1: //Tjek type af input
                    selectedOption = resetMenu(selectedOption);
                    Dynamic dynamicR = new Dynamic();
                    dynamicR.Draw();
                    break;
                case 2://Alder
                    selectedOption = resetMenu(selectedOption);
                    AlderOpgave alder = new AlderOpgave();
                    alder.Alder();
                    break;
                case 3://Medarbejder oversigt
                    MedarbejderProgram medarbejderOplysninger = new MedarbejderProgram(); 
                    medarbejderOplysninger.MedarbejderMenu(keyInfo);
                break;
                case 8: //Afslut
                    usingProgram = false;
                    break;
                default:
                    break;
            }
        }
    }
    public void drawCenteredProgram(string[] linesToWrite, string title)
    {
        Console.Clear();
        DrawBorders(title);
        Console.SetCursorPosition(Console.WindowWidth / 2 - (title.Length / 2), 2);
        Console.Write(title);
        int height = 5;
        for (int i = 0; i < linesToWrite.Length; i++)
        {
            string line = TilpasLinjeTilVindue(linesToWrite[i] ?? "");
            int width = Math.Max(0, Console.WindowWidth / 2 - (line.Length / 2));
            Console.SetCursorPosition(width, height + i);
            Console.Write(line);
        }

    }
   public void drawCenteredListe(string[] linesToWrite, string title, int page, int linjerPerSide)
{
    Console.Clear();
    DrawBorders(title);

    Console.SetCursorPosition(Console.WindowWidth / 2 - (title.Length / 2), 2);
    Console.Write(title);

    int startIndex = page * linjerPerSide;
    int endIndex = Math.Min(startIndex + linjerPerSide, linesToWrite.Length);

    int height = 5;

    for (int i = startIndex; i < endIndex; i++)
    {
        string line = TilpasLinjeTilVindue(linesToWrite[i] ?? "");
        int width = Math.Max(0, Console.WindowWidth / 2 - (line.Length / 2));
        Console.SetCursorPosition(width, height + (i - startIndex));
        Console.Write(line);
    }

    int totalPages = (int)Math.Ceiling(linesToWrite.Length / (double)linjerPerSide);
    string pageText = $"Side {page + 1}/{Math.Max(totalPages, 1)}";

    Console.SetCursorPosition(Console.WindowWidth / 2 - (pageText.Length / 2), height + linjerPerSide + 1);
    Console.Write(pageText);
}

    private string TilpasLinjeTilVindue(string line)
    {
        int maxWidth = Math.Max(1, Console.WindowWidth - 6);
        if (line.Length <= maxWidth)
        {
            return line;
        }

        if (maxWidth <= 3)
        {
            return line.Substring(0, maxWidth);
        }

        return line.Substring(0, maxWidth - 3) + "...";
    }

    public void DrawBorders(string title)
    {
        int borderClosenessToEdge = 2;
        int widthBorderEnd = Console.WindowWidth - borderClosenessToEdge;
        int heightBorderEnd = Console.WindowHeight - borderClosenessToEdge;
        int widthBorderStart = borderClosenessToEdge;
        int heightBorderStart = borderClosenessToEdge;
        for (int i = widthBorderStart; i < widthBorderEnd; i++)
        {
            DrawSymbol('═', i, heightBorderStart);
            DrawSymbol('═', i, heightBorderEnd);
        }
        for (int i = heightBorderStart; i < heightBorderEnd; i++)
        {
            DrawSymbol('║', widthBorderStart, i);
            DrawSymbol('║', widthBorderEnd, i);
        }
        DrawSymbol('╔', widthBorderStart, heightBorderStart);
        DrawSymbol('╚', widthBorderStart, heightBorderEnd);
        DrawSymbol('╗', widthBorderEnd, heightBorderStart);
        DrawSymbol('╝', widthBorderEnd, heightBorderEnd);
        
        DrawSymbol(' ', Console.WindowWidth / 2 - (title.Length / 2) - 1, heightBorderStart);
        Console.SetCursorPosition(Console.WindowWidth / 2 - (title.Length / 2), heightBorderStart);
        Console.Write(title);
        DrawSymbol(' ', Console.WindowWidth / 2 - (title.Length / 2) + title.Length, heightBorderStart);
        

    }
    public void DrawSymbol(char symbol, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }
    public int resetMenu(int selectedOption)
    {
        selectedOption = 0;
        return selectedOption;
    }

    public int MainMenu(int stage, ConsoleKeyInfo keyInfo)
    {

        while (true)
        {
            string[] menuOptions =
        {
            "Søg efter et ord (Uge 1)",
            "Tjek typer af input (Uge 1)",
            "Pensionsalderudregning (Uge 1)",
            "Medarbejder oversigt (Uge 2)",
            "Under konstruktion",
            "Under konstruktion",
            "Under konstruktion",
            "Under konstruktion",
            "Afslut program"
        };


            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    return stage;
                    break;
                case ConsoleKey.UpArrow:
                    if (stage > 0)
                    {
                        stage--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (stage < menuOptions.Length - 1)
                    {
                        stage++;
                    }
                    break;
                default:
                    break;
            }
            DrawMenu(stage, menuOptions, "Hovedmenu");
            keyInfo = Console.ReadKey();
        }

    }
    public void DrawMenu(int stage, string[] menuOptions, string menuTitle)
    {

        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == stage)
            {
                menuOptions[i] = ">> " + menuOptions[i] + " <<";
            }
        }
        Console.Clear();
        DrawBorders(menuTitle);
        Console.SetCursorPosition(Console.WindowWidth / 2 - (menuTitle.Length / 2), 2);
        Console.Write(menuTitle);
        int height = 5;
        for (int i = 0; i < menuOptions.Length; i++)
        {
            int width = Console.WindowWidth / 2 - (menuOptions[i].Length / 2);
            Console.SetCursorPosition(width, height + i);
            Console.Write(menuOptions[i]);
        }

    }

    public int MenuControls(char input)
    {
        int returnValue = 0;
        return returnValue;
    }
}
