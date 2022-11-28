using System;
using System.Collections.Generic;
using System.Text;
using PdfSharpCore.Pdf;

namespace App1
{
    public interface IFileService
    {
        void Save(PdfDocument pdf, string name, string location = "temp");
    }
}
