 <%@ Page Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="SelectMemberData.aspx.cs" Inherits="WebApplication1.ManagerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/all.css" rel="stylesheet" />
    <script src="js/all.js"></script>
    <script src="My97DatePicker/WdatePicker.js"></script>
   <script src="My97DatePicker/WdatePicker.js"></script>
   <style>
        /* Icon when the collapsible content is shown */
        .btn:after {
            font-family: "Glyphicons Halflings";
            
            margin-left: 15px;
        }
        /* Icon when the collapsible content is hidden */
        .btn.collapsed:after {
            content: "\e080";
        }

        .clickable {
            cursor: pointer;
        }

        .panel-heading span {
            margin-top: -20px;
            font-size: 15px;
        }

        html {
            height: 100%;
        }



        .auto-style1 {
            width: 533px;
        }

        .panel-primary {
            width: 30vw;
        }
    </style>
  <script>
        $(document).on('click', '.panel-heading.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
            }
        })
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    </asp:UpdatePanel>
    <br />

    <div class="panel panel-primary">
        <div class="panel-heading pull clickable">
            <h3 class="panel-title "><span>給予權限<i class="glyphicon glyphicon-chevron-up"></i></span></h3>
        </div>
        <div class="panel-body">
            <table>
                <tr>
                    <td class="auto-style1">
                        <div class="panel panel-default" >

                            <div class="panel-heading">
                                單位<br/>
                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN"></asp:DropDownList><br/>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp) ORDER BY [TID]">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="Dp" Name="Tp" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                
                                名稱<br/>
                                <asp:DropDownList ID="DDL_Name" runat="server" DataTextField="Name" DataValueField="Name" DataSourceID="SqlDataSource3" AutoPostBack="True"></asp:DropDownList>
                                <asp:DropDownList ID="DDL_EID" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="EID" DataValueField="EID">
                                </asp:DropDownList>
                                <br/>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [EID] FROM [UserInfo] WHERE ([Name] = @Name)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DDL_Name" Name="Name" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource><br/>
                               
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [Name] FROM [UserInfo] WHERE ([Department] = @Department)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownList2" Name="Department" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource><br/>
                                <br />
                                將從 <input type="text" name="DS" class="Wdate" id="DS" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm', minDate: '%y-%M-%d,%H,%m', maxDate: '#F{$dp.$D(\'DE\')}' })" />

                               到 <input type="text" name="DE" class="Wdate" id="DE" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm', minDate: '#F{$dp.$D(\'DS\')}' })" />&nbsp; 止<br />
                                <br />
                                擁有權限<asp:DropDownList ID="DDL_Permission" runat="server">
                                    <asp:ListItem Value="1"></asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />

                            </div>
                            <div class="panel-body">

                                <br />
                                <br />
                                <asp:Button ID="Btn_Send_Permission" runat="server" Text="send" OnClick="Btn_Send_Permission_Click" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        &nbsp;</td>
                </tr>
            </table>
        </div>

    </div>











</asp:Content>
