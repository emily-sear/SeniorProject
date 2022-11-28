using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using PdfSharp.Fonts;
using System.IO;

namespace App1
{
    class FontProvider : IFontResolver
    {
        public string DefaultFontName => throw new NotImplementedException();

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                Console.WriteLine("made it: " + faceName);
                using (var fs = File.Open(faceName, FileMode.Open))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Verdana", StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo("Fonts/verdana-bold-italic.ttf");
                }
                else if (isBold)
                {
                    return new FontResolverInfo("Fonts/verdana-bold.ttf");
                }
                else
                {
/*                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
                    Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory */
                   // string filePath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "drawable/verdana.tff");

                   return new FontResolverInfo(@"Fonts\verdana.ttf");
                }
            }
            return null;
        }
    }
}
