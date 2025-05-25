using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie1;

namespace Zadanie1UnitTests
{
    [TestClass]
    public class UnitTestCopier
    {
        [TestMethod]
        public void Copier_PowerOn_IncrementsCounterOnce()
        {
            var copier = new Copier();
            copier.PowerOn();
            Assert.AreEqual(1, copier.Counter);
        }

        [TestMethod]
        public void Copier_ScanAndPrint_CountersUpdateCorrectly()
        {
            var copier = new Copier();
            copier.PowerOn();

            copier.ScanAndPrint();

            Assert.AreEqual(1, copier.ScanCounter);
            Assert.AreEqual(1, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_OffState_IgnoresScanPrint()
        {
            var copier = new Copier();

            IDocument doc;
            copier.Scan(out doc, IDocument.FormatType.PDF);
            copier.Print(new PDFDocument("test.pdf"));

            Assert.AreEqual(0, copier.ScanCounter);
            Assert.AreEqual(0, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_PrintAndScan_WritesCorrectOutput()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var copier = new Copier();
            copier.PowerOn();
            IDocument doc = new PDFDocument("doc1.pdf");
            copier.Print(in doc);

            copier.Scan(out IDocument scannedDoc, IDocument.FormatType.JPG);

            var result = output.ToString();
            Assert.IsTrue(result.Contains("Print: doc1.pdf"));
            Assert.IsTrue(result.Contains("Scan: ImageScan1.jpg"));
        }
    }
}
