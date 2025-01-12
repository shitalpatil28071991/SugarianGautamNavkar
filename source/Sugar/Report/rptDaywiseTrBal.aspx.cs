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

public partial class Sugar_Report_rptDaywiseTrBal : System.Web.UI.Page
{
    string fromdt = string.Empty;
    string todt = string.Empty;
    string mail = string.Empty;
    int company_code;
    int year_code;
    string unitno = string.Empty;
    string ac_code = string.Empty;
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

                ac_code = Request.QueryString["accode"];
                company_code = Convert.ToInt32(Session["Company_Code"].ToString());
                year_code = Convert.ToInt32(Session["year"].ToString());
                fromdt = Request.QueryString["fromDt"];
                todt = Request.QueryString["ToDt"];
                pagehead = "Do Cash Diff Report from " + fromdt + " To " + todt;


                datefrom = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                dateto = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                company_name = Session["Company_Name"].ToString();
                DataTable dt = GetData(datefrom, dateto);
                SqlDataAdapter da = new SqlDataAdapter();




                rprt1.Load(Server.MapPath("Crydaywisetrialbalance.rpt"));
                rprt1.SetDataSource(dt);
                CryDaywisetrbal.ReportSource = rprt1;

                company_name = Session["Company_Name"].ToString();
                // State = Session["state"].ToString();
                //Address = Session["address"].ToString();
                rprt1.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
                rprt1.DataDefinition.FormulaFields["pagehead"].Text = "\"" + pagehead + "\"";
                rprt1.DataDefinition.FormulaFields["mydate"].Text = "\"" + fromdt + "\"";
                CryDaywisetrbal.RefreshReport();
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
        string qry = "";
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            //if (ac_code != "0")
            //{
            //    SqlCommand cmd = new SqlCommand("select * from qrydohead where  doc_date between '" + fromdt +
            //       "' and '" + todt + "' and SaleBillTo='" + ac_code + "' and Company_Code=" + company_code + " and Year_Code=" + year_code + " ", con);
            //    cmd.CommandType = CommandType.Text;
            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    sda.Fill(dt);
            //}
            //else
            //{
            qry="SELECT     SUM(CASE WHEN DOC_DATE < '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'D' THEN AMOUNT ELSE - amount END ELSE 0 END) AS opening, "+
                     " SUM(CASE WHEN DOC_DATE = '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'D' THEN AMOUNT ELSE 0 END ELSE 0 END) AS debit, "+
                     " SUM(CASE WHEN DOC_DATE = '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'C' THEN AMOUNT ELSE 0 END ELSE 0 END) AS credit, "+
                     " SUM(CASE WHEN DOC_DATE < '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'D' THEN AMOUNT ELSE - amount END ELSE 0 END) "+
                     " + SUM(CASE WHEN DOC_DATE = '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'D' THEN AMOUNT ELSE 0 END ELSE 0 END) "+
                     " - SUM(CASE WHEN DOC_DATE = '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'C' THEN AMOUNT ELSE 0 END ELSE 0 END) AS balance, dbo.nt_1_gledger.AC_CODE, "+
                     " dbo.nt_1_accountmaster.Ac_Name_E, dbo.nt_1_citymaster.city_name_e "+
                     " FROM         dbo.nt_1_gledger INNER JOIN "+
                     " dbo.nt_1_accountmaster ON dbo.nt_1_gledger.ac = dbo.nt_1_accountmaster.accoid INNER JOIN "+
                     " dbo.nt_1_citymaster ON dbo.nt_1_accountmaster.cityid = dbo.nt_1_citymaster.cityid "+
                     " GROUP BY dbo.nt_1_gledger.AC_CODE, dbo.nt_1_accountmaster.Ac_Name_E, dbo.nt_1_accountmaster.Group_Code, dbo.nt_1_citymaster.city_name_e "+
                     " HAVING     (dbo.nt_1_accountmaster.Group_Code IN (23, 24)) AND (SUM(CASE WHEN DOC_DATE < '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'D' THEN AMOUNT ELSE - amount END ELSE 0 END) "+
                     " + SUM(CASE WHEN DOC_DATE = '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'D' THEN AMOUNT ELSE 0 END ELSE 0 END) "+
                     " - SUM(CASE WHEN DOC_DATE = '" + fromdt + "' THEN CASE WHEN dbo.nt_1_gledger.DRCR = 'C' THEN AMOUNT ELSE 0 END ELSE 0 END) <> 0) ";
            SqlCommand cmd;
            cmd = new SqlCommand(qry , con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            //}
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
            string filename = filepath + "\\SaleBillBalancenew" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
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
            string filename = filepath + "\\SaleBillBalance" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);
            //rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "SaleBillBalance";
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
                msg.Body = "SaleBillBalance";
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

 
}