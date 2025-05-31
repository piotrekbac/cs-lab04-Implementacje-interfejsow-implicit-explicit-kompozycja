using System;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków
//Program stworzony samodzielnie do realizacji zadania nr 3 – kserokopiarka jako kompozycja urządzeń

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witam w zadaniu pierwszym! Piotr Bacior 15 722 \n");

            //Tworzymy instancję klasy Copier, która łączy skaner i drukarkę w jednym urządzeniu
            var NaszeKseroPB = new Copier();

            //Włączamy urządzenie - oba moduły powinny zmienić stan na 'on'
            NaszeKseroPB.PowerOn();

            //Tworzymy nowy dokument PDF i przesyłamy go do druku
            IDocument dokumentPDF = new PDFDocument("raport.pdf");
            NaszeKseroPB.Print(in dokumentPDF);

            //Skanujemy dokument - domyślnie w formacie JPG
            NaszeKseroPB.Scan(out IDocument zeskanowanyDokument);

            //Wykonujemy operację skanowania oraz od razu drukowania zeskanowanego dokumentu
            NaszeKseroPB.ScanAndPrint();

            //Wyłączamy urządzenie - oba moduły zmieniają stan na 'off'
            NaszeKseroPB.PowerOff();

            //Wyświetlamy teraz dane statystyczne dotyczące działania naszej kserokopiarki
            Console.WriteLine("\n=== Statystyki działania kserokopiarkiPB ===");
            Console.WriteLine($"Liczba uruchomień: {NaszeKseroPB.Counter}");
            Console.WriteLine($"Liczba wydrukowanych dokumentów: {NaszeKseroPB.PrintCounter}");
            Console.WriteLine($"Liczba zeskanowanych dokumentów: {NaszeKseroPB.ScanCounter}");
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...PozdrawiamPB");
            Console.ReadKey();
        }
    }
}
