using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie1;
using Zadanie3;

//Piotr Bacior 15 722 - WSEI Kraków

//Alias dla unikania konfliktu z klasą Copier z Zadanie1 pozwala jednoznacznie wskazać, że testujemy Copier z Zadanie3.
using Zadanie3Copier = Zadanie3.Copier;

namespace UnitTestCopierZad3
{
    //Klasa pomocnicza do przekierowania wypisywania konsoli (Console.WriteLine) do bufora stringów umożliwia testowanie, czy dane wyjście zostało wypisane.
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter); 
        }

        public string GetOutput() => stringWriter.ToString();

        public void Dispose()
        {
            Console.SetOut(originalOutput); 
            stringWriter.Dispose();
        }
    }

    [TestClass]
    public class UnitTestCopierZad3
    {
        [TestMethod]
        public void Copier_GetState_InitialStateOff()
        {
            //Test sprawdza czy nowo utworzona instancja kopiarki ma początkowy stan OFF
            var copier = new Zadanie3Copier();
            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_PowerOn_ChangesStateAndCounts()
        {
            //Test sprawdza czy PowerOn poprawnie zmienia stan urządzenia na ON oraz czy licznik uruchomień zwiększa się tylko raz przy kolejnych wywołaniach
            var copier = new Zadanie3Copier();
            copier.PowerOn();
            copier.PowerOn(); 

            Assert.AreEqual(IDevice.State.on, copier.GetState());
            Assert.AreEqual(1, copier.Counter); 
        }

        [TestMethod]
        public void Copier_PowerOff_ChangesState()
        {
            //Test sprawdza czy PowerOff zmienia stan kopiarki na OFF
            var copier = new Zadanie3Copier();
            copier.PowerOn();
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_Print_DeviceOn_PrintsSuccessfully()
        {
            //Test sprawdza czy po włączeniu urządzenia możliwy jest wydruk oraz czy na konsoli pojawił się napis zawierający słowo "Print"
            var copier = new Zadanie3Copier();
            copier.PowerOn();

            string output;
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new PDFDocument("test.pdf");
                copier.Print(in doc);
                output = consoleOutput.GetOutput();
            }

            Assert.IsTrue(output.Contains("Print"));
            Assert.AreEqual(1, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_Scan_DeviceOn_ScansSuccessfully()
        {
            //Test sprawdza czy skanowanie działa poprawnie po włączeniu urządzenia oraz czy został wypisany komunikat zawierający "Scan"
            var copier = new Zadanie3Copier();
            copier.PowerOn();

            IDocument doc;
            string output;

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.Scan(out doc);
                output = consoleOutput.GetOutput();
            }

            Assert.IsNotNull(doc); 
            Assert.IsTrue(output.Contains("Scan"));
            Assert.AreEqual(1, copier.ScanCounter);
        }

        [TestMethod]
        public void Copier_ScanAndPrint_CombinesBothActions()
        {
            // Test sprawdza czy metoda ScanAndPrint działa poprawnie, wykonuje najpierw skan, a następnie drukuje zeskanowany dokument
            var copier = new Zadanie3Copier();
            copier.PowerOn();

            string output;
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                output = consoleOutput.GetOutput();
            }

            Assert.IsTrue(output.Contains("Scan"));
            Assert.IsTrue(output.Contains("Print"));
            Assert.AreEqual(1, copier.ScanCounter);
            Assert.AreEqual(1, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_LazyPowerOnCounter_CountsOnlyDistinctOns()
        {
            //Test sprawdza czy licznik PowerOn zwiększa się tylko przy rzeczywistych zmianach stanu
            var copier = new Zadanie3Copier();
            copier.PowerOn();     //pierwsze włączenie - powinno zwiększyć licznik
            copier.PowerOn();     //już włączone - brak zmiany licznika
            copier.PowerOff();    //wyłączenie
            copier.PowerOn();     //kolejne realne włączenie - powinno zwiększyć licznik

            Assert.AreEqual(2, copier.Counter);
        }
    }
}
