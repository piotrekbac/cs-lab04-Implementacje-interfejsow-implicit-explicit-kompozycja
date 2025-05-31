//Piotr Bacior 15 722 - WSEI Kraków

namespace Zadanie4
{
    //Definiujemy interfejs IDocument reprezentujący dowolny dokument możliwy do przetwarzania przez urządzenie
    public interface IDocument
    {
        //Typ wyliczeniowy FormatType określa format dokumentu: tekstowy (TXT), graficzny (JPG), PDF
        enum FormatType { TXT, PDF, JPG }

        //Zwraca typ formatu dokumentu (np. PDF, JPG, TXT)
        FormatType GetFormatType();

        //Zwraca nazwę pliku dokumentu
        string GetFileName();
    }

    //Abstrakcyjna klasa AbstractDocument implementuje interfejs IDocument i stanowi bazę dla konkretnych typów dokumentów
    public abstract class AbstractDocument : IDocument
    {
        //Prywatne pole przechowujące nazwę pliku dokumentu
        private string fileName;

        //Konstruktor przypisujący nazwę pliku
        public AbstractDocument(string fileName) => this.fileName = fileName;

        //Zwraca nazwę pliku dokumentu
        public string GetFileName() => fileName;

        //Metoda abstrakcyjna – wymusza implementację typu formatu w klasach pochodnych
        public abstract IDocument.FormatType GetFormatType();
    }

    //Klasa PDFDocument reprezentuje dokument typu PDF
    public class PDFDocument : AbstractDocument
    {
        public PDFDocument(string filename) : base(filename) { }

        //Zwraca typ dokumentu jako PDF
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.PDF;
    }

    //Klasa ImageDocument reprezentuje dokument graficzny typu JPG
    public class ImageDocument : AbstractDocument
    {
        public ImageDocument(string filename) : base(filename) { }

        //Zwraca typ dokumentu jako JPG
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.JPG;
    }

    //Klasa TextDocument reprezentuje dokument tekstowy typu TXT
    public class TextDocument : AbstractDocument
    {
        public TextDocument(string filename) : base(filename) { }

        //Zwraca typ dokumentu jako TXT
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.TXT;
    }
}
