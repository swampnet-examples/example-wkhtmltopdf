using DinkToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Peedeef
{
    class Program
    {
		static HttpClient _httpClient = new HttpClient();

		static void Main(string[] args)
        {
            var html = File.ReadAllText(@"data\test-one.html");
			//var html = LoadHtml("https://www.bbc.co.uk/");
            var raw = BuildPdf(html);

            File.WriteAllBytes("out.pdf", raw);

            Console.WriteLine("key");
            Console.ReadKey();
        }

		private static string LoadHtml(string url)
		{

			var response = _httpClient.GetAsync(url).Result;
			if (!response.IsSuccessStatusCode)
			{
				throw new InvalidOperationException($"FetchHtml failed {response.StatusCode} : {response.ReasonPhrase}");
			}
			return response.Content.ReadAsStringAsync().Result;
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
                },
                GlobalSettings = new GlobalSettings()
                {
                    DocumentTitle = "My dumb PDF document",
                    Orientation = Orientation.Portrait                    
                }
            });
        }
    }
}
