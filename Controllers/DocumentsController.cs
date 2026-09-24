using Microsoft.AspNetCore.Mvc;
using reactSyncfusionApp.Services;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;

namespace reactSyncfusionApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly SalesInvoiceService _salesInvoiceService;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public DocumentsController(
     SalesInvoiceService salesInvoiceService,
     IWebHostEnvironment hostingEnvironment)
    {
        _salesInvoiceService = salesInvoiceService;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet("invoice/{orderId}")]
    public async Task<IActionResult> GenerateInvoice(int orderId)
    {
        var invoice = await _salesInvoiceService.GetInvoice(orderId);

        if (invoice == null)
            return NotFound();

        // Syncfusion document generation will go here.

        string basePath = _hostingEnvironment.WebRootPath;

        string dataPath = Path.Combine(
            basePath,
            "Word",
            "SalesInvoiceDemo.doc"
        );

        FileStream fileStream = new FileStream(
            dataPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite
        );

        WordDocument doc = new WordDocument();

        doc.Open(fileStream, FormatType.Automatic);

        //orders group

        var orderData = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                { "OrderId", invoice.OrderId },
                { "CustomerName", invoice.CustomerName ?? "" },
                { "ShipAddress", invoice.ShipAddress ?? "" },
                { "ShipCity", invoice.ShipCity ?? "" },
                { "ShipPostalCode", invoice.ShipPostalCode ?? "" },
                { "ShipCountry", invoice.ShipCountry ?? "" },
                { "OrderDate", invoice.OrderDate?.ToString() ?? "" },
                { "RequiredDate", invoice.RequiredDate?.ToString() ?? "" }, 
                { "ShippedDate", invoice.ShippedDate?.ToString() ?? "" }

            }
        };

        
        var mailMergeDataTableOrder =
            new MailMergeDataTable("Orders", orderData);

        doc.MailMerge.ExecuteGroup(mailMergeDataTableOrder);

        // order group - invoice details   

        var detailData = invoice.Details
            .Select(x => new Dictionary<string, object> 
            { 
                { "ProductName", x.ProductName ?? "" }, 
                { "Quantity", x.Quantity ?? 0 }, 
                { "UnitPrice", x.UnitPrice ?? 0 }, 
                { "Discount", x.Discount ?? 0 }, 
                { "ExtendedPrice", x.ExtendedPrice ?? 0 } 
            })
                .ToList();

        var mailMergeDataTableDetails = new MailMergeDataTable("Order", detailData);

        doc.MailMerge.ExecuteGroup(mailMergeDataTableDetails);

        //orderTotals group
        var totalsData = new List<Dictionary<string, object>> 
        { 
            new Dictionary<string, object> 
            { 
                { "Subtotal", invoice.Subtotal ?? 0 }, 
                { "Freight", invoice.Freight ?? 0 }, 
                { "Total", invoice.Total ?? 0 } 
            } 
        };

        var mailMergeDataTableTotals = new MailMergeDataTable("OrderTotals", totalsData); 
        doc.MailMerge.ExecuteGroup(mailMergeDataTableTotals);

        //save generated document

        MemoryStream ms = new MemoryStream();

        doc.Save(ms, FormatType.Docx);

        doc.Close();
        fileStream.Close();

        ms.Position = 0;

        return File(
            ms,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "SalesInvoice.docx"
        );
    }
}