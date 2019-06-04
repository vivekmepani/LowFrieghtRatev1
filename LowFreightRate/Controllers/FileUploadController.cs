using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using LowFreightRate.Data;
using LowFreightRate.Models.PostQuote;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using Microsoft.Extensions.Configuration;


using System.Data.SqlClient;

using System.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace LowFreightRate.Controllers
{
    public class FileUploadController : Controller
    {

        SqlConnection con;

        string sqlconn;

        private readonly IHostingEnvironment _env;

        public FileUploadController(IHostingEnvironment env)
        {
            _env = env;
        }

       public IConfiguration Configuration { get; }
       

        

       

        public IActionResult Dashboard_Admin()
        {
            return View();

        }
        [HttpPost, Route("Dashboard_Admin")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Dashboard_Admin(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string filePath = Path.Combine(_env.WebRootPath, "csvfiles");
            string csvPath = String.Empty;


            foreach (var formFile in files)
            {
                
                    using (var stream = new FileStream(Path.Combine(filePath,formFile.FileName), FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                        csvPath = Path.Combine(filePath, formFile.FileName);
                }
                
            }


            //updating database

            //Creating object of datatable  
            DataTable tblcsv = new DataTable();
            //creating columns for the new data table with the name of the csv file columns
            tblcsv.Columns.AddRange(new DataColumn[11]
            {
            new DataColumn("Receipt", typeof(Int32)),
            new DataColumn("Delivery", typeof(Int32)),
            new DataColumn("Effective Date", typeof(DateTime)),
            new DataColumn("Expiry Date", typeof(DateTime)),
            new DataColumn("Service Mode", typeof(int)),
            new DataColumn("Commodity Name", typeof(int)),
            new DataColumn("Charge", typeof(string)),
            new DataColumn("Rate Basis", typeof(int)),
            new DataColumn("Container Size", typeof(int)),
            new DataColumn("Currency", typeof(string)),
            new DataColumn("Rate", typeof(decimal))

            });

            // reading the contents of csv file

            string csvData = System.IO.File.ReadAllText(csvPath);

            int line = 0;
            foreach (string csvRow in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow) && line > 0)
                {
                    //Adding each row into datatable  
                    tblcsv.Rows.Add();
                    int count = 0;
                    foreach (string FileRec in csvRow.Split(','))
                    {
                        tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                        count++;
                    }
                }
                line++;

            }

            //Calling insert Functions  
            InsertCSVRecords(tblcsv);

            return Ok(new { count = files.Count, size, filePath });

        }

        private void connection()
        {
            sqlconn = Configuration.GetConnectionString("NewConnection");
            con = new SqlConnection(sqlconn);

        }

        private void InsertCSVRecords(DataTable csvdt)
        {

            connection();
            //creating object of SqlBulkCopy    
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            //assigning Destination table name    
            objbulk.DestinationTableName = "tblRates";
            //Mapping Table column    
            objbulk.ColumnMappings.Add("Receipt", "StartingLocation");
            objbulk.ColumnMappings.Add("Delivery", "Destination");
            objbulk.ColumnMappings.Add("Effective Date", "EffectiveDate");
            objbulk.ColumnMappings.Add("Expiry Date", "ExpiryDate");
            objbulk.ColumnMappings.Add("Service Mode", "ServiceModeID");
            objbulk.ColumnMappings.Add("Commodity Name", "CommodityType");
            objbulk.ColumnMappings.Add("Charge", "ChargeCode");
            objbulk.ColumnMappings.Add("Rate Basis", "RateBasisID");
            objbulk.ColumnMappings.Add("Container Size", "ContainerSizeTypeID");
            objbulk.ColumnMappings.Add("Currency", "CurrencyCode");
            objbulk.ColumnMappings.Add("Rate", "Rate");
            //inserting Datatable Records to DataBase    
            con.Open();
            objbulk.WriteToServer(csvdt);
            con.Close();


        }
    }

    
}