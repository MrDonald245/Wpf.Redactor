using System.IO;

namespace Wpf.Redactor.Documents
{
    abstract class DocumentBase : IDocument
    {
        public string FullDocumentPath { get; set; }
        public bool IsSaved { get; set; }
        public string Content { get; set; }

        public static string FileOpenFilter { get; set; } = "All files (*.*)|*.*";


        protected DocumentBase(string name)
        {
            FullDocumentPath = name;
        }


        public abstract void Open();

        public virtual void Save(string path)
        {
            File.WriteAllText(path, Content);
            FullDocumentPath = path;
        }
        public abstract void Close();
    }
}
