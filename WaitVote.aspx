<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="WaitVote.aspx.cs" Inherits="WebApplication1.WaitVote" %>
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

        .auto-style2 {
            width: 526px;
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
                <div class="panel panel-primary">
                <div class="panel-heading pull clickable panel-collapsed">
                    <h3 class="panel-title "><span>查詢<i class="glyphicon glyphicon-chevron-down"></i></span></h3>
                </div>
                <div class="panel-body collapse">
                    <table>
                        <tr>
                            <td class="auto-style1">
                                <div class="panel panel-default" style="width: 500px">
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
                            </td>
                            <td class="auto-style2">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        關鍵字查詢
                                    </div>
                                    <div class="panel-body">
                                        文號：<asp:TextBox placeholder="輸入文號代碼" ID="Txt_SID" class="form-control" runat="server"></asp:TextBox>
                                        <br />
                                        主旨：<asp:TextBox placeholder="輸入關鍵字" ID="Txt_Title" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:Button ID="Btn_Select" class="btn btn-outline-default" runat="server" Text="查詢" OnClick="Btn_Select_Click" />
                        &nbsp;
                    <asp:Button ID="Btn_cancel" class="btn btn-outline-default " runat="server" OnClick="Btn_cancel_Click" Text="顯示全部" />
                        &nbsp;
                    </div>
                </div>
            </div>
            <div>
                每頁顯示<asp:TextBox ID="TxtPageSize" runat="server" Height="16px" TextMode="Number" Width="39px"></asp:TextBox>
                筆資料
            <asp:Button ID="Change" runat="server" CausesValidation="False" class="btn btn-default" OnClick="Change_Click" Text="更改" />
            </div>
            <asp:GridView ID="Menu" Style="border: 2px #ccc solid; border-radius: 10px;" runat="server" AutoGenerateColumns="False" DataKeyNames="SID" OnRowCommand="GridView1_RowCommand" AllowSorting="True" OnRowDataBound="GridView1_RowDataBound" GridLines="None" Width="100%" EmptyDataText="暫無待處理項目"
                AllowPaging="True" OnRowCreated="Gridview_OnRowCreated"
                OnSorting="Gv_Sorting" PageSize="3" OnPageIndexChanging="gv_PageIndexChanging">
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#ffffff" HorizontalAlign="Center" />
                <SortedAscendingHeaderStyle CssClass="asc" />
                <SortedDescendingHeaderStyle CssClass="dsc" />
                <Columns>
                    <asp:BoundField DataField="Date" DataFormatString="{0:D}" HeaderText="公告日期" SortExpression="Date"/>
                    <asp:TemplateField HeaderText="截止日期">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("DeadLine")%>' ID="Lbl_DeadLine" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="公文主旨" SortExpression="Title">
                        <ItemTemplate>
                            <asp:LinkButton data-toggle="tooltip" Text='<%#Eval("Title")%>' ID="Lb_SID" runat="server" CommandName="SelData" CausesValidation="False"></asp:LinkButton>
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
                                <asp:TemplateField HeaderText="SID" Visible="False">
                <ItemTemplate>
                    <asp:Label Text='<%#Eval("SID")%>' ID="Lbl_SID" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
                <HeaderStyle BackColor="#F2F2F2" Font-Size="Large" />
                <RowStyle Font-Size="Large" BackColor="White" />
            </asp:GridView>
            <div>
                每頁
            <asp:Label ID="lbl_1" runat="server" Text=""></asp:Label>
                筆 / 共
            <asp:Label ID="lbl_2" runat="server" Text=""></asp:Label>
                筆 / 第
            <asp:Label ID="lbl_3" runat="server" Text=""></asp:Label>
                頁 / 共
            <asp:Label ID="lbl_4" runat="server" Text=""></asp:Label>
                頁
            </div>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="FT" Name="Tp" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

    <asp:Label ID="Lbl_null" runat="server" Text="尚無待處理公文" Visible="False"></asp:Label>
    <asp:Label ID="Lbl_EID" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
