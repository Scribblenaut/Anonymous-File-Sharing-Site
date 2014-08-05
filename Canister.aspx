<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Canister.aspx.cs" Inherits="Canister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 id = "CanisterId" style="color: #FFFFFF; border-radius:10px 10px 0px 0px; margin-bottom: 20px; background-color: #172941;" align="center" runat = "server"></h1>
    <div id = "Container" style="padding: 40px" >
            
        <asp:GridView id="FilesGrid" 
            style="margin: 0px auto 0px auto; display: inline-block;" runat="server" DataSourceID="LinqDataSource1" 
            AllowPaging="True" AutoGenerateColumns="False" 
            onrowcommand="FilesGrid_RowCommand" CellPadding="4" ForeColor="#333333" 
            GridLines="None" EmptyDataText="No Files">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate> 
                        <asp:Button id="Button1" runat="server" Text="Download" onclick="Button1_Click"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FileName" HeaderText="FileName" ReadOnly="True" 
                    SortExpression="FileName" />
                <asp:TemplateField HeaderText="FileID" SortExpression="FileID">
                    <ItemTemplate>
                    <asp:HyperLink runat="server"><asp:Label ID="Label1" runat="server" Text='<%# Bind("FileID") %>'></asp:Label></asp:HyperLink>
                        
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("FileID") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FileLocation" HeaderText="FileLocation" 
                    ReadOnly="True" SortExpression="FileLocation" />
                <asp:BoundField DataField="FileType" HeaderText="FileType" ReadOnly="True" 
                    SortExpression="FileType" />
                <asp:BoundField DataField="FileSize" HeaderText="FileSize" ReadOnly="True" 
                    SortExpression="FileSize" />
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <sortedascendingcellstyle backcolor="#E9E7E2" />
            <sortedascendingheaderstyle backcolor="#506C8C" />
            <sorteddescendingcellstyle backcolor="#FFFDF8" />
            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
            ContextTypeName="DataClassesDataContext" EntityTypeName="" Select="new (FileID, FileName, FileLocation, FileType, FileSize)" 
            TableName="Files" Where="CanisterID == Guid(@CanisterID)">
            <whereparameters>
                <asp:QueryStringParameter DbType="Guid" Name="CanisterID" 
                    QueryStringField="CanisterID" />
            </whereparameters>
        </asp:LinqDataSource>
        <asp:button runat="server" text="DownloadMarked" />
        <asp:button runat="server" text="DeleteMarked" />
        <asp:button runat="server" text="Upload" onclick="Unnamed3_Click" />
        <!--<script type="text/javascript">
            var Rename = document.getElementById("Rename");
            var Delete = document.getElementById("Delete");
            var Download = document.getElementById("Download");
            var DropDown = document.getElementById('<%=DropDownList.ClientID %>')

            if (DropDown.value == 'Rename') {
                Rename.style.display = 'block';
                Delete.style.display = 'none';
                Download.style.display = 'none';

            }
            if (DropDown.value == 'Delete') {
                Rename.style.display = 'none';
                Delete.style.display = 'block';
                Download.style.display = 'none';
                
            }
            if (DropDown.value == 'Download') {
                Rename.style.display = 'none';
                Delete.style.display = 'none';
                Download.style.display = 'block';
                
            }
        </script>-->
        <!--<div id ="action" align="left" style="padding: 40px">

            Edit Marked Files: <asp:DropDownList id="DropDownList" 
                runat="server" onselectedindexchanged="DropDownList_SelectedIndexChanged">
                <asp:ListItem>Rename</asp:ListItem>
                <asp:ListItem>Download</asp:ListItem>
                <asp:ListItem>Delete</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id = "Rename" style="display: none">
            New Name: 
            <asp:TextBox id="NewName" runat="server">
            </asp:TextBox>
            <asp:Button id="SubmitNewName" runat="server" Text="Submit" 
                onclick="SubmitNewName_Click" />
        </div>
        <div id = "Download" style="display: none">
            <asp:Button id="DownloadMarked" runat="server" Text="Download Marked Files" 
                onclick="DownloadMarked_Click" />
        </div>
        <div id = "Delete" style="display: none">
            <asp:Button id="DeleteMarked" runat="server" Text="Delete Marked Files" 
                onclick="DeleteMarked_Click" />
        </div>-->

    </div>
</asp:Content>

