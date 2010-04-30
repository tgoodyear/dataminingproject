using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

namespace DataMiningApp
{
    public partial class _Default : System.Web.UI.Page
    {
        // Control templates
        public TextBox[] textboxes;
        public Label labels;

        // Control counters
        int labelcounter = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Program variables

            // Define database connection objects
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader reader;

            // Specify connection string to database

            // Microsoft Access
            //connection = new SqlConnection("Driver={Microsoft Access Driver (*.mdb)};DBQ=" + Server.MapPath("/App_Data/database.mdb") + ";UID=;PWD=;");

            // Microsoft SQL Server
            connection = new SqlConnection("Data Source=RANJAN-PC\\SQLEXPRESS;Initial Catalog=DMP;UId=webapp;Password=password;");

            // Create SQL query to find out table size
            string tablesize_query = "SELECT MAX(LAYOUT_X), MAX(LAYOUT_Y) FROM WEBAPP_LAYOUT";
            
            // Establish connection
            reader = openconnection(tablesize_query, connection);

            // Read table size
            reader.Read();
                int max_layout_cols = (int)reader[0];
                int max_layout_rows = (int)reader[1];
            closeconnection(reader, connection);

            // Create SQL query to pull table layout information for this job and step
            string layout_query = "SELECT LAYOUT_X, LAYOUT_Y, ROWSPAN, COLSPAN, CONTROL_TYPE, FILL_DATANAME, OUTPUT_DATANAME FROM WEBAPP_LAYOUT";
            command = new SqlCommand(layout_query, connection);

            // Open connection and execute query using SQL Reader
            connection.Open();
            reader = command.ExecuteReader();

            // Control array - last index is for control type (0), fill data name (1), and output data name (2)
            string[, ,] controlarray = new string[max_layout_cols, max_layout_rows, 3];
            int[, ,] spanarray = new int[max_layout_cols, max_layout_rows, 2];
            int layout_x, layout_y;

            // Read through layout table for this step and algorithm
            while (reader.Read())
            {
                // Populate control array
                layout_x = (int)reader[0] - 1;                              // Table x index
                layout_y = (int)reader[1] - 1;                              // Table y index
               
                controlarray[layout_x, layout_y, 0] = (string)reader[4];    // Control type
                controlarray[layout_x, layout_y, 1] = (string)reader[5];    // Fill data name
                controlarray[layout_x, layout_y, 2] = (string)reader[6];    // Output data name

                spanarray[layout_x, layout_y, 0] = (int)reader[2];          // Rowspan
                spanarray[layout_x, layout_y, 1] = (int)reader[3];          // Colspan
            }

            connection.Close();

            // Build interface

            // Run through rows
            for (int row_traverse = 0; row_traverse < max_layout_rows; row_traverse++)
            {
                // Add row
                layouttable.Rows.Add(new HtmlTableRow());
                
                // Run through columns
                for (int col_traverse = 0; col_traverse < max_layout_cols; col_traverse++)
                {
                    // Check if this is a valid cell
                    if (spanarray[col_traverse, row_traverse, 0] > 0 && spanarray[col_traverse, row_traverse, 1] > 0)
                    {
                        // Create new cell object
                        HtmlTableCell newcell = new HtmlTableCell();

                        // Set column and row span properties (merge cells)
                        newcell.RowSpan = spanarray[col_traverse, row_traverse, 0];
                        newcell.ColSpan = spanarray[col_traverse, row_traverse, 1];
                        
                        // Add cell to table
                        layouttable.Rows[row_traverse].Cells.Add(newcell);
                    
                        // Add control, if applicable

                        // Label control
                        if (controlarray[col_traverse, row_traverse, 0] == "LABEL")
                        {
                            // Create new control
                            labels = new Label();
                            labels.ID = "control_" + col_traverse + "_" + row_traverse;
                            
                            // Set control properties
                            labels.Font.Name = "Arial"; labels.Font.Size = 11;
                            
                            // Fill data
                            
                            // Get fill query from controlarray
                            string control_query = controlarray[col_traverse, row_traverse, 1];
                            
                            // Check if fill query is specified
                            if (control_query != "NONE" && control_query != "")
                            {
                                // Initialize reader and get data
                                reader = openconnection(control_query, connection);
                                reader.Read();

                                // Load label text into string and set control value
                                string datavalue = (string)reader[0];
                                labels.Text = datavalue;

                                // Close reader and connection
                                closeconnection(reader, connection);
                            }

                            // Add control
                            newcell.Controls.Add(labels);

                        }
                    }
                }
            }
        }
        
        // Reusable function to open data connection and execute reader given query string and SqlConnection object
        SqlDataReader openconnection(string query, SqlConnection connection)
        {
            SqlDataReader reader;
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            reader = command.ExecuteReader();

            return reader;
        }

        // Reusable function to close data connection and reader
        void closeconnection(SqlDataReader reader, SqlConnection connection)
        {
            reader.Close();
            connection.Close();
        }

        // Handler for next button click
        protected void next_button_Click(object sender, EventArgs e)
        {
            foreach (Control c in Form.Controls)
            {
                Response.Write(c.GetType());
            }
            
            //mylabel.Text = "super";
        }
    }
}
