<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/OHRI.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DocParser.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
        <div class="col-sm-12">
            <asp:FileUpload ID="fuDoc" runat="server" AllowMultiple="true"/>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12">
            <asp:Button ID="btnParse" runat="server" CssClass="btn btn-primary" Text="Parse" OnClick="btnParse_Click" />
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12">
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>        
    </div>    
</asp:Content>
