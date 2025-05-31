using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie1;
using Zadanie2;

//Piotr Bacior 15 722 - WSEI Kraków

namespace UnitTestZad2
{
    //Tworzymy klasę ConsoleRedirectionToStringWriter, która będzie służyć do przekierowania konsoli do StringWriter
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

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);     
            stringWriter.Flush();               
            stringWriter.Dispose();             
        }
    }

    //Tworzymy klasę testową dla MultifunctionalDevice, która będzie zawierać testy jednostkowe
    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        //Test sprawdza czy po włączeniu urządzenia wykonanie metody Print wypisuje słowo "Print"
        [TestMethod]
        public void MFD_Print_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            string output;
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new PDFDocument("doc.pdf");
                mfd.Print(in doc);
                output = consoleOutput.GetOutput(); 
            }

            Assert.IsTrue(output.Contains("Print"));
        }

        //Test sprawdza czy po wyłączeniu urządzenia nie wypisuje się żaden komunikat przy próbie drukowania
        [TestMethod]
        public void MFD_Print_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new PDFDocument("doc.pdf");
                mfd.Print(in doc);
                string output = consoleOutput.GetOutput();
                Assert.IsFalse(output.Contains("Print"));
            }
        }

        //Test sprawdza czy po wyłączeniu urządzenia nie wypisuje się żaden komunikat przy próbie skanowania
        [TestMethod]
        public void MFD_Scan_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc;
                mfd.Scan(out doc);
                string output = consoleOutput.GetOutput();
                Assert.IsFalse(output.Contains("Scan"));
            }
        }


        //Test sprawdza czy po wyłączeniu urządzenia nie wypisuje się żaden komunikat przy próbie wysłania faksu
        [TestMethod]
        public void MFD_SendFax_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new PDFDocument("fax.pdf");
                mfd.SendFax(in doc, "123456789");
                string output = consoleOutput.GetOutput();
                Assert.IsFalse(output.Contains("Fax"));
            }
        }

        //Test sprawdza czy po wysłaniu faksu wypisywane są słowo "Fax" oraz numer odbiorcy
        [TestMethod]
        public void MFD_SendFax_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            string output;
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc = new PDFDocument("fax.pdf");
                mfd.SendFax(in doc, "123456789");
                output = consoleOutput.GetOutput(); 
            }

            Assert.IsTrue(output.Contains("Fax"));
            Assert.IsTrue(output.Contains("123456789"));
        }

        //Test sprawdza czy poprawnie zliczane są liczniki:
        // - liczba uruchomień (Counter)
        // - liczba wydruków (PrintCounter)
        // - liczba skanów (ScanCounter)
        // - liczba wysłanych faksów (FaxCounter)
        [TestMethod]
        public void MFD_Counters_WorkProperly()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            string output;
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument docPrint = new TextDocument("x.txt");
                mfd.Print(in docPrint);

                IDocument doc1;
                mfd.Scan(out doc1);
                mfd.SendFax(in doc1, "123");

                IDocument doc2;
                mfd.Scan(out doc2);
                mfd.SendFax(in doc2, "456");

                output = consoleOutput.GetOutput(); 
            }

            Assert.AreEqual(1, mfd.PrintCounter);
            Assert.AreEqual(2, mfd.ScanCounter);
            Assert.AreEqual(2, mfd.FaxCounter);
            Assert.AreEqual(1, mfd.Counter);
            Assert.IsTrue(output.Contains("Print"));
            Assert.IsTrue(output.Contains("Scan"));
            Assert.IsTrue(output.Contains("Fax"));
        }
    }
}
