using DinkToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peedeef
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = File.ReadAllText(@"data\test-one.html");
            var raw = BuildPdf(html);

            File.WriteAllBytes("out.pdf", raw);

            Console.WriteLine("key");
            Console.ReadKey();
        }

        private static byte[] BuildPdf(string html)
        {
            var pdfConverter = new SynchronizedConverter(new PdfTools());
            return pdfConverter.Convert(new HtmlToPdfDocument()
            {
                Objects =
                {
                    new ObjectSettings
                    {                        
                        HtmlContent = html
                    }
                }
            });
        }
    }
}
