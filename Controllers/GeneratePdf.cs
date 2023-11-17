using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetAngularAuthWebApi.helpers;
using NetAngularAuthWebApi.Services;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using Serilog;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratePdf : ControllerBase
    {
        private static readonly string[] Summaries = new[]
       {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ILogger<GeneratePdf> _logger;
        private readonly IConverter _converter;
        private readonly RabbitMQSettings _rabbitMQSettings;
        private readonly pdfGenerateDinkToPdf _dinktoPdf;

        public GeneratePdf(IConverter converter, ILogger<GeneratePdf> logger, IOptions<RabbitMQSettings> rabbitMQSettings, pdfGenerateDinkToPdf dinktoPdf)
        {
            _converter = converter;
            _logger = logger;
            _rabbitMQSettings = rabbitMQSettings.Value;
            _dinktoPdf = dinktoPdf;
        }

        [HttpGet("generate-pdf")]
        public async Task<IActionResult> GeneratePdfInvoice(string InvoiceNo)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/controller.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            Log.Information("Generating pdf");


            var document = new PdfDocument();
            _logger.LogInformation("Seri Log is Working");
            _logger.LogInformation("Test log");
            string host = _rabbitMQSettings.Host;
            int port = _rabbitMQSettings.Port;
            string userName = _rabbitMQSettings.UserName;
            string password = _rabbitMQSettings.Password;
            string virtualHost = _rabbitMQSettings.VirtualHost;
            // Read HTML content from the template file
            string templateFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"C:\Users\Angga Lesmana\Documents\mine\coding\C#\NetAngularAuth\NetAngularAuthWebApi\Controllers\Template\", "InvoiceTemplate.html");
            string HtmlContent = System.IO.File.ReadAllText(templateFilePath);

            // Perform any dynamic content replacement if needed
            HtmlContent = HtmlContent.Replace("{InvoiceNo}", InvoiceNo);
            //to set data into variable html 
            HtmlContent = HtmlContent.Replace("{Dimensi.SesuaiSUT}", "1244 UUT");
            HtmlContent = HtmlContent.Replace("{ttd.Hrd}", "https://hastaprakarsa.co.id/wp-content/uploads/2020/02/tanda-tangan-mujiono.png");

            // Generate PDF using the HTML content
            // PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);

            byte[] response;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            if (response != null && response.Length > 0)
            {
                string FileName = "Invoice" + InvoiceNo + ".pdf";
                return File(response, "application/pdf", FileName);
            }
            else
            {
                // Handle the case where the PDF generation fails
                return BadRequest("Failed to generate PDF");
            }
        }

        [HttpPost("generate-dink-topdf")]
        public async Task<IActionResult> GenerateDinkToPdf(){
            string imageUrl = "https://hastaprakarsa.co.id/wp-content/uploads/2020/02/tanda-tangan-mujiono.png";
            string HtmlContent = $"<html><body><h1>Hallo</h1>" + 
            $"<img src='https://hastaprakarsa.co.id/wp-content/uploads/2020/02/tanda-tangan-mujiono.png' " +
            "alt='example'></body></html>";
            //for html content bisa juga digunakan via link atau gettampilan via link
            byte[] pdfbyte = _dinktoPdf.DinkToPdf(HtmlContent);
            return File(pdfbyte, "applicatoin/pdf", "generate.pdf");
        }

        // [HttpGet("get-pdf-with-image")]
        // public async Task<IActionResult> GeneratePdfWithImage(){
        //     var document = new PdfDocument();
        //     string htmlElement = "<div>";
        //     string imageurl = "https://i.ibb.co/d4QvBBH/tam.jpg";
        //     htmlElement += "<img style='width:100px' src'"+imageurl+"' />";
        //     htmlElement += "<h2>bissmillah bisa</h2>";
        //     htmlElement += "</div>";
        //     PdfGenerator.AddPdfPages(document, htmlElement, PageSize.A4);
        //     byte[] response = null;
        //     using(MemoryStream ms = new MemoryStream()){
        //         document.Save(ms);
        //         response = ms.ToArray();
        //     }
        //     return File(response, "application/pdf", "PdfWithImage.pdf");
        // }

        //     [HttpGet("get-pdf-with-image")]
        //     public async Task<IActionResult> GeneratePdfWithImage()
        //     {
        //         string imageUrl = "https://cdn1-production-images-kly.akamaized.net/wYlgfuYkSSzxG0mLgv0Cl_B-OsU=/800x450/smart/filters:quality(75):strip_icc():format(webp)/kly-media-production/medias/908624/original/090170500_1435112456-spiderman3.jpeg";

        //         var document = new PdfDocument();

        //         string htmlElement = $@"
        //     <div>
        //         <img src='{imageUrl}' />
        //         <b>welcome in amartek</b>
        //     </div>
        // ";

        //         PdfGenerator.AddPdfPages(document, htmlElement, PageSize.A4);

        //         byte[] response = null;

        //         using (MemoryStream ms = new MemoryStream())
        //         {
        //             document.Save(ms);
        //             response = ms.ToArray();
        //         }

        //         return File(response, "application/pdf", "PdfWithImage.pdf");
        //     }

        [HttpGet("get-pdf-with-image")]
        public async Task<IActionResult> GeneratePdfWithImageDink()
        {
            string imageUrl = "https://cdn1-production-images-kly.akamaized.net/wYlgfuYkSSzxG0mLgv0Cl_B-OsU=/800x450/smart/filters:quality(75):strip_icc():format(webp)/kly-media-production/medias/908624/original/090170500_1435112456-spiderman3.jpeg";

            var htmlContent = $@"
            <div>
                <img src='{imageUrl}' />
                <b>welcome in amartek</b>
            </div>
        ";

            var doc = new HtmlToPdfDocument()
            {
                Objects = {
                new ObjectSettings
                {
                    HtmlContent = htmlContent
                }
            }
            };

            var pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf", "PdfWithImage.pdf");
        }

    }
}