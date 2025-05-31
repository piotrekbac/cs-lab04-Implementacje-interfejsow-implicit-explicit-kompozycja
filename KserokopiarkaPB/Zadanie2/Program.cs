using System;
using Zadanie2;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków
//Program stworzony samodzielnie do realizacji zadania nr 2 - urządzenie wielofunkcyjne

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witam w zadaniu drugim! Piotr Bacior 15 722 \n");

            //Tworzymy teraz instancję klasy MultifunctionalDevice i przypisujemy ją do zmiennej o naszej nazwie.
            var NaszeMFD_PB = new MultifunctionalDevice();

            //Włączamy urządzenie - zmienia stan na 'on'
            NaszeMFD_PB.PowerOn();

            //Tworzymy dokument PDF do testowego wydruku
            IDocument dokumentPDF = new PDFDocument("faktura.pdf");

            //Wysyłamy go do druku
            NaszeMFD_PB.Print(in dokumentPDF);

            //Skanujemy dokument w domyślnym formacie (JPG)
            IDocument zeskanowanyDokument;
            NaszeMFD_PB.Scan(out zeskanowanyDokument);

            //Wysyłamy zeskanowany dokument jako faks pod konkretny numer
            NaszeMFD_PB.SendFax(in zeskanowanyDokument, "987654321");

            //Skanujemy dokument jako plik tekstowy
            IDocument dokumentTXT;
            NaszeMFD_PB.Scan(out dokumentTXT, IDocument.FormatType.TXT);

            //Wysyłamy zeskanowany dokument tekstowy ponownie jako faks
            NaszeMFD_PB.SendFax(in dokumentTXT, "555123456");

            //Wyłączamy nasze urządzenie - stan przechodzi na 'off'
            NaszeMFD_PB.PowerOff();

            //Poniższe operacje nie powinny się wykonać (urządzenie jest wyłączone)
            NaszeMFD_PB.Print(in dokumentPDF);
            NaszeMFD_PB.Scan(out zeskanowanyDokument);
            NaszeMFD_PB.SendFax(in dokumentPDF, "000000000");

            //Wypisujemy statystyki działania naszego urządzenia
            Console.WriteLine("\n=== Statystyki pracy urządzenia - PB ===");
            Console.WriteLine($"Liczba uruchomień urządzenia: {NaszeMFD_PB.Counter}");
            Console.WriteLine($"Liczba wydrukowanych dokumentów: {NaszeMFD_PB.PrintCounter}");
            Console.WriteLine($"Liczba zeskanowanych dokumentów: {NaszeMFD_PB.ScanCounter}");
            Console.WriteLine($"Liczba wysłanych faksów: {NaszeMFD_PB.FaxCounter}");
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...PozdrawiamPB");
            Console.ReadKey();
        }
    }
}
