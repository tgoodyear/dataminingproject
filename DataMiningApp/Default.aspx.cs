using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.Odbc;

namespace DataMiningApp
{
    public partial class _Default : System.Web.UI.Page
    {
        public TextBox[] textboxes;

        protected void Page_Load(object sender, EventArgs e)
        {

            /* Database connection experimentation */

            OdbcConnection connection;
            OdbcCommand command;
            OdbcDataReader reader;

            /* Define database connection */
            connection = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};DBQ=" + Server.MapPath("/App_Data/database.mdb") + ";UID=;PWD=;");
            
            /* Create SQL command */
            string query = "CREATE TABLE datatable(datacolumn1 numeric, datacolumn2 numeric)";   // Direct query version
            
            command = new OdbcCommand(query,connection);
            
            connection.Open();
            reader = command.ExecuteReader();

            /* Loop through database records */
            while (reader.Read())
            {
                Response.Write(reader.GetString(0));
            }

            connection.Close();

            /* Layout setup experimentation */

            
            layouttable.Rows.Add(new HtmlTableRow());
            
            int numtextboxes = 3;

            textboxes = new TextBox[numtextboxes];

            for (int i=0; i < numtextboxes; i++)
            {
                HtmlTableCell newcell = new HtmlTableCell();
                layouttable.Rows[0].Cells.Add(newcell);
            
                textboxes[i] = new TextBox();
                textboxes[i].ID = "test" + i;
                textboxes[i].Text = textboxes[i].ID;

                newcell.Controls.Add(textboxes[i]);
            }
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
