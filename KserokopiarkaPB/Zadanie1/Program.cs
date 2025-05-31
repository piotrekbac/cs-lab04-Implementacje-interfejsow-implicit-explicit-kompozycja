using System;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Witam w zadaniu pierwszym! Piotr Bacior 15 722 \n");

            //Tworzymy teraz nową instancję naszej kserokopiarki i nazywamy ją NaszeKseroPB.
            var NaszeKseroPB = new Copier();

            //Włączamy urządzenie, co spowoduje ustawienie jego stanu na "on".
            NaszeKseroPB.PowerOn();

            //Tworzymy dokument PDF o nazwie "aha.pdf" i przypisujemy go do zmiennej doc1.
            IDocument doc1 = new PDFDocument("aha.pdf");

            //Drukujemy teraz dokument doc1, co spowoduje zwiększenie licznika wydruków i wypisanie informacji o wydruku na konsolę.
            NaszeKseroPB.Print(in doc1);

            //Skanujemy teraz nowy dokument i zapisujemy go w formacie TXT.
            IDocument doc2;
            NaszeKseroPB.Scan(out doc2, IDocument.FormatType.TXT);

            //Wykonujemy operację skanowania i odrazu także drukowania zeskanowanego dokumentu.
            NaszeKseroPB.ScanAndPrint();

            //Wypisujemy teraz na konsolę liczniki urządzenia, które pokazują liczbę uruchomień, wydruków i skanów.
            Console.WriteLine("\n=== Statystyki pracy urządzenia - PB ===");

            //Wypisujemy liczbę uruchomień urządzenia
            Console.WriteLine($"Liczba uruchomień urządzenia: {NaszeKseroPB.Counter}");

            //Wypisujemy liczbę wydrukowanych dokumentów
            Console.WriteLine($"Liczba wydrukowanych dokumnetów: {NaszeKseroPB.PrintCounter}");

            //Wypisujemy liczbę zeskanowanych dokumentów
            Console.WriteLine($"Liczba zeskanowanych dokumnetów: {NaszeKseroPB.ScanCounter}");
        }
    }
}