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
        public TextBox[] textboxes;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Program constants

            // Design limit for layout table to 2 columns, 6 rows
            const int max_layout_cols = 2;
            const int max_layout_rows = 6;

            // Define database connection objects
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader reader;

            // Specify connection string to database

            // Microsoft Access
            //connection = new SqlConnection("Driver={Microsoft Access Driver (*.mdb)};DBQ=" + Server.MapPath("/App_Data/database.mdb") + ";UID=;PWD=;");

            // Microsoft SQL Server
            connection = new SqlConnection("Data Source=RANJAN-PC\\SQLEXPRESS;Initial Catalog=DMP;UId=webapp;Password=password;");

            // Create SQL query
            string query = "SELECT LAYOUT_X, LAYOUT_Y, ROWSPAN, COLSPAN, CONTROL_TYPE, FILL_DATANAME, OUTPUT_DATANAME FROM WEBAPP_LAYOUT";
            command = new SqlCommand(query, connection);

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

            for (int row_traverse = 0; row_traverse < max_layout_rows; row_traverse++)
            {
                layouttable.Rows.Add(new HtmlTableRow());
                for (int col_traverse = 0; col_traverse < max_layout_cols; col_traverse++)
                {
                    HtmlTableCell newcell = new HtmlTableCell();
                    newcell.RowSpan = spanarray[col_traverse, row_traverse, 0];
                    newcell.ColSpan = spanarray[col_traverse,row_traverse,1];
                    layouttable.Rows[row_traverse].Cells.Add(newcell);
                }
            }


            /*
            int numtextboxes = 3;

            textboxes = new TextBox[numtextboxes];

            for (int i=0; i < numtextboxes; i++)
            {
                HtmlTableCell newcell = new HtmlTableCell();
                
            
                textboxes[i] = new TextBox();
                textboxes[i].ID = "test" + i;
                textboxes[i].Text = textboxes[i].ID;

                newcell.Controls.Add(textboxes[i]);
            }
             * */
        }
        /*
        ' Wizard construction table:
        ' --------------------------
        ' Algorithm ID, step, row, column, controltype, datafillprocedure, outputparameter

        ' Algorithm ID defines the algorithm that owns the row (PCA, MDS, etc.)
        ' Step is the current step in the wizard for this algorithm
        ' Row and column determine where in the layout table the control is placed
        ' Controltype specifies what type of control - textbox, checkbox, label, etc. is placed (need an upload item as well)
        ' Datafillprocedure is the SQL stored procedure that fills the control (ex. SELECT for drop down list contents)
        ' Outputparameter is the name of the variable that will be associated with the chosen data item

        ' Parameter Result Table:
        ' -----------------------
        ' Algorithm ID, run ID, parametername, parametervalue

        ' I forsee two types of data tables - the generic parameter table, and the custom data set tables
        ' This is the generic parameter table that can be used to fill control with predefined options and text

        ' Algorithm execution table:
        ' --------------------------
        ' Algorithm ID, step, stepprocedure

        ' Stepprocedure is the SQL stored procedure that will handle the current step of the algorithm

        ' Data storage table:
        ' -------------------
        ' Tough one. Probably try to let webpage dynamically create tables based on user upload
        ' Columns are entirely custom. Algorithm procedure will have to know how to handle it.
        */
        protected void next_button_Click(object sender, EventArgs e)
        {
            textboxes[0].Text = "Changed!";
            textboxes[1].Text = textboxes[2].Text;
        }
    }
}
