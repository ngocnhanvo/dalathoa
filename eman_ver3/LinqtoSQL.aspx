<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinqtoSQL.aspx.cs" Inherits="LinqtoSQL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%=Security.UrlBase() %>js/Public_script/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="<%=Security.UrlBase() %>js/Public_script/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <style type="text/css">
    #btn_Linq
    {
        position: absolute;
        margin: -25px 0 0px -60px;
        padding: 12px;
    }
    
    #txt_Linq
    {
        margin: 0px; width: 768px; height: 359px;
    }
    </style>
</head>
<body>
    <form id="form_Linq" runat="server" align="center">
    <asp:TextBox TextMode="MultiLine" ID="txt_Linq" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="btn_Linq" runat="server" OnClick="btn_Linq_Click" Text="Generate SQL Syntax" />
    </form>

    <script type="text/javascript">
        
        $.post('Controller/PublicFunction/LinqtoSQL.ashx?oper=linqtosql', function (result) {
            $('#txt_Linq').val(result);
        });
    </script>
</body>
</html>
