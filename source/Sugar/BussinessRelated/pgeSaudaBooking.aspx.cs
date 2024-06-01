using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.IO;
public partial class pgeSaudaBooking : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string qryAccountList = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    int Detail_Id = 2;
    int Rowaction = 3;
    int Tender_No = 4;
    int Cityname = 5;
    int SubArea = 6;
    int MillCode = 7;
    int MillCode_name = 8;

    int Grade = 9;
    int SelfQty = 10;
    int Season = 11;
    int Quantal = 12;
    int Salerate = 13;
    int PaymentDate = 14;
    int LiftingDate = 15;
    int Googlemap = 16;
    int Image = 17;
    int SellingParty = 18;
    int SellingParty_name = 19;
    //int =21;
    int GroupCode = 20;
    int GroupCode_name = 21;
    //int =24;
    int Timedate = 22;
    int mc = 23;
    int sp = 24;
    int gc = 25;
    int CheckDetail_Id = 26;

    int Srno = 27;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "SaudaBookinghead";
            tblDetails = "SaudaBookingDetail";
            qryCommon = "qrysaudaBooking";
            user = Session["user"].ToString();
            qryAccountList = "qrymstaccountmaster";
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    pnlPopup.Style["display"] = "none";
                    hdnf.Value = Request.QueryString["Entryid"];
                    ViewState["currentTable"] = null;
                    clsButtonNavigation.enableDisable("N");
                    this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    this.showLastRecord();
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
                if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
                {
                    pnlPopup.Style["display"] = "none";
                }
                else
                {
                    pnlPopup.Style["display"] = "block";
                    objAsp = btnSearch;
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Session["Company_code"].ToString();
                obj.code = "Entryid";
                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ViewState["mode"] != null)
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    txtEntryNo.Text = ds.Tables[0].Rows[0][0].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [makeEmptyForm]
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                pnlPopup.Style["display"] = "none";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtEntryNo.Text = "Choose No";
                btntxtEntryNo.Enabled = false;
                txtEditDoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                txtEntry_Date.Enabled = false;
                CalendarExtenderDatetxtEntry_Date.Enabled = false;
                txtTender_No.Enabled = false;
                txtCityname.Enabled = false;
                txtSubArea.Enabled = false;
                txtMillCode.Enabled = false;
                btntxtMillCode.Enabled = false;
                txtGrade.Enabled = false;
                txtSelfQty.Enabled = false;
                txtSeason.Enabled = false;
                txtQuantal.Enabled = false;
                txtSalerate.Enabled = false;
                txtPaymentDate.Enabled = false;
                CalendarExtenderDatetxtPaymentDate.Enabled = false;
                txtLiftingDate.Enabled = false;
                CalendarExtenderDatetxtLiftingDate.Enabled = false;
                txtGooglemap.Enabled = false;
                txtImage.Enabled = false;
                txtSellingParty.Enabled = false;
                btntxtSellingParty.Enabled = false;
                txtGroupCode.Enabled = false;
                btntxtGroupCode.Enabled = false;
                txtTimedate.Enabled = false;
                btnUpload.Enabled = false;
            }
            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btnSave.Text = "Save";
                btntxtEntryNo.Text = "Change No";
                btntxtEntryNo.Enabled = true;
                txtEditDoc_no.Enabled = false;
                txtEntryNo.Enabled = false;
                ViewState["currentTable"] = null;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                ViewState["currentTable"] = null;
                txtTender_No.Enabled = true;
                txtCityname.Enabled = true;
                txtSubArea.Enabled = true;
                txtMillCode.Enabled = true;
                lblmillname.Text = string.Empty;
                btntxtMillCode.Enabled = true;
                txtGrade.Enabled = true;
                txtSelfQty.Enabled = true;
                txtSeason.Enabled = true;
                txtQuantal.Enabled = true;
                txtSalerate.Enabled = true;
                txtPaymentDate.Enabled = true;
                txtPaymentDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtPaymentDate.Enabled = true;
                txtLiftingDate.Enabled = true;
                txtLiftingDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtLiftingDate.Enabled = true;
                txtGooglemap.Enabled = true;
                txtImage.Enabled = true;
                txtSellingParty.Enabled = true;
                lblsellingname.Text = string.Empty;
                btntxtSellingParty.Enabled = true;
                txtGroupCode.Enabled = true;
                lblgroupname.Text = string.Empty;
                btntxtGroupCode.Enabled = true;
                txtTimedate.Enabled = true;
                txtEntry_Date.Enabled = true;
                btnUpload.Enabled = true;
                txtEntry_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtEntry_Date.Enabled = true;
                pnlgrdDetail.Enabled = true;
                #region set Business logic for save
                #endregion
            }
            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                btntxtEntryNo.Text = "Choose No";
                btntxtEntryNo.Enabled = false;
                txtEditDoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                txtEntry_Date.Enabled = false;
                CalendarExtenderDatetxtEntry_Date.Enabled = false;
                txtTender_No.Enabled = false;
                txtCityname.Enabled = false;
                txtSubArea.Enabled = false;
                txtMillCode.Enabled = false;
                btntxtMillCode.Enabled = false;
                txtGrade.Enabled = false;
                txtSelfQty.Enabled = false;
                txtSeason.Enabled = false;
                txtQuantal.Enabled = false;
                txtSalerate.Enabled = false;
                txtPaymentDate.Enabled = false;
                CalendarExtenderDatetxtPaymentDate.Enabled = false;
                txtLiftingDate.Enabled = false;
                CalendarExtenderDatetxtLiftingDate.Enabled = false;
                txtGooglemap.Enabled = false;
                txtImage.Enabled = false;
                txtSellingParty.Enabled = false;
                btntxtSellingParty.Enabled = false;
                txtGroupCode.Enabled = false;
                btntxtGroupCode.Enabled = false;
                txtTimedate.Enabled = false;
                txtTender_No.Text = string.Empty;
                txtCityname.Text = string.Empty;
                txtSubArea.Text = string.Empty;
                txtMillCode.Text = string.Empty;
                btntxtMillCode.Enabled = false;
                txtGrade.Text = string.Empty;
                txtSelfQty.Text = string.Empty;
                txtSeason.Text = string.Empty;
                txtQuantal.Text = string.Empty;
                txtSalerate.Text = string.Empty;
                txtPaymentDate.Text = string.Empty;
                CalendarExtenderDatetxtPaymentDate.Enabled = false;
                txtLiftingDate.Text = string.Empty;
                CalendarExtenderDatetxtLiftingDate.Enabled = false;
                txtGooglemap.Text = string.Empty;
                txtImage.Text = string.Empty;
                txtSellingParty.Text = string.Empty;
                btntxtSellingParty.Enabled = false;
                txtGroupCode.Text = string.Empty;
                btntxtGroupCode.Enabled = false;
                txtTimedate.Text = string.Empty;
                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnUpload.Enabled = false;
                btnClosedetails.Enabled = false;
            }
            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btntxtEntryNo.Text = "Choose No";
                btntxtEntryNo.Enabled = true;
                txtEditDoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                txtEntry_Date.Enabled = true;
                CalendarExtenderDatetxtEntry_Date.Enabled = true;
                txtTender_No.Enabled = true;
                txtCityname.Enabled = true;
                txtSubArea.Enabled = true;
                txtMillCode.Enabled = true;
                btntxtMillCode.Enabled = true;
                txtGrade.Enabled = true;
                txtSelfQty.Enabled = true;
                txtSeason.Enabled = true;
                txtQuantal.Enabled = true;
                txtSalerate.Enabled = true;
                txtPaymentDate.Enabled = true;
                CalendarExtenderDatetxtPaymentDate.Enabled = true;
                txtLiftingDate.Enabled = true;
                CalendarExtenderDatetxtLiftingDate.Enabled = true;
                txtGooglemap.Enabled = true;
                txtImage.Enabled = true;
                txtSellingParty.Enabled = true;
                btntxtSellingParty.Enabled = true;
                txtGroupCode.Enabled = true;
                btntxtGroupCode.Enabled = true;
                txtTimedate.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btnUpload.Enabled = true;
            }
            #region Always check this
            #endregion
        }
        catch
        {
        }
    }
    #endregion
    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            string EntryNo = clsCommon.getString("select max(EntryNo) as Entryid from " + tblHead +
                " where Company_code=" + Session["Company_code"].ToString());
            hdnf.Value = clsCommon.getString("select  Entryid from " + tblHead +
                " where Company_code=" + Session["Company_code"].ToString() + " and EntryNo=" + EntryNo);
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                btnAdd.Focus();
            }
            else                     //new code
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(query);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    RecordCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
        }
        if (RecordCount != 0 && RecordCount == 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
        else if (RecordCount != 0 && RecordCount > 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = true;
        }
        if (txtEntryNo.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [EntryNo] from " + tblHead +
                    " where Entryid>" + Convert.ToInt32(hdnf.Value) +
                    " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY EntryNo asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //next record exist
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnNext.Enabled = false;
                            btnLast.Enabled = false;
                        }
                    }
                }
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [EntryNo] from " + tblHead + " where Entryid<" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY EntryNo asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //previous record exist
                            btnPrevious.Enabled = true;
                            btnFirst.Enabled = true;
                        }
                        else
                        {
                            btnPrevious.Enabled = false;
                            btnFirst.Enabled = false;
                        }
                    }
                }
                #endregion
            }
        }

        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Entryid from " + tblHead + " where Entryid=(select MIN(Entryid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion
    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEntryNo.Text != string.Empty)
            {
                string query = "SELECT top 1 [Entryid] from " + tblHead + " where Entryid<" + Convert.ToInt32(hdnf.Value) +
                             "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                             " ORDER BY Entryid DESC  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Next]
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEntryNo.Text != string.Empty)
            {

                string query = "SELECT top 1 [Entryid] from " + tblHead + " where Entryid>" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY Entryid asc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Last]
    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Entryid from " + tblHead + " where Entryid=(select MAX(Entryid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        setFocusControl(txtEntry_Date);
        pnlPopupDetails.Style["display"] = "none";
        Int32 EntryNo = Convert.ToInt32(clsCommon.getString("select IDENT_CURRENT('" + tblHead + "') as Doc_No"));
        if (EntryNo != 0)
        {
            int doc_no = EntryNo + 1;
            EntryNo = doc_no;
        }
        else
        {
            EntryNo = 1;
        }
        txtEntryNo.Text = Convert.ToString(EntryNo);
        bindtenderdetails();
    }
    #endregion

    private void bindtenderdetails()
    {

        //string qry = "select  Tender_No,tenderid,millshortname,Grade,Mill_Rate,(Sale_Rate+Commission_Rate) as Sale_Rate,Lifting_DateConverted as Tender_Date,Buyer_Quantal," +
        //    "DESPATCH,BALANCE,tenderdoshortname,Buyer,tenderdetailid from " +
        //   "qrytenderdobalanceview where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and "
        //    + "  year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and  Buyer_Quantal!=0 and Buyer=2 "
        //   + " GROUP BY Tender_No,tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Tender_Date,Buyer_Quantal,DESPATCH,BALANCE,tenderdoname,Lifting_DateConverted,tenderdoshortname,Buyer,Commission_Rate,tenderdetailid order by millshortname ";

        //qry = "select 0+ ROW_NUMBER() OVER(ORDER BY Tender_No) as  Detail_Id,Tender_No as Tender_No,Cityname,SubArea,Mill_Code as MillCode,millname as millname as MillCode_Name,Grade as Grade,BALANCE as SelfQty,Season,Buyer_Quantal as Quantal,Salerate," +
        //                    "Lifting_DateConverted as PaymentDate,Lifting_DateConverted as LiftingDate,GoogleMap,Image,Buyer_Party as SellingParty,sellpartyname as SellingParty_Name,GroupCode,groupname as GroupCode_Name,Timedate," +
        //                    "mc,buyerpartyid as sp,gc,'0' as CheckDetail_Id,rowAction,SrNo from qrytenderdobalanceview where Company_Code='" 
        //                    + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  and "
        //                     + "  year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and BALANCE>0 and Buyer_Quantal!=0 and Buyer=2 "
        //   + " GROUP BY Tender_No,tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Tender_Date,Buyer_Quantal,DESPATCH,BALANCE,tenderdoname,Lifting_DateConverted,tenderdoshortname,Buyer,Commission_Rate,tenderdetailid order by millshortname ";

        qry = "select 0+ ROW_NUMBER() OVER(ORDER BY Tender_No) as  Detail_Id,'A' as rowAction,Tender_No as Tender_No,millcityname as Cityname,millsubarea as SubArea," +
            "Mill_Code as MillCode,millname  as MillCode_Name,Grade as Grade,BALANCE as SelfQty," +
                "'' as Season,'0' as Quantal,'0' as Salerate,Lifting_DateConverted as PaymentDate," +
                "Lifting_DateConverted as LiftingDate,'' as GoogleMap,'' as Image,Payment_To as SellingParty,Paymenttoname as SellingParty_Name," +
                "'0' as GroupCode,'' as  GroupCode_Name,'' as Timedate,mc,pt as sp,'0' gc,'0' as CheckDetail_Id," +
                "0+ ROW_NUMBER() OVER(ORDER BY Tender_No) as SrNo from qryTenderBalanceSaudaBooking where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                  "and   year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and BALANCE>0 and Buyer_Quantal!=0 and Buyer=2  " +
                    "GROUP BY Tender_No,Mill_Code,tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Tender_Date,Buyer_Quantal,DESPATCH,BALANCE,tenderdoname," +
                "Lifting_DateConverted,tenderdoshortname,Buyer,Commission_Rate,tenderdetailid,Lifting_DateConverted,millname,mc,millcityname,millsubarea,Paymenttoname,Payment_To,pt order by millshortname ";

        DataSet bindqry = clsDAL.SimpleQuery(qry);
        DataTable dt = new DataTable();
        if (bindqry.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = bindqry.Tables[0];
            if (dt.Rows.Count > 0)
            {
                int rowIndex = 0;
                int maxIndex = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    rowIndex = maxIndex + 1;
                    dt.Rows[i]["Detail_Id"] = rowIndex;
                    dt.Rows[i]["SrNo"] = rowIndex;
                    maxIndex = maxIndex + 1;
                }
                grdDetail.DataSource = dt;
                grdDetail.DataBind();
                ViewState["currentTable"] = dt;
            }
        }
    }

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtEntryNo.Enabled = false;
        setFocusControl(txtEntryNo);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnf.Value = Request.QueryString["Entryid"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(Entryid),0) from "
                + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'"));
            hdnf.Value = Convert.ToString(maxno);
            ViewState["currentTable"] = null;
            clsButtonNavigation.enableDisable("N");

            this.makeEmptyForm("N");
            ViewState["mode"] = "I";
            this.showLastRecord();
            this.enableDisableNavigateButtons();
            setFocusControl(btnEdit);

        }
    }
    #endregion
    #region [fetchrecord]
    private bool fetchRecord(string qry)
    {
        try
        {
            bool recordExist = false;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");
                        if (lblCreated != null)
                        {
                            lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        }
                        if (lblModified != null)
                        {
                            lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        }
                        Label lblCreatedDate = (Label)Master.FindControl("MasterlblCreatedDate");
                        Label lblModifiedDate = (Label)Master.FindControl("MasterlblModifiedDate");
                        if (lblCreatedDate != null)
                        {
                            if (dt.Rows[0]["Created_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblCreatedDate.Text = "Created Date" + DateTime.Parse(dt.Rows[0]["Created_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                            }
                        }
                        if (lblModifiedDate != null)
                        {
                            if (dt.Rows[0]["Modified_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblModifiedDate.Text = "Modified Date" + DateTime.Parse(dt.Rows[0]["Modified_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                            }
                        }
                        //  txtEditDoc_no.Text = dt.Rows[0]["EditDoc_no"].ToString();
                        txtEntryNo.Text = dt.Rows[0]["EntryNo"].ToString();
                        // lbldoc_no.Text = dt.Rows[0][""].ToString();
                        txtEntry_Date.Text = dt.Rows[0]["Entry_Date"].ToString();
                        hdnfEntryid.Value = hdnf.Value;
                        hdnfdocno.Value = txtEntryNo.Text;
                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "select Detail_Id,'' as rowAction,Tender_No,Cityname,SubArea,MillCode,millname as MillCode_Name,Grade,SelfQty,Season,Quantal,Salerate," +
                            "PaymentDate,LiftingDate,GoogleMap,Image,SellingParty,sellpartyname as SellingParty_Name,GroupCode,groupname as GroupCode_Name,Timedate," +
                            "mc,sp,gc,CheckDetail_Id from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Entryid=" + hdnf.Value;
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    //dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }
                                    grdDetail.DataSource = dt;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = dt;
                                }
                                else
                                {
                                    grdDetail.DataSource = null;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = null;
                                }
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        #endregion
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtEditDoc_no")
            {
                setFocusControl(txtEditDoc_no);
            }
            if (strTextBox == "txtEntryNo")
            {
                string acname = "";
                if (txtEntryNo.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtEntryNo.Text);
                    if (a == false)
                    {
                        btntxtEntryNo_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtEntryNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            hdnf.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtEntryNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lbldoc_no.Text = acname;
                            setFocusControl(txtEntryNo);
                        }
                        else
                        {
                            txtEntryNo.Text = string.Empty;
                            setFocusControl(txtEntryNo);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtEntryNo);
                }
            }
            if (strTextBox == "txtEntry_Date")
            {
                setFocusControl(txtTender_No);
            }
            if (strTextBox == "txtTender_No")
            {
                setFocusControl(txtTender_No);
            }
            if (strTextBox == "txtCityname")
            {
                setFocusControl(txtCityname);
            }
            if (strTextBox == "txtSubArea")
            {
                setFocusControl(txtSubArea);
            }
            if (strTextBox == "txtMillCode")
            {
                string acname = "";
                if (txtMillCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMillCode.Text);
                    if (a == false)
                    {
                        btntxtMillCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_type='M' and Ac_Code=" + txtMillCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            // hdnf.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtMillCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lblmillname.Text = acname;
                            setFocusControl(txtGrade);
                        }
                        else
                        {
                            txtMillCode.Text = string.Empty;
                            setFocusControl(txtMillCode);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtMillCode);
                }
            }
            if (strTextBox == "txtGrade")
            {
                setFocusControl(txtGrade);
            }
            if (strTextBox == "txtSelfQty")
            {
                setFocusControl(txtSelfQty);
            }
            if (strTextBox == "txtSeason")
            {
                setFocusControl(txtSeason);
            }
            if (strTextBox == "txtQuantal")
            {
                setFocusControl(txtQuantal);
            }
            if (strTextBox == "txtSalerate")
            {
                setFocusControl(txtSalerate);
            }
            if (strTextBox == "txtPaymentDate")
            {
                setFocusControl(txtPaymentDate);
            }
            if (strTextBox == "txtLiftingDate")
            {
                setFocusControl(txtLiftingDate);
            }
            if (strTextBox == "txtGooglemap")
            {
                setFocusControl(txtGooglemap);
            }
            if (strTextBox == "txtImage")
            {
                setFocusControl(txtImage);
            }
            if (strTextBox == "txtSellingParty")
            {
                string acname = "";
                if (txtSellingParty.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtSellingParty.Text);
                    if (a == false)
                    {
                        btntxtSellingParty_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtSellingParty.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            hdnf.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtSellingParty.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lblsellingname.Text = acname;
                            setFocusControl(txtGroupCode);
                        }
                        else
                        {
                            txtSellingParty.Text = string.Empty;
                            setFocusControl(txtSellingParty);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtSellingParty);
                }
            }
            if (strTextBox == "txtGroupCode")
            {
                string acname = "";
                if (txtGroupCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGroupCode.Text);
                    if (a == false)
                    {
                        btntxtGroupCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select System_Name_E from nt_1_systemmaster where System_Type='G' and System_Code=" + txtGroupCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            // string qry = "select System_Code as Group_code,System_Name_E as Group_Name from nt_1_systemmaster where System_Type='G' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                            //hdnf.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtGroupCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lblgroupname.Text = acname;
                            setFocusControl(txtTimedate);
                        }
                        else
                        {
                            txtGroupCode.Text = string.Empty;
                            setFocusControl(txtGroupCode);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtGroupCode);
                }
            }
            if (strTextBox == "txtTimedate")
            {
                setFocusControl(txtTimedate);
            }
            pnlPopup.Style["display"] = "none";
        }
        catch
        {
        }
    }
    #endregion
    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon +
                " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Entryid=" + hdnf.Value;
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion
    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtEntryNo.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
            }
            else
            {
                showLastRecord();
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {
        btnAdddetails.Text = "ADD";
        pnlPopupDetails.Style["display"] = "block";
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtImage.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('select Image and upload!')", true);

                return;
            }
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["Detail_Id"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["Detail_Id"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["Detail_Id"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_Id"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_Id from " + tblDetails + " where Detail_Id='" + lblID.Text + "' and Detail_Id=" + lblID.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (id != string.Empty && id != "0")
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                    #region [Write here columns]
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("Tender_No", typeof(int))));
                    dt.Columns.Add((new DataColumn("Cityname", typeof(string))));
                    dt.Columns.Add((new DataColumn("SubArea", typeof(string))));
                    dt.Columns.Add((new DataColumn("MillCode", typeof(int))));
                    dt.Columns.Add((new DataColumn("MillCode_Name", typeof(string))));
                    //  dt.Columns.Add((new DataColumn("", typeof(int))));
                    dt.Columns.Add((new DataColumn("Grade", typeof(string))));
                    dt.Columns.Add((new DataColumn("SelfQty", typeof(double))));
                    dt.Columns.Add((new DataColumn("Season", typeof(string))));
                    dt.Columns.Add((new DataColumn("Quantal", typeof(double))));
                    dt.Columns.Add((new DataColumn("Salerate", typeof(double))));
                    dt.Columns.Add((new DataColumn("PaymentDate", typeof(string))));
                    dt.Columns.Add((new DataColumn("LiftingDate", typeof(string))));
                    dt.Columns.Add((new DataColumn("Googlemap", typeof(string))));
                    dt.Columns.Add((new DataColumn("Image", typeof(string))));
                    dt.Columns.Add((new DataColumn("SellingParty", typeof(int))));
                    dt.Columns.Add((new DataColumn("SellingParty_Name", typeof(string))));
                    //dt.Columns.Add((new DataColumn("", typeof(int))));
                    dt.Columns.Add((new DataColumn("GroupCode", typeof(int))));
                    dt.Columns.Add((new DataColumn("GroupCode_Name", typeof(string))));
                    //dt.Columns.Add((new DataColumn("", typeof(int))));
                    dt.Columns.Add((new DataColumn("Timedate", typeof(string))));
                    dt.Columns.Add((new DataColumn("mc", typeof(int))));
                    dt.Columns.Add((new DataColumn("sp", typeof(int))));
                    dt.Columns.Add((new DataColumn("gc", typeof(int))));
                    dt.Columns.Add((new DataColumn("CheckDetail_Id", typeof(int))));

                    #endregion

                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["Detail_Id"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("Tender_No", typeof(int))));
                dt.Columns.Add((new DataColumn("Cityname", typeof(string))));
                dt.Columns.Add((new DataColumn("SubArea", typeof(string))));
                dt.Columns.Add((new DataColumn("MillCode", typeof(int))));
                dt.Columns.Add((new DataColumn("MillCode_Name", typeof(string))));
                //dt.Columns.Add((new DataColumn("", typeof(int))));
                dt.Columns.Add((new DataColumn("Grade", typeof(string))));
                dt.Columns.Add((new DataColumn("SelfQty", typeof(double))));
                dt.Columns.Add((new DataColumn("Season", typeof(string))));
                dt.Columns.Add((new DataColumn("Quantal", typeof(double))));
                dt.Columns.Add((new DataColumn("Salerate", typeof(double))));
                dt.Columns.Add((new DataColumn("PaymentDate", typeof(string))));
                dt.Columns.Add((new DataColumn("LiftingDate", typeof(string))));
                dt.Columns.Add((new DataColumn("Googlemap", typeof(string))));
                dt.Columns.Add((new DataColumn("Image", typeof(string))));
                dt.Columns.Add((new DataColumn("SellingParty", typeof(int))));
                dt.Columns.Add((new DataColumn("SellingParty_Name", typeof(string))));
                //  dt.Columns.Add((new DataColumn("", typeof(int))));
                dt.Columns.Add((new DataColumn("GroupCode", typeof(int))));
                dt.Columns.Add((new DataColumn("GroupCode_Name", typeof(string))));
                // dt.Columns.Add((new DataColumn("", typeof(int))));
                dt.Columns.Add((new DataColumn("Timedate", typeof(string))));
                dt.Columns.Add((new DataColumn("mc", typeof(int))));
                dt.Columns.Add((new DataColumn("sp", typeof(int))));
                dt.Columns.Add((new DataColumn("gc", typeof(int))));
                dt.Columns.Add((new DataColumn("CheckDetail_Id", typeof(int))));

                #endregion

                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            if (hdnfCheckDetail_Id.Value != string.Empty)
            {
                dr["CheckDetail_Id"] = hdnfCheckDetail_Id.Value;
            }
            else
            {
                dr["CheckDetail_Id"] = "0";
            }
            dr["Tender_No"] = txtTender_No.Text;
            dr["Cityname"] = txtCityname.Text;
            dr["SubArea"] = txtSubArea.Text;
            dr["MillCode"] = txtMillCode.Text;

            dr["MillCode_Name"] = lblmillname.Text;
            dr["Grade"] = txtGrade.Text;
            dr["SelfQty"] = txtSelfQty.Text;
            dr["Season"] = txtSeason.Text;
            dr["Quantal"] = txtQuantal.Text;
            dr["Salerate"] = txtSalerate.Text;
            // dr["PaymentDate"] = txtPaymentDate.Text;
            dr["PaymentDate"] = DateTime.Parse(txtPaymentDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            //dr["LiftingDate"] = txtLiftingDate.Text;
            dr["LiftingDate"] = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            dr["Googlemap"] = txtGooglemap.Text;
            if (txtImage.Text.Trim() != string.Empty && txtImage.Text != " ")
            {
                dr["Image"] = txtImage.Text;
            }
            else
            {
                txtImage.Text = string.Empty;
            }
            dr["SellingParty"] = txtSellingParty.Text;

            dr["SellingParty_Name"] = lblsellingname.Text;
            dr["GroupCode"] = txtGroupCode.Text;

            dr["GroupCode_Name"] = lblgroupname.Text;
            const string FMT = "O";
            DateTime now1 = DateTime.Now;
            string strDate = now1.ToString(FMT);
            txtTimedate.Text = strDate;
            dr["Timedate"] = txtTimedate.Text;

            dr["mc"] = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_code=" + dr["MillCode"] + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            dr["sp"] = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_code=" + dr["SellingParty"] + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            dr["gc"] = clsCommon.getString("select systemid from qrymstgrademaster where System_Code=" + dr["GroupCode"] + " and Company_Code=" + Session["Company_Code"].ToString() + "");



            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dt.Rows.Add(dr);
            }
            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion
            grdDetail.DataSource = dt;
            grdDetail.DataBind();
            ViewState["currentTable"] = dt;
            if (btnAdddetails.Text == "ADD")
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtTender_No);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            txtTender_No.Text = string.Empty;
            txtCityname.Text = string.Empty;
            txtSubArea.Text = string.Empty;
            txtMillCode.Text = string.Empty;
            txtGrade.Text = string.Empty;
            txtSelfQty.Text = string.Empty;
            txtSeason.Text = string.Empty;
            txtQuantal.Text = string.Empty;
            txtSalerate.Text = string.Empty;
            //txtPaymentDate.Text = string.Empty;
            //txtLiftingDate.Text = string.Empty;
            txtGooglemap.Text = string.Empty;
            txtImage.Text = string.Empty;
            txtSellingParty.Text = string.Empty;
            txtGroupCode.Text = string.Empty;
            txtTimedate.Text = string.Empty;
            lblmillname.Text = string.Empty;
            lblsellingname.Text = string.Empty;
            lblgroupname.Text = string.Empty;

            btnAdddetails.Text = "ADD";
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        lblNo.Text = string.Empty;
        lblID.Text = string.Empty;
        txtTender_No.Text = string.Empty;
        txtCityname.Text = string.Empty;
        txtSubArea.Text = string.Empty;
        txtMillCode.Text = string.Empty;
        txtGrade.Text = string.Empty;
        txtSelfQty.Text = string.Empty;
        txtSeason.Text = string.Empty;
        txtQuantal.Text = string.Empty;
        txtSalerate.Text = string.Empty;
        txtPaymentDate.Text = string.Empty;
        txtLiftingDate.Text = string.Empty;
        txtGooglemap.Text = string.Empty;
        txtImage.Text = string.Empty;
        txtSellingParty.Text = string.Empty;
        txtGroupCode.Text = string.Empty;
        txtTimedate.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtTender_No);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        txtTender_No.Text = Server.HtmlDecode(gvrow.Cells[Tender_No].Text);
        txtCityname.Text = Server.HtmlDecode(gvrow.Cells[Cityname].Text);
        txtSubArea.Text = Server.HtmlDecode(gvrow.Cells[SubArea].Text);
        txtMillCode.Text = Server.HtmlDecode(gvrow.Cells[MillCode].Text);
        lblmillname.Text = Server.HtmlDecode(gvrow.Cells[MillCode_name].Text);
        //hdnf.Value= Server.HtmlDecode(gvrow.Cells[].Text);
        txtGrade.Text = Server.HtmlDecode(gvrow.Cells[Grade].Text);
        txtSelfQty.Text = Server.HtmlDecode(gvrow.Cells[SelfQty].Text);
        txtSeason.Text = Server.HtmlDecode(gvrow.Cells[Season].Text);
        txtQuantal.Text = Server.HtmlDecode(gvrow.Cells[Quantal].Text);
        txtSalerate.Text = Server.HtmlDecode(gvrow.Cells[Salerate].Text);
        txtPaymentDate.Text = Server.HtmlDecode(gvrow.Cells[PaymentDate].Text);
        txtLiftingDate.Text = Server.HtmlDecode(gvrow.Cells[LiftingDate].Text);
        txtGooglemap.Text = Server.HtmlDecode(gvrow.Cells[Googlemap].Text);

        txtImage.Text = Server.HtmlDecode(gvrow.Cells[Image].Text.Trim().Replace(" ", string.Empty));
        // string a = txtImage.Text.Replace(" ", String.Empty);
        txtSellingParty.Text = Server.HtmlDecode(gvrow.Cells[SellingParty].Text);
        lblsellingname.Text = Server.HtmlDecode(gvrow.Cells[SellingParty_name].Text);
        //hdnf.Value= Server.HtmlDecode(gvrow.Cells[].Text);
        txtGroupCode.Text = Server.HtmlDecode(gvrow.Cells[GroupCode].Text);
        lblgroupname.Text = Server.HtmlDecode(gvrow.Cells[GroupCode_name].Text);
        hdnfCheckDetail_Id.Value = Server.HtmlDecode(gvrow.Cells[CheckDetail_Id].Text);
        txtTimedate.Text = Server.HtmlDecode(gvrow.Cells[Timedate].Text);
        //hdnfmc.Value= Server.HtmlDecode(gvrow.Cells[mc].Text);
        //hdnfsp.Value= Server.HtmlDecode(gvrow.Cells[sp].Text);
        //hdnfgc.Value= Server.HtmlDecode(gvrow.Cells[gc].Text);
    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
        try
        {
            int rowIndex = gridViewRow.RowIndex;
            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Detail_Id"].ToString());
                string IDExisting = clsCommon.getString("select Detail_Id from " + tblDetails + " where Doc_No='" + hdnf.Value + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "D";// rowAction Index add 
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "N";// Add rowaction id
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "R";       // add row action R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            // e.Row.Cells[2].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[2].Style["overflow"] = "hidden";

            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            //--------------------------------------------------
            e.Row.Cells[CheckDetail_Id].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[CheckDetail_Id].Style["overflow"] = "hidden";
            e.Row.Cells[CheckDetail_Id].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Tender_No].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Tender_No].Style["overflow"] = "hidden";
            e.Row.Cells[Tender_No].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Cityname].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[Cityname].Style["overflow"] = "hidden";
            e.Row.Cells[Cityname].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[SubArea].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[SubArea].Style["overflow"] = "hidden";
            e.Row.Cells[SubArea].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[MillCode].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[MillCode].Style["overflow"] = "hidden";
            e.Row.Cells[MillCode].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[MillCode_name].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[MillCode_name].Style["overflow"] = "hidden";
            e.Row.Cells[MillCode_name].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //e.Row.Cells[].ControlStyle.Width = new Unit("120px");
            //e.Row.Cells[].Style["overflow" ] = "hidden";
            //e.Row.Cells[].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Grade].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Grade].Style["overflow"] = "hidden";
            e.Row.Cells[Grade].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[SelfQty].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[SelfQty].Style["overflow"] = "hidden";
            e.Row.Cells[SelfQty].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[Season].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Season].Style["overflow"] = "hidden";
            e.Row.Cells[Season].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Quantal].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Quantal].Style["overflow"] = "hidden";
            e.Row.Cells[Quantal].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[Salerate].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Salerate].Style["overflow"] = "hidden";
            e.Row.Cells[Salerate].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[PaymentDate].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[PaymentDate].Style["overflow"] = "hidden";
            e.Row.Cells[PaymentDate].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[LiftingDate].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[LiftingDate].Style["overflow"] = "hidden";
            e.Row.Cells[LiftingDate].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Googlemap].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Googlemap].Style["overflow"] = "hidden";
            e.Row.Cells[Googlemap].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Image].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Image].Style["overflow"] = "hidden";
            e.Row.Cells[Image].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[SellingParty].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[SellingParty].Style["overflow"] = "hidden";
            e.Row.Cells[SellingParty].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[SellingParty_name].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[SellingParty_name].Style["overflow"] = "hidden";
            e.Row.Cells[SellingParty_name].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //e.Row.Cells[].ControlStyle.Width = new Unit("120px");
            //e.Row.Cells[].Style["overflow" ] = "hidden";
            //e.Row.Cells[].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[GroupCode].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[GroupCode].Style["overflow"] = "hidden";
            e.Row.Cells[GroupCode].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[GroupCode_name].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[GroupCode_name].Style["overflow"] = "hidden";
            e.Row.Cells[GroupCode_name].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //e.Row.Cells[].ControlStyle.Width = new Unit("120px");
            //e.Row.Cells[].Style["overflow" ] = "hidden";
            //e.Row.Cells[].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Timedate].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Timedate].Style["overflow"] = "hidden";
            e.Row.Cells[Timedate].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[mc].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[mc].Style["overflow"] = "hidden";
            e.Row.Cells[mc].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[sp].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[sp].Style["overflow"] = "hidden";
            e.Row.Cells[sp].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[gc].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[gc].Style["overflow"] = "hidden";
            e.Row.Cells[gc].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[CheckDetail_Id].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[CheckDetail_Id].Style["overflow"] = "hidden";
            e.Row.Cells[CheckDetail_Id].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[Rowaction].Style["overflow"] = "hidden";
            e.Row.Cells[Rowaction].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[Srno].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[Srno].Style["overflow"] = "hidden";
            e.Row.Cells[Srno].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            //    e.Row.Cells[0].Visible =true;
            //}
        }
        catch
        {
        }
    }
    #endregion
    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hidden";
            //    e.Row.Cells[0].Visible =true;
        }
    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    #endregion
    #region [grdPopup_RowCreated]
    protected void grdPopup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState == DataControlRowState.Normal ||
           e.Row.RowState == DataControlRowState.Alternate))
            {
                e.Row.TabIndex = -1;
                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion
    #region [RowCommand]
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail.Rows[rowindex].Cells[Rowaction].Text != "D" && grdDetail.Rows[rowindex].Cells[Rowaction].Text != "R")//add row action id
                        {
                            pnlPopupDetails.Style["display"] = "none";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "UPDATE";
                            setFocusControl(txtTender_No);
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        this.DeleteDetailsRow(grdDetail.Rows[rowindex], action);
                        break;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [txtEditDoc_no_TextChanged]
    protected void txtEditDoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEditDoc_no.Text;
        strTextBox = "txtEditDoc_no";
        csCalculations();
    }
    #endregion
    #region [txtEntryNo_TextChanged]
    protected void txtEntryNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEntryNo.Text;
        strTextBox = "txtEntryNo";
        csCalculations();
    }
    #endregion
    #region [btntxtEntryNo_Click]
    protected void btntxtEntryNo_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtEntryNo";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtEntry_Date_TextChanged]
    protected void txtEntry_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEntry_Date.Text;
        strTextBox = "txtEntry_Date";
        csCalculations();
    }
    #endregion

    #region [txtTender_No_TextChanged]
    protected void txtTender_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtTender_No.Text;
        strTextBox = "txtTender_No";
        csCalculations();
    }
    #endregion


    #region [txtCityname_TextChanged]
    protected void txtCityname_TextChanged(object sender, EventArgs e)
    {
        // searchString = txtCityname.Text;
        strTextBox = "txtCityname";
        csCalculations();
    }
    #endregion


    #region [txtSubArea_TextChanged]
    protected void txtSubArea_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtSubArea.Text;
        strTextBox = "txtSubArea";
        csCalculations();
    }
    #endregion


    #region [txtMillCode_TextChanged]
    protected void txtMillCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMillCode.Text;
        strTextBox = "txtMillCode";
        csCalculations();
    }
    #endregion

    #region [btntxtMillCode_Click]
    protected void btntxtMillCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMillCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtGrade_TextChanged]
    protected void txtGrade_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtGrade.Text;
        strTextBox = "txtGrade";
        csCalculations();
    }
    #endregion


    #region [txtSelfQty_TextChanged]
    protected void txtSelfQty_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtSelfQty.Text;
        strTextBox = "txtSelfQty";
        csCalculations();
    }
    #endregion


    #region [txtSeason_TextChanged]
    protected void txtSeason_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtSeason.Text;
        strTextBox = "txtSeason";
        csCalculations();
    }
    #endregion


    #region [txtQuantal_TextChanged]
    protected void txtQuantal_TextChanged(object sender, EventArgs e)
    {
        //  searchString = txtQuantal.Text;
        strTextBox = "txtQuantal";
        csCalculations();
    }
    #endregion


    #region [txtSalerate_TextChanged]
    protected void txtSalerate_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtSalerate.Text;
        strTextBox = "txtSalerate";
        csCalculations();
    }
    #endregion


    #region [txtPaymentDate_TextChanged]
    protected void txtPaymentDate_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtPaymentDate.Text;
        strTextBox = "txtPaymentDate";
        csCalculations();
    }
    #endregion


    #region [txtLiftingDate_TextChanged]
    protected void txtLiftingDate_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtLiftingDate.Text;
        strTextBox = "txtLiftingDate";
        csCalculations();
    }
    #endregion


    #region [txtGooglemap_TextChanged]
    protected void txtGooglemap_TextChanged(object sender, EventArgs e)
    {
        //  searchString = txtGooglemap.Text;
        strTextBox = "txtGooglemap";
        csCalculations();
    }
    #endregion


    #region [txtImage_TextChanged]
    protected void txtImage_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtImage.Text;
        strTextBox = "txtImage";
        csCalculations();
    }
    #endregion


    #region [txtSellingParty_TextChanged]
    protected void txtSellingParty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSellingParty.Text;
        strTextBox = "txtSellingParty";
        csCalculations();
    }
    #endregion

    #region [btntxtSellingParty_Click]
    protected void btntxtSellingParty_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSellingParty";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtGroupCode_TextChanged]
    protected void txtGroupCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGroupCode.Text;
        strTextBox = "txtGroupCode";
        csCalculations();
    }
    #endregion

    #region [btntxtGroupCode_Click]
    protected void btntxtGroupCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGroupCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtTimedate_TextChanged]
    protected void txtTimedate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTimedate.Text;
        strTextBox = "txtTimedate";
        csCalculations();
    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //  txtSearchText.Text = "";
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtEntryNo" || hdnfClosePopup.Value == "txtEditDoc_no")
            {
                if (btntxtEntryNo.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtEntryNo.Text = string.Empty;
                    txtEntryNo.Enabled = true;
                    btnSave.Enabled = false;
                    setFocusControl(txtEntryNo);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtEntryNo.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select EntryNo from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and code='%like" + txtSearchText.Text + "' order by EntryNo";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtEntryNo")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtMillCode")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Ac_type='M' and  Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtSellingParty")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList +
                    " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text +
                    "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGroupCode")
            {
                lblPopupHead.Text = "--Select--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                string qry = "select System_Code as Group_code,System_Name_E as Group_Name from nt_1_systemmaster where System_Type='G' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and ( System_Name_E like '%" + txtSearchText.Text + "%')";

                this.showPopup(qry);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Popup Button Code]
    protected void showPopup(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdPopup.DataSource = dt;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfClosePopup.Value = "Close";
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
        }
        catch
        {
        }
    }
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
    #region [txtSearchText_TextChanged]
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "Close")
            {
                txtSearchText.Text = string.Empty;
                pnlPopup.Style["display"] = "none";
                grdPopup.DataSource = null;
                grdPopup.DataBind();
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                searchString = txtSearchText.Text;
                strTextBox = hdnfClosePopup.Value;
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ABC", "javascript:pagevalidation();", true);
    }
    #endregion
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fu1.HasFile)
        {
            String imageName = Path.GetFileName(fu1.PostedFile.FileName); ; //your code to fetch imagename (eg. demo.jpg)

            fu1.SaveAs(@"D:\TenderImage\" + imageName);
            //lblimage.Text = "File Uploaded: " + fu1.FileName;
            txtImage.Text = imageName;
        }
        setFocusControl(txtSellingParty);
    }
}

