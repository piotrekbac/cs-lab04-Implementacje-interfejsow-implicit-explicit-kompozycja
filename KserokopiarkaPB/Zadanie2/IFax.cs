using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie1;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie2
{
    //Definiujemy interfejs IFax, który rozszerza interfejs IDevice,
    //co oznacza, że każde urządzenie implementujące IFax musi obsługiwać
    //funkcjonalność podstawową (włączenie, wyłączenie, licznik) oraz możliwość wysyłania faksu
    public interface IFax : IDevice
    {
        //Definiujemy metodę SendFax, która odpowiada za wysyłanie dokumentu jako faks.
        //Metoda przyjmuje dwa parametry:
        // - document: dokument do wysłania (typ IDocument, przekazywany jako parametr wejściowy przez referencję),
        // - recipientNumber: numer odbiorcy faksu w postaci łańcucha znaków.
        //Metoda powinna działać tylko wtedy, gdy urządzenie jest włączone (State == on)
        void SendFax(in IDocument document, string recipientNumber);
    }
}
