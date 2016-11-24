namespace Wpf.Redactor.Documents
{
    public interface IDocument
    {
        string FullDocumentPath { get; set; }
        bool IsSaved { get; set; }
        string Content { get; set; }
        

        void Open();
        void Save(string path);
        void Close();
    }
}
