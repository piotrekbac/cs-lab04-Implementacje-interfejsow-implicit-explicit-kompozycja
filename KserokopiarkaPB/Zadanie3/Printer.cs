using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie3
{
    //Definiujemy klasę Printer, która dziedziczy po klasie bazowej BaseDevice oraz implementuje interfejs IPrinter
    //Reprezentuje samodzielny moduł drukarki, który może być częścią większego systemu (np. kserokopiarki)
    public class Printer : BaseDevice, IPrinter
    {
        //Prywatny licznik wydrukowanych dokumentów przez moduł drukarki
        public int PrintCounter { get; private set; } = 0;

        //Metoda odpowiedzialna za drukowanie dokumentu
        public void Print(in IDocument document)
        {
            //Jeśli urządzenie nie jest włączone lub dokument jest nullem, nie wykonujemy żadnej operacji
            if (GetState() != IDevice.State.on || document == null)
                return;

            //Zwiększamy licznik wydruków o jeden
            PrintCounter++;

            //Wypisujemy na konsolę informację o wykonaniu wydruku wraz z nazwą dokumentu i aktualną datą
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} Print: {document.GetFileName()}");
        }
    }
}
