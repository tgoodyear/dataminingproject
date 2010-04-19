using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DataMiningApp
{
    public partial class _Default : System.Web.UI.Page
    {
        public TextBox[] textboxes;

        protected void Page_Load(object sender, EventArgs e)
        {
          
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

        protected void next_button_Click(object sender, EventArgs e)
        {
            textboxes[0].Text = "Changed!";
            textboxes[1].Text = textboxes[2].Text;
        }
    }
}
