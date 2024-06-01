﻿<%@ Page Title="Group Utility" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGroupUtility.aspx.cs" Inherits="Sugar_BussinessRelated_pgeGroupUtility" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/DateValidation.js">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>

    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>


    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>

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
                if (hdnfClosePopupValue == "txtmillcode") {
                    document.getElementById("<%=txtmillcode.ClientID %>").focus();
                }


                if (hdnfClosePopupValue == "BU") {
                    document.getElementById("<%=txtMemberName.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtotherpaidAc") {
                    document.getElementById("<%=txtotherpaidAc.ClientID %>").focus();
                   }
              
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>

    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            debugger;
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
                if (hdnfClosePopupValue == "txtmillcode") {

                    document.getElementById("<%=txtmillcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=lblmillname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    document.getElementById("<%=txttender.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=hdnftenderno.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    //document.getElementById("<%=hdnfID.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[14].innerText;
                    document.getElementById("<%=lblselfqty.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[6].innerText;
                    document.getElementById("<%=lblgrade.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%=lbllifting.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=lblmillrate.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    document.getElementById("<%=lblGroupName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[7].innerText;   
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtmillcode.ClientID %>").focus();

                }


                if (hdnfClosePopupValue == "BU") {

                    document.getElementById("<%=txtMemberName.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMemberName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtMemberName.ClientID %>").focus();
                }


                if (hdnfClosePopupValue == "txtsaudaReverseAc") {
                    document.getElementById("<%=txtsaudaReverseAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblsaudaReverseAcName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtsaudaReverseAc.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtotherpaidAc") {
                    document.getElementById("<%=txtotherpaidAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblOtherPaidName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                      document.getElementById("<%=txtotherpaidAc.ClientID %>").focus();
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

        function Party(e) {
            debugger;

            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnMember.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMemberName.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMemberName.ClientID %>").val(unit);
                __doPostBack("txtMemberName", "TextChanged");

            } 
        }
function sendingaccode(e) {
    debugger;
    if (e.keyCode == 112) {
        debugger;
        e.preventDefault();
        $("#<%=pnlPopup.ClientID %>").show();

        $("#<%=btntxtmillcode.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtmillcode.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtmillcode.ClientID %>").val(unit);
        __doPostBack("txtmillcode", "TextChanged");

    }

}

        function otherpaidAc(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnotherpaidAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtotherpaidAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtotherpaidAc.ClientID %>").val(unit);
                __doPostBack("txtotherpaidAc", "TextChanged");

            }

        }

        function ReverseAc(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnsaudaReverseAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtsaudaReverseAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtsaudaReverseAc.ClientID %>").val(unit);
                __doPostBack("txtsaudaReverseAc", "TextChanged");

            }

        }

function qtl(e) {
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtBuyerQuantal.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtBuyerQuantal.ClientID %>").val(unit);
        __doPostBack("txtBuyerQuantal", "TextChanged");

    }
} 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <asp:Label ID="label1" runat="server" Text="Group Utility" Font-Names="verdana"
                        ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
            </fieldset>
            
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnfpopup" runat="server" />
            <asp:HiddenField ID="hdnfID" runat="server" />
            <asp:HiddenField ID="hdnfGroupCode" runat="server" />
            <asp:HiddenField ID="hdnftenderno" runat="server" />
            <asp:HiddenField ID="hdnfmember" runat="server" />
            <asp:HiddenField ID="hdnfotherpaid" runat="server" />
            <asp:HiddenField ID="hdnfReverseAcId" runat="server" />
            <asp:HiddenField ID="hdnfProfit" runat="server" />
            <asp:HiddenField ID="hdnfMillRate" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">

                <table style="width: 60%;" align="center" cellpadding="4" cellspacing="4">
                      <tr>
                        <td align="right" style="width: 30%;">Mill Code:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox Height="24px" ID="txtmillcode" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" OnkeyDown="sendingaccode(event);" AutoPostBack="false"
                                OnTextChanged="txtmillcode_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtmillcode" runat="server" Text="..."
                                OnClick="btntxtmillcode_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblmillname" runat="server" CssClass="lblName"></asp:Label>
                             <asp:Label ID="lblGroupName" runat="server" CssClass="lblName"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Tender No:
                        </td>
                        <td align="left" style="width: 30%;">
                            <asp:TextBox Height="24px" ID="txttender" runat="Server" CssClass="txt" TabIndex="2"
                              ReadOnly="true"    Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            <asp:Label ID="lblselfqty" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblgrade" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lbllifting" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblmillrate" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblseason" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblpurcrate" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Member Name:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtMemberName" runat="server" Width="80px" Height="24px" AutoPostBack="false"
                                CssClass="txt" OnTextChanged="txtMemberName_TextChanged" onkeydown="Party(event);" TabIndex="14"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyer" FilterType="Numbers" TargetControlID="txtMemberName"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btnMember" runat="server" Text="..." Height="24px" Width="20px" CssClass="btnHelp"
                                OnClick="btnMember_Click" />
                            <asp:Label ID="lblMemberName" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblMemberID" runat="server" Visible="false"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Buyer Quantal:
                                
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtBuyerQuantal" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtBuyerQuantal_TextChanged" TabIndex="15"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerQuantal" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtBuyerQuantal">
                            </ajax1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtBuyerQuantal" runat="server" ControlToValidate="txtBuyerQuantal"
                                CssClass="validator" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true"
                                Text="Required" ValidationGroup="addBuyerDetails">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Sale Rate:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtBuyerSaleRate" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtBuyerSaleRate_TextChanged" TabIndex="16"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerSaleRate" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtBuyerSaleRate">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Item Amount:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtItemAmount" runat="server" CssClass="txt" Height="24px"
                                TabIndex="17" Width="80px" AutoPostBack="true" OnTextChanged="txtItemAmount_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerCommission" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtItemAmount">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Paid:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtPaid" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtPaid_TextChanged" TabIndex="18"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">paid Date:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtDetailpaidDate" runat="server" CssClass="txt" Width="100px"
                                AutoPostBack="True" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)"
                                onkeydown="return DateFormat(this,event.keyCode)" OnTextChanged="txtDetailpaidDate_TextChanged"
                                TabIndex="19" Height="24px"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                Height="15px" />
                            <ajax1:CalendarExtender ID="calendertxtDetailpaidDate" runat="server" TargetControlID="txtDetailpaidDate"
                                PopupButtonID="Image1" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>

                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Narration:
                        </td>
                        <td align="left" style="width: 20%;">

                            <asp:TextBox ID="txtBuyerNarration" runat="server" Width="360px" CssClass="txt" Height="50px"
                                TextMode="MultiLine" TabIndex="20" AutoPostBack="true" OnTextChanged="txtBuyerNarration_TextChanged"></asp:TextBox>

                            <%--<td colspan="2" align="center">--%>
                             
                               
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Other Paid Name:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtotherpaidAc" runat="server" Width="80px" Height="24px" AutoPostBack="false"
                                CssClass="txt" OnTextChanged="txtotherpaidAc_TextChanged" onkeydown="otherpaidAc(event);" TabIndex="21"></asp:TextBox>
                            <asp:Button ID="btnotherpaidAc" runat="server" Text="..." Height="24px" Width="20px" CssClass="btnHelp"
                                OnClick="btnotherpaidAc_Click" />
                            <asp:Label ID="lblOtherPaidName" runat="server" CssClass="lblName"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Other   Paid:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtotherPaid" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtotherPaid_TextChanged" TabIndex="22"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Sauda Reverse Ac:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtsaudaReverseAc" runat="server" Width="80px" Height="24px" AutoPostBack="false"
                                CssClass="txt" OnTextChanged="txtsaudaReverseAc_TextChanged" onkeydown="ReverseAc(event);" TabIndex="23"></asp:TextBox>
                            <asp:Button ID="btnsaudaReverseAc" runat="server" Text="..." Height="24px" Width="20px" CssClass="btnHelp"
                                OnClick="btnsaudaReverseAc_Click" />
                            <asp:Label ID="lblsaudaReverseAcName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Rate Per Qtl:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtRatePerQtl" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtRatePerQtl_TextChanged" TabIndex="24"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Reverse Amount:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtReverseAmount" runat="server" Width="80px" Height="24px" CssClass="txt"
                                ReadOnly="true" AutoPostBack="true" OnTextChanged="txtReverseAmount_TextChanged" TabIndex="25"></asp:TextBox>

                        </td>
                    </tr>
                     <tr align="right">
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" TabIndex="26"
                                Width="90px" Height="24px" ValidationGroup="save"  OnClick="btnUpdate_Click" />
                           <%--  <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" TabIndex="21"
                                Width="90px" Height="24px" ValidationGroup="save" OnClientClick = "Confirm()" OnClick="btnUpdate_Click" />--%>
                             

                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="85%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%; margin: 0 auto;">
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
                            <asp:Panel ID="pnlInner" runat="server" ScrollBars="Both" Width="100%" Direction="LeftToRight"
                                BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="13" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

