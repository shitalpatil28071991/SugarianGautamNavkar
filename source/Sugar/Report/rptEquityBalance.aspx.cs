﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;
using System.Configuration;
using System.Drawing.Printing;

using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Data.SqlClient;

public partial class Sugar_Report_rptEquityBalance : System.Web.UI.Page
{
    string fromdt = string.Empty;
    string todt = string.Empty;
    string mail = string.Empty;
    int company_code;
    int year_code;
    string unitno = string.Empty;
    string Accode = string.Empty;
    string company_name = string.Empty;
    string Email = string.Empty;
    string State = string.Empty;
    string Address = string.Empty;
    ReportDocument rprt1 = new ReportDocument();
    string datefrom = "";
    string dateto = "";
    string pagehead = string.Empty;

    string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                Accode = Request.QueryString["Ac_Code"];
                company_code = Convert.ToInt32(Session["Company_Code"].ToString());
                year_code = Convert.ToInt32(Session["year"].ToString());
                fromdt = Request.QueryString["fromdt"];
                todt = Request.QueryString["todt"];
                pagehead = "Equity Balance Register from " + fromdt + " To " + todt;

                datefrom = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                dateto = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

                DataTable dt = GetData(fromdt, todt);
                SqlDataAdapter da = new SqlDataAdapter();

                rprt1.Load(Server.MapPath("CryEquityBalance.rpt"));
                rprt1.SetDataSource(dt);
                CryEquityBalance.ReportSource = rprt1;

                company_name = Session["Company_Name"].ToString();
                // State = Session["state"].ToString();
                //Address = Session["address"].ToString();
                rprt1.DataDefinition.FormulaFields["company_name"].Text = "\"" + company_name + "\"";
                rprt1.DataDefinition.FormulaFields["pagehead"].Text = "\"" + pagehead + "\"";
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }


        }

        catch (Exception)
        {
            throw;
        }
    }
    private DataTable GetData(string fromdt, string todt)
    {
        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            //string qry = "SELECT     dbo.qryEquityPurchase.Doc_No, dbo.qryEquityPurchase.Tran_Type, dbo.qryEquityPurchase.Date, dbo.qryEquityPurchase.LedgerEntry, dbo.qryEquityPurchase.Script_Code, " +
            //         " dbo.qryEquityPurchase.Purc_No, dbo.qryEquityPurchase.Purc_Year_Code, dbo.qryEquityPurchase.Purc_Company_code, dbo.qryEquityPurchase.Qty, dbo.qryEquityPurchase.Bonus, " +
            //         " dbo.qryEquityPurchase.Rate, dbo.qryEquityPurchase.Value, dbo.qryEquityPurchase.Profit_Loss, dbo.qryEquityPurchase.Sale_To, dbo.qryEquityPurchase.Brokage, " +
            //         " dbo.qryEquityPurchase.Cess_Tax, dbo.qryEquityPurchase.STT, dbo.qryEquityPurchase.Service_Tax, dbo.qryEquityPurchase.Stamp_Charge, dbo.qryEquityPurchase.TurnOver_Tax," + 
            //         " dbo.qryEquityPurchase.Net_Value, dbo.qryEquityPurchase.Net_Rate, dbo.qryEquityPurchase.Narration, dbo.qryEquityPurchase.Company_Code, dbo.qryEquityPurchase.Year_Code, " +
            //         " dbo.qryEquityPurchase.Script_Name, dbo.qryEquityPurchase.FNO_Type, dbo.qryEquityPurchase.Ac_Name_E, CASE WHEN SUM(dbo.qryEquitysale.Qty) != 0 THEN SUM(dbo.qryEquitysale.Qty) " +
            //         " ELSE 0 END AS Sale, dbo.qryEquityPurchase.Qty - (CASE WHEN SUM(dbo.qryEquitysale.Qty) != 0 THEN SUM(dbo.qryEquitysale.Qty) ELSE 0 END) AS Balance, " +
            //         " CASE WHEN SUM(dbo.qryEquitysale.Bonus) != 0 THEN SUM(dbo.qryEquitysale.Bonus) ELSE 0 END AS SaleBonus, dbo.qryEquityPurchase.Bonus - (CASE WHEN SUM(dbo.qryEquitysale.Bonus) " +
            //         " != 0 THEN SUM(dbo.qryEquitysale.Bonus) ELSE 0 END) AS BalanceBonus " +
            //         " FROM         dbo.qryEquityPurchase LEFT OUTER JOIN " +
            //          " dbo.qryEquitysale ON dbo.qryEquityPurchase.Year_Code = dbo.qryEquitysale.Year_Code AND dbo.qryEquityPurchase.Company_Code = dbo.qryEquitysale.Company_Code AND " +
            //       "   dbo.qryEquityPurchase.Doc_No = dbo.qryEquitysale.Purc_No AND dbo.qryEquityPurchase.Company_Code = dbo.qryEquitysale.Purc_Company_code AND " +
            //         " dbo.qryEquityPurchase.Year_Code = dbo.qryEquitysale.Purc_Year_Code " +
            //       " where dbo.qryEquityPurchase.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            //    " and dbo.qryEquityPurchase.Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + 
            //        " GROUP BY dbo.qryEquityPurchase.Doc_No, dbo.qryEquityPurchase.Tran_Type, dbo.qryEquityPurchase.Date, dbo.qryEquityPurchase.LedgerEntry, dbo.qryEquityPurchase.Script_Code, " + 
            //        "  dbo.qryEquityPurchase.Purc_No, dbo.qryEquityPurchase.Purc_Year_Code, dbo.qryEquityPurchase.Purc_Company_code, dbo.qryEquityPurchase.Qty, dbo.qryEquityPurchase.Bonus, " + 
            //          "dbo.qryEquityPurchase.Rate, dbo.qryEquityPurchase.Value, dbo.qryEquityPurchase.Profit_Loss, dbo.qryEquityPurchase.Sale_To, dbo.qryEquityPurchase.Brokage,  " +
            //        "  dbo.qryEquityPurchase.Cess_Tax, dbo.qryEquityPurchase.STT, dbo.qryEquityPurchase.Service_Tax, dbo.qryEquityPurchase.Stamp_Charge, dbo.qryEquityPurchase.TurnOver_Tax,  " +
            //         " dbo.qryEquityPurchase.Net_Value, dbo.qryEquityPurchase.Net_Rate, dbo.qryEquityPurchase.Narration, dbo.qryEquityPurchase.Company_Code, dbo.qryEquityPurchase.Year_Code,  " +
            //         " dbo.qryEquityPurchase.Script_Name, dbo.qryEquityPurchase.FNO_Type, dbo.qryEquityPurchase.Ac_Name_E ";

            string qry = "SELECT     dbo.qryEquityPurchase.Doc_No, dbo.qryEquityPurchase.Tran_Type, dbo.qryEquityPurchase.Date, dbo.qryEquityPurchase.LedgerEntry, dbo.qryEquityPurchase.Script_Code, " +
                      " dbo.qryEquityPurchase.Purc_No, dbo.qryEquityPurchase.Purc_Year_Code, dbo.qryEquityPurchase.Purc_Company_code, dbo.qryEquityPurchase.Qty, dbo.qryEquityPurchase.Bonus, " +
                      " dbo.qryEquityPurchase.Rate, dbo.qryEquityPurchase.Value, dbo.qryEquityPurchase.Profit_Loss, dbo.qryEquityPurchase.Sale_To, dbo.qryEquityPurchase.Brokage, " +
                      " dbo.qryEquityPurchase.Cess_Tax, dbo.qryEquityPurchase.STT, dbo.qryEquityPurchase.Service_Tax, dbo.qryEquityPurchase.Stamp_Charge, dbo.qryEquityPurchase.TurnOver_Tax, " +
                      " dbo.qryEquityPurchase.Net_Value, dbo.qryEquityPurchase.Net_Rate, dbo.qryEquityPurchase.Narration, dbo.qryEquityPurchase.Company_Code, dbo.qryEquityPurchase.Year_Code, " +
                      " dbo.qryEquityPurchase.Script_Name, dbo.qryEquityPurchase.FNO_Type, dbo.qryEquityPurchase.Ac_Name_E, CASE WHEN SUM(dbo.qryEquitysale.Qty) != 0 THEN SUM(dbo.qryEquitysale.Qty) " +
                      " ELSE 0 END AS Sale, dbo.qryEquityPurchase.Qty - (CASE WHEN SUM(dbo.qryEquitysale.Qty) != 0 THEN SUM(dbo.qryEquitysale.Qty) ELSE 0 END) AS Balance, " +
                      " CASE WHEN SUM(dbo.qryEquitysale.Bonus) != 0 THEN SUM(dbo.qryEquitysale.Bonus) ELSE 0 END AS SaleBonus, dbo.qryEquityPurchase.Bonus - (CASE WHEN SUM(dbo.qryEquitysale.Bonus) " +
                      " != 0 THEN SUM(dbo.qryEquitysale.Bonus) ELSE 0 END) AS BalanceBonus" +
                      " FROM         dbo.qryEquityPurchase LEFT OUTER JOIN" +
                      " dbo.qryEquitysale ON dbo.qryEquityPurchase.Doc_No = dbo.qryEquitysale.Purc_No AND dbo.qryEquityPurchase.Company_Code = dbo.qryEquitysale.Purc_Company_code AND " +
                      " dbo.qryEquityPurchase.Year_Code = dbo.qryEquitysale.Purc_Year_Code" +
                      " where dbo.qryEquityPurchase.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                      " and dbo.qryEquityPurchase.Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + 
                      " GROUP BY dbo.qryEquityPurchase.Doc_No, dbo.qryEquityPurchase.Tran_Type, dbo.qryEquityPurchase.Date, dbo.qryEquityPurchase.LedgerEntry, dbo.qryEquityPurchase.Script_Code, " +
                      " dbo.qryEquityPurchase.Purc_No, dbo.qryEquityPurchase.Purc_Year_Code, dbo.qryEquityPurchase.Purc_Company_code, dbo.qryEquityPurchase.Qty, dbo.qryEquityPurchase.Bonus, " +
                      " dbo.qryEquityPurchase.Rate, dbo.qryEquityPurchase.Value, dbo.qryEquityPurchase.Profit_Loss, dbo.qryEquityPurchase.Sale_To, dbo.qryEquityPurchase.Brokage, " +
                      " dbo.qryEquityPurchase.Cess_Tax, dbo.qryEquityPurchase.STT, dbo.qryEquityPurchase.Service_Tax, dbo.qryEquityPurchase.Stamp_Charge, dbo.qryEquityPurchase.TurnOver_Tax, " +
                      " dbo.qryEquityPurchase.Net_Value, dbo.qryEquityPurchase.Net_Rate, dbo.qryEquityPurchase.Narration, dbo.qryEquityPurchase.Company_Code, dbo.qryEquityPurchase.Year_Code, " +
                      " dbo.qryEquityPurchase.Script_Name, dbo.qryEquityPurchase.FNO_Type, dbo.qryEquityPurchase.Ac_Name_E ";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
        }
        return dt;
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath=@"D:\pdffiles\cryChequePrinting.pdf";
            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename = filepath + "\\SupplierPurchaseOrder" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);

            //open PDF File

            //System.Diagnostics.Process.Start(filename);
            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(filename);

            if (FileBuffer != null)
            {

                Response.ContentType = "application/pdf";

                Response.AddHeader("content-length", FileBuffer.Length.ToString());

                Response.BinaryWrite(FileBuffer);

            }
        }
        catch (Exception e1)
        {
            Response.Write("PDF err:" + e1);
            return;
        }
        //   Response.Write("<script>alert('PDF successfully Generated');</script>");

    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath = @"D:\ashwini\bhavani10012019\accowebBhavaniNew\PAN\cryChequePrinting.pdf";
            //string filepath = @"E:\Lata Software Backup\accowebnavkar\PAN\Saudapending.pdf";
            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename = filepath + "\\SupplierPurchaseOrder_" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);
            //rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "SupplierPurchaseOrder";
                // Attachment attachment = new Attachment(Server.MapPath(filename), contentType);
                Attachment attachment = new Attachment(filename);
                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                EncryptPass enc = new EncryptPass();
                emailPassword = enc.Decrypt(emailPassword);
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "SupplierPurchaseOrder";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                msg.Subject = "No:";
                //msg.IsBodyHtml = true;
                if (smtpPort != string.Empty)
                {
                    SmtpServer.Port = Convert.ToInt32(smtpPort);
                }
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                SmtpServer.Send(msg);
                attachment.Dispose();
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                Response.Write("<script>alert('Mail Send successfully');</script>");
            }


        }
        catch (Exception e1)
        {
            Response.Write("Mail err:" + e1);
            return;
        }


    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        //rprt1.Close();
        //rprt1.Clone();
        //rprt1.Dispose();
        //GC.Collect();
        this.CryEquityBalance.ReportSource = null;

        CryEquityBalance.Dispose();

        if (rprt1 != null)
        {

            rprt1.Close();

            rprt1.Dispose();

            rprt1 = null;

        }

        GC.Collect();

        GC.WaitForPendingFinalizers();
    }
}