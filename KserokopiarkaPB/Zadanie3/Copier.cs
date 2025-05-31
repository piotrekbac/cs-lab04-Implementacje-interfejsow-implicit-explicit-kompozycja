using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie3
{
    //Definiujemy klasę Copier, która implementuje interfejs IDevice.
    //Kserokopiarka jest zbudowana z dwóch niezależnych modułów: drukarki oraz skanera.
    public class Copier : IDevice
    {
        //Prywatne pola przechowujące instancje drukarki i skanera
        private Printer printer = new Printer();
        private Scanner scanner = new Scanner();

        //Aktualny stan kserokopiarki (on/off)
        private IDevice.State state = IDevice.State.off;

        //Licznik włączeń urządzenia (przejść ze stanu off do on)
        private int powerOnCounter = 0;

        //Metoda włączająca kserokopiarkę — włącza również moduł drukarki i skanera
        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                powerOnCounter++;
                printer.PowerOn();
                scanner.PowerOn();
                state = IDevice.State.on;

                //Wyświetlamy informację o włączeniu kserokopiarki
                Console.WriteLine("Copier is ON");
            }
        }

        //Metoda wyłączająca kserokopiarkę — wyłącza również drukarkę i skaner
        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                printer.PowerOff();
                scanner.PowerOff();
                state = IDevice.State.off;

                //Wyświetlamy informację o wyłączeniu kserokopiarki
                Console.WriteLine("Copier is OFF");
            }
        }

        //Zwraca aktualny stan kserokopiarki (on/off)
        public IDevice.State GetState() => state;

        //Zwraca liczbę uruchomień kserokopiarki
        public int Counter => powerOnCounter;

        //Zwraca liczbę wykonanych wydruków przez moduł drukarki
        public int PrintCounter => printer.PrintCounter;

        //Zwraca liczbę wykonanych skanów przez moduł skanera
        public int ScanCounter => scanner.ScanCounter;

        //Metoda wykonująca wydruk dokumentu za pomocą wewnętrznego modułu drukarki
        public void Print(in IDocument document)
        {
            printer.Print(in document);
        }

        //Metoda wykonująca skan w domyślnym formacie JPG
        public void Scan(out IDocument document)
        {
            scanner.Scan(out document, IDocument.FormatType.JPG);
        }

        //Metoda wykonująca skan w określonym formacie (JPG, PDF, TXT)
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            scanner.Scan(out document, formatType);
        }

        //Metoda łącząca operacje skanowania i drukowania
        public void ScanAndPrint()
        {
            Scan(out IDocument doc, IDocument.FormatType.JPG);
            if (doc != null)
                Print(in doc);
        }
    }
}
