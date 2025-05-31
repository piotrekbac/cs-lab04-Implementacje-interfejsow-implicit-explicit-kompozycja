using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie2
{
    //Definiujemy klasę MultifunctionalDevice, która dziedziczy po klasie BaseDevice
    //oraz implementuje interfejsy IPrinter, IScanner i IFax.
    //Oznacza to, że urządzenie posiada funkcjonalność drukarki, skanera oraz faksu.
    public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner, IFax
    {
        //Właściwość przechowująca liczbę wykonanych operacji drukowania
        public int PrintCounter { get; private set; } = 0;

        //Właściwość przechowująca liczbę wykonanych operacji skanowania
        public int ScanCounter { get; private set; } = 0;

        //Właściwość przechowująca liczbę wykonanych operacji faksowania
        public int FaxCounter { get; private set; } = 0;

        //Implementacja metody Print — wykonuje wydruk dokumentu, jeśli urządzenie jest włączone
        public void Print(in IDocument document)
        {
            //Sprawdzamy, czy urządzenie jest włączone i czy dokument nie jest null
            if (GetState() != IDevice.State.on || document == null)
                return;

            //Zwiększamy licznik drukowania
            PrintCounter++;

            //Wyświetlamy informację o wydruku wraz z czasem i nazwą dokumentu
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Print: {document.GetFileName()}");
        }

        //Implementacja metody Scan — wykonuje skan w podanym formacie dokumentu
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;

            //Jeśli urządzenie nie jest włączone, zakończ skanowanie
            if (GetState() != IDevice.State.on)
                return;

            //Zwiększamy licznik skanowania
            ScanCounter++;

            //Generujemy nazwę pliku na podstawie formatu i licznika
            string filename = formatType switch
            {
                IDocument.FormatType.PDF => $"PDFScan{ScanCounter}.pdf",
                IDocument.FormatType.JPG => $"ImageScan{ScanCounter}.jpg",
                IDocument.FormatType.TXT => $"TextScan{ScanCounter}.txt",
                _ => $"UnknownScan{ScanCounter}.bin"
            };

            //Tworzymy odpowiedni typ dokumentu na podstawie wybranego formatu
            document = formatType switch
            {
                IDocument.FormatType.PDF => new PDFDocument(filename),
                IDocument.FormatType.JPG => new ImageDocument(filename),
                IDocument.FormatType.TXT => new TextDocument(filename),
                _ => null
            };

            //Jeśli udało się utworzyć dokument, wyświetlamy komunikat
            if (document != null)
            {
                Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Scan: {document.GetFileName()}");
            }
        }

        //Przeciążona wersja metody Scan bez parametru formatu — domyślnie JPG
        public void Scan(out IDocument document)
        {
            Scan(out document, IDocument.FormatType.JPG);
        }

        //Implementacja metody SendFax — wysyła dokument pod wskazany numer, jeśli urządzenie jest włączone
        public void SendFax(in IDocument document, string recipientNumber)
        {
            //Sprawdzamy, czy urządzenie jest włączone, dokument nie jest null oraz numer nie jest pusty
            if (GetState() != IDevice.State.on || document == null || string.IsNullOrWhiteSpace(recipientNumber))
                return;

            //Zwiększamy licznik wysłanych faksów
            FaxCounter++;

            //Wyświetlamy informację o wysłaniu faksu z nazwą dokumentu i numerem odbiorcy
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Fax: {document.GetFileName()} sent to {recipientNumber}");
        }
    }
}
