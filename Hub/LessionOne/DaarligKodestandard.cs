/* // DaarligKodestandard.cs
//
// ADVARSEL: Denne fil viser MED VILJE dårlig navngivning, så du kan se
// forskellen på god og dårlig kodestandard. Koden virker rent teknisk,
// men bryder Microsofts kodekonventioner flere steder.
//
// Sammenlign med GodKodestandard.cs og se, hvor mange fejl du selv kan finde,
// før du læser kommentarerne.

using System;

namespace productcalculator // Fejl: namespace bør også skrives med stort (PascalCase)
{
    class program // Fejl: klassenavn skal være PascalCase -> "Program"
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Vareberegner ===");

            // Fejl: ungarsk notation ("i" foran heltal) og ikke-meningsfulde navne
            int iQuantity = readInteger("Indtast antal varer: ");

            // Fejl: ungarsk notation ("d" foran double) og forkortet, uklart navn
            double dPrice = read_DecimalNumber("Indtast pris pr. vare: ");

            // Fejl: uforklarligt variabelnavn, man skal gætte hvad "x" er
            double x = calculateTot(iQuantity, dPrice);

            // Fejl: endnu et uklart navn, og metodenavn i camelCase i stedet for PascalCase
            double y = CalculateDiscount(x);

            Console.WriteLine("Samlet pris før rabat: " + x + " kr.");

            if (y < x)
            {
                Console.WriteLine("Der gives rabat.");
            }

            Console.WriteLine("Samlet pris efter rabat: " + y + " kr.");
        }

        // Fejl: metodenavn skal være PascalCase, ikke camelCase -> "LaesHeltal"
        static int readInteger(string message)
        {
            Console.Write(message);
            // Fejl: ungarsk notation ("str" foran string)
            string strInput = Console.ReadLine();
            return int.Parse(strInput);
        }

        // Fejl: understregning i metodenavn er ikke C#-konvention, og
        // navnet blander stil ("laes_Decimaltal" er hverken camelCase eller PascalCase)
        static double read_DecimalNumber(string message)
        {
            Console.Write(message);
            string strInput = Console.ReadLine();
            return double.Parse(strInput);
        }

        // Fejl: "beregnTot" er en uklar forkortelse - hvad er "Tot"?
        // Fejl: parametre "A" og "P" giver ingen mening uden at læse selve koden
        static double calculateTot(int A, double P)
        {
            return A * P;
        }

        // Fejl: "magisk tal" 500 og 0.15 skrevet direkte i koden i stedet for
        // som navngivne konstanter - svært at se, hvad tallene betyder.
        static double CalculateDiscount(double totalPrice)
        {
            if (totalPrice > 500)
            {
                return totalPrice - (totalPrice * 0.15);
            }

            return totalPrice;
        }
    }
}
 */