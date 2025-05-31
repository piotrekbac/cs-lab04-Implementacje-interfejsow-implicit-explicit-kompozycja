using System;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie4
{
    //Definiujemy interfejs IDevice jako wspólny interfejs dla urządzeń
    //Zawiera stany urządzenia oraz domyślne implementacje metod zarządzających stanem
    public interface IDevice
    {
        //Definiujemy możliwe stany urządzenia:
        //off - urządzenie wyłączone,
        //on - urządzenie włączone,
        //standby - tryb energooszczędny
        public enum State { off, on, standby }

        //Metoda ustawiająca stan urządzenia (musi być public w interfejsie z domyślną implementacją)
        void SetState(State state);

        //Zwraca aktualny stan urządzenia
        State GetState();

        //Zwraca licznik uruchomień urządzenia
        int Counter { get; }

        //Domyślna implementacja metody PowerOn - ustawia stan urządzenia na on
        void PowerOn()
        {
            SetState(State.on);
            Console.WriteLine("Urządzenie jest włączone (ON)");
        }

        //Domyślna implementacja metody PowerOff - ustawia stan urządzenia na off
        void PowerOff()
        {
            SetState(State.off);
            Console.WriteLine("Urządzenie jest wyłączone (OFF)");
        }

        //Domyślna implementacja metody StandbyOn - ustawia stan urządzenia na standby
        void StandbyOn()
        {
            SetState(State.standby);
            Console.WriteLine("Urządzenie jest w stanie STANDBY");
        }

        //Domyślna implementacja metody StandbyOff - przywraca stan on z trybu standby
        void StandbyOff()
        {
            SetState(State.on);
            Console.WriteLine("Wychodzenie ze stanu STANDBY");
        }
    }

    //Definiujemy interfejs IPrinter - rozszerza interfejs IDevice i reprezentuje urządzenie drukujące
    public interface IPrinter : IDevice
    {
        //Zwraca licznik wydruków
        int PrintCounter { get; }

        //Metoda realizująca drukowanie dokumentu
        void Print(in IDocument document);
    }

    //Definiujemy interfejs IScanner - rozszerza interfejs IDevice i reprezentuje urządzenie skanujące
    public interface IScanner : IDevice
    {
        //Zwraca licznik skanów
        int ScanCounter { get; }

        //Metoda realizująca skanowanie dokumentu
        void Scan(out IDocument document);
    }
}
