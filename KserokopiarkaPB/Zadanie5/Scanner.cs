using System;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie5
{
    //Klasa Scanner implementuje interfejs IScanner i reprezentuje funkcjonalność skanera
    public class Scanner : IScanner
    {
        //Przechowuje aktualny stan skanera (off, on, standby)
        private IDevice.State state = IDevice.State.off;

        //Licznik wszystkich wykonanych skanów
        private int scanCounter = 0;

        //Licznik liczby uruchomień skanera (włączeń z off do on)
        private int powerOnCounter = 0;

        //Licznik kolejnych skanów od ostatniego wejścia w standby
        private int sinceStandby = 0;

        //Zwraca liczbę wykonanych skanów
        public int ScanCounter => scanCounter;

        //Zwraca liczbę uruchomień skanera
        public int Counter => powerOnCounter;

        //Zwraca aktualny stan skanera
        public IDevice.State GetState() => state;

        //Ustawia nowy stan skanera i aktualizuje licznik uruchomień
        public void SetState(IDevice.State newState)
        {
            //Jeśli urządzenie było wyłączone i zostało włączone, zwiększamy licznik uruchomień
            if (state == IDevice.State.off && newState == IDevice.State.on)
                powerOnCounter++;

            state = newState;
        }

        //Metoda odpowiedzialna za wykonanie skanu dokumentu
        public void Scan(out IDocument document)
        {
            //Na początku ustawiamy dokument jako null
            document = null;

            //Jeśli skaner jest wyłączony, nie wykonujemy nic
            if (state == IDevice.State.off) return;

            //Jeśli skaner jest w trybie standby, wybudzamy go
            if (state == IDevice.State.standby)
            {
                state = IDevice.State.on;
                Console.WriteLine("Scanner wakes from STANDBY");
            }

            //Zwiększamy licznik skanów i licznik od ostatniego standby
            scanCounter++;
            sinceStandby++;

            //Tworzymy nowy dokument JPG jako wynik skanowania
            document = new ImageDocument($"Scan{scanCounter}.jpg");

            //Wypisujemy informację o wykonanym skanie
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} Scan: {document.GetFileName()}");

            //Po każdych 2 skanach przechodzimy automatycznie w tryb standby
            if (sinceStandby >= 2)
            {
                state = IDevice.State.standby;
                sinceStandby = 0;
                Console.WriteLine("Scanner auto-standby");
            }
        }
    }
}
