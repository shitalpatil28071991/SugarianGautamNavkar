﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDateWiseSaleRegister1millwise.aspx.cs" Inherits="Sugar_Report_rptDateWiseSaleRegister1millwise" %>

<%@ Register Assembly="CrystalDecisions.Web,Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div>
            <asp:Button ID="btnPDF" runat="server" Text="Open PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnPDF_Click" />
            <asp:Button ID="btnMail" runat="server" Text="Mail PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnMail_Click" />
            <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>

            <CR:CrystalReportViewer ID="CrySaleReportMillWise" runat="server" AutoDataBind="true" BestFitPage="False" Width="1500px" />

        </div>
    </form>
</body>
</html>
