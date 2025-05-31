using System;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie4
{
    //Definiujemy klasę Copier, która implementuje interfejsy IPrinter oraz IScanner
    //Klasa reprezentuje energooszczędną kserokopiarkę, która składa się z dwóch osobnych modułów: drukarki i skanera
    public class Copier : IPrinter, IScanner
    {
        //Definiujemy stany osobno dla modułu drukarki oraz skanera
        private IDevice.State printerState = IDevice.State.off;
        private IDevice.State scannerState = IDevice.State.off;

        //Definiujemy liczniki uruchomień, wydruków i skanów
        private int powerOnCounter = 0;
        private int printCounter = 0;
        private int scanCounter = 0;

        //Licznik służący do automatycznego przejścia do stanu standby po n drukowania/skanowania
        private int printSinceStandby = 0;
        private int scanSinceStandby = 0;

        //Właściwości dostępowe do liczników
        public int Counter => powerOnCounter;
        public int PrintCounter => printCounter;
        public int ScanCounter => scanCounter;

        //Metoda zwraca stan całego urządzenia na podstawie stanu jego dwóch modułów
        public IDevice.State GetState()
        {
            if (printerState == IDevice.State.off && scannerState == IDevice.State.off)
                return IDevice.State.off;
            if (printerState == IDevice.State.standby && scannerState == IDevice.State.standby)
                return IDevice.State.standby;
            return IDevice.State.on;
        }

        //Metoda wymagana przez interfejs IDevice, pozwala ustawić stan obu modułów
        public void SetState(IDevice.State state)
        {
            printerState = state;
            scannerState = state;
        }

        //Metoda PowerOn - jawna implementacja interfejsu
        void IDevice.PowerOn()
        {
            //Zliczamy tylko pierwsze włączenie (z OFF do ON)
            if (printerState == IDevice.State.off && scannerState == IDevice.State.off)
                powerOnCounter++;

            printerState = IDevice.State.on;
            scannerState = IDevice.State.on;

            Console.WriteLine("Kserokopiarka jest włączona (ON)");
        }

        //Metoda PowerOff - wyłącza oba moduły
        void IDevice.PowerOff()
        {
            printerState = IDevice.State.off;
            scannerState = IDevice.State.off;
            Console.WriteLine("Kserokopiarka jest wyłączona (OFF)");
        }

        //Metoda StandbyOn - przełącza oba moduły w tryb standby
        void IDevice.StandbyOn()
        {
            printerState = IDevice.State.standby;
            scannerState = IDevice.State.standby;
            Console.WriteLine("Kserokopiarka jest w stanie STANDBY");
        }

        //Metoda StandbyOff - przywraca oba moduły do stanu on
        void IDevice.StandbyOff()
        {
            printerState = IDevice.State.on;
            scannerState = IDevice.State.on;
            Console.WriteLine("Kserokopiarka wychodzi ze stanu STANDBY");
        }

        //Metoda Print - obsługuje drukowanie z uwzględnieniem logiki stanu drukarki i skanera
        public void Print(in IDocument document)
        {
            if (printerState == IDevice.State.off)
                return;

            if (scannerState == IDevice.State.on)
            {
                scannerState = IDevice.State.standby;
                Console.WriteLine("Skaner wchodzi w tryb STANDBY (podczas drukowania)");
            }

            if (printerState == IDevice.State.standby)
            {
                printerState = IDevice.State.on;
                Console.WriteLine("Drukarka wybudza się ze stanu STANDBY");
            }

            printCounter++;
            printSinceStandby++;

            Console.WriteLine($"{DateTime.Now:HH:mm:ss} Print: {document.GetFileName()}");

            if (printSinceStandby >= 3)
            {
                printerState = IDevice.State.standby;
                printSinceStandby = 0;
                Console.WriteLine("Drukarka wchodzi w stan STANDBY (automatycznie po 3 wydrukach)");
            }
        }

        //Metoda Scan - obsługuje skanowanie z uwzględnieniem logiki stanu drukarki i skanera
        public void Scan(out IDocument document)
        {
            document = null;
            if (scannerState == IDevice.State.off)
                return;

            if (printerState == IDevice.State.on)
            {
                printerState = IDevice.State.standby;
                Console.WriteLine("Drukarka wchodzi w stan STANDBY (podczas procesu skanowania)");
            }

            if (scannerState == IDevice.State.standby)
            {
                scannerState = IDevice.State.on;
                Console.WriteLine("Skaner wybudza się ze stanu STANDBY.");
            }

            scanCounter++;
            scanSinceStandby++;

            document = new ImageDocument($"ImageScan{scanCounter}.jpg");
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} Scan: {document.GetFileName()}");

            if (scanSinceStandby >= 2)
            {
                scannerState = IDevice.State.standby;
                scanSinceStandby = 0;
                Console.WriteLine("Skaner wchodzi w stan STANDBY (automatycznie po dwóch skanach)");
            }
        }
    }
}
