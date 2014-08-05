<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <h1 id="title" runat ="server" style="color: #FFFFFF; border-radius:10px 10px 0px 0px; margin-bottom: 20px; background-color: #172941;" 
            align="center">Upload Files</h1>
        Your Url: <asp:hyperlink id="Url" runat="server">HyperLink</asp:hyperlink>
        <div id = "Container" style="padding: 20px">
            <asp:datalist id="FileData" runat="server" RepeatColumns="10" CellPadding="4" 
                ForeColor="#333333">
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:datalist>
            <asp:fileupload id="FileUpload" runat="server">
            </asp:fileupload>
        <div id="DragDrop" style="margin: 20px auto 20px auto; width: 50%;"center">
            <div id="TextUpload" align="center"  style="margin: 0px auto 0px auto">
            Text/Url Upload:<asp:TextBox id="Text" runat="server">
            </asp:TextBox>
            </div>
        </div>
        <br />
        <div id = "CanisterInfo"style="width: 70%;margin: 20px auto 20px auto;" align="center">
            An innovative way to store and share your files simply, anonymously, and from anywhere.
            <br />
            <asp:Button id = "Button1" runat="server" Text="Save and Download Canister Credentials" 
                onclick="Unnamed1_Click" UseSubmitBehavior="False" />
        </div>
    </div>
        <!--<script type = "text/javascript">
            $(document).ready(function () {
    $("div .product").each(function () {
        this.addEventListener('dragstart', OnDragStart, false);
    });
 
    $("div .bag").each(function () {
        this.addEventListener('dragenter', OnDragEnter, false);
        this.addEventListener('dragleave', OnDragLeave, false);
        this.addEventListener('dragover', OnDragOver, false);
        this.addEventListener('drop', OnDrop, false);
        this.addEventListener('dragend', OnDragEnd, false);
    });
})
function OnDragStart(e) {
    this.style.opacity = '0.3';
    srcElement = this;
    e.dataTransfer.effectAllowed = 'move';
}

function OnDragOver(e) {
    if (e.preventDefault) {
        e.preventDefault();
    }
    e.dataTransfer.dropEffect = 'move';
    return false;
}

function OnDragEnter(e) {
}

function OnDragLeave(e) {
}

function OnDrop(e) {
    if (e.stopPropagation) {
        e.stopPropagation();
    }
    srcElement.style.opacity = '1';
}

function OnDragEnd(e) {
    this.style.opacity = '1';
}    
        </script>-->
</asp:Content>

