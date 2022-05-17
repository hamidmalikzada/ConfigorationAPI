using ConfigAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public class Import : Controller
{
    private readonly MyContext _context;

    public Import(MyContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(IFormFile fileBatch)
    {
        if (fileBatch.Length > 0)
        {
            var stream = fileBatch.OpenReadStream();

            List<Component> compList = new List<Component>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.First();
                var rows = sheet.Dimension.Rows;
                int startImportRow = 0;
                int tableHeaderRow = 0;

                for (int i = 1; i <= rows; i++)
                {
                    bool importRow = false;
                    Component comp = new Component();

                    for (int j = 1; j < sheet.Dimension.Columns; j++)
                    {
                        var value = sheet.Cells[i, j].Value;

                        if (value != null)
                        {

                            if (value.ToString() == "Part Number")
                            {
                                startImportRow = i + 2;
                                tableHeaderRow = i;
                            }
                        }

                        if (i >= startImportRow && startImportRow != 0)
                        {

                            if (value != null)
                            {
                                importRow = true;

                                switch (sheet.Cells[tableHeaderRow, j].Value.ToString())
                                {
                                    case "Part Number":
                                        comp.Name = value.ToString();
                                        break;
                                    case "Description":
                                        comp.Description = value.ToString();
                                        break;
                                    case "Manufacture":
                                        comp.Manufacturer = value.ToString();
                                        break;
                                    case "Manufacture Part Number":
                                        comp.ManufacturerPartId = value.ToString();
                                        break;
                                    case "Supplier Unit Price( SEK )":
                                        comp.Price = Double.Parse(value.ToString());
                                        break;
                                }
                            }
                        }
                    }

                    if (importRow == true)
                    {
                        compList.Add(comp);
                    }

                }
            }


            foreach (var component in compList)
            {
                _context.Add(component);
            }

            _context.SaveChanges();
        }



        return Redirect("http://localhost:3000/AllComponents");
    }
}