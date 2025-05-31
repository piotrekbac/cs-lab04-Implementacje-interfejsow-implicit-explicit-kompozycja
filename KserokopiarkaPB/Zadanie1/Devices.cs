using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie1
{
    //Definiujemy interfejs IDevice, który będzie zawierał metody do zarządzania stanem urządzenia oraz licznikiem uruchomień
    public interface IDevice
    {
        //Definiujemy enum State, który będzie reprezentował dwa stany urządzenia: włączone (on) i wyłączone (off)
        enum State { on, off };

        //Definiujemy metodę PowerOn, która będzie włączać urządzenie
        void PowerOn();

        //Definiujemy metodę PowerOff, która będzie wyłączać urządzenie
        void PowerOff();

        //Definiujemy metodę GetState, która będzie zwracać aktualny stan urządzenia
        State GetState();

        //Definiujemy właściwość Counter, która będzie zwracać liczbę uruchomień urządzenia
        int Counter { get; }
    }

    //Teraz przechodzimy do definicji klasy BaseDevice, która implementuje interfejs IDevice
    public abstract class BaseDevice : IDevice
    {
        //Teraz definiujemy pole state, które będzie przechowywać aktualny stan urządzenia
        protected IDevice.State state = IDevice.State.off;

        //Przechodzimy do implementacji metod interfejsu IDevice
        public IDevice.State GetState() => state;

        //Metoda PowerOff ustawia stan urządzenia na wyłączony (off) i wypisuje komunikat na konsolę
        public void PowerOff()
        {
            //Ustawiamy stan urządzenia na wyłączony (off)
            state = IDevice.State.off;

            //Wypisujemy komunikat na konsolę, że urządzenie zostało wyłączone
            Console.WriteLine("... Urządzenie jest wyłączone! (OFF) ");
        }

        //Metoda PowerOn ustawia stan urządzenia na włączony (on), zwiększa licznik uruchomień i wypisuje komunikat na konsolę
        public void PowerOn()
        {
            //Jeżeli urządzenie było wcześniej wyłączone, zwiększamy licznik uruchomie - liczymy tylko pierwsze włączenie z off na on
            if (state == IDevice.State.off)
            {
                //Zwiększamy licznik uruchomień o 1
                Counter++;
            }

            //Ustawiamy stan urządzenia na włączony (on)
            state = IDevice.State.on;

            //Wypisujemy komunikat na konsolę, że urządzenie zostało włączone
            Console.WriteLine("Urządzenie jest włączone... (ON) ");

        }

        //Definiujemy teraz właściwość Counter zwraca liczbę uruchomień urządzenia
        public int Counter { get; private set; } = 0;
    }

    //Teraz definiujemy interfejsy IPrinter i IScanner, które będą rozszerzać interfejs IDevice
    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    //Definiujemy interfejs IScanner, który będzie rozszerzać interfejs IDevice
    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
}