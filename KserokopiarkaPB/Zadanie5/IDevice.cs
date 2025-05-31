using System;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie5
{
    //Interfejs IDevice definiuje podstawowe operacje oraz stany każdego urządzenia elektronicznego
    public interface IDevice
    {
        //Definicja możliwych stanów urządzenia:
        // - off      = wyłączone
        // - on       = włączone i aktywne
        // - standby  = tryb oszczędzania energii
        public enum State { off, on, standby }

        //Metoda pozwala ustawić nowy stan urządzenia
        void SetState(State state);

        //Zwraca aktualny stan urządzenia
        State GetState();

        //Zwraca liczbę uruchomień urządzenia (PowerOn z off)
        int Counter { get; }

        //Domyślna implementacja metody PowerOn
        //Włącza urządzenie i wypisuje komunikat
        void PowerOn()
        {
            SetState(State.on);
            Console.WriteLine("Device ON");
        }

        //Domyślna implementacja metody PowerOff
        //Wyłącza urządzenie i wypisuje komunikat
        void PowerOff()
        {
            SetState(State.off);
            Console.WriteLine("Device OFF");
        }

        //Domyślna implementacja metody StandbyOn
        //Wprowadza urządzenie w tryb oszczędzania energii
        void StandbyOn()
        {
            SetState(State.standby);
            Console.WriteLine("Device in STANDBY");
        }

        //Domyślna implementacja metody StandbyOff
        //Przywraca urządzenie z trybu standby do stanu on
        void StandbyOff()
        {
            SetState(State.on);
            Console.WriteLine("Device exited STANDBY");
        }
    }

    //Rozszerzenie interfejsu IDevice o funkcję drukowania
    public interface IPrinter : IDevice
    {
        //Właściwość zwraca liczbę wykonanych wydruków
        int PrintCounter { get; }

        //Drukuje dokument, jeśli urządzenie jest aktywne
        void Print(in IDocument document);
    }

    //Rozszerzenie interfejsu IDevice o funkcję skanowania
    public interface IScanner : IDevice
    {
        //Właściwość zwraca liczbę wykonanych skanów
        int ScanCounter { get; }

        //Skanuje dokument, jeśli urządzenie jest aktywne
        void Scan(out IDocument document);
    }
}
