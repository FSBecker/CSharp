/* // GodKodestandard.cs
//
// Eksempel på et lille konsolprogram, der følger Microsofts officielle
// kodekonventioner for C#:
//   - PascalCase for klasser, metoder og properties
//   - camelCase for lokale variable og parametre
//   - ingen ungarsk notation (ingen "str", "i", "b" foran variabelnavne)
//   - meningsfulde navne, der forklarer sig selv
//
// Sammenlign denne fil med DaarligKodestandard.cs, som viser det modsatte.

using System;

namespace ProductCalculator
{
    // Klassenavne skrives i PascalCase.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Vareberegner ===");

            // Lokale variable skrives i camelCase.
            int quantity = ReadInteger("Indtast antal varer: ");
            double unitPrice = ReadDecimalNumber("Indtast pris pr. vare: ");

            double totalPrice = CalculateTotalPrice(quantity, unitPrice);
            double priceAfterDiscount = CalculatePriceWithDiscount(totalPrice);

            Console.WriteLine($"Samlet pris før rabat: {totalPrice} kr.");

            if (priceAfterDiscount < totalPrice)
            {
                Console.WriteLine("Der gives 15% rabat.");
            }

            Console.WriteLine($"Samlet pris efter rabat: {priceAfterDiscount} kr.");
        }

        // Metodenavne skrives i PascalCase og fortæller tydeligt, hvad metoden gør.
        static int ReadInteger(string message)
        {
            Console.Write(message);
            string inputText = Console.ReadLine();
            return int.Parse(inputText);
        }

        static double ReadDecimalNumber(string message)
        {
            Console.Write(message);
            string inputText = Console.ReadLine();
            return double.Parse(inputText);
        }

        // Parametre skrives i camelCase, ligesom lokale variable.
        static double CalculateTotalPrice(int quantity, double unitPrice)
        {
            return quantity * unitPrice;
        }

        // "Magiske tal" er samlet i navngivne konstanter i stedet for at
        // skrive 500 og 0.15 direkte inde i udregningen.
        const double DiscountThreshold = 500;
        const double DiscountRate = 0.15;

        static double CalculatePriceWithDiscount(double totalPrice)
        {
            if (totalPrice > DiscountThreshold)
            {
                return totalPrice - (totalPrice * DiscountRate);
            }

            return totalPrice;
        }
    }
}
 */