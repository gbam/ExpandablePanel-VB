<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="FoldingTable._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!--
  ----------------------------------------------------------------------------
  Project ExpandPanelGridView
  Sample C# code to show details in an expandable panel inside a GridView
 
  Written by Amauri Rodrigues - September 2008
  email : rramauri@hotmail.com
 
  Feel free to modify this peace of code - It is a sample.
  Feel free to use in commercial and non-commercial projects.
 
  Requires : Microsoft Ajax (http: www.asp.net/ajax)

  If you want to run this sample with data from AdventureWorks database :
  1) Download from http: http://www.codeplex.com/MSFTDBProdSamples
  2) Modify connectionString in Web.config :
     <connectionStrings>
       <add name="AdventureWorksConnectionString" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=AdventureWorks;User ID=sa"
         providerName="System.Data.SqlClient" />
     </connectionStrings>
   ----------------------------------------------------------------------------
-->
<head id="Head1" runat="server">
	<title>Expandable panel inside a GridView</title>
<script type="text/javascript">

    function gvrowtoggle(row) {
        try {
            row_num = row; //row to be hidden
            ctl_row = row - 1; //row where show/hide button was clicked
            rows = document.getElementById('<%= gridStores.ClientID %>').rows;
            rowElement = rows[ctl_row]; //elements in row where show/hide button was clicked
            img = rowElement.cells[0].firstChild; //the show/hide button

            if (rows[row_num].className !== 'hidden') //if the row is not currently hidden 
            //(default)...
            {
                rows[row_num].className = 'hidden'; //hide the row
                img.src = '../Images/detail.gif'; //change the image for the show/hide button
            }
            else {
                rows[row_num].className = ''; //set the css class of the row to default 
                //(to make it visible)
                img.src = '../Images/close.gif'; //change the image for the show/hide button
            }
        }
        catch (ex) { alert(ex) }
    }
</script>
<link type="text/css" rel="stylesheet" media="all" href="Stylesheet1.css">
</head>
<body>
	<h1>
		Top stores
	</h1>
	<h2>
		Click the button on the first column to see the top models
	</h2>
	<form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" />
		<div class="block">
			<asp:RadioButton ID="radSimulatedData" runat="server" AutoPostBack="true" Checked="true" EnableViewState="false" GroupName="DataSource" Text="Simulated data" ToolTip="Check to see simulated data" />
			<asp:RadioButton ID="radSqlData" runat="server" AutoPostBack="true" Checked="false" EnableViewState="false" GroupName="DataSource" Text="SQL Server data" ToolTip="Check to see data from SQL Server database" />
			<br />
			<br />
		</div>
		<div id="panel-grid">
			<asp:GridView ID="gridStores" runat="server" AllowSorting="False" AutoGenerateColumns="False" CssClass="grid-main" DataSourceID="odsStores" EnableViewState="False" OnDataBound="gridStores_DataBound" OnRowDataBound="gridStores_RowDataBound">
				<Columns>
					<asp:TemplateField ShowHeader="False">
						<ItemTemplate>
							<asp:Image ID="imgModel" runat="server" AlternateText="Click to see the top models" CssClass="img-details" EnableViewState="False" ImageUrl="~/img/detail.gif" />
						</ItemTemplate>
						<ItemStyle CssClass="grid-main-detail" />
					</asp:TemplateField>
					<asp:BoundField DataField="StoreName" HeaderText="Store" ReadOnly="True" />
					<asp:BoundField DataField="Sales" DataFormatString="{0:n2}" HeaderText="Sales" HtmlEncode="False" ReadOnly="True">
						<ItemStyle CssClass="grid-main-celr" />
					</asp:BoundField>
				</Columns>
			</asp:GridView>
			<asp:ObjectDataSource ID="odsStores" runat="server" EnableViewState="False" OnObjectCreating="odsStores_ObjectCreating" OnObjectDisposing="odsStores_ObjectDisposing" OnSelecting="odsStores_Selecting" SelectCountMethod="GetRowCount" SelectMethod="GetData" TypeName="FoldingTable.TopStore"></asp:ObjectDataSource>
		</div>
		<div class="block">
		<br />
		<a href="http://www.codeproject.com/KB/ajax/ExpandPanelGridView.aspx">Visit the Codeproject site and read the article.</a>
		</div>
		<!-- DynamicPopulateExtender and Panel to show top models for each store -->
		<cc1:DynamicPopulateExtender ID="dinPopMod" runat="server" BehaviorID="dpBehaviorMod" CacheDynamicResults="false" ClearContentsDuringUpdate="true" EnableViewState="False" ServiceMethod="ExpandModelsService" TargetControlID="painelAux">
		</cc1:DynamicPopulateExtender>
		<asp:Panel ID="painelAux" runat="server" EnableViewState="false" Style="background-image:url('img/loading.gif'); background-repeat: no-repeat; display: none;">
		</asp:Panel>
		<!-- Error messages - BEGIN  -->
		<asp:Panel ID="painelMsgErro" runat="server" CssClass="painelMsgErro" Style="display: none;">
			<div class="painelMsgErroTopo">
				<asp:Label ID="lblMsgErroTitulo" runat="server" Text="Label">TITLE</asp:Label>
			</div>
			<div class="painelMsgErroMsg">
				<asp:Label ID="lblMsgErro" runat="server" Text="Label">Message</asp:Label>
			</div>
			<div class="painelMsgErroBotoes">
				<asp:LinkButton ID="btnErroOk" runat="server">OK</asp:LinkButton>
			</div>
		</asp:Panel>
		<asp:Button ID="hiddenTargetControlForModalPopup" runat="server" CssClass="painelMsgErroBotoes" Style="display: none;" ToolTip="Click to close this panel" />
		<cc1:ModalPopupExtender ID="popupMsgErro" runat="server" DropShadow="true" OkControlID="btnErroOk" PopupControlID="painelMsgErro" TargetControlID="hiddenTargetControlForModalPopup" Y="300">
		</cc1:ModalPopupExtender>
		<!-- Error messages - END  -->
	</form>
</body>
</html>
