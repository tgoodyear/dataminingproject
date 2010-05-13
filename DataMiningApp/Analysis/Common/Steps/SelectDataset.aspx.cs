using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Diagnostics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

using System.Data;



namespace DataMiningApp
{
    public partial class SelectDataset : System.Web.UI.Page
    {
        System.Data.DataTable dt;
        OleDbDataAdapter da;
        OleDbConnection connection;
        Registry.Registry registry; 

        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("in page load");
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Debug.WriteLine("in page init");
            registry = Registry.Registry.getRegistry(Application);
            dt = (DataTable)Session["table"];
            da = (OleDbDataAdapter)Session["adapter"];
            connection = (OleDbConnection)Session["connection"];
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            if (DataSetUpload.HasFile)
            {
                //move most to uploader
                String uploadPath = System.IO.Path.Combine(System.IO.Path.GetTempPath().ToString(), DataSetUpload.FileName);
                DataSetUpload.SaveAs(uploadPath);
                String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetTempPath().ToString() + ";Extended Properties='text;HDR=Yes;FMT=Delimited'";
                connection = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM " +  DataSetUpload.FileName, connection);
                da = new OleDbDataAdapter(cmd);
                connection.Open();
                dt = new System.Data.DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Application.Add("dataset", dt);
            }

        }

        protected void DataSetUpload_Init(object sender, EventArgs e)
        {
           // DataSetUpload.Width = 400;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
                dt.Rows.RemoveAt(e.RowIndex);
        }

        protected void UploadAs_Click(object sender, EventArgs e)
        {
            
            if (DataSetName.Text.Trim().Length != 0 && DataSetUpload.HasFile)
            {
                //move most to uploader
                //handle spaces in file name
                String uploadPath = System.IO.Path.Combine(System.IO.Path.GetTempPath().ToString(), DataSetUpload.FileName);
                DataSetUpload.SaveAs(uploadPath);
                String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetTempPath().ToString() + ";Extended Properties='text;HDR=Yes;FMT=Delimited'";
                connection = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM " + DataSetUpload.FileName, connection);
                da = new OleDbDataAdapter(cmd);
                connection.Open();
                dt = new System.Data.DataTable();
                da.Fill(dt);
                System.Data.DataSet ds = new System.Data.DataSet(DataSetName.Text.Trim());
                String[] parameters = {"main"};
                ds.Load(dt.CreateDataReader(),System.Data.LoadOption.OverwriteChanges, parameters);
                
                Session.Add("table", dt);
                Session.Add("connection", connection);
                Session.Add("adapter", da);

                Registry.Registry registry = Registry.Registry.getRegistry(Session);
                registry.registerDataset(ds);
                DatasetList.Items.Add(ds.DataSetName);
            }

        }

        protected void DatasetList_Init(object sender, EventArgs e)
        {
            Debug.WriteLine("in list init");
            foreach (String dataSetName in registry.GetDatasetNames())
            {
                DatasetList.Items.Add(dataSetName);
            }

        }

        protected void doPCA_Click(object sender, EventArgs e)
        {
            /*    Matrix m = new DenseMatrix(2, 2, new double[] { 10.0, -18.0, 6.0, -11.0 });
                Vector a = new DenseVector(new double[] { 10.0, -18.0 });
                Vector result = m.Multiply(a);
                Debug.WriteLine(result.ToString());

                if (DatasetList.Items.Count >0){
                    DataSet data = registry.GetDataset(0);
                    DataTable table = data.Tables["main"];
                    da.Fill(table);
                    foreach (Object item in table.Rows[0].ItemArray)
                        Debug.WriteLine(item.GetType().ToString());
           
                } */
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            if (DatasetList.Text != ""){

                Analysis.Analysis analysis = (Analysis.Analysis) Session["analysis"];
                Analysis.ParameterStream stream = Analysis.ParameterStream.getStream(Session);
                stream.set("dataSetName", DatasetList.Text);
                analysis.next(Response, Session);
            }
        }      
    }
}