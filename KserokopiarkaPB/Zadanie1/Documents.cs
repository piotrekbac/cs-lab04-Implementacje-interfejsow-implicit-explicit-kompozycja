using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie1
{
    //Definiujemy interfejs IDevice, który będzie zawierał metody do zarządzania stanem urządzenia oraz licznikiem uruchomień.
    public interface IDocument
    {
        //Definiujemy enum FormatType, który będzie zawierał trzy typy formatów dokumentów: TXT, PDF i JPG
        enum FormatType { TXT, PDF, JPG }

        /// <summary>
        /// Zwraca typ formatu dokumentu
        /// </summary>
        FormatType GetFormatType();

        /// <summary>
        /// Zwraca nazwę pliku dokumentu - nie może być `null` ani pusty `string`
        /// </summary>
        string GetFileName();
    }

    //Definiujemy teraz abstrakcyjną klasę AbstractDocument, która implementuje interfejs IDocument
    public abstract class AbstractDocument : IDocument
    {
        //Definiujemy pole fileName, które będzie przechowywać nazwę pliku dokumentu
        private string fileName;

        //Konstruktor klasy AbstractDocument, który przyjmuje nazwę pliku jako parametr
        public AbstractDocument(string fileName) => this.fileName = fileName;

        //Implementujemy metodę GetFileName, która zwraca nazwę pliku dokumentu
        public string GetFileName() => fileName;

        //Implementujemy metodę ChangeFileName, która zmienia nazwę pliku dokumentu
        public void ChangeFileName(string newFileName) => fileName = newFileName;

        //Definiujemy metodę GetFormatType, która będzie abstrakcyjna i musi być zaimplementowana w klasach dziedziczących
        public abstract IDocument.FormatType GetFormatType();
    }

    //Teraz definiujemy klasę PDFDocument, która dziedziczy po klasie AbstractDocument i implementuje metodę GetFormatType, reprezentującą format PDF
    public class PDFDocument : AbstractDocument
    {
        //Konstruktor klasy PDFDocument, który przyjmuje nazwę pliku jako parametr i przekazuje ją do konstruktora klasy bazowej AbstractDocument
        public PDFDocument(string filename) : base(filename) { }

        //Implementacja metody GetFormatType, która zwraca typ formatu dokumentu jako PDF
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.PDF;
    }

    //Teraz definiujemy klasy ImageDocument i TextDocument, które również dziedziczą po klasie AbstractDocument i implementują metodę GetFormatType dla formatów JPG i TXT
    public class ImageDocument : AbstractDocument
    {
        //Konstruktor klasy ImageDocument, który przyjmuje nazwę pliku jako parametr i przekazuje ją do konstruktora klasy bazowej AbstractDocument
        public ImageDocument(string filename) : base(filename) { }

        //Implementacja metody GetFormatType, która zwraca typ formatu dokumentu jako JPG
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.JPG;
    }

    public class TextDocument : AbstractDocument
    {
        //Konstruktor klasy TextDocument, który przyjmuje nazwę pliku jako parametr i przekazuje ją do konstruktora klasy bazowej AbstractDocument
        public TextDocument(string filename) : base(filename) { }

        //Implementacja metody GetFormatType, która zwraca typ formatu dokumentu jako TXT
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.TXT;
    }
}
