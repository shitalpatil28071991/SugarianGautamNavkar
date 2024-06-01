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
//using System.Printing;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using System.Net.Mail;


public partial class Sugar_Report_rptGroupPaindingBanance : System.Web.UI.Page
{
    int company_code;
    int year_code;
    string Ac_Code = string.Empty;
    string Mill_Code = string.Empty;
    string mail = string.Empty;
    ReportDocument rprt2 = new ReportDocument();
    ReportDocument rpt = new ReportDocument();
    string company_name = string.Empty;
    string Confim = string.Empty;
    string isAccounted = string.Empty;
    string Accounted = string.Empty;

    string Prosess = string.Empty;
    string isProsess = string.Empty;
    string calculateStock = string.Empty;
    string iscalculateStock = string.Empty;
    string radio = string.Empty;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    string AccountNumber= string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Ac_Code = Request.QueryString["Ac_Code"];
        Mill_Code = Request.QueryString["Mill_Code"];
        company_code = Convert.ToInt32(Session["Company_Code"].ToString());
        year_code = Convert.ToInt32(Session["year"].ToString());
        isAccounted = Request.QueryString["isAccounted"];
        Prosess = Request.QueryString["Prosess"];
        calculateStock = Request.QueryString["IsCalculated"];
        radio = Request.QueryString["radio"];
        FromDt = Request.QueryString["FromDt"];
        ToDt = Request.QueryString["ToDt"];
        //DataTable dt = new DataTable();
        company_name = Session["Company_Name"].ToString();
        DataTable dt = GetData();
        SqlDataAdapter da = new SqlDataAdapter();

        rpt.Load(Server.MapPath("~/Sugar/Report/cryGroupPaindingBanance.rpt"));
        rpt.SetDataSource(dt);

        cryGroupPaindingBanance.ReportSource = rpt;
        cryGroupPaindingBanance.RefreshReport();


    }

    private DataTable GetData()
    {

        if (isAccounted == "A")
        {
            Accounted = "";
        }
        else {
            Accounted = "and isAccounted='" + isAccounted + "'";
        }

        if (Prosess == "A")
        {
            isProsess = "";
        }
        else {
            isProsess = "and Prosess='"+ Prosess +"'";
        }

        if (calculateStock == "A")
        {
            iscalculateStock = "";
        }
        else
        {
            iscalculateStock = "and calculateStock='" + calculateStock + "'";
        }

        if (radio == "B")
        {
            if (Ac_Code == "0")
            {
                AccountNumber = "";
            }
            else
            {
                AccountNumber = "and Group_Account=" + Ac_Code;
            }
        }
        else if (radio == "A")
        {
            if (Ac_Code == "0")
            {
                AccountNumber = "";
            }
            else
            {
                AccountNumber = "and membercode=" + Ac_Code;
            }
        }
        else
        {
            AccountNumber = "";
        }

        if (Mill_Code == "0")
        {
            Mill_Code = "";
        }
        else {
            Mill_Code = "and Mill_Code=" + Mill_Code;
        }
        string datefrom = DateTime.Parse(FromDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string dateto = DateTime.Parse(ToDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            SqlCommand cmd = new SqlCommand("select * from qryGroupTenderHeadDetail where  Company_Code= '" + company_code + "' " + AccountNumber +
                        " " + Accounted + " " + isProsess + " " + iscalculateStock + " " + Mill_Code +
                              "  and Tender_Date between '" + datefrom + "' and '" + dateto + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            //}
            //else
            //{
            //    SqlCommand cmd = new SqlCommand("select Tender_No ,GroupName,Tender_dateConverted,Lifting_DateConverted,Grade,Mill_Rate,Quantal, millName,MillDeff, membername, sum(Buyer_Quantal) as Buyer_Quantal,sum(ItemAmount ) as ItemAmount,sum(profit) as profit , " +
            //                                    " sum(indivisulProfit) as indivisulProfit,sum(millContribution) as millContribution,sum(paidorreseive) as paidorreseive from qryGroupTenderHeadDetail " +
            //                                    " where Tender_No='" + Tender_No + "' and Company_Code= '" + company_code + "' and Year_Code= '" + year_code + "' " +
            //                                    " group by Tender_No,Quantal,Tender_dateConverted,Lifting_DateConverted,Grade,Mill_Rate,GroupName,membername,MillDeff,millName " +
            //                                    "  having sum(Buyer_Quantal)<>0", con);
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    sda.Fill(dt);
            //}



        }
        return dt;
    }

    protected void Print(object sender, EventArgs e)
    {

        // Refresh Report.
        rpt.Refresh();
        //PrintDialog myPrintDialog = new PrintDialog();
        rpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;

        // Set Paper Size.
        rpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

        rpt.PrintOptions.PrinterName = GetDefaultPrinter();

        rpt.PrintToPrinter(1, false, 0, 0);



    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath=@"D:\pdffiles\cryChequePrinting.pdf";
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\UTR.pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            //open PDF File
            //System.Diagnostics.Process.Start(pdfname);
            // string FilePath = Server.MapPath("javascript1-sample.pdf");

            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(pdfname);

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
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\UTR.pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "UTR";
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "UTR";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                msg.Subject = "DOC.No:";
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
                if (File.Exists(pdfname))
                {
                    File.Delete(pdfname);
                }
            }


        }
        catch (Exception e1)
        {
            Response.Write("Mail err:" + e1);
            return;
        }
        Response.Write("<script>alert('Mail Send successfully');</script>");

    }


    private string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();

        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
            {
                return printer;
            }
        }
        return string.Empty;
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        this.cryGroupPaindingBanance.ReportSource = null;

        cryGroupPaindingBanance.Dispose();

        if (rpt != null)
        {

            rpt.Close();

            rpt.Dispose();

            rpt = null;

        }

        GC.Collect();

        GC.WaitForPendingFinalizers();
    }
}