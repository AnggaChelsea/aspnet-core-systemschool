using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace NetAngularAuthWebApi.Services
{
    
    public class pdfGenerateDinkToPdf
    {
     private readonly IConverter _converter;

        public pdfGenerateDinkToPdf(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] DinkToPdf(string HtmlContent){
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top=  10, Bottom = 10, Left = 10, Right = 10 },
                DocumentTitle = "Generate Pdf Insyallah"
            };

            var objectSetting = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = HtmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 12, Right = "Page [Page] of [toPage]", Line=true, Spacing= 2.812 },
                FooterSettings = { FontSize = 12, Line = true, Right= "c " + DateTime.Now.Year }
            };

            var HtmlDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = {objectSetting}
            };
           return _converter.Convert(HtmlDocument);
        }
    }
}