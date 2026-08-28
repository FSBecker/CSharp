/* // TypeSikkerhed.cs
// Formål: Demonstrere (1) forskellen på værdi- og referencetyper, og
// (2) hvorfor "decimal" skal bruges til penge i stedet for "double".
//
// Brug filen som et selvstændigt konsolprojekt, fx:
//   dotnet new console -n TypeSikkerhedDemo
//   (kopiér indholdet af denne fil ind i Program.cs)
//   dotnet run

using System;

class Program
{
    static void Main(string[] args)
    {
        DemonstrateValueTypes();
        Console.WriteLine();
        DemonstrateReferenceTypes();
        Console.WriteLine();
        DemonstrateDecimalVsDouble();
    }

    // --------------------------------------------------------------
    // 1. Værditype-eksempel: kopiering giver en selvstændig kopi.
    // --------------------------------------------------------------
    static void DemonstrateValueTypes()
    {
        Console.WriteLine("--- Værdityper (int) ---");

        int a = 10;
        int b = a;   // b er en selvstændig KOPI af a's værdi
        b = 20;

        Console.WriteLine($"a = {a}");  // Skriver 10 — a er upåvirket af ændringen i b
        Console.WriteLine($"b = {b}");  // Skriver 20
    }

    // --------------------------------------------------------------
    // 2. Referencetype-eksempel: kopiering giver en ny REFERENCE til
    //    samme data — begge variable peger på samme array.
    // --------------------------------------------------------------
    static void DemonstrateReferenceTypes()
    {
        Console.WriteLine("--- Referencetyper (array) ---");

        int[] numbers1 = { 1, 2, 3 };
        int[] numbers2 = numbers1;   // numbers2 peger på SAMME array som numbers1 — ikke en kopi!
        numbers2[0] = 99;

        Console.WriteLine($"tal1[0] = {numbers1[0]}");  // Skriver 99! Overraskelsen for mange nye programmører.
        Console.WriteLine($"tal2[0] = {numbers2[0]}");  // Skriver 99

        // Vil du have en RIGTIG kopi af et array, skal du eksplicit lave en:
        int[] numbers3 = (int[])numbers1.Clone();
        numbers3[0] = 1;
        Console.WriteLine($"tal1[0] efter Clone-ændring = {numbers1[0]}");  // Stadig 99 — tal3 er en selvstændig kopi
    }

    // --------------------------------------------------------------
    // 3. decimal vs. double: hvorfor penge ALTID skal beregnes med decimal.
    // --------------------------------------------------------------
    static void DemonstrateDecimalVsDouble()
    {
        Console.WriteLine("--- decimal vs. double ---");

        double doubleA = 0.1;
        double doubleB = 0.2;
        Console.WriteLine($"double:  0.1 + 0.2 = {doubleA + doubleB}");
        // Output: 0.30000000000000004 — fordi double er en binær repræsentation,
        // der ikke kan udtrykke 0.1 og 0.2 helt præcist.

        decimal decimalA = 0.1m;   // Bemærk "m"-suffikset — markerer en decimal-literal
        decimal decimalB = 0.2m;
        Console.WriteLine($"decimal: 0.1 + 0.2 = {decimalA + decimalB}");
        // Output: 0.3 — præcis, fordi decimal er designet til titalssystemets decimaltal.

        // Konsekvens i praksis: forestil dig 100.000 posteringer i et regnskabssystem,
        // hver med en lille, usynlig afrundingsfejl som ovenfor. Fejlene kan akkumulere
        // til et beløb, der rent faktisk kan ses i regnskabet — og det vil en revisor
        // (med rette) ikke acceptere. Derfor: ALTID decimal til penge.
    }
}
 */