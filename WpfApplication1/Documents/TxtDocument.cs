using System;
using System.IO;

namespace Wpf.Redactor.Documents
{
    class TxtDocument : DocumentBase
    {
        public TxtDocument(string fullDocumentPath) : base(fullDocumentPath)
        {
        }

        public override void Open() => Content = File.ReadAllText(FullDocumentPath);

        public override void Close()
        {
            throw new System.NotImplementedException();
        }

    }
}
