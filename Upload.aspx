<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1 id = "title" runat = "server" style="color: #FFFFFF; border-radius:10px 10px 0px 0px; margin-bottom: 20px; background-color: #172941;" 
    align="center">New Canister</h1>
    <asp:fileupload id="FileUpload" runat="server">
    </asp:fileupload>
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
    <asp:panel id="Panel" runat="server" Visible="False">
    <div id = "OverWrite" 
        
        
        style="border: thick dashed #172941; padding: 20px; background-color: #FFFFFF; width:300px; height:150px; ">
        <h1 style="color: #172941">Overwrite?</h1>
        <p>This file already exists. Would you like to overwrite it?</p>
        <asp:button runat="server" text="Yeah!" BackColor="#172941" 
            BorderColor="#244066" ForeColor="White" />
        <asp:button runat="server" text="Naw" BackColor="#172941" BorderColor="#244066" 
            ForeColor="White" />
        <asp:button runat="server" text="Cancel" BackColor="#172941" 
            BorderColor="#244066" ForeColor="White" />
    </div>
    </asp:panel>
</asp:Content>

