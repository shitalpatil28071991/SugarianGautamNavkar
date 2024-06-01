﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data;

public partial class Report_rptmillwisepurchesdispnew : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string Mill_Code = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string f = "../GSReports/DispMillWise_.htm";
    string f_Main = "../Report/DispMillWise_";
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    DataSet dsbind = new DataSet();
    DataSet dsitembind = new DataSet();
    DataTable ddate = new DataTable();
    double grandqntlTotal = 0.00;
    double grandamounttotal = 0.00;
    double grandtotalTDSAmt = 0.00;
    double grandtotalCGSTAmt = 0.00;
    double grandtotalSGSTAmt = 0.00;
    double grandtotalIGSTAmt = 0.00;
    double grandtotalBillAmt = 0.00;
    double grandtotalTCSAmt = 0.00;
    double grandtotalTCSNetpayAmt = 0.00;
    protected void Page_Load(object sender, EventArgs e)
    {

        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Mill_Code = Request.QueryString["Mill_Code"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        string isAuthenticate = string.Empty;
        string user = string.Empty;
        user = Session["user"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Mill_Code"]))
                {
                    string millCode = Request.QueryString["Mill_Code"].ToString();
                    txtEmail.Text = clsCommon.getString("Select Email_Id from " + tblPrefix + "AccountMaster where Ac_Code=" + millCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                }
                if (string.IsNullOrEmpty(Request.QueryString["Mill_Code"]))
                {

                    qry = "select distinct mill_code as millcode,isnull(millshortname,millname) as mill,millname from qrypurchaseheaddetail d " +
               " where  d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and PURCNO!=0 order by millname";

                }
                else
                {

                    qry = "select distinct mill_code as millcode,isnull(millshortname,millname) as mill,millname from qrypurchaseheaddetail d " +
                 " where  d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and mill_code=" + Mill_Code + "   and PURCNO!=0 order by millname";

                }


                dsbind = clsDAL.SimpleQuery(qry);

                if (string.IsNullOrEmpty(Request.QueryString["Mill_Code"]))
                {


                    qry = "select doc_no as do_no,doc_dateConverted as do_date,isnull(Quantal,0) as Quantal, isnull(NETQNTL,0) as millrate,isnull(subTotal,0) as subtotal,isnull(Bill_Amount,0) AS amount," +
          " suppliername as getpass,LORRYNO as truck,mill_code, isnull(TDS_Amt,0) as TDS_Amt ,isnull(CGSTAmount,0) as CGSTAmount ,isnull(SGSTAmount,0) as SGSTAmount,isnull(IGSTAmount,0) as IGSTAmount,isnull(Bill_Amount,0) as Bill_Amount  ,isnull(TCS_Amt,0) as TCS_Amt ,isnull(TCS_Net_Payable,0) as TCS_Net_Payable from qrypurchaseheaddetail "
            + " where doc_date between'" + fromDT + "' and '" + toDT + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and PURCNO!=0 ";

                }
                else
                {

                    qry = "select doc_no as do_no,doc_dateConverted as do_date,isnull(Quantal,0) as Quantal, isnull(NETQNTL,0) as millrate,isnull(subTotal,0) as subtotal,isnull(Bill_Amount,0) AS amount," +
          " suppliername as getpass,LORRYNO as truck,mill_code, isnull(TDS_Amt,0) as TDS_Amt ,isnull(CGSTAmount,0) as CGSTAmount ,isnull(SGSTAmount,0) as SGSTAmount,isnull(IGSTAmount,0) as IGSTAmount,isnull(Bill_Amount,0) as Bill_Amount  ,isnull(TCS_Amt,0) as TCS_Amt ,isnull(TCS_Net_Payable,0) as TCS_Net_Payable from qrypurchaseheaddetail "
             + " where doc_date between'" + fromDT + "' and '" + toDT + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and mill_code=" + Mill_Code + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and PURCNO!=0 ";



                }

                dsitembind = clsDAL.SimpleQuery(qry);
                if (dsbind != null)
                {
                    ddate = dsbind.Tables[0];
                    if (ddate.Rows.Count > 0)
                    {
                        datalist.DataSource = ddate;
                        datalist.DataBind();

                        lblgrandqntltotal.Text = grandqntlTotal.ToString();
                        lblgrandamounttotal.Text = grandamounttotal.ToString();
                        lblgrandtotalTDSAmt.Text = grandtotalTDSAmt.ToString();
                        lblgrandtotalCGSTAmt.Text = grandtotalCGSTAmt.ToString();
                        lblgrandtotalSGSTAmt.Text = grandtotalSGSTAmt.ToString();
                        lblgrandtotalIGSTAmt.Text = grandtotalIGSTAmt.ToString();
                        lblgrandtotalBillAmt.Text = grandtotalBillAmt.ToString();
                        lblgrandtotalTCSAmt.Text = grandtotalTCSAmt.ToString();
                        lblgrandtotalTCSNetpayAmt.Text = grandtotalTCSNetpayAmt.ToString();

                    }
                }
            }
        }

    }

    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            if (!string.IsNullOrEmpty(Request.QueryString["Mill_Code"]))
            {
                string millCode = Request.QueryString["Mill_Code"].ToString();
                txtEmail.Text = clsCommon.getString("Select Email_Id from " + tblPrefix + "AccountMaster where Ac_Code=" + millCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
            }

            if (string.IsNullOrEmpty(Branch_Code))
            {
                if (string.IsNullOrEmpty(Request.QueryString["Mill_Code"]))
                {
                    qry = "select distinct mill_code as millcode,isnull(millshortname,millname) as mill,millname from qrypurchaseheaddetail d" +
                          "  where  d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.PURCNO!=0 order by millname";

                }
                else
                {
                    qry = "select distinct mill_code as millcode,isnull(millshortname,millname) as mill,millname from qrypurchaseheaddetail d" +
                            "  where d.mill_code=" + Mill_Code + "  and d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.PURCNO!=0 order by millname";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Request.QueryString["Mill_Code"]))
                {
                    qry = "select distinct mill_code as millcode,isnull(millshortname,millname) as mill,millname from qrypurchaseheaddetail d" +
                          " where  d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and d.PURCNO!=0 order by mill";

                }
                else
                {
                    qry = "select distinct mill_code as millcode,isnull(millshortname,millname) as mill,millname from qrypurchaseheaddetail d" +
                            "  where d.mill_code=" + Mill_Code + "  and d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and d.PURCNO!=0 order by mill";
                }
            }
            ds = new DataSet();
            //ds = obj.GetDataSet(qry);
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        datalist.DataSource = dt;
                        datalist.DataBind();
                    }
                    else
                    {
                        datalist.DataSource = null;
                        datalist.DataBind();
                    }
                }
            }
            // }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            clsDAL.CloseConnection();
        }

    }
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataTable dtlistbind = new DataTable();
            dtlistbind = dsitembind.Tables[0];

            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblMillCode = (Label)e.Item.FindControl("lblMillCode");
            Label lblqntltotal = (Label)e.Item.FindControl("lblqntltotal");
            Label lblamounttotal = (Label)e.Item.FindControl("lblamounttotal");
            Label lbltotalamount = (Label)e.Item.FindControl("lbltotalamount");

            Label lbltotalTDSAmt = (Label)e.Item.FindControl("lbltotalTDSAmt");
            Label lbltotalCGSTAmt = (Label)e.Item.FindControl("lbltotalCGSTAmt");
            Label lbltotalSGSTAmt = (Label)e.Item.FindControl("lbltotalSGSTAmt");
            Label lbltotalIGSTAmt = (Label)e.Item.FindControl("lbltotalIGSTAmt");
            Label lbltotalBillAmt = (Label)e.Item.FindControl("lbltotalBillAmt");
            Label lbltotalTCSAmt = (Label)e.Item.FindControl("lbltotalTCSAmt");
            Label lbltotalTCSNetpayAmt = (Label)e.Item.FindControl("lbltotalTCSNetpayAmt");

            string millcode = lblMillCode.Text;

            DataView view1 = new DataView(dtlistbind, "mill_code='" + lblMillCode.Text + "'", "mill_code", DataViewRowState.CurrentRows);
            //DataTable dtAcData = view1.ToTable(true, "do_no", "desp_type", "do_date", "quantal", "millrate", "amount", "salerate", "totalamount", "getpass", "nar1", "do", "truck");

            DataTable dtAcData = view1.ToTable(true, "do_no", "do_date", "quantal", "millrate", "amount", "TDS_Amt", "CGSTAmount", "SGSTAmount", "IGSTAmount", "Bill_Amount", "TCS_Amt", "TCS_Net_Payable", "getpass", "truck", "subtotal");


            if (dsitembind != null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("do_no", typeof(string)));
                dt.Columns.Add(new DataColumn("do_date", typeof(string)));
                dt.Columns.Add(new DataColumn("quantal", typeof(double)));
                dt.Columns.Add(new DataColumn("millrate", typeof(string)));
                dt.Columns.Add(new DataColumn("amount", typeof(double)));
                dt.Columns.Add(new DataColumn("salerate", typeof(string)));
                // dt.Columns.Add(new DataColumn("totalamount", typeof(double)));
                dt.Columns.Add(new DataColumn("TDS_Amt", typeof(double)));
                dt.Columns.Add(new DataColumn("CGSTAmount", typeof(double)));
                dt.Columns.Add(new DataColumn("SGSTAmount", typeof(double)));
                dt.Columns.Add(new DataColumn("IGSTAmount", typeof(double)));
                dt.Columns.Add(new DataColumn("Bill_Amount", typeof(double)));
                dt.Columns.Add(new DataColumn("TCS_Amt", typeof(double)));
                dt.Columns.Add(new DataColumn("TCS_Net_Payable", typeof(double)));
                dt.Columns.Add(new DataColumn("narration", typeof(string)));
                if (dsitembind.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dtAcData.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["do_no"] = dtAcData.Rows[i]["do_no"].ToString();
                        dr["do_date"] = dtAcData.Rows[i]["do_date"].ToString();
                        dr["quantal"] = dtAcData.Rows[i]["quantal"].ToString();

                        double netqnl = double.Parse(dtAcData.Rows[i]["millrate"].ToString());
                        double subtotal = double.Parse(dtAcData.Rows[i]["subTotal"].ToString());

                        dr["millrate"] = subtotal / netqnl;
                        double qntl = double.Parse(dtAcData.Rows[i]["quantal"].ToString());
                        double amount = (qntl * (subtotal / netqnl));

                        dr["amount"] = amount;

                        

                        dr["TDS_Amt"] = dtAcData.Rows[i]["TDS_Amt"].ToString();
                        dr["CGSTAmount"] = dtAcData.Rows[i]["CGSTAmount"].ToString();
                        dr["SGSTAmount"] = dtAcData.Rows[i]["SGSTAmount"].ToString();
                        dr["IGSTAmount"] = dtAcData.Rows[i]["IGSTAmount"].ToString();
                        dr["Bill_Amount"] = dtAcData.Rows[i]["Bill_Amount"].ToString();
                        dr["TCS_Amt"] = dtAcData.Rows[i]["TCS_Amt"].ToString();
                        dr["TCS_Net_Payable"] = dtAcData.Rows[i]["TCS_Net_Payable"].ToString();
                        string getpass = dtAcData.Rows[i]["getpass"].ToString();
                        ////string nar1 = dtAcData.Rows[i]["nar1"].ToString();
                        ////string DO = dtAcData.Rows[i]["do"].ToString();
                        string truckno = dtAcData.Rows[i]["truck"].ToString();
                        ////dr["narration"] = getpass + " " + nar1 + " " + DO + " " + truckno;
                        dr["narration"] = getpass + " " + truckno;

                        dt.Rows.Add(dr);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        lblqntltotal.Text = Convert.ToString(dt.Compute("SUM(quantal)", string.Empty));
                        double qntlTotal = Convert.ToDouble(dt.Compute("SUM(quantal)", string.Empty));
                        grandqntlTotal = grandqntlTotal + qntlTotal;

                        lblamounttotal.Text = Convert.ToString(dt.Compute("SUM(amount)", string.Empty));
                        double amounttotal = Convert.ToDouble(dt.Compute("SUM(amount)", string.Empty));
                        grandamounttotal = grandamounttotal + amounttotal;

                        lbltotalTDSAmt.Text = Convert.ToString(dt.Compute("SUM(TDS_Amt)", string.Empty));
                        double TDS_Amt = Convert.ToDouble(dt.Compute("SUM(TDS_Amt)", string.Empty));
                        grandtotalTDSAmt = grandtotalTDSAmt + TDS_Amt;

                        lbltotalCGSTAmt.Text = Convert.ToString(dt.Compute("SUM(CGSTAmount)", string.Empty));
                        double CGSTAmount = Convert.ToDouble(dt.Compute("SUM(CGSTAmount)", string.Empty));
                        grandtotalCGSTAmt = grandtotalCGSTAmt + CGSTAmount;

                        lbltotalSGSTAmt.Text = Convert.ToString(dt.Compute("SUM(SGSTAmount)", string.Empty));
                        double SGSTAmount = Convert.ToDouble(dt.Compute("SUM(SGSTAmount)", string.Empty));
                        grandtotalSGSTAmt = grandtotalSGSTAmt + SGSTAmount;

                        lbltotalIGSTAmt.Text = Convert.ToString(dt.Compute("SUM(IGSTAmount)", string.Empty));
                        double IGSTAmount = Convert.ToDouble(dt.Compute("SUM(IGSTAmount)", string.Empty));
                        grandtotalIGSTAmt = grandtotalIGSTAmt + IGSTAmount;


                        lbltotalBillAmt.Text = Convert.ToString(dt.Compute("SUM(Bill_Amount)", string.Empty));
                        double Bill_Amount = Convert.ToDouble(dt.Compute("SUM(Bill_Amount)", string.Empty));
                        grandtotalBillAmt = grandtotalBillAmt + Bill_Amount;


                        lbltotalTCSAmt.Text = Convert.ToString(dt.Compute("SUM(TCS_Amt)", string.Empty));
                        double TCS_Amt = Convert.ToDouble(dt.Compute("SUM(TCS_Amt)", string.Empty));
                        grandtotalTCSAmt = grandtotalTCSAmt + TCS_Amt;


                        lbltotalTCSNetpayAmt.Text = Convert.ToString(dt.Compute("SUM(TCS_Net_Payable)", string.Empty));
                        double TCS_Net_Payable = Convert.ToDouble(dt.Compute("SUM(TCS_Net_Payable)", string.Empty));
                        grandtotalTCSNetpayAmt = grandtotalTCSNetpayAmt + TCS_Net_Payable;

                        dtlDetails.DataSource = dt;
                        dtlDetails.DataBind();
                    }
                    else
                    {
                        dtlDetails.DataSource = null;
                        dtlDetails.DataBind();
                    }
                }
                else
                {
                    dtlDetails.DataSource = null;
                    dtlDetails.DataBind();
                }
            }
            // }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkDO = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkDO.NamingContainer;
            string ps = lnkDO.Text;
            Session["DO_NO"] = ps;
            Int32 Action = 1;
            Int32 dono = Convert.ToInt16(clsCommon.getString("SELECT purchaseid  from nt_1_sugarpurchase where doc_no=" + ps + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  "));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kdjsd", "javascript:Donew('" + dono + "','" + Action + "');", true);
            lnkDO.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        pnlMain.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        string strFile = "report.xls";
        string strcontentType = "application/excel";
        Response.ClearContent();
        Response.ClearHeaders();
        Response.BufferOutput = true;
        Response.ContentType = strcontentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        Response.Write(StrExport.ToString());
        Response.Flush();
        Response.Close();
        Response.End();
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text != string.Empty)
        {
            try
            {
                string email = txtEmail.Text;
                StringWriter sw = new StringWriter();
                HtmlTextWriter tw = new HtmlTextWriter(sw);
                pnlMain.RenderControl(tw);
                string s = sw.ToString();

                try
                {
                    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine(s);
                        }
                    }
                }
                catch (Exception)
                {
                    f = f_Main + ".htm";
                    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine(s);
                        }
                    }
                }

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(email);
                msg.Body = "Dispatch Mill Wise Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Dispatch Mill Wise Report " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
                msg.IsBodyHtml = true;
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
            }
            catch (Exception e1)
            {
                //Response.Write("mail err:" + e1);
                Response.Write("<script>alert('Error sending Mail');</script>");
                return;
            }
            Response.Write("<script>alert('Mail sent successfully');</script>");
        }
    }
}