using System;

//Piotr Bacior 15 722 - WSEI Kraków
//Zadanie 5: Urządzenie z kompozycją niezależnych modułów (drukarka + skaner)

namespace Zadanie5
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Witam w zadaniu piątym! Piotr Bacior 15 722 \n");

            //Tworzymy instancję kserokopiarki złożonej z osobnych modułów
            var NaszeKseroPB = new Copier();

            //Włączamy całe urządzenie (obie części: drukarka i skaner)
            ((IDevice)NaszeKseroPB).PowerOn();

            //Drukujemy 4 dokumenty PDF (drukarka przejdzie w STANDBY po 3)
            NaszeKseroPB.Print(new PDFDocument("file1.pdf"));
            NaszeKseroPB.Print(new PDFDocument("file2.pdf"));
            NaszeKseroPB.Print(new PDFDocument("file3.pdf"));
            NaszeKseroPB.Print(new PDFDocument("file4.pdf"));

            //Skanujemy 3 dokumenty JPG (skaner przejdzie w STANDBY po 2)
            NaszeKseroPB.Scan(out IDocument zeskanowany1);
            NaszeKseroPB.Scan(out IDocument zeskanowany2);
            NaszeKseroPB.Scan(out IDocument zeskanowany3);

            //Wprowadzamy całe urządzenie ręcznie w tryb STANDBY
            ((IDevice)NaszeKseroPB).StandbyOn();

            //Wyłączamy tryb STANDBY (uruchamiamy urządzenie)
            ((IDevice)NaszeKseroPB).StandbyOff();

            //Wyłączamy całkowicie urządzenie
            ((IDevice)NaszeKseroPB).PowerOff();

            //Wyświetlamy podsumowanie działania urządzenia
            Console.WriteLine("\n=== Statystyki działnia urządzenia - PB ===");
            Console.WriteLine($"Liczba wydruków: {NaszeKseroPB.PrintCounter}");
            Console.WriteLine($"Liczba skanów: {NaszeKseroPB.ScanCounter}");
            Console.WriteLine($"Liczba uruchomień (PowerOn): {NaszeKseroPB.Counter}");
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...PozdrawiamPB");
            Console.ReadKey();
        }
    }
}
