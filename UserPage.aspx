<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="WebApplication1.UserPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="My97DatePicker/WdatePicker.js"></script>
    <style>
        /* Icon when the collapsible content is shown */
        .btn:after {
            font-family: "Glyphicons Halflings";
            content: "\e114";
            float: right;
            margin-left: 15px;
        }
        /* Icon when the collapsible content is hidden */
        .btn.collapsed:after {
            content: "\e080";
        }

        .row {
            margin-top: 40px;
            padding: 0 10px;
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

        body {
            background-image: url(background.jpg);
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
            background-size: cover;
        }
    </style>
    <script>
        $(document).on('click', '.panel-heading span.clickable', function (e) {
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


    
        <div class="container">
            <div style="height: 87px; width: 1128px;">
                <div style="margin-top: 40px;">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title "><span class="pull clickable">查詢<i class="glyphicon glyphicon-chevron-up"></i></span></h3>

                            </div>
                            <div class="panel-body">
                                <div class="col-xs-6">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            日期查詢
                                        </div>
                                        <div class="panel-body">
                                            發文日期：
    <input type="text" name="d1" class="Wdate form-control" id="d1" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'d2\')||\'%y-%M-%d\'}' })" />
                                            到
                                        <input type="text" name="d2" class="Wdate form-control" id="d2" onclick="WdatePicker({ minDate: '#F{$dp.$D(\'d1\')}', maxDate: '%y-%M-%d' })" />
                                            截止日期：
    <input type="text" name="d3" class="Wdate form-control" id="d3" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'d4\')}' })" />
                                            到
                                        <input type="text" name="d4" class="Wdate form-control" id="d4" onclick="WdatePicker({ minDate: '#F{$dp.$D(\'d3\')}' })" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            關鍵字查詢
                                        </div>
                                        <div class="panel-body">
                                            文號：<asp:TextBox placeholder="輸入文號代碼" ID="Txt_SID" class="form-control" runat="server"></asp:TextBox>
                                            <br />
                                            公文類型：<asp:DropDownList ID="Ddl_Type" runat="server" DataSourceID="SqlDataSource2" DataTextField="TN" DataValueField="TN" class="col-xs-offset-0 form-control" Width="200px">
                                            </asp:DropDownList>
                                            <br />
                                            主旨：<asp:TextBox placeholder="輸入關鍵字" ID="Txt_Title" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <asp:Button ID="Btn_Select" class="btn btn-outline-default" runat="server" Text="查詢" OnClick="Btn_Select_Click" />
                                    &nbsp;
                    <asp:Button ID="Btn_cancel" class="btn btn-outline-default " runat="server" OnClick="Btn_cancel_Click" Text="顯示全部" />
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="Menu" Style="border: 2px #ccc solid; border-radius: 10px;" runat="server" AutoGenerateColumns="False" DataKeyNames="SID" OnRowCommand="GridView1_RowCommand" AllowSorting="True" OnRowDataBound="GridView1_RowDataBound" GridLines="None" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="發文日期">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Date" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="截止日期">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_DeadLine" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="公文類別">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Type" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="公文主旨">
                                    <ItemTemplate>
                                        <asp:LinkButton data-toggle="tooltip" Text='<%#Eval("SID")%>' ID="LinkButton1" runat="server" CommandName="SelData" CausesValidation="False"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="發布單位">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Dep" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="發布人">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Peo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle BackColor="#F2F2F2" Font-Size="Large" />
                            <RowStyle Font-Size="Large" />
                        </asp:GridView>
                    </div>
                </div>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FT" Name="Tp" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>
        </div>
    


    <asp:Label ID="Lbl_EID" runat="server" Text="Label" Visible="False"></asp:Label>



</asp:Content>
