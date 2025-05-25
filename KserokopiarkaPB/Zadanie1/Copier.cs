using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Zadanie1.IDevice;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie1
{
    //Definiujemy klasę Copier, która dziedziczy po BaseDevice i implementuje interfejsy IPrinter oraz IScanner.
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        //Definiujemy licznik wydruków, który przechowuje liczbę wydrukowanych dokumentów przez to urządzenie.
        public int PrintCounter { get; private set; } = 0;

        //Defoiniujemy licznik skanów, który przechowuje liczbę zeskanowanych dokumentów przez to urządzenie.
        public int ScanCounter { get; private set; } = 0;

        //Przechodzimy do zdefiniowanai metody Print, która przyjmuje dokument jako parametr.
        public void Print(in IDocument document)
        {
            //Sprawdzamy, czy urządzenie jest włączone. Jeśli nie, to kończymy działanie metody.
            if (GetState() != State.on) return;

            //Zwiększamy licznik wydruków o 1.
            PrintCounter++;

            //Wypisujemy na konsolę informację o wydruku dokumentu wraz z jego nazwą.
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

            //W zależności od formatu dokumentu, tworzymy odpowiedni obiekt dokumentu i przypisujemy go do zmiennej document.
            string filename = formatType switch
            {
                IDocument.FormatType.PDF => $"PDFScan{ScanCounter}.pdf",
                IDocument.FormatType.JPG => $"ImageScan{ScanCounter}.jpg",
                IDocument.FormatType.TXT => $"TextScan{ScanCounter}.txt",
                _ => $"NieznanyScan{ScanCounter}.bin"
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
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Scan: {document.GetFileName()}");
        }

        //Definiujemy metodę ScanAndPrint, która skanuje dokument i drukuje go.
        public void ScanAndPrint()
        {
            //Sprawdzamy czy skanowanie się udało i czy dokument nie jest null, w takim wypadku drukujemy zeskanowany dokument.
            Scan(out IDocument doc, IDocument.FormatType.JPG);
            if (doc != null)
            {
                Print(in doc);
            }
        }
    }
}
