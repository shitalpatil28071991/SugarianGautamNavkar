<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeSaudaBooking.aspx.cs" Inherits="pgeSaudaBooking" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function Confirm() {
            debugger;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;
            if (KeyCode == 40) {
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            }
            else if (KeyCode == 38) {
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            }
            else if (KeyCode == 13) {
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                var grid = document.getElementById("<%= grdPopup.ClientID %>");
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;
                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtMillCode") {
                    document.getElementById("<%=txtMillCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtMillCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMillCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSellingParty") {
                    document.getElementById("<%=txtSellingParty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblsellingname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSellingParty.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGroupCode") {
                    document.getElementById("<%=txtGroupCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblgroupname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGroupCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_no.ClientID %>").focus();
                }


            }
}
function SelectRow(CurrentRow, RowIndex) {
    UpperBound = parseInt('<%= this.grdPopup.Rows.Count %>') - 1;
    LowerBound = 0;
    if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)
        if (SelectedRow != null) {
            SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
            SelectedRow.style.color = SelectedRow.originalForeColor;
        }
    if (CurrentRow != null) {
        CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
        CurrentRow.originalForeColor = CurrentRow.style.color;
        CurrentRow.style.backgroundColor = '#DCFC5C';
        CurrentRow.style.color = 'Black';
    }
    SelectedRow = CurrentRow;
    SelectedRowIndex = RowIndex;
    setTimeout("SelectedRow.focus();", 0);
}
    </script>
    <script type="text/javascript" language="javascript">
        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }
            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();
                debugger;
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;

                if (hdnfClosePopupValue == "txtMillCode") {
                    document.getElementById("<%=txtMillCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSellingParty") {
                    document.getElementById("<%=txtSellingParty.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGroupCode") {
                    document.getElementById("<%=txtGroupCode.ClientID %>").focus();
                }

                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                document.getElementById("<%= txtSearchText.ClientID %>").value = "";

            }
        });
    </script>
    <script type="text/javascript" language="javascript">

        function MillCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtMillCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMillCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMillCode.ClientID %>").val(unit);
                __doPostBack("txtMillCode", "TextChanged");

            }

        }
        function SellingParty(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtSellingParty.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSellingParty.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSellingParty.ClientID %>").val(unit);
                __doPostBack("txtSellingParty", "TextChanged");

            }

        }
        function GroupCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGroupCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGroupCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGroupCode.ClientID %>").val(unit);
                __doPostBack("txtGroupCode", "TextChanged");

            }

        }
        function tender(e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }
        }

    </script>
    <script type="text/javascript" language="javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();

            var DocNo = document.getElementById("<%= hdnfdocno.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfEntryid.ClientID %>").value;
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var XML = "<ROOT><SaudaBookinghead EntryNo='" + DocNo + "' Entryid='" + Autoid + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "'></SaudaBookinghead></ROOT>";
            var spname = "SP_SaudaBookinghead";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
        }
        function Validate() {
            $("#loader").show();
            var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';
            return true;
        }
        function pagevalidation() {
            debugger;
            $("#loader").hide();
            var Doc_No = 0, headautoid = 0, autodetailid = 0, GId = 0, headauto_id = 0, doc_no = 0, Entryid = 0;
            var spname = "SP_SaudaBookinghead";
            var XML = "<ROOT>";
            var status = document.getElementById("<%=btnSave.ClientID %>").value;
            if (status == "Update") {
                Doc_No = document.getElementById("<%=hdnfdocno.ClientID %>").value;

                doc_no = document.getElementById("<%=hdnfdocno.ClientID %>").value;
                Entryid = document.getElementById("<%=hdnfEntryid.ClientID %>").value;
            }
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");
            var Tran_Type = "PS";
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var EntryNo = "0";
            //var d = "";
            var d = $("#<%=txtEntry_Date.ClientID %>").val();
            var Entry_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            if (status == "Save") {
                DOCNO = "Entryid='" + Entryid + "'";
            }
            else {
                DOCNO = "EntryNo='" + doc_no + "' Entryid='" + Entryid + "'";
            }
            var XML = XML + "<SaudaBookinghead "
            + DOCNO +
            " Entry_Date ='" + Entry_Date +
            "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' >";
            var RowAction = 3;
            var ddid = Entryid;
            for (var i = 1; i < grdrow.length; i++) {
                var Detail_Id = gridView.rows[i].cells[2].innerHTML;

                var Tender_No = gridView.rows[i].cells[4].innerHTML;
                var Cityname = gridView.rows[i].cells[5].innerHTML;
                var SubArea = gridView.rows[i].cells[6].innerHTML;
                if (SubArea == "" || SubArea == "&nbsp;") {
                    SubArea = 0;
                }
                var MillCode = gridView.rows[i].cells[7].innerHTML;
                // var  = gridView.rows[i].cells[9].innerHTML;
                var Grade = gridView.rows[i].cells[9].innerHTML;
                var SelfQty = gridView.rows[i].cells[10].innerHTML;
                var Season = gridView.rows[i].cells[11].innerHTML;
                if (Season == "" || Season == "&nbsp;") {
                    Season = 0;
                }
                var Quantal = gridView.rows[i].cells[12].innerHTML;
                var Salerate = gridView.rows[i].cells[13].innerHTML;
                var d = gridView.rows[i].cells[14].innerHTML;
                var PaymentDate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var d = gridView.rows[i].cells[15].innerHTML;
                var LiftingDate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Googlemap = gridView.rows[i].cells[16].innerHTML;
                if (Googlemap == "" || Googlemap == "&nbsp;") {
                    Googlemap = 0;
                }
                var Image = gridView.rows[i].cells[17].innerHTML;
                if (Image == "" || Image == "&nbsp;") {
                    Image = 0;
                }
                // Image = Image.replace(/ /g, "");
                var SellingParty = gridView.rows[i].cells[18].innerHTML;
                //var  = gridView.rows[i].cells[21].innerHTML;
                var GroupCode = gridView.rows[i].cells[20].innerHTML;
                // var  = gridView.rows[i].cells[24].innerHTML;
                var Timedate = gridView.rows[i].cells[22].innerHTML;
                if (Timedate == "" || Timedate == "&nbsp;") {
                    Timedate = 0;
                }
                var mc = gridView.rows[i].cells[23].innerHTML;
                var sp = gridView.rows[i].cells[24].innerHTML;
                var gc = gridView.rows[i].cells[25].innerHTML;
                var CheckDetail_Id = gridView.rows[i].cells[26].innerHTML;
                var RowAction = 3;
                if (gridView.rows[i].cells[RowAction].innerHTML == "A") {
                    ddid = ddid + 1;
                    XML = XML + "<DetailInsert Doc_No='" + doc_no +
                    "' Detail_Id ='" + Detail_Id +
                    "' CheckDetail_Id ='" + CheckDetail_Id +
                    "' Tender_No ='" + Tender_No +
                    "' Cityname ='" + Cityname +
                    "' SubArea ='" + SubArea +
                    "' MillCode ='" + MillCode +

                    "' Grade ='" + Grade +
                    "' SelfQty ='" + SelfQty +
                    "' Season ='" + Season +
                    "' Quantal ='" + Quantal +
                    "' Salerate ='" + Salerate +
                    "' PaymentDate ='" + PaymentDate +
                    "' LiftingDate ='" + LiftingDate +
                    "' GoogleMap ='" + Googlemap +
                    "' Image ='" + Image +
                    "' SellingParty ='" + SellingParty +

                    "' GroupCode ='" + GroupCode +

                    "' Timedate ='" + Timedate +
                    "' mc ='" + mc +
                    "' sp ='" + sp +
                    "' gc ='" + gc +
                    "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code +
                    "' Entryid='" + Entryid + "' />";
                }
                if (gridView.rows[i].cells[RowAction].innerHTML == "U") {
                    XML = XML + "<DetailUpdate Doc_No='" + doc_no +
                    "' Detail_Id ='" + Detail_Id +
                    "' CheckDetail_Id ='" + CheckDetail_Id +
                    "' Tender_No ='" + Tender_No +
                    "' Cityname ='" + Cityname +
                    "' SubArea ='" + SubArea +
                    "' MillCode ='" + MillCode +

                    "' Grade ='" + Grade +
                    "' SelfQty ='" + SelfQty +
                    "' Season ='" + Season +
                    "' Quantal ='" + Quantal +
                    "' Salerate ='" + Salerate +
                    "' PaymentDate ='" + PaymentDate +
                    "' LiftingDate ='" + LiftingDate +
                    "' GoogleMap ='" + Googlemap +
                    "' Image ='" + Image +
                    "' SellingParty ='" + SellingParty +

                    "' GroupCode ='" + GroupCode +

                    "' Timedate ='" + Timedate +
                    "' mc ='" + mc +
                    "' sp ='" + sp +
                    "' gc ='" + gc +
                    "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code +
                    "' Entryid='" + Entryid + "' />";
                }
                if (gridView.rows[i].cells[RowAction].innerHTML == "D") {
                    XML = XML + "<DetailDelete Doc_No='" + doc_no +
                    "' Detail_Id ='" + Detail_Id +
                    "' CheckDetail_Id ='" + CheckDetail_Id +
                    "' Tender_No ='" + Tender_No +
                    "' Cityname ='" + Cityname +
                    "' SubArea ='" + SubArea +
                    "' MillCode ='" + MillCode +

                    "' Grade ='" + Grade +
                    "' SelfQty ='" + SelfQty +
                    "' Season ='" + Season +
                    "' Quantal ='" + Quantal +
                    "' Salerate ='" + Salerate +
                    "' PaymentDate ='" + PaymentDate +
                    "' LiftingDate ='" + LiftingDate +
                    "' GoogleMap ='" + Googlemap +
                    "' Image ='" + Image +
                    "' SellingParty ='" + SellingParty +

                    "' GroupCode ='" + GroupCode +

                    "' Timedate ='" + Timedate +
                    "' mc ='" + mc +
                    "' sp ='" + sp +
                    "' gc ='" + gc +
                    "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code +
                    "' Entryid='" + Entryid + "' />";
                }
            }
            XML = XML + "</SaudaBookinghead></ROOT>"
            ProcessXML(XML, status, spname);
        }
        function ProcessXML(XML, status, spname) {
            $.ajax({
                type: "POST",
                url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    $("#loader").hide();
                    alert(response.d);
                },
                error: function (response) {
                    $("#loader").hide();
                    alert(response.d);
                }
            });
            function OnSuccess(response) {
                $("#loader").hide();
                if (status != "Delete") {
                    if (response.d.length > 0) {
                        var word = response.d;
                        var len = word.length;
                        var pos = word.indexOf(",");
                        var id = word.slice(0, pos);
                        var doc = word.slice(pos + 1, len);
                        if (status == "Save") {
                            alert('Sucessfully Added Record !!! Doc_no=' + doc)
                        }
                        else {
                            alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                        }
                        window.open('../BussinessRelated/pgeSaudaBooking.aspx', "_self");
                    }
                }
                else {
                    var num = parseInt(response.d);
                    if (isNaN(num)) {
                        swal("" + response.d + "", "", "warning");
                    }
                    else {
                        //Enter utility page name with folder location
                        // window.open('..//.aspx', "self");
                    }
                }
            }
        }
    </script>
    <style type="text/css">
        #loader {
            width: 100%;
            height: 100%;
            background-color: gray;
            position: fixed;
            margin: -0.7%;
            opacity: 0.6;
        }
    </style>


    <script src="../../JQuery/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../JQuery/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <%--<script src="Scripts/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"> </script>--%>

    <style type="text/css">
        .watermark {
            opacity: 0.6;
            color: seagreen;
        }

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }

        .container {
            border-radius: 5px;
            background-color: whitesmoke;
            padding: 20px;
        }


        .button-62 {
            /*background: linear-gradient(to bottom right, #19b259, #000000);*/
            border: 0;
            border-radius: 7px;
            color: #FFFFFF;
            cursor: pointer;
            display: inline-block;
            font-family: -apple-system, system-ui, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
            font-size: 12px;
            font-weight: 500;
            line-height: 2.5;
            outline: transparent;
            padding: 0 1rem;
            text-align: center;
            text-decoration: none;
            transition: box-shadow .2s ease-in-out;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            white-space: nowrap;
        }

            .button-62:not([disabled]):focus {
                box-shadow: 0 0 .25rem rgba(0, 0, 0, 0.5), -.125rem -.125rem 1rem rgba(239, 71, 101, 0.5), .125rem .125rem 1rem rgba(255, 154, 90, 0.5);
            }

            .button-62:not([disabled]):hover {
                box-shadow: 0 0 .25rem rgba(0, 0, 0, 0.5), -.125rem -.125rem 1rem rgba(239, 71, 101, 0.5), .125rem .125rem 1rem rgba(255, 154, 90, 0.5);
            }

        /* CSS */
        .button-grid {
            background: linear-gradient(to bottom right, #fff, #e4dada);
            border: 1px;
            border-radius: 12px;
            color: black;
            cursor: pointer;
            display: inline-block;
            font-family: -apple-system, system-ui, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
            font-size: 16px;
            font-weight: 500;
            line-height: 2.5;
            outline: transparent;
            padding: 0 1rem;
            text-align: center;
            text-decoration: none;
            transition: box-shadow .2s ease-in-out;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            white-space: nowrap;
        }
        .gridview-container {
        overflow-x: hidden;
    }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=grdDetail.ClientID %>').Scrollable({

                ScrollHeight: 600,
                IsInUpdatePanel: true
            });
        });

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Enter page name " Font-Names="verdana" ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfdocno" runat="server" />
            <asp:HiddenField ID="hdnfautoid" runat="server" />
            <asp:HiddenField ID="hdnfEntryid" runat="server" />
            <asp:HiddenField ID="hdnfCheckDetail_Id" runat="server" />
            <%--<asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />--%>
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp" OnClick="btnFirst_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp" OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp" OnClick="btnNext_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp" OnClick="btnLast_Click" Width="90px" Height="24px" />
                    </td>
                </tr>
            </table>

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black" Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">

                <table style="width: 50%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Names="verdana" Font-Italic="true" Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </tr>
                    <tr>

                        <td align="left" style="width: 10%;">changeno
                       
                            <asp:TextBox Height="24px" ID="txtEditDoc_no" runat="Server" CssClass="txt" TabIndex="1" Width="50px" Style="text-align: left;"
                                AutoPostBack="false" OnTextChanged="txtEditDoc_no_TextChanged"></asp:TextBox>
                        </td>



                        <td align="left" style="width: 10%;">Entry No
                       
                            <asp:TextBox Height="24px" ID="txtEntryNo" runat="Server" CssClass="txt" TabIndex="2" Width="50px" Style="text-align: left;"
                                AutoPostBack="false" OnTextChanged="txtEntryNo_TextChanged"
                                onkeydown="EntryNo(event)"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtEntryNo" runat="server" Text="..."
                                OnClick="btntxtEntryNo_Click" CssClass="btnHelp" />
                            <asp:Label ID="lbldoc_no" runat="server" CssClass="lblName"></asp:Label></td>



                        <td align="left" style="width: 10%;">EntryDate
                        
                            <asp:TextBox Height="24px" ID="txtEntry_Date" runat="Server" CssClass="txt" TabIndex="4" Width="90px" Style="text-align: left;"
                                AutoPostBack="false" OnTextChanged="txtEntry_Date_TextChanged" onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtEntry_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtEntry_Date" runat="server" TargetControlID="txtEntry_Date" PopupButtonID="imgcalendertxtEntry_Date" Format="dd/MM/yyyy"></ajax1:CalendarExtender>
                        </td>


                        <tr>
                            <td colspan="4" align="left">
                                <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp" Width="80px" Height="25px"
                                    OnClick="btnOpenDetailsPopup_Click" TabIndex="28" Visible="false" />
                            </td>

                        </tr>
                </table>
                <%-- <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                </fieldset>--%>
            </asp:Panel>

            <table width="100%" align="left" style="font-weight: bold;">

                <tr>
                    <td colspan="4" align="center" style="background-color: lightslategrey; color: White;">
                        <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana" Text="Tender Details"></asp:Label>

                        <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>


                    <td align="left" style="width: 10%;">Tender_no
                        <asp:TextBox ID="txtTender_No" runat="Server" CssClass="txt" TabIndex="8" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtTender_No_TextChanged" Height="24px" onKeyDown="tender(event);"></asp:TextBox>
                        Cityname
                   
                        <asp:TextBox ID="txtCityname" runat="Server" CssClass="txt" TabIndex="9" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtCityname_TextChanged" Height="24px"></asp:TextBox>
                        SubArea
                   
                        <asp:TextBox ID="txtSubArea" runat="Server" CssClass="txt" TabIndex="10" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtSubArea_TextChanged" Height="24px"></asp:TextBox>

                        MillCode
                   
                        <asp:TextBox ID="txtMillCode" runat="Server" CssClass="txt" TabIndex="11" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtMillCode_TextChanged" Height="24px"
                            onkeydown="MillCode(event)"></asp:TextBox>
                        <asp:Button ID="btntxtMillCode" runat="server" Text="..."
                            OnClick="btntxtMillCode_Click" CssClass="btnHelp" />
                        <asp:Label ID="lblmillname" runat="server" CssClass="lblName"></asp:Label>


                        Grade
                   
                        <asp:TextBox ID="txtGrade" runat="Server" CssClass="txt" TabIndex="12" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtGrade_TextChanged" Height="24px"></asp:TextBox>
                        SelfQty
                   
                        <asp:TextBox ID="txtSelfQty" runat="Server" CssClass="txt" TabIndex="13" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtSelfQty_TextChanged" Height="24px"></asp:TextBox>
                        Season
                   
                        <asp:TextBox ID="txtSeason" runat="Server" CssClass="txt" TabIndex="14" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtSeason_TextChanged" Height="24px"></asp:TextBox>
                    </td>


                </tr>
                <tr>

                    <td align="left" style="width: 10%;">Quantal
                   
                        <asp:TextBox ID="txtQuantal" runat="Server" CssClass="txt" TabIndex="15" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtQuantal_TextChanged" Height="24px"></asp:TextBox>
                        Salerate
                   
                        <asp:TextBox ID="txtSalerate" runat="Server" CssClass="txt" TabIndex="16" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtSalerate_TextChanged" Height="24px"></asp:TextBox>
                        PaymentDate
                   
                        <asp:TextBox ID="txtPaymentDate" runat="Server" CssClass="txt" TabIndex="17" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtPaymentDate_TextChanged" onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)" Height="24px"></asp:TextBox>
                        <asp:Image ID="imgcalendertxtPaymentDate" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtPaymentDate" runat="server" TargetControlID="txtPaymentDate" PopupButtonID="imgcalendertxtPaymentDate" Format="dd/MM/yyyy"></ajax1:CalendarExtender>
                        LiftingDate
                    
                        <asp:TextBox ID="txtLiftingDate" runat="Server" CssClass="txt" TabIndex="18" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtLiftingDate_TextChanged" onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)" Height="24px"></asp:TextBox>
                        <asp:Image ID="imgcalendertxtLiftingDate" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtLiftingDate" runat="server" TargetControlID="txtLiftingDate" PopupButtonID="imgcalendertxtLiftingDate" Format="dd/MM/yyyy"></ajax1:CalendarExtender>




                        Googlemap
                    
                        <asp:TextBox ID="txtGooglemap" runat="Server" CssClass="txt" TabIndex="19" Width="200px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtGooglemap_TextChanged" Height="24px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>Image
                    
                        <asp:TextBox ID="txtImage" runat="Server" CssClass="txt" TabIndex="20" Width="150px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtImage_TextChanged" Height="24px"></asp:TextBox>


                        <asp:UpdatePanel runat="server" ID="up2">
                            <ContentTemplate>

                                <asp:FileUpload runat="server" ID="fu1" ForeColor="White" TabIndex="21" />

                                <asp:Button runat="server" ID="btnUpload" CssClass="btnHelp" Text="Upload" Width="70px"
                                    Height="25px" OnClick="btnUpload_Click" TabIndex="22" />

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>SellingParty
                    
                        <asp:TextBox ID="txtSellingParty" runat="Server" CssClass="txt" TabIndex="23" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtSellingParty_TextChanged" Height="24px"
                            onkeydown="SellingParty(event)"></asp:TextBox>
                        <asp:Button ID="btntxtSellingParty" runat="server" Text="..."
                            OnClick="btntxtSellingParty_Click" CssClass="btnHelp" />
                        <asp:Label ID="lblsellingname" runat="server" CssClass="lblName"></asp:Label>GroupCode
                  
                        <asp:TextBox ID="txtGroupCode" runat="Server" CssClass="txt" TabIndex="24" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtGroupCode_TextChanged" Height="24px"
                            onkeydown="GroupCode(event)"></asp:TextBox>
                        <asp:Button ID="btntxtGroupCode" runat="server" Text="..."
                            OnClick="btntxtGroupCode_Click" CssClass="btnHelp" />
                        <asp:Label ID="lblgroupname" runat="server" CssClass="lblName"></asp:Label>




                        Timedate
                   
                        <asp:TextBox ID="txtTimedate" runat="Server" CssClass="txt" TabIndex="25" Width="90px" Style="text-align: left;"
                            AutoPostBack="false" OnTextChanged="txtTimedate_TextChanged" Height="24px"></asp:TextBox>
                    </td>


                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px" Height="25px"
                            OnClick="btnAdddetails_Click" TabIndex="26" />
                        <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px" Height="25px"
                            OnClick="btnClosedetails_Click" TabIndex="27" />
                    </td>

                </tr>
            </table>


            <table width="100%" align="left" style="font-weight: bold;">
                <tr>
                    <td>


                        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">


                            <ContentTemplate>
                                <div class="container" style="width: 2000px;">
                                    <div class="panel panel-default">

                                        <div class="panel-body">
                                            <div style="font-size: 12px; width: 100%; height: 500px;" runat="server" id="DivGrid" visible="true">

                                                <asp:Panel ID="pnlgrdDetail" runat="server"  Height="500px" 
                                                    Width="1700px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana" Font-Size="11px" 
                                                    BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                                                    <%-- <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" HeaderStyle-CssClass="FixedHeader"
                                Width="100%" OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5"
                                OnRowDataBound="grdDetail_RowDataBound" Style="table-layout: fixed;">--%>

                                                    <asp:GridView ID="grdDetail" EmptyDataText="No Record Found"
                                                        runat="server" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"
                                                        CssClass="grid table-condensed table-bordered button-grid"
                                                        AlternatingRowStyle-CssClass="alt" GridLines="Both" Width="100%"
                                                        OnRowCommand="grdDetail_RowCommand" OnRowDataBound="grdDetail_RowDataBound">

                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="500px" HeaderText="Edit">
                                                                <ItemStyle Width="500px" />
                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Text="     Edit      " CommandArgument="lnk"
                                                                        Width="50px"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Delete">
                                                                <ItemStyle Width="500px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" Text="Delete" CommandArgument="lnk" Width="50px"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="ID" DataField="Detail_Id" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20px" />
                                                            <asp:BoundField HeaderText="" DataField="rowAction" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" />
                                                            <asp:BoundField HeaderText="Tender_No" DataField="Tender_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="Cityname" DataField="Cityname" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="SubArea" DataField="SubArea" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="MillCode" DataField="MillCode" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="Mill Name" DataField="MillCode_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="50px" />
                                                            <asp:BoundField HeaderText="Grade" DataField="Grade" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="SelfQty" DataField="SelfQty" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="Season" DataField="Season" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="Quantal" DataField="Quantal" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="Salerate" DataField="Salerate" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="PaymentDate" DataField="PaymentDate" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="LiftingDate" DataField="LiftingDate" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="GoogleMap" DataField="GoogleMap" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="Image" DataField="Image" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />

                                                            <asp:BoundField HeaderText="SellingParty" DataField="SellingParty" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />

                                                            <asp:BoundField HeaderText="SellingParty_Name" DataField="SellingParty_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="GroupCode" DataField="GroupCode" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="GroupCode_Name" DataField="GroupCode_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="Timedate" DataField="Timedate" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="mc" DataField="mc" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />

                                                            <asp:BoundField HeaderText="sp" DataField="sp" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="gc" DataField="gc" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />

                                                            <asp:BoundField HeaderText="CheckDetail_Id" DataField="CheckDetail_Id" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />
                                                            <asp:BoundField HeaderText="SrNo" DataField="SrNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" />












                                                        </Columns>
                                                        <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            



            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="80%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="95%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
                                    <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                    <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                                    <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px" BorderColor="Teal" BorderWidth="1px" Height="300px"
                BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

