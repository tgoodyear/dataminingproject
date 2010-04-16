Partial Public Class _Default
    Inherits System.Web.UI.Page

    ' Declare control arrays public for modification in event handlers
    Public textboxes() As TextBox
    Public dropdownlists() As DropDownList
    Public checkboxes() As CheckBox
    Public labels() As Label

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

        ' Format panel
        mainpanel.Attributes("style") = "padding:10px;margin:10px;alignment=center;margin-left:auto;margin-right:auto;"

        layouttable.Rows.Add(New HtmlTableRow)

        ' Number of text boxes to generate
        Dim numtextboxes As Integer
        numtextboxes = 3

        ' Create array of type TextBox
        ReDim textboxes(numtextboxes)

        ' Run through text boxes
        For i = 1 To numtextboxes

            Dim newcell As New HtmlTableCell
            layouttable.Rows(0).Cells.Add(newcell)

            ' Instantiate text box
            textboxes(i) = New TextBox

            ' Set text box ID and caption
            textboxes(i).ID = "test" & i
            textboxes(i).Text = textboxes(i).ID

            ' Add text box to form control list and in table cell
            newcell.Controls.Add(textboxes(i))

        Next

    End Sub

    Protected Sub next_button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles next_button.Click

        textboxes(1).Text = "Changed!"
        textboxes(2).Text = textboxes(3).Text
    End Sub
End Class

