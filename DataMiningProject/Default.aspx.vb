Partial Public Class _Default
    Inherits System.Web.UI.Page

    ' Declare control arrays public for modification in event handlers
    Public textboxs() As TextBox
    Public dropdownlists() As DropDownList
    Public checkboxes() As CheckBox
    Public labels() As Label

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' Format panel
        mainpanel.Attributes("style") = "padding:10px;margin:10px;alignment=center;margin-left:auto;margin-right:auto;"

        layouttable.Rows.Add(New HtmlTableRow)

        ' Number of text boxes to generate
        Dim numtextboxes As Integer
        numtextboxes = 3

        ' Create array of type TextBox
        ReDim textboxs(numtextboxes)

        ' Run through text boxes
        For i = 1 To numtextboxes

            Dim newcell As New HtmlTableCell
            layouttable.Rows(0).Cells.Add(newcell)

            ' Instantiate text box
            textboxs(i) = New TextBox

            ' Set text box ID and caption
            textboxs(i).ID = "test" & i
            textboxs(i).Text = textboxs(i).ID

            ' Add text box to form control list and in table cell
            newcell.Controls.Add(textboxs(i))

        Next

    End Sub

    Protected Sub next_button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles next_button.Click

        textboxs(1).Text = "Changed!"

    End Sub
End Class

