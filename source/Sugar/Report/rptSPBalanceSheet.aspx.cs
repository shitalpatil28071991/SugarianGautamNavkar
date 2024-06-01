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


public partial class Sugar_Report_rptSPBalanceSheet : System.Web.UI.Page
{
    string AL1 = string.Empty;
    string AL2 = string.Empty;
    string AL3 = string.Empty;
    string AL4 = string.Empty;
    string other = string.Empty;
    string bankdetail = string.Empty;

    string podetail = string.Empty;
    string no = string.Empty;
    string grade = string.Empty;

    string grade1 = "";
    string deliverytype = string.Empty;
    string salerate = string.Empty;
    string salerate1 = string.Empty;
    string commission = string.Empty;
    string Sale_Rate = string.Empty;
    string Sale_Rate1 = "";

    string billto = string.Empty;
    int company_code;
    int year_code;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    string ac_code;
    string utr_no;
    string salebillparty;
    string AcType = string.Empty;
    string mail = string.Empty;
    string mailnew = string.Empty;
    string doc_no = string.Empty;
    string season = string.Empty;
    string FssaiNO = string.Empty;
    string TinNo = string.Empty;
    string GSTNO = string.Empty;
    string panno = string.Empty;
    string eway = string.Empty;
    string Inv = string.Empty;
    string sbno = string.Empty;
    string lorryno = string.Empty;
    string cornumber = string.Empty;
    string purno = string.Empty;
    string chkwaybillno = string.Empty;
    string transoprt;
    string millinvoice = string.Empty;
    string netqntl = string.Empty;
    string dono = string.Empty;

    string cornumberChecking = string.Empty;
    ReportDocument rprt1 = new ReportDocument();
    ReportDocument rprt2 = new ReportDocument();
    ReportDocument rpt = new ReportDocument();
    string company_name = string.Empty;
    string pagehead = string.Empty;
    string singleitem = "1";
    string millname = string.Empty;
    string partyname = string.Empty;
    string Cwhere = string.Empty;
    string date = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            date=Request.QueryString["Cwhere"];
            Cwhere = DateTime.Parse(date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            company_code = Convert.ToInt32(Session["Company_Code"].ToString());
            year_code = Convert.ToInt32(Session["year"].ToString());
            ViewState["BalDate"] = Cwhere;
            pagehead = "Balance Sheet  from " + DateTime.Parse(ViewState["BalDate"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
              company_name = Session["Company_Name"].ToString();
             DataTable dt = GetData();


            if (!IsPostBack)
            {

                //  txtEmail.Text = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + salebillparty + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            }
            SqlDataAdapter da = new SqlDataAdapter();
            rpt.Load(Server.MapPath("cryBalanceSheet.rpt"));
            rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
            rpt.DataDefinition.FormulaFields["pagehead"].Text = "\"" + pagehead + "\"";
        
            rpt.SetDataSource(dt);



            cryBalanceSheet.ReportSource = rpt;
            //rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
            //rpt.DataDefinition.FormulaFields["printhead"].Text = "\"Y\"";
            //rpt.DataDefinition.FormulaFields["docno"].Text = "\"" + "SB" + sbno + "\"";
            //rpt.DataDefinition.FormulaFields["grade"].Text = "\"" + grade + "\"";
            //rpt.DataDefinition.FormulaFields["grade1"].Text = "\"" + grade1 + "\"";
            //rpt.DataDefinition.FormulaFields["Title"].Text = "\"" + Title + "\"";
            //rpt.DataDefinition.FormulaFields["podetail"].Text = "\"" + podetail + "\"";
            //string imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
            //     + Session["Company_Code"].ToString() + "'");
            //String path = Server.MapPath("") + "\\" + imagepath;

            //imagepath = path.Replace("Report", "Images");

            //string logo = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='S' and Company_Code='"
            // + Session["Company_Code"].ToString() + "'");
            //String logopath = Server.MapPath("") + "\\" + logo;

            //logo = logopath.Replace("Report", "Images");

            //rpt.DataDefinition.FormulaFields["img"].Text = "\"" + imagepath + "\"";
            //rpt.DataDefinition.FormulaFields["logo"].Text = "\"" + logo + "\"";

            cryBalanceSheet.RefreshReport();
        }
        catch (Exception)
        {

            throw;
        }

    }
    private DataTable GetData()
    {
        try
        {
            DataTable dt = new DataTable();
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("SELECT     dbo.nt_1_bsgroupmaster.group_Code, dbo.nt_1_bsgroupmaster.group_Name_E, dbo.nt_1_bsgroupmaster.group_Type,"+
                      " SUM(CASE WHEN nt_1_gledger.DRCR = 'D' THEN nt_1_gledger.amount ELSE - nt_1_gledger.amount END) AS BALANCE, dbo.nt_1_bsgroupmaster.Company_Code"+
                      " FROM dbo.nt_1_bsgroupmaster INNER JOIN"+
                      " dbo.nt_1_accountmaster ON dbo.nt_1_bsgroupmaster.group_Code = dbo.nt_1_accountmaster.Group_Code AND "+
                      " dbo.nt_1_bsgroupmaster.Company_Code = dbo.nt_1_accountmaster.company_code INNER JOIN"+
                      " dbo.nt_1_gledger ON dbo.nt_1_accountmaster.Ac_Code = dbo.nt_1_gledger.AC_CODE AND dbo.nt_1_accountmaster.company_code = dbo.nt_1_gledger.COMPANY_CODE"+
                      " WHERE     (dbo.nt_1_bsgroupmaster.group_Type = 'B') and dbo.nt_1_bsgroupmaster.Company_Code=" + company_code + " and nt_1_gledger.doc_date<='" + Cwhere + "' " +
                      " GROUP BY dbo.nt_1_bsgroupmaster.group_Code, dbo.nt_1_bsgroupmaster.group_Name_E, dbo.nt_1_bsgroupmaster.group_Type, dbo.nt_1_bsgroupmaster.Company_Code "+
                      " having SUM(CASE WHEN nt_1_gledger.DRCR = 'D' THEN nt_1_gledger.amount ELSE - nt_1_gledger.amount END)!=0 ", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            return dt;
        }
        catch
        {
            return null;
        }
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
            string pdfname = filepath + "\\Salebill" + sbno + "-" + lorryno + ".pdf";
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
            string pdfname = filepath + "\\SaleBillNO" + sbno + "" + lorryno + ".pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty && txtEmail.Text != "0")
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                if (lorryno != string.Empty)
                {
                    lorryno = "Lorry:" + lorryno;
                }
                if (netqntl != string.Empty)
                {
                    netqntl = "NETQNTL:" + netqntl;
                }
                if (partyname != string.Empty)
                {
                    partyname = "billtoname:" + partyname;
                }
                if (dono != string.Empty)
                {
                    dono = "DO_No:" + dono;
                }


                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "Salebill_DocNo:" + sbno + "-" + lorryno;
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "Salebill";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                //msg.Subject = "DOC.No:";
                msg.Subject = "Bill No:" + sbno + " " + lorryno + " " + netqntl + " " + partyname + " " + dono;

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
                Response.Write("<script>alert('Mail Send successfully');</script>");
            }
            else
            {
                Response.Write("<script>alert('Enter Email Id');</script>");
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
        this.cryBalanceSheet.ReportSource = null;

        cryBalanceSheet.Dispose();

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