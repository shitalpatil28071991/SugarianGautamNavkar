﻿<%@ Page Title="Address" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeAddressforvoucher.aspx.cs" Inherits="Sugar_pgeAddressforvoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnfyearcode" runat="server" />
    <asp:HiddenField ID="hdnfcompanycode" runat="server" />
    <asp:HiddenField ID="hdnfga" runat="server" />
    <asp:HiddenField ID="hdnfpa" runat="server" />


    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Header Address   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlMain">
        <table width="60%" align="center" cellspacing="5">
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Branch 1 Address Line 1:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtAddressLine1" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Branch 1 Address Line 2:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtAddressLine2" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Branch 2 Address Line 1:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtAddressLine3" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Branch 2 Address Line 2:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtAddressLine4" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Other:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtOtherDetail" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Bill Footer:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtBillFooter" Width="600px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td align="right" style="width: 30%;">
                    <b>Bank Detail:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtbankdetail" Width="600px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td align="right" style="width: 30%;">
                    <b>DB Backup Drive:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtdbbackup" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>



               <tr>
                <td align="right" style="width: 30%;">
                    <b>Google Pay Bank A/c</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtGooglepayac" AutoPostBack="True"  OnTextChanged="txtGooglepayac_Click" Width="50px" Height="24px"></asp:TextBox>

                    <asp:Button ID="btntxtGooglepayac" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                        Width="20px" OnClick="btntxtGooglepayac_Click" />
                    <asp:Label ID="lblGooglepayacname" runat="server" CssClass="lblName"></asp:Label>
                </td>
            </tr>

            <tr>
                <td align="right" style="width: 30%;">
                    <b>Phone Pay Bank A/c</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtPhonepayac" AutoPostBack="True" OnTextChanged="txtPhonepayac_Click"  Width="50px" Height="24px"></asp:TextBox>

                    <asp:Button ID="btntxtPhonepayac" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                        Width="20px" OnClick="btntxtPhonepayac_Click" />
                    <asp:Label ID="lblPhonepayacname" runat="server" CssClass="lblName"></asp:Label>
                </td>
            </tr>
             <tr>
                <td align="right" style="width: 30%;">
                    <b>UnLock Do:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtunLockDo" AutoPostBack="True" OnTextChanged="txtunLockDo_Click"  Width="50px" Height="24px"></asp:TextBox>

                    <asp:Button ID="btnUnLockDo" runat="server" Text="UnLock Do" CssClass="btnHelp" Height="24px"
                        Width="100px" OnClick="btnUnLockDo_Click" /> 
                </td>
            </tr>



            <tr>
                <td align="center" colspan="2">
                    <asp:Button runat="server" ID="btnSave" Text="SAVE" CssClass="btnHelp" Width="100px"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
