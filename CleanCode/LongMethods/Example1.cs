using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FooFoo
{
    public partial class Download : System.Web.UI.Page
    {
        private readonly DataTableToCsvMapper _memoryFileCreator = new DataTableToCsvMapper();
        private readonly DataTableConsulter _tableReader = new DataTableConsulter();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClearResponse();

            SetCacheability();

            WriteContentToResponse(GetCsv());
        }

        private byte[] GetCsv()
        {
            System.IO.MemoryStream ms = _memoryFileCreator.Map(_tableReader.GetDataTable("tableName"));
            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteArray;
        }

        private void WriteContentToResponse(byte[] byteArray)
        {
            Response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
            Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
            Response.ContentType = "text/comma-separated-values";
            Response.AddHeader("Content-Disposition", "attachment; filename=FooFoo.csv");
            Response.AddHeader("Content-Length", byteArray.Length.ToString());
            Response.BinaryWrite(byteArray);
        }

        private void SetCacheability()
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.CacheControl = "private";
            Response.AppendHeader("Pragma", "cache");
            Response.AppendHeader("Expires", "60");
        }

        private void ClearResponse()
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();
        }
    }

    public class DataTableToCsvMapper
    {
        public System.IO.MemoryStream Map(DataTable dataTable)
        {
            MemoryStream ReturnStream = new MemoryStream();

            StreamWriter streamWritter = new StreamWriter(ReturnStream);

            WriteColumnNames(dataTable, streamWritter);

            WriteRows(dataTable, streamWritter);

            streamWritter.Flush();
            streamWritter.Close();
            
            return ReturnStream;
        }

        private static void WriteRows(DataTable dataTable, StreamWriter streamWritter)
        {
            foreach(DataRow row in dataTable.Rows)
            {
                WriteRow(dataTable, row, streamWritter);
                streamWritter.WriteLine();
            }
        }

        private static void WriteRow(DataTable dataTable, DataRow row, StreamWriter streamWritter)
        {
            for(int index = 0; index < dataTable.Columns.Count; index++)
            {
                WriteCell(row, streamWritter, index);

                WriteSeparatorIfRequired(dataTable, streamWritter, index);
            }
        }

        private static void WriteSeparatorIfRequired(DataTable dataTable, StreamWriter streamWritter, int index)
        {
            if(index < dataTable.Columns.Count - 1)
            {
                streamWritter.Write(",");
            }
        }

        private static void WriteCell(DataRow row, StreamWriter streamWritter, int index)
        {
            if(!Convert.IsDBNull(row[index]))
            {
                string str = String.Format("\"{0:c}\"", row[index].ToString()).Replace("\r\n", " ");
                streamWritter.Write(str);
            }
            else
            {
                streamWritter.Write("");
            }
        }

        private static void WriteColumnNames(DataTable dataTable, StreamWriter streamWritter)
        {
            for(int index = 0; index < dataTable.Columns.Count; index++)
            {
                streamWritter.Write(dataTable.Columns[index]);
                if(index < dataTable.Columns.Count - 1)
                {
                    streamWritter.Write(",");
                }
            }
            streamWritter.WriteLine();
        }
    }

    public class DataTableConsulter
    {
        private readonly string _connectionString = "FooFooConnectionString";

        public DataTable GetDataTable(string tableName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[_connectionString].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM ["+tableName+"] ORDER BY id ASC", connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tableName);
            DataTable dataTable = dataSet.Tables[tableName];
            return dataTable;
        }
    }
}
