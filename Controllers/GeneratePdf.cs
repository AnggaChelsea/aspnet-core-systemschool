using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratePdf : ControllerBase
    {
        [HttpGet("generate-pdf")]
        public async Task<IActionResult> GeneratePdfInvoice(string InvoiceNo)
        {
            var document = new PdfDocument();
            string HtmlContent = @"
                   <!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 10;
            padding: 10;
            font-size:10px;
        }

        .header {
            text-align: center;
            padding-top: 20px;
        }

        .logo {
            /* Add your logo styling here */
            width:150px;
            text-align:left;
        }

        .line {
            border-top: 2px solid black;
            margin-top: 10px;
        }

        .address {
            text-align: left;
            margin-top: 10px;
            margin-bottom: 30px;
        }

        .date {
            margin-top: -50px;
            margin-bottom: 30px;
            text-align: right;

        }

        .recipient {
            margin-bottom: 30px;
            text-align: right;

        }

        .content {
            margin-bottom: 50px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 50px;
        }

        table, th, td {
            border: 1px solid black;
        }

        th, td {
            padding: 10px;
            text-align: left;
        }

        .footer {
            margin-bottom: 150px;
        }

        .signature {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }

        .signature .director {
            text-align: center;
        }
    </style>
</head>
<body>
    <div class='header'>
        <div class='logo'>
            <img src='https://imagetolink.com/ib/RCH0NKSJEr.jpeg' alt=''>
        </div>
        <div class='line'></div>
        <div class='address'>
            PT. TOYOTA – ASTRA MOTOR <br>
            Jl. Laks. Yos Sudarso, Sunter II <br>
            Jakarta Utara - Indonesia <br>
            Phone: 62-21 – 6515551 <br>
            PO. Box: 1420 – Jakarta 10014
        </div>
        <div class='date'>
            Jakarta, 26 Oktober 2022
        </div>
        <div class='recipient'>
            Kepada : <br>
            PT. Astra International Tbk-TSO <br>
            Bandung-Soekarno Hatta Branch <br>
            Jl. Soekarno Hatta No. 145 <br>
            Bandung 40223
        </div>
    </div>
    <div class='content'>
        <p>Dengan Hormat,</p>
        <p>Sehubungan dengan surat PT. Astra International Tbk-TSO Bandung-Soekarno Hatta Branch yang saudara sampaikan No. : 142/AIT-SH/GSO/UM/X/2023 perihal Surat Rekomendasi Modifikasi berupa Mobil Ambulance untuk Karoseri CV. Sarana Motor.</p>
        <p>Adapun mengenai data – data kendaraan sebagai berikut :</p>
        <p>I. Kendaraan yang dimodifikasi :</p>
        <ul>
            <li>Merek : TOYOTA</li>
            <li>Tipe : INNOVA 2.0 G M/T BENSIN</li>
            <li>Peruntukan : MOBIL AMBULANCE</li>
        </ul>
        <p>II. Detail Modifikasi :</p>
        <table>
            <tr>
                <th>Test Item</th>
                <th>Dimensi Kendaraan Sesuai SUT</th>
                <th>No.: SK.4438/AJ.402/DRJD/2018</th>
                <th>Dimensi Kendaraan setelah dimodifikasi</th>
            </tr>
            <tr>
                <td>Jarak Sumbu / WB</td>
                <td>2.750 mm</td>
                <td>2.750 mm</td>
                <td>2.750 mm</td>
            </tr>
            <!-- Add more rows as needed -->
        </table>
        <p>Kami Agen Tunggal Pemegang Merek (ATPM) mengetahui adanya perubahan bentuk berupa MOBIL AMBULANCE sesuai dengan gambar terlampir dengan memperhatikan persyaratan sebagai berikut:</p>
        <ol>
            <li>Tidak ada perubahan desain, performa mesin dan kerangka landasan diluar spesifikasi kendaraan dasar</li>
            <li>Proses Perubahan bentuk / fungsi sebagaimana tersebut di atas harus dengan kualitas yang baik dan standar regulasi dan ketentuan yang berlaku sehingga dapat saudara pertanggungjawabkan secara hukum dan tidak dilakukan modifikasi di luar yang sudah disampaikan kepada kami ATPM</li>
        </ol>
        <p>Demikian surat keterangan ini kami sampaikan dan hanya untuk digunakan terkait perubahan bentuk kendaraan sebagaimana informasi diatas.</p>
        <p>Atas perhatian dan kerjasamanya kami ucapkan terimakasih.</p>
    </div>
    <div class='footer'>
         <div class='signature'>
            <div class='director'>
                PT. TOYOTA ASTRA MOTOR <br>

                <img width='50' src='https://hastaprakarsa.co.id/wp-content/uploads/2020/02/tanda-tangan-mujiono.png' alt=''>



                <br> Anton Jimmi Suwandy
            </div>
            <div class='director'>
                PT. TOYOTA ASTRA MOTOR <br>
                <img width='50' src='https://hastaprakarsa.co.id/wp-content/uploads/2020/02/tanda-tangan-mujiono.png' alt=''>
                <br>
                Fumitaka Kawashima
            </div>
        </div>
    </div>
</body>
</html>

                    ";
            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);

            byte[] response; // Change from byte[]? to byte[]
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray(); // Assign the generated PDF content to response
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
    }
}