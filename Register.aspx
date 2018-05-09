<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <%--中間分三格，下面一層--%>
    <style>
        * {
            box-sizing: border-box;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
        }



        /* Create three equal columns that floats next to each other */
        .column {
            float: left;
            width: 60%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        .column1 {
            float: left;
            width: 20%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        .column2 {
            float: left;
            width: 20%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        /* Style the footer */
        .footer {
            background-color: #f1f1f1;
            padding: 10px;
            text-align: center;
        }

        /* Responsive layout - makes the three columns stack on top of each other instead of next to each other */
        @media (max-width: 600px) {
            .column {
                width: 100%;
            }
        }
    </style>

    <%--背景漸層--%>
    <%--<style>
        #grad1 {
            height: 1000px;
            background: red; /* For browsers that do not support gradients */
            background: linear-gradient(#fac592, #fae9df); /* Standard syntax (must be last) */
        }
    </style>--%>

</head>
<body>

    <form id="form1" runat="server">

      <%--中間層--%>
        <div class="row">
            <div class="column1" style="background-color: #949494;">
            </div>
            <div class="column" style="background-color: #f1f1f1;">
                <h3>註冊新帳戶</h3>
                <br />
                <br />
                <div class="form-horizontal">

                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Id" CssClass="col-md-2 control-label">使用者名稱</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" placeholder="須以字母開頭，長度在6~18之間，只能包含字符、數字和下劃線。" ID="Id" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Id"
                                CssClass="text-danger" ErrorMessage="必須填寫使用者名稱欄位。" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="validatorRegExId" runat="server" ControlToValidate="Id"
                                Display="Dynamic" ErrorMessage="格式無效，使用者名稱需以字母開頭，長度在6-18之間，只能包含字符、數字和下劃線。" CssClass="text-danger" ValidationExpression="^[a-zA-Z]\w{5,17}$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">密碼</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" placeholder="須以字母開頭，長度在6~18之間，只能包含字符、數字和下劃線。" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="必須填寫密碼欄位。" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="validatorRegExPassword0" runat="server" ControlToValidate="Password"
                                Display="Dynamic" ErrorMessage="格式無效，密碼需以字母開頭，長度在6-18之間，只能包含字符、數字和下劃線。" CssClass="text-danger" ValidationExpression="^[a-zA-Z]\w{5,17}$" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="密碼不相同" ForeColor="Red"></asp:CompareValidator>
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">確認密碼</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" placeholder="須以字母開頭，長度在6~18之間，只能包含字符、數字和下劃線。" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="必須填寫確認密碼欄位。" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Password"
                                Display="Dynamic" ErrorMessage="格式無效，密碼需以字母開頭，長度在6-18之間，只能包含字符、數字和下劃線。" CssClass="text-danger" ValidationExpression="^[a-zA-Z]\w{5,17}$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">姓名</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" MaxLength="10" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="必須填寫姓名欄位。" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Gender" CssClass="col-md-2 control-label">性別</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="Gender" runat="server">
                                <asp:ListItem>男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Gender"
                                CssClass="text-danger" ErrorMessage="必須填寫性別欄位。" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" TextMode="Email" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="必須填寫Email欄位。" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="validatorRegExEmail" runat="server" ControlToValidate="Email"
                                Display="Dynamic" ErrorMessage="E-mail格式無效" CssClass="text-danger" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Tel" CssClass="col-md-2 control-label">電話號碼</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" placeholder="格式為xxx-xxxxxxxx" ID="Tel" TextMode="Phone" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Tel"
                                CssClass="text-danger" ErrorMessage="必須填寫電話號碼欄位。" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="validatorRegExPhoneNumber" runat="server" ControlToValidate="Tel"
                                Display="Dynamic" ErrorMessage="電話號碼的格式無效。" CssClass="text-danger" ValidationExpression="^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Cel" CssClass="col-md-2 control-label">手機號碼</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" placeholder="格式為09xxxxxxxx" ID="Cel" TextMode="Phone" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Cel"
                                CssClass="text-danger" ErrorMessage="必須填寫手機號碼欄位。" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="validatorRegExCellPhoneNumber0" runat="server" ControlToValidate="Cel"
                                Display="Dynamic" ErrorMessage="手機號碼的格式無效。正確格式為09xxxxxxxx" CssClass="text-danger" ValidationExpression="((\d{10})|(((\(\d{2}\))|(\d{2}-))?\d{4}(-)?\d{3}(\d)?))" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Birthday" CssClass="col-md-2 control-label">生日</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Birthday" TextMode="Date" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Birthday"
                                CssClass="text-danger" ErrorMessage="必須填寫生日欄位。" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="Department" CssClass="col-md-2 control-label">部門</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="Department" runat="server" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Dp" Name="Tp" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Department"
                                CssClass="text-danger" ErrorMessage="必須選擇部門。" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <asp:Label runat="server" AssociatedControlID="position" CssClass="col-md-2 control-label">職位</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="position" runat="server" DataSourceID="SqlDataSource2" DataTextField="TN" DataValueField="TN"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="PO" Name="Tp" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="position"
                                CssClass="text-danger" ErrorMessage="必須選擇職位。" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="form-group" style="width: 650px; margin-left: 30%;">
                        <div class="col-md-offset-2 col-md-10">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" OnClick="Button1_Click" Text="註冊" CssClass="btn btn-default" BackColor="#CCCCCC" Height="45px" Width="93px" />
                             
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="Button2" runat="server" Text="返回" BackColor="#CCCCCC" Height="45px" Width="93px" CausesValidation="False" PostBackUrl="~/Home.aspx" />
                            
                        </div>
                    </div>

                </div>
            </div>
            <div class="column2" style="background-color: #949494;">
            </div>
        </div>

        <%--下面層--%>
        <div class="footer" style="background-color: #484646; color: #FFFFFF;">
            <p>資管專題小組</p>
        </div>




    </form>
</body>
</html>
