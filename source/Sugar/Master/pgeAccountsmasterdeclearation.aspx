﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeAccountsmasterdeclearation.aspx.cs" Inherits="Sugar_Master_pgeAccountsmasterdeclearation" %>


<%@ MasterType VirtualPath="~/MasterPage2.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        debugger;
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
                if (hdnfClosePopupValue == "txtcustomer_code") {
                    document.getElementById("<%=txtcustomer_code.ClientID %>").focus();
                }
               
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }
        });
    </script>
    <script type="text/javascript">
        function Transfer() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= txtdoc_no.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnflvid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><Declearation doc_no='" + DocNo + "' id='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                   "Year_Code='" + Year_Code + "'></Declearation></ROOT>";
            var spname = "SP_Declearation";
            var status = "Transfer";
            ProcessXML(XML, status, spname);
        }

    </script>
    <script type="text/javascript">
        function Confirm() {
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

                if (hdnfClosePopupValue == "txtcustomer_code") {
                    document.getElementById("<%=txtcustomer_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblcustomer.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtcustomer_code.ClientID %>").focus();
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
    <script type="text/javascript">
        debugger;
        function customer(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtcustomer_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtcustomer_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtcustomer_code.ClientID %>").val(unit);
                __doPostBack("txtcustomer_code", "TextChanged");

            }

        } 
    </script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();

            {
                var DocNo = document.getElementById("<%= hdnflvdoc.ClientID %>").value;
                var Autoid = document.getElementById("<%= hdnflvid.ClientID %>").value;
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';

                var XML = "<ROOT><Declearation doc_no='" + DocNo + "' id='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                       "Year_Code='" + Year_Code + "'></Declearation></ROOT>";
                var spname = "SP_Declearation";
                var status = document.getElementById("<%= btnDelete.ClientID %>").value;
                $.ajax({
                    type: "POST",
                    url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                    data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //success: OnSuccess,
                    //failure: function (response) {
                    //    alert(response.d);
                    //    $("#loader").hide();
                    //},
                    //error: function (response) {
                    //    alert(response.d);
                    //    $("#loader").hide();
                    //}
                });
                window.open('../Transaction/pgePaymentNote_Utility.aspx', "_self");
                //   ProcessXML(XML, status, spname);
            }
        }
        function Validate() {
            $("#loader").show();
            debugger;

            var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';

            var d = $("#<%=txtDOC_DATE.ClientID %>").val();
            var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
            EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

            if (DocDates >= StartDate && DocDates <= EndDate) {
            }
            else {
                $("#<%=txtDOC_DATE.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtcustomer_code.ClientID %>").val() == "") {
                $("#<%=txtcustomer_code.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

           
            return true;
        }

        function pagevalidation() {
            debugger;
            try {
                $("#loader").show();
                var doc_no = 0, id = 0;
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                var spname = "SP_Declearation";
                var XML = "<ROOT>";
                if (status == "Update") {
                    doc_no = document.getElementById("<%= hdnflvdoc.ClientID %>").value;
                    id = document.getElementById("<%= hdnflvid.ClientID %>").value;
                }

                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var Doc_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var customer_code = $("#<%=txtcustomer_code.ClientID  %>").val() == "" ? 0 : $("#<%=txtcustomer_code.ClientID %>").val(); 
                var tdstcslink = $("#<%=txttdstcslink.ClientID %>").val(); 

                var ac = document.getElementById("<%= hdnfacid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfacid.ClientID %>").value; 

                var USER = '<%= Session["user"] %>';
                var Branch_Id = '<%= Session["Branch_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';

               

                var LocalVoucherInsertUpdet;
                var Gledger_Insert = ""; Gledger_values = "";
                var Gledger_Delete = "";
                debugger;
                var DOCNO = "";
                if (status == "Save") {
                    LocalVoucherInsertUpdet = "Created_By='" + USER + "' Modified_By=''";
                    DOCNO = "doc_date='" + Doc_Date + "'";
                }
                else {
                    LocalVoucherInsertUpdet = "Modified_By='" + USER + "' Created_By=''";
                    DOCNO = "doc_no='" + doc_no + "' doc_date='" + Doc_Date + "'";
                }
                debugger;

                XML = XML + "<Declearation " + DOCNO + " customer_code='" + customer_code + "'  tdstcslink='" + tdstcslink + "' " +
                   "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' acid='" + ac + "' id='" + id + "'>";

 
                XML = XML + "</Declearation></ROOT>";
                ProcessXML(XML, status, spname);
            }
            catch (exx) {
                $("#loader").hide();
                alert(exx)
            }

            function ProcessXML(XML, status, spname) {
                debugger;

                $.ajax({
                    type: "POST",
                    url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                    data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);
                        $("#loader").hide();
                    },
                    error: function (response) {
                        alert(response.d);
                        $("#loader").hide();
                    }
                });


                function OnSuccess(response) {
                    debugger;
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
                            window.open('../Master/pgeAccountsmasterdeclearation.aspx?id=' + id + '&Action=1', "_self");

                        }
                    }
                    else {
                        var num = parseInt(response.d);

                        if (isNaN(num)) {
                            alert(response.d)

                        }
                        else {
                            window.open('../Master/pgeAccountsmasterdeclearation.aspx', "_self");
                        }
                    }

                }

            }

        }
    </script>
    <style type="text/css">
        #loader
        {
            width: 100%;
            height: 100%;
            background-color: gray;
            position: fixed;
            margin: -0.7%;
            opacity: 0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
            margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
            border-left: 0px; border-right: 0px; height: 7px;">
            <legend style="text-align: center;">
                <asp:Label ID="label1" runat="server" Text="   Payment Note   " Font-Names="verdana"
                    ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
        </fieldset>
        <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px;
            float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px;
            border-right: 0px;">
            <img height="20%" width="20%" src="../Images/ajax-loader3.gif" />
        </div>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdconfirm" runat="server" />
                <asp:HiddenField ID="hdnfClosePopup" runat="server" />
                <asp:HiddenField ID="hdnf" runat="server" />
                <asp:HiddenField ID="hdnfSuffix" runat="server" />
                <asp:HiddenField ID="hdHelpPageCount" runat="server" />
                <asp:HiddenField ID="hdnfdocno" runat="server" />
                <asp:HiddenField ID="hdnfacid" runat="server" /> 
                <asp:HiddenField ID="hdnfyearcode" runat="server" />
                <asp:HiddenField ID="hdnfcompanycode" runat="server" />
                <asp:HiddenField ID="hdnflvdoc" runat="server" />
                <asp:HiddenField ID="hdnflvid" runat="server" />
                <asp:HiddenField ID="hdnfAcode" runat="server" />
                <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;" BackColor="Yellow">
                    <table width="80%" align="left">
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                    ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                                &nbsp;
                                <asp:Button ID="btnSave" runat="server" OnClientClick="if (!Validate()) return false;"
                                    Text="Save" CssClass="btnHelp" Width="90px" TabIndex="7" Height="24px" ValidationGroup="add"
                                    OnClick="btnSave_Click" />
                                &nbsp;
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                    TabIndex="8" Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                                &nbsp;
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                    Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                                    TabIndex="9" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                    TabIndex="10" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                    ValidationGroup="save" Height="24px" OnClientClick="Back();" />
                                &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" Width="90px"
                                    TabIndex="11" Height="24px" OnClick="btnPrint_Click" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;" align="left" cellpadding="2" cellspacing="5">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">
                                Change No:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                    TargetControlID="txtEditDoc_No">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">
                                Declearation No:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtdoc_no_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtDOC_NO" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                    CssClass="btnHelp" Height="24px" />
                            </td>
                            <%--<td align="left">
                        </td>--%>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">
                                Date:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDOC_DATE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">
                               Customer Code:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtcustomer_code" runat="Server" CssClass="txt" TabIndex="3" Width="90px"
                                    onkeydown="customer(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtcustomer_code_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtcustomer_code" runat="server" Text="..." OnClick="btntxtcustomer_code_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblcustomer" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <td align="right" style="width: 30%;">
                                Gst No:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtGst_No" runat="Server" CssClass="txt" TabIndex="5" Width="160px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">
                                Link:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txttdstcslink" runat="Server" CssClass="txt" TabIndex="6" Width="350px"
                                    Style="text-align: right;"  AutoPostBack="True" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <br />
                <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" autosize="true"
                    Width="80%" align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                    Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
                    min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
                    left: 10%; top: 10%;">
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
                            <td>
                                Search Text:
                                <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                    Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                    CssClass="btnSubmit" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlInner" runat="server" ScrollBars="Auto" autosize="true" Width="100%"
                                    Direction="LeftToRight" BackColor="#FFFFE4" Style="z-index: 5000; float: right;
                                    overflow: auto; height: 400px">
                                    <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                        AllowPaging="true" PageSize="25" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                        HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                        OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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
                <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                    BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999;
                    left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                    <table width="80%" align="center">
                        <tr>
                            <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                                <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                    Text="Tender Details"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>