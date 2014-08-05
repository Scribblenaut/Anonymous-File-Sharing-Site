<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body style="padding: 0px; margin:0px;">
    <form id="form1" runat="server">
    <div style="height: 52px; width: 100%; background-color: #172941;border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #2D3B4D;">
    <div id = "Logo" 
            style="width: 300px; height: 52px; color: #FFFFFF; float: left; line-height: 52px; " 
            align="right">Pseudo Anonymous</div>
        <div id ="Menu" style="float: left; margin-right: 5px; margin-left: 15px;">
        <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="About" Value="About"></asp:MenuItem>
                <asp:MenuItem Text="Stash" Value="Stash"></asp:MenuItem>
                <asp:MenuItem Text="Donate" Value="Donate"></asp:MenuItem>
            </Items>
        </asp:Menu>
        </div>
    </div>
    <div id="Intro" style="background-color: #172941; width: 100%; color: #FFFFFF;" 
        align="right">
        <br />
        <h1 style="padding: 0px; margin: 0px; margin-top: -20px; padding-top: 70px; padding-left: 400px; padding-right: 40px;">Store all your files anonymously and easily</h1>
        <p style="font-size: 21px; padding-left: 400px; padding-right: 40px; margin:0px;padding-bottom: 100px; "> With Psuedo Anonymous, your privacy is of upmost importance. Here, You are completely anonymous.
         You can upload all your files without registering. We don't record ip adresses or use cookies. Your files are private(unless mark them as public) and encrypted.</p>
    </div>
    <div id = "Second" 
        style="background-color: #dbdbc1; width: 100%; color: #000000; ">
        <h1 style="margin: 0px; padding: 0px; margin-top: -20px; padding-top: 70px; padding-left: 40px; padding-right: 400px;">Get instant acess anywhere</h1>
        <p style="font-size: 21px; padding-left:40px; padding-right:400px;">When you store your files with pseudo anonymous, you can acess and share your files in any way imaginable. You get a link to your canister and files for easy sharing.
         You can also acess send, retrieve and modify your files using any of these methods: </p>
         <ul style="margin: 0px; padding-bottom:100px">
         <li>In your canister webpage.</li>
         <li>Through email.</li>
         <li>Through text messages.</li>
         </ul>
    </div>
    <div id = "Third" 
        style="padding: 40px 0px; background-color:#5a9da3 ; text-align: center; width: 100%; color: #000000;">
        <p align="center" style="color: #FFFFFF; display: inline-block;">
    Want to get started? Click the button below to create a new canister and upload your files.</p><br />
    <a id = "NewCanister" href="Default.aspx" runat = "server"
            style="margin: 0px auto 0px auto; border: 1px solid #FFFFFF; display: inline-block; padding: 5px 10px 5px 10px; border-radius:5px; color: #FFFFFF; display: inline-block; text-decoration: none;">Generate Canister</a>
    </div>
    <div id = "Footer" 
        
        style="background-color: #172941; width: auto; color: #FFFFFF; height: 200px; " 
        align="center">
        <div id = "FooterContent" align="center" style="margin: 0px auto 0px auto; display: inline-block;">
        <asp:Menu ID="Menu2" runat="server" Orientation="Horizontal" align="center">
            <Items>
                <asp:MenuItem Text="About" Value="About"></asp:MenuItem>
                <asp:MenuItem Text="Contact" Value="Contact"></asp:MenuItem>
                <asp:MenuItem Text="Legal" Value="Legal"></asp:MenuItem>
            </Items>
        </asp:Menu>
    </div>
    </div>
    </form>
</body>
</html>
