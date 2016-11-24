using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;
using Wpf.Redactor.Documents;

namespace Wpf.Redactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// All documents in the window.s
        /// </summary>
        private List<IDocument> _documents = new List<IDocument>();
        /// <summary>
        /// An active document.
        /// </summary>
        private IDocument _activeDocument;
        /// <summary>
        /// Shows if a file was saved
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();



            DocumentBase.FileOpenFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        }

        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
        private void NewCmdCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
        private void CloseCmdCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void SaveAsCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_activeDocument != null)
                e.CanExecute = true;
        }

        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_activeDocument?.FullDocumentPath != null)
                e.CanExecute = true;
        }


        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = DocumentBase.FileOpenFilter,
                FileName = "New file"
            };

            if (fileDialog.ShowDialog() == true)
            {
                string fileExtension = Path.GetExtension(fileDialog.SafeFileName);

                switch (fileExtension)
                {
                    case ".txt":
                        DocumentBase txtDocument = new TxtDocument(fileDialog.FileName);
                        _documents.Add(txtDocument);
                        _activeDocument = txtDocument;
                        break;
                }

                try
                {
                    _activeDocument.Open();

                    InputRichTextBox.IsEnabled = true;

                    new TextRange(InputRichTextBox.Document.ContentStart,
                        InputRichTextBox.Document.ContentEnd)
                    { Text = _activeDocument.Content };
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Exception occured", MessageBoxButton.OK);
                }
            }
        }


        private void NewCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TxtDocument newDocument = new TxtDocument(null);
            _documents.Add(newDocument);
            _activeDocument = newDocument;
            InputRichTextBox.IsEnabled = true;
        }


        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _activeDocument.Save(_activeDocument.FullDocumentPath);
        }

        private void SaveAsCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog()
            {
                Filter = DocumentBase.FileOpenFilter
            };

            if (fileDialog.ShowDialog(this) == true)
                _activeDocument.Save(fileDialog.FileName);
        }

        private void CloseCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void InputRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _activeDocument.Content =
                new TextRange(InputRichTextBox.Document.ContentStart, InputRichTextBox.Document.ContentEnd).Text;
        }
    }
}
