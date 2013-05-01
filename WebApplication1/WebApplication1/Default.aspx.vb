Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports System.Globalization
Imports FoldingTable

Public Class _Default
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Collection of top stores
    ''' </summary>
    Private _TopStore As TopStore

    ''' <summary>
    ''' Number of visible columns
    ''' </summary>
    Private _VisibleColumns As Integer = -1

    ''' <summary>
    ''' Stores the identifications of buttons.
    ''' <para>Will be used to enable/disable all buttons.</para>
    ''' </summary>
    Private _Buttons As System.Collections.Generic.LinkedList(Of [String])

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        '    gridStores.DataSource = TopStoreSimulated.GetData()

        '  gridStores.DataBind()
        Page_Load(Nothing, Nothing)
        '<asp:ObjectDataSource ID="odsStores" runat="server" EnableViewState="False" OldValuesParameterFormatString="original_{0}" OnObjectCreating="odsStores_ObjectCreating" OnObjectDisposing="odsStores_ObjectDisposing" OnSelecting="odsStores_Selecting" SelectCountMethod="GetRowCount" SelectMethod="GetData" TypeName="FoldingTable.TopStoreSimulated"></asp:ObjectDataSource>

    End Sub
    ''' <summary>
    ''' Page load.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(sender As Object, e As EventArgs)

        radSimulatedData.Visible = True
        radSimulatedData.Checked = True


        PrepareExpandableLines()
    End Sub

    ''' <summary>
    ''' Releases allocated memory
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_UnLoad(sender As Object, e As EventArgs)

        If _TopStore IsNot Nothing Then
            _TopStore = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Register the script to expand/contract details in order to show models
    ''' </summary>
    Private Sub PrepareExpandableLines()

        ' First, handle some specifics from Firefox
        ' Reference : IE/Firefox show/hide/colspan
        '             http://forums.codewalkers.com/client-side-things-82/ie-firefox-show-hide-colspan-902435.html

        Dim StyleDisplayBlock As String = "block"

        If Me.Request.Browser.Browser.ToLower() = "firefox" Then
            StyleDisplayBlock = "table-row"
        End If

        Dim js As String = [String].Format(CultureInfo.InvariantCulture, "var StyleDisplayBlock = '{0}';" & vbLf, StyleDisplayBlock)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.[GetType](), "StyleDisplayBlock", js, True)

        ' Then, include the script

        ScriptManager.RegisterClientScriptInclude(Me.Page, Me.Page.[GetType](), "expand_models", "script/ExpandPanel.js")
    End Sub

    ''' <summary>
    ''' Select simulated or data base stored data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub odsStores_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsStores.Selecting

        If e.ExecutingSelectCount Then
            Return
        End If

        ' If radSimulatedData.Checked Then
        'e.InputParameters("simulated") = True
        '  Else
        'e.InputParameters("simulated") = False
        '  End If
    End Sub

    ''' <summary>
    ''' Instantiates the business logic layer object, in order to fill the main grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub odsStores_ObjectCreating(sender As Object, e As ObjectDataSourceEventArgs) Handles odsStores.ObjectCreating

        Try
            _TopStore = New TopStore()

            e.ObjectInstance = _TopStore
        Catch ex As System.Exception

            ' TODO: Error LOG

            ShowErrorMessage()
        End Try
    End Sub

    ''' <summary>
    ''' This method is called when the object odsStores releases memory used by the business logic layer object.
    ''' <para>If some error was found when reading from database, this method will show the error message.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub odsStores_ObjectDisposing(sender As Object, e As ObjectDataSourceDisposingEventArgs) Handles odsStores.ObjectDisposing

        Dim topStore As TopStore = TryCast(e.ObjectInstance, TopStore)

        '     If topStore IsNot Nothing AndAlso topStore.[Error] Then
        ' ShowErrorMessage()
        ' gridStores.Visible = False
        ' End If

        e.Cancel = True
        ' topStore will be used in gridStores_RowDataBound
    End Sub

    ''' <summary>
    ''' Called for each row of the main grid, calls a method to prepare the details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub gridStores_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridStores.RowDataBound
        Try
            If e.Row.RowType <> DataControlRowType.DataRow Then
                Return
            End If


            Dim storeRow As Store = TryCast(e.Row.DataItem, Store)

            InsertModelsRow(e.Row, storeRow)
        Catch ex As System.Exception
            ' TODO: Error LOG

            ShowErrorMessage()
        End Try
    End Sub

    ''' <summary>
    ''' Called after the main grid was bound
    ''' <para>Will create a Javascrip array, used to enable/disable the buttons</para>
    ''' <para>See function EnableExpandButtons in ExpandPanel.js</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub gridStores_DataBound(sender As Object, e As EventArgs) Handles gridStores.DataBound

        If _Buttons IsNot Nothing AndAlso _Buttons.Count > 0 Then

            For Each element As String In _Buttons

                ' The array's name
                ' The button ID
                ScriptManager.RegisterArrayDeclaration(Me.Page, "ExpandButtons", [String].Format(CultureInfo.InvariantCulture, "'{0}'", element))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Prepares to show the top models for one store.
    ''' <para>A new hidden row is inserted below the current row in the grid.</para>
    ''' <para>That row will have only one cell, spanned to the entire row.</para>
    ''' <para>Inside the new row, a panel is inserted as a simple HTML 'fieldset', just to show a border and some header.</para>
    ''' <para>Inside the fieldset, a new panel is inserted. That panel will be filled with details when the user click the button.</para>
    ''' </summary>
    ''' <param name="gridViewRow">One row from the main grid</param>
    ''' <param name="storeRow">One store</param>
    Private Sub InsertModelsRow(gridViewRow As GridViewRow, storeRow As Store)

        ' Gets the index for the new row

        Dim htmlTab As Table = TryCast(gridStores.Controls(0), Table)

        Dim newRowIndex As Integer = htmlTab.Rows.GetRowIndex(gridViewRow) + 1

        ' Creates two panels : external and internal

        ' External panel : A simple <fieldset>
        Dim externalPanel As System.Web.UI.WebControls.Panel = New Panel()
        externalPanel.EnableViewState = False
        externalPanel.GroupingText = "Top models"
        externalPanel.ID = [String].Format(CultureInfo.InvariantCulture, "pModelE_{0}", newRowIndex)
        externalPanel.CssClass = "expand-painel"

        ' Internal panel : A <div> as a container for top models
        Dim internalPanel As System.Web.UI.WebControls.Panel = New Panel()
        internalPanel.EnableViewState = False
        internalPanel.ID = [String].Format(CultureInfo.InvariantCulture, "pModel_{0}", newRowIndex)

        externalPanel.Controls.Add(internalPanel)

        ' Inserts the panels in one cell

        Dim cell As New TableCell()
        'cell.HorizontalAlign = HorizontalAlign.Left;
        cell.BorderStyle = BorderStyle.None
        cell.Controls.Add(externalPanel)
        cell.ColumnSpan = VisibleColumns

        ' Inserts the cell in a collection of cell (only one cell)

        Dim cells As TableCell() = New TableCell(0) {}

        cells(0) = cell

        ' Inserts one row under the current row

        Dim newRow As New GridViewRow(newRowIndex, newRowIndex, DataControlRowType.DataRow, DataControlRowState.Normal)

        newRow.ID = [String].Format(CultureInfo.InvariantCulture, "linM_{0}", newRowIndex)

        newRow.Style.Add(HtmlTextWriterStyle.Display, "none")

        ' Inserts the cell in the new row

        newRow.Cells.AddRange(cells)

        ' Inserts the new row in the controls collection of the table (grid)
        Dim temp As String = newRow.ClientID
        htmlTab.ClientIDMode = UI.ClientIDMode.Static
        htmlTab.Controls.AddAt(newRowIndex, newRow)


        ' contextKey = CustomerId + radSimulatedData.Checked, separated by '-'
        Dim contextKey As String = [String].Format(CultureInfo.InvariantCulture, "{0}-{1}", storeRow.CustomerId, (If(radSimulatedData.Checked, 1, 0)))

        ' Locates the button and prepares the 'onclick' event

        Dim btn As Image = TryCast(gridViewRow.FindControl("imgModel"), Image)

        btn.Attributes("onClick") = [String].Format(CultureInfo.InvariantCulture, "ExpandModels('{0}', '{1}', '{2}', '{3}');", contextKey, newRow.ClientID, internalPanel.ClientID, btn.ClientID)

        ' Insert the ID into the buttons list

        If _Buttons Is Nothing Then
            _Buttons = New System.Collections.Generic.LinkedList(Of String)()
        End If

        _Buttons.AddLast(btn.ClientID)
    End Sub

    ''' <summary>
    ''' Number of visible columns
    ''' </summary>
    Private ReadOnly Property VisibleColumns() As Integer
        Get
            If _VisibleColumns = -1 Then
                _VisibleColumns = 0
                For i As Integer = 0 To gridStores.Columns.Count - 1
                    If gridStores.Columns(i).Visible Then
                        _VisibleColumns += 1
                    End If
                Next
            End If
            Return _VisibleColumns
        End Get
    End Property

    ''' <summary>
    ''' Shows a generic error message to the user
    ''' </summary>
    Private Sub ShowErrorMessage()

        lblMsgErroTitulo.Text = "ERROR"
        lblMsgErro.Text = "Can not query the database"
        popupMsgErro.Show()
    End Sub

    ''' <summary>
    ''' Builds a string to populate the expanded line with the models
    ''' </summary>
    ''' <param name="contextKey">CustomerId + radSimulatedData.Checked, separated by '-'</param>
    ''' <returns>HTML table filled with top models</returns>
    <System.Web.Services.WebMethod()> _
    <System.Web.Script.Services.ScriptMethod()> _
    Public Shared Function ExpandModelsService(contextKey As String) As String


        ' Small delay to provide time to show 'loading.gif'
        System.Threading.Thread.Sleep(2000)

        Dim sb As New StringBuilder()

        Dim topModels As System.Collections.ObjectModel.ReadOnlyCollection(Of Model) = Nothing

        ' Extracts CustomerID and 'radSimulatedData.Checked' from the context key

        Dim keys As String() = contextKey.Split("-"c)

        Dim simulatedData As Boolean = (keys(1)(0) = "1"c)

        Dim customerId As Integer = 0

        Dim hasError As Boolean = False

        If Not Int32.TryParse(keys(0), customerId) Then
            ' TODO: Error LOG
            hasError = True
            sb.Append("<span class=""error-models"">ERROR : Can not get the top models</span>")
        Else

            Try
                topModels = TopModel.GetTopModels(customerId, simulatedData)
            Catch ex As System.Exception
                ' TODO: Error LOG
                hasError = True
                sb.Append("<span class=""error-models"">ERROR : Can not get the top models</span>")
            End Try

            If Not hasError Then

                If topModels Is Nothing OrElse topModels.Count = 0 Then

                    sb.Append("<span class=""notfound-models"">Top models does not found for this store.</span>")
                Else
                    sb.Append(PopulateDetailsTable(topModels))
                End If
            End If
        End If

        If topModels IsNot Nothing Then
            topModels = Nothing
        End If

        Return sb.ToString()


    End Function

    ''' <summary>
    ''' Populates a HTML table with the top models data
    ''' </summary>
    ''' <param name="topModels">Data about the top models</param>
    ''' <returns>HTML table filled with top models</returns>
    Private Shared Function PopulateDetailsTable(topModels As System.Collections.ObjectModel.ReadOnlyCollection(Of Model)) As String

        Dim sb As New StringBuilder("<table class = 'tab-mod'> " & vbLf & "<tr> " & vbLf & "<th>Model</th> " & vbLf & "<th>Qty</th> " & vbLf & "<th>Total price</th> " & vbLf & "<th>Last sale</th> " & vbLf & "</tr> " & vbLf, topModels.Count * 180)

        For Each modelRow As Model In topModels

            sb.Append("<tr> " & vbLf)

            sb.AppendFormat(CultureInfo.CurrentCulture, "<td>{0}</td> ", modelRow.Name)

            sb.AppendFormat(CultureInfo.CurrentCulture, "<td class=""cellright"">{0}</td> ", modelRow.Qty)

            sb.AppendFormat(CultureInfo.CurrentCulture, "<td class=""cellright"">{0:n2}</td> ", modelRow.TotalPrice)

            sb.AppendFormat(CultureInfo.CurrentCulture, "<td class=""cellcenter"">{0}</td> ", modelRow.LastSale.ToShortDateString())

            sb.Append("</tr> " & vbLf)
        Next

        sb.Append("</table> " & vbLf)

        Return sb.ToString()
    End Function

End Class

