using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie4;

//Piotr Bacior 15 722 - WSEI Kraków

namespace UnitTestZad4
{
    using Device = Zadanie4.IDevice;
    using Copier = Zadanie4.Copier;

    [TestClass]
    public class CopierTests
    {

        //Test sprawdza czy PowerOn ustawia stan na ON i zwiększa licznik tylko raz
        [TestMethod]
        public void PowerOn_Should_SetStateOn_And_IncrementCounterOnce()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();
            ((Device)copier).PowerOn();

            Assert.AreEqual(1, copier.Counter);
            Assert.AreEqual(Device.State.on, copier.GetState());
        }

        //Test sprawdza czy PowerOff ustawia stan kopiarki na OFF
        [TestMethod]
        public void PowerOff_Should_SetStateToOff()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();
            ((Device)copier).PowerOff();

            Assert.AreEqual(Device.State.off, copier.GetState());
        }

        //Test sprawdza czy metody StandbyOn i StandbyOff poprawnie zmieniają stany
        [TestMethod]
        public void StandbyOn_And_Off_Should_ChangeDeviceState()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();
            ((Device)copier).StandbyOn();
            Assert.AreEqual(Device.State.standby, copier.GetState());

            ((Device)copier).StandbyOff();
            Assert.AreEqual(Device.State.on, copier.GetState());
        }

        //Test sprawdza czy po 3 wydrukach drukarka przechodzi w tryb STANDBY
        [TestMethod]
        public void Print_Should_IncrementPrintCounter_And_AutoStandbyAfter3()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();

            copier.Print(new PDFDocument("doc1.pdf"));
            copier.Print(new PDFDocument("doc2.pdf"));
            copier.Print(new PDFDocument("doc3.pdf"));

            Assert.AreEqual(3, copier.PrintCounter);
        }

        //Test sprawdza czy drukarka wybudza się z STANDBY przy kolejnym wydruku
        [TestMethod]
        public void Print_Should_WakePrinterFromStandby()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();

            copier.Print(new PDFDocument("doc1.pdf"));
            copier.Print(new PDFDocument("doc2.pdf"));
            copier.Print(new PDFDocument("doc3.pdf"));
            copier.Print(new PDFDocument("doc4.pdf"));

            Assert.AreEqual(4, copier.PrintCounter);
        }

        //Test sprawdza czy skaner po 2 skanach przechodzi automatycznie w STANDBY
        [TestMethod]
        public void Scan_Should_IncrementScanCounter_And_AutoStandbyAfter2()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();

            copier.Scan(out IDocument doc1);
            copier.Scan(out IDocument doc2);

            Assert.AreEqual(2, copier.ScanCounter);
            Assert.IsNotNull(doc1);
            Assert.IsNotNull(doc2);
        }

        //Test sprawdza czy skaner wybudza się ze STANDBY przy kolejnym skanie
        [TestMethod]
        public void Scan_Should_WakeScannerFromStandby()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();

            copier.Scan(out IDocument doc1);
            copier.Scan(out IDocument doc2);
            copier.Scan(out IDocument doc3);

            Assert.AreEqual(3, copier.ScanCounter);
            Assert.IsNotNull(doc3);
        }

        //Test sprawdza czy stan kopiarki jest zależny od stanów skanera i drukarki
        [TestMethod]
        public void CopierState_Should_ReflectModuleStatesCorrectly()
        {
            var copier = new Copier();
            ((Device)copier).PowerOn();
            ((Device)copier).StandbyOn();
            Assert.AreEqual(Device.State.standby, copier.GetState());

            ((Device)copier).PowerOff();
            Assert.AreEqual(Device.State.off, copier.GetState());
        }
    }
}
