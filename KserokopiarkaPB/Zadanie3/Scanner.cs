using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie3
{
    //Definiujemy klasę Scanner, która dziedziczy po klasie bazowej BaseDevice oraz implementuje interfejs IScanner
    //Reprezentuje samodzielny moduł skanujący, mogący działać jako część większego urządzenia (np. kserokopiarki)
    public class Scanner : BaseDevice, IScanner
    {
        //Prywatny licznik zeskanowanych dokumentów przez moduł skanera
        public int ScanCounter { get; private set; } = 0;

        //Metoda odpowiedzialna za skanowanie dokumentu w określonym formacie
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            //Inicjalizujemy zmienną wyjściową jako null
            document = null;

            //Jeśli skaner nie jest włączony, nie wykonujemy żadnych działań
            if (GetState() != IDevice.State.on)
                return;

            //Zwiększamy licznik skanów o jeden
            ScanCounter++;

            //Tworzymy nazwę pliku na podstawie formatu dokumentu i bieżącego numeru skanu
            string filename = formatType switch
            {
                IDocument.FormatType.PDF => $"PDFScan{ScanCounter}.pdf",
                IDocument.FormatType.JPG => $"ImageScan{ScanCounter}.jpg",
                IDocument.FormatType.TXT => $"TextScan{ScanCounter}.txt",
                _ => $"UnknownScan{ScanCounter}.bin"
            };

            //Tworzymy odpowiedni obiekt dokumentu na podstawie typu formatu
            document = formatType switch
            {
                IDocument.FormatType.PDF => new PDFDocument(filename),
                IDocument.FormatType.JPG => new ImageDocument(filename),
                IDocument.FormatType.TXT => new TextDocument(filename),
                _ => null
            };

            //Jeśli dokument został poprawnie utworzony, wypisujemy informację o zeskanowanym pliku
            if (document != null)
            {
                Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Scan: {document.GetFileName()}");
            }
        }
    }
}
