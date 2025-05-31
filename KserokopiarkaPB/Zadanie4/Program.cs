using System;
using Zadanie4;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków
//Program demonstracyjny do zadania 4 – obsługa trybu STANDBY oraz automatyzacji stanu modułów

namespace Zadanie4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witam w zadaniu pierwszym! Piotr Bacior 15 722 \n");

            //Tworzymy instancję naszej zaawansowanej kserokopiarki z obsługą STANDBY
            Copier NaszeKseroPB = new Copier();

            //Włączamy całe urządzenie (drukarka + skaner)
            ((IDevice)NaszeKseroPB).PowerOn();

            //Tworzymy dwa dokumenty PDF do druku
            IDocument doc1 = new PDFDocument("test1.pdf");
            IDocument doc2 = new PDFDocument("test2.pdf");

            //Drukujemy 3 dokumenty - po trzecim drukarka wejdzie automatycznie w STANDBY
            NaszeKseroPB.Print(in doc1);
            NaszeKseroPB.Print(in doc2);
            NaszeKseroPB.Print(in doc1); // STANDBY po 3 wydrukach

            //Drukujemy kolejny dokument - drukarka powinna wybudzić się z STANDBY
            NaszeKseroPB.Print(in doc2);

            //Skanujemy 3 dokumenty - po 2 skanowaniach skaner przejdzie w STANDBY
            NaszeKseroPB.Scan(out IDocument zeskanowany1);
            NaszeKseroPB.Scan(out IDocument zeskanowany2); // STANDBY po 2 skanach
            NaszeKseroPB.Scan(out IDocument zeskanowany3); // wybudzenie z STANDBY

            //Ręcznie ustawiamy STANDBY dla całego urządzenia (oba moduły)
            ((IDevice)NaszeKseroPB).StandbyOn();

            //Ręczne wyjście ze STANDBY
            ((IDevice)NaszeKseroPB).StandbyOff();

            //Wyłączamy całe urządzenie
            ((IDevice)NaszeKseroPB).PowerOff();

            //Wyświetlamy podsumowanie statystyczne
            Console.WriteLine("\n=== Statystyki działania urządzeniaPB ===");
            Console.WriteLine($"Liczba uruchomień (PowerOn): {NaszeKseroPB.Counter}");
            Console.WriteLine($"Liczba wydruków: {NaszeKseroPB.PrintCounter}");
            Console.WriteLine($"Liczba skanów: {NaszeKseroPB.ScanCounter}");
            Console.WriteLine("\nProgram zakończył działanie. Naciśnij dowolny klawisz...PozdrawiamPV");
            Console.ReadKey();
        }
    }
}
