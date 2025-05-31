using System;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie5
{
    //Klasa Printer implementuje interfejs IPrinter i reprezentuje funkcjonalność drukarki
    public class Printer : IPrinter
    {
        //Przechowuje aktualny stan drukarki (off, on, standby)
        private IDevice.State state = IDevice.State.off;

        //Licznik wszystkich wykonanych wydruków
        private int printCounter = 0;

        //Licznik liczby uruchomień urządzenia (włączeń z off do on)
        private int powerOnCounter = 0;

        //Licznik kolejnych wydruków od ostatniego wejścia w standby
        private int sinceStandby = 0;

        //Zwraca liczbę wydrukowanych dokumentów
        public int PrintCounter => printCounter;

        //Zwraca liczbę uruchomień drukarki
        public int Counter => powerOnCounter;

        //Zwraca aktualny stan urządzenia
        public IDevice.State GetState() => state;

        //Ustawia nowy stan drukarki i aktualizuje licznik uruchomień
        public void SetState(IDevice.State newState)
        {
            //Jeśli urządzenie było wyłączone i zostało włączone, zwiększamy licznik uruchomień
            if (state == IDevice.State.off && newState == IDevice.State.on)
                powerOnCounter++;

            state = newState;
        }

        //Metoda odpowiedzialna za drukowanie dokumentu
        public void Print(in IDocument document)
        {
            //Jeśli drukarka jest wyłączona, nie wykonujemy nic
            if (state == IDevice.State.off) return;

            //Jeśli drukarka jest w trybie standby, wybudzamy ją
            if (state == IDevice.State.standby)
            {
                state = IDevice.State.on;
                Console.WriteLine("Drukarka wybudza się ze stanu STANDBY");
            }

            //Wypisujemy informację o drukowanym dokumencie
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} Print: {document.GetFileName()}");

            //Zwiększamy licznik wydruków i licznik od ostatniego standby
            printCounter++;
            sinceStandby++;

            //Po każdych 3 wydrukach przechodzimy automatycznie w tryb standby
            if (sinceStandby >= 3)
            {
                state = IDevice.State.standby;
                sinceStandby = 0;
                Console.WriteLine("Drukarka w stanie auto-standby");
            }
        }
    }
}
