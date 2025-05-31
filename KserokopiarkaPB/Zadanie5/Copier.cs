using System;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie5
{
    //Klasa Copier implementuje funkcjonalność kopiarki jako połączenie drukarki i skanera
    //Zaimplementowane interfejsy: IPrinter oraz IScanner
    public class Copier : IPrinter, IScanner
    {
        //Składniki kopiarki: moduł drukujący i moduł skanujący
        private Printer printer = new Printer();
        private Scanner scanner = new Scanner();

        //Właściwość zwracająca liczbę wykonanych wydruków
        public int PrintCounter => printer.PrintCounter;

        //Właściwość zwracająca liczbę wykonanych skanów
        public int ScanCounter => scanner.ScanCounter;

        //Właściwość zwracająca liczbę uruchomień kopiarki (suma z obu modułów)
        public int Counter => printer.Counter + scanner.Counter;

        //Metoda zwraca aktualny stan kopiarki na podstawie stanów jej modułów
        public IDevice.State GetState()
        {
            if (printer.GetState() == IDevice.State.off && scanner.GetState() == IDevice.State.off)
                return IDevice.State.off;

            if (printer.GetState() == IDevice.State.standby && scanner.GetState() == IDevice.State.standby)
                return IDevice.State.standby;

            return IDevice.State.on;
        }

        //Ustawia stan dla obu modułów kopiarki
        public void SetState(IDevice.State state)
        {
            printer.SetState(state);
            scanner.SetState(state);
        }

        //Metoda drukująca dokument; jeśli skaner jest aktywny – przechodzi w tryb standby
        public void Print(in IDocument document)
        {
            if (scanner.GetState() == IDevice.State.on)
            {
                scanner.SetState(IDevice.State.standby);
                Console.WriteLine("Scanner goes to STANDBY (printing)");
            }

            printer.Print(in document);
        }

        //Metoda skanująca dokument; jeśli drukarka jest aktywna – przechodzi w tryb standby
        public void Scan(out IDocument document)
        {
            if (printer.GetState() == IDevice.State.on)
            {
                printer.SetState(IDevice.State.standby);
                Console.WriteLine("Printer goes to STANDBY (scanning)");
            }

            scanner.Scan(out document);
        }
    }
}
