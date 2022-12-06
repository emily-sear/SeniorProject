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

                if (isBold)
                {
                    return new FontResolverInfo(@"Fonts\verdana-bold.ttf");
                }
                else
                {
                   return new FontResolverInfo(@"Fonts\verdana.ttf");
                }
            }
            return null;
        }
    }
}
