using System;
using static Zadanie1.IDevice;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie1
{
    //Definiujemy klasę Copier, która dziedziczy po BaseDevice i implementuje interfejsy IPrinter oraz IScanner.
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        //Definiujemy licznik wydruków, który przechowuje liczbę wydrukowanych dokumentów przez to urządzenie.
        public int PrintCounter { get; private set; } = 0;

        //Definiujemy licznik skanów, który przechowuje liczbę zeskanowanych dokumentów przez to urządzenie.
        public int ScanCounter { get; private set; } = 0;

        //Przechodzimy do zdefiniowania metody Print, która przyjmuje dokument jako parametr.
        public void Print(in IDocument document)
        {
            //Jeśli urządzenie jest wyłączone, nie wykonujemy żadnej operacji.
            if (GetState() != State.on)
                return;

            //Jeśli dokument jest nullem, zgłaszamy wyjątek.
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            //Zwiększamy licznik wydruków.
            PrintCounter++;

            //Wypisujemy informację o wydruku dokumentu. WYMAGANE słowo "Print"
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Print: {document.GetFileName()}");
        }

        //Przechodzimy do zdefiniowania metody Scan, która przyjmuje dwa parametry: dokument oraz format dokumentu.
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            //Inicjalizujemy dokument jako null. Jeśli urządzenie nie jest włączone, to kończymy działanie metody.
            document = null;
            if (GetState() != State.on) return;

            //Zwiększamy licznik skanów o 1.
            ScanCounter++;

            //Tworzymy nazwę pliku w zależności od formatu.
            string filename = formatType switch
            {
                IDocument.FormatType.PDF => $"PDFScan{ScanCounter}.pdf",
                IDocument.FormatType.JPG => $"ImageScan{ScanCounter}.jpg",
                IDocument.FormatType.TXT => $"TextScan{ScanCounter}.txt",
                _ => $"UnknownScan{ScanCounter}.bin"
            };

            //W zależności od formatu dokumentu, tworzymy odpowiedni obiekt dokumentu i przypisujemy go do zmiennej document.
            document = formatType switch
            {
                IDocument.FormatType.PDF => new PDFDocument(filename),
                IDocument.FormatType.JPG => new ImageDocument(filename),
                IDocument.FormatType.TXT => new TextDocument(filename),
                _ => null
            };

            //Jeśli dokument został poprawnie utworzony, wypisujemy na konsolę informację o skanowaniu dokumentu wraz z jego nazwą.
            if (document != null)
            {
                Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Scan: {document.GetFileName()}");
            }
        }

        //Przeciążenie metody Scan wymagane przez testy - domyślnie skanujemy do formatu JPG.
        public void Scan(out IDocument document)
        {
            Scan(out document, IDocument.FormatType.JPG);
        }

        //Definiujemy metodę ScanAndPrint, która skanuje dokument i drukuje go.
        public void ScanAndPrint()
        {
            //Sprawdzamy, czy urządzenie jest włączone. Jeśli nie, kończymy działanie metody.
            if (GetState() != State.on) return;

            //Skanujemy dokument do formatu JPG.
            Scan(out IDocument doc, IDocument.FormatType.JPG);

            //Jeśli skanowanie się udało, drukujemy dokument.
            if (doc != null)
            {
                Print(in doc);
            }
        }
    }
}
