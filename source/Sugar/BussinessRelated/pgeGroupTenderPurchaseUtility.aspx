﻿<%@ Page Title="Group Tender Utility" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGroupTenderPurchaseUtility.aspx.cs" Inherits="Sugar_BussinessRelated_pgeGroupTenderPurchaseUtility" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

        table
        {
            border: 1px solid #ccc;
        }

            table th
            {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
                height: 30px;
                font-size: 18px;
                font-family: 'Times New Roman';
                text-align: center;
            }

            table th, table td
            {
                padding: 5px;
                border-color: #ccc;
                font-weight: bolder;
            }

        .Pager span
        {
            color: #333;
            background-color: #F7F7F7;
            font-weight: bold;
            text-align: center;
            display: inline-block;
            width: 50px;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #ccc;
        }

        .Pager a
        {
            text-align: center;
            display: inline-block;
            width: 20px;
            border: 1px solid #ccc;
            color: #fff;
            color: #333;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }

        .highlight
        {
            background-color: #FFFFAF;
        }

        #gvCustomers
        {
            margin-left: auto;
            margin-right: auto;
            width: 200px;
        }
            /*#gvCustomers th
          {
        background-color:;
        color:#ffffff;
         }*/
            #gvCustomers tr:nth-child(even)
            {
                background-color: #ffffff;
            }

            #gvCustomers tr:nth-child(odd)
            {
                /*background-color: #cccccc;*/
                background-color: lightblue;
            }

            #gvCustomers tr.MouseOver:hover
            {
                background-color: coral;
            }

        td
        {
            cursor: pointer;
        }
    </style>


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JQuery/ASPSnippets_Pager.min.js" type="text/javascript"></script>


    <script type="text/javascript">
        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopEnterKey;
    </script>



    <script type="text/javascript">
        //    $("#drpTrnType").change(function(){
        //    GetCustomers(1);
        //}
        $(function () {
            GetCustomers(1);
        });
        $("[id*=txtSearch]").live("keyup", function () {
            GetCustomers(parseInt(1));
        });
        $(".Pager .page").live("click", function () {
            GetCustomers(parseInt($(this).attr('page')));
        });
        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };
        var value = $("#<%=drpPagesize.ClientID %>").val();
        var value = $('#drpTrnType-options option:checked').val();
        //var val = $('#drpTrnType').val();
        function GetCustomers(pageIndex) {
            debugger;
            $.ajax({
                type: "POST",
                url: "../BussinessRelated/pgeGroupTenderPurchaseUtility.aspx/GetCustomers",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: "' + pageIndex + '",PageSize: "' + $("#<%=drpPagesize.ClientID %>").val() + '",Company_Code: "' + '<%= Session["Company_Code"] %>' + '",year: "' + '<%= Session["year"] %>' + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
        var row;
        function OnSuccess(response) {
            debugger;
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var customers = xml.find("Customers");
            if (row == null) {
                row = $("[id*=gvCustomers] tr:last-child").clone(true);
            }
            $("[id*=gvCustomers] tr").not($("[id*=gvCustomers] tr:first-child")).remove();
            if (customers.length > 0) {
                $.each(customers, function () {
                    var customer = $(this);
                    $("td", row).eq(0).html($(this).find("Tender_No").text());
                    $("td", row).eq(1).html($(this).find("Tender_dateConverted").text());
                    $("td", row).eq(2).html($(this).find("GroupName").text());
                    $("td", row).eq(3).html($(this).find("millShortName").text());
                    $("td", row).eq(4).html($(this).find("Quantal").text());
                    $("td", row).eq(5).html($(this).find("Mill_Rate").text());
                    $("td", row).eq(6).html($(this).find("Grade").text());   
                    $("td", row).eq(7).html($(this).find("ourTenderNo").text());
                    $("td", row).eq(8).html($(this).find("Buyer_Quantal").text());
                    $("td", row).eq(9).html($(this).find("isAccounted").text());
                    $("td", row).eq(10).html($(this).find("grouptenderid").text());
                    $("td", row).eq(11).html($(this).find("IsDeleted").text());


                    $("[id*=gvCustomers]").append(row);
                    row = $("[id*=gvCustomers] tr:last-child").clone(true);
                });
                var pager = xml.find("Pager");
                $(".Pager").ASPSnippets_Pager({
                    ActiveCssClass: "current",
                    PagerCssClass: "pager",
                    PageIndex: parseInt(pager.find("PageIndex").text()),
                    PageSize: parseInt(pager.find("PageSize").text()),
                    RecordCount: parseInt(pager.find("RecordCount").text())
                });

                $(".millshortname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".paymenttoname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".tenderdoname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".brokershortname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });


            } else {
                var empty_row = row.clone(true);
                $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                $("td:first-child", empty_row).attr("align", "center");
                $("td:first-child", empty_row).html("No records found for the search criteria.");
                $("td", empty_row).not($("td:first-child", empty_row)).remove();
                $("[id*=gvCustomers]").append(empty_row);
            }
            $('#gvCustomers  tr').each(function () {
                debugger;
                var number = $(this).children('td:eq(11)').text();

                if (number == '0') {
                    $(this).children('td').css('background', 'LightPink');
                }
            })
        };

    </script>
    <script type="text/javascript">
        debugger;
        $(document).ready(function () {
            //=================================================================
            //click on table body
            //$("#tableMain tbody tr").click(function () {
            $('#gvCustomers ').on('dblclick', 'tr', function () {
                debugger;
                //get row contents into an array
                var ptsID = $(this).parent("tr").find("td:first").text();

                var tableData = $(this).children("td").map(function () {
                    return $(this).text();
                }).get();
                var row_index = $(this).index();
                var Action = 1;
                var Ac_Code = tableData[10];
                if (row_index > 0) {
                    if (isNaN(Ac_Code)) {
                    }
                    else {
                        window.open('../BussinessRelated/pgeGroupTenderPurchase.aspx?grouptenderid=' + Ac_Code + '&Action=' + Action);
                    }
                }

            });


        });
    </script>
    <script type="text/javascript">
        function TenderHead() {
            var Action = 2;
            var Ac_Code = 0;
            window.open("../BussinessRelated/pgeGroupTenderPurchaseUtility.aspx", "_self")
            window.open('../BussinessRelated/pgeGroupTenderPurchase.aspx?Tender_No=' + Ac_Code + '&Action=' + Action);
        }
    </script>
    <script type="text/javascript">
        function functionToTriggerClick() {
            debugger;
            var drpval = $('#<%=drpPagesize.ClientID %> OPTION:selected').val();
            GetCustomers(1);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Show" Font-Bold="True"
                ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpPagesize" runat="server" AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="15" Value="15" Selected="True"></asp:ListItem>
                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                <asp:ListItem Text="100" Value="100"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label2" runat="server" Text="Entries" Font-Bold="True"
                ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
            <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btnHelp" OnClientClick="TenderHead()"
                ValidationGroup="save" Width="90px" Height="24px" OnClick="btnAdd_Click" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;    
             <asp:Label ID="lblUtilityName" runat="server" Text="Group Tender Purchase" Font-Bold="True"
                 ForeColor="#000080" Font-Size="X-Large"></asp:Label>
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                 <asp:Label ID="Label3" runat="server" Text=" Search:" Font-Bold="True"
                     ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Height="25px" Font-Bold="true" />
            <div id="popup" style="max-height: 600px; overflow-y: scroll;">

                <center>
                    &nbsp;
               
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" PageSize="10" Width="1700px" Height="80"
                    RowStyle-CssClass="MouseOver" ClientIDMode="Static" RowStyle-Height="30px">

                    <Columns>



                        <asp:BoundField HeaderStyle-Width="10px" DataField="Tender_No" HeaderText="Tender No"
                            ItemStyle-CssClass="Ac_Code" ControlStyle-BackColor="#ff0066" FooterStyle-BorderWidth="15" FooterStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="Tender_dateConverted" HeaderText="Tender Date" AccessibleHeaderText="center" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="GroupName" HeaderText="Group Name " />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="millShortName" HeaderText="Mill Short Name" />
                        <asp:BoundField HeaderStyle-Width="10px" DataField="Quantal" HeaderText="Quantal" />
                        <asp:BoundField HeaderStyle-Width="10px" DataField="Mill_Rate" HeaderText="Mill_Rate" />
                        <asp:BoundField HeaderStyle-Width="10px" DataField="Grade" HeaderText="Grade" />  
                        <asp:BoundField HeaderStyle-Width="10px" DataField="ourTenderNo" HeaderText="Our TenderNo" />
                        <asp:BoundField HeaderStyle-Width="10px" DataField="Buyer_Quantal" HeaderText="Self Quantal" />
                        <asp:BoundField HeaderStyle-Width="5px" DataField="isAccounted" HeaderText="Is Accounted" />
                        <asp:BoundField HeaderStyle-Width="10px" DataField="grouptenderid" HeaderText="TenderID" />
                        <asp:BoundField HeaderStyle-Width="10px" DataField="IsDeleted" HeaderText="Is Deleted" />
                    </Columns>
                </asp:GridView>
                </center>
                <br />

            </div>
            <div class="Pager">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

