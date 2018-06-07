﻿<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="WriteDocument.aspx.cs" Inherits="WebApplication1.WriteDocument" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Register Src="~/ucGridViewChoiceAll.ascx" TagPrefix="uc1" TagName="ucGridViewChoiceAll" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function SelectAllCheckboxes(spanChk) {
            elm = document.forms[0];
            for (i = 0; i <= elm.length - 1; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != spanChk.id) {
                    if (elm.elements[i].checked != spanChk.checked)
                        elm.elements[i].click();
                }
            }
        }
    </script>
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
    <style type="text/css">
        table {
            width: 100%;
            border: #ffffff 1px solid;
            border-bottom: 1px #dddddd solid;
        }
        body{
            font-size:large
        }


                /*.btn:after {
            font-family: "Glyphicons Halflings";
            content: "\e114";
            float: right;
            margin-left: 15px;
        }*/
        /* Icon when the collapsible content is hidden */
        /*.btn.collapsed:after {
            content: "\e080";
        }*/


        .clickable {
            cursor: pointer;
        }

        .panel-heading span {
            margin-top: -20px;
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="modal fade" id="FileModal" role="dialog" style="top: 18%;">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">所有上傳檔案</h4>
                </div>
                <div class="modal-body">
                    <asp:GridView runat="server" ID="gv_showTempFile" AutoGenerateColumns="false" OnRowDeleting="gv_RowDeleting"
                        DataKeyNames="FNO">
                        <Columns>
                            <asp:TemplateField HeaderText="已上傳的檔案">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_filename" OnClick="OpenDoc" runat="server" Text='<%# Eval("Name") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="刪除">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete"
                                        OnClientClick="javascript:return confirm('確定刪除?')">刪除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">關閉</button>
                </div>
            </div>

        </div>
    </div>
    <asp:Label ID="Lbl_EID" runat="server" Visible="False"></asp:Label>
    <br />
    <div class="panel panel-warning">
        <div class="panel-heading pull clickable panel">
            <h3 class="panel-title "><span>撰寫內文<i class="glyphicon glyphicon-chevron-up"></i></span></h3>
        </div>
        <div class="panel-body">
            <table style="border: none">
                <tr style="height: 25px">
                    <td style="width:80px">文　　號</td>
                    <td>
                        <asp:Label ID="Lbl_SID" runat="server"></asp:Label>
                    </td>
                    <td colspan="4">發布日期</td>
                    <td>
                        <asp:Label ID="Lbl_Date" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="4">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>發布人</td>
                    <td>
                        <asp:Label ID="Lbl_Sender" runat="server"></asp:Label>
                    </td>
                    <td colspan="4">速　　別</td>
                    <td>
                        <asp:DropDownList ID="Ddl_Speed" runat="server" DataSourceID="SqlDataSource2" DataTextField="TN" DataValueField="TN"></asp:DropDownList>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="4">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>公文類型</td>
                    <td>
                        <asp:DropDownList ID="Ddp_Type" runat="server" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN" Height="30px" Width="185px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="4">&nbsp;</td>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="Sp" Name="Tp" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td>&nbsp;</td>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString2 %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE (([Tp] = @Tp) AND ([TID] &lt;&gt; @TID))">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="FT" Name="Tp" Type="String" />
                                <asp:Parameter DefaultValue="6" Name="TID" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td colspan="4">&nbsp;</td>
                    <td style="font-size: large;">&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>保存期限</td>
                    <td>
                        <asp:DropDownList ID="Ddp_YOS" runat="server" Width="70px">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>永久</asp:ListItem>
                        </asp:DropDownList>
                        年</td>
                    <td colspan="4">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>&nbsp;</td>
                    <td style="font-size: large;" colspan="2">&nbsp;</td>
                    <td style="font-size: large;" colspan="2">&nbsp;</td>
                    <td style="font-size: large;" colspan="2">&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>附　　件</td>
                    <td colspan="3">
                        <asp:FileUpload runat="server" ID="fu_upload" />
                        <br />
                        <asp:Button Text="上傳檔案"
                            ID="btn_upload" runat="server" OnClick="btn_upload_Click" />
                        <asp:Label ID="Lbl_FileCount" runat="server"></asp:Label>
                        <br />
                    </td>
                    <td colspan="3">
                        <button type="button" class="btn btn-warning" data-toggle="modal" data-target="#FileModal">顯示上傳檔案</button>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td>&nbsp;</td>
                    <td style="font-size: large;" colspan="6">&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>主　　旨</td>
                    <td style="font-size: large;" colspan="6">
                        <asp:TextBox class="form-control" ID="Txt_Title" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td>&nbsp;</td>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>說　　明</td>
                    <td colspan="6">
                        <%--<asp:TextBox ID="" class="form-control" runat="server" Height="99px" TextMode="MultiLine"></asp:TextBox>--%>
                        <CKEditor:CKEditorControl ID="Txt_Text" runat="server"></CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td>&nbsp;</td>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <tr style="height: 25px">
                    <td>擬　　辦</td>
                    <td style="font-size: large;" colspan="6">
                        <asp:TextBox ID="txt_Proposition" class="form-control" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <br />

    <div class="panel panel-warning" style="width: 100%">
        <div class="panel-heading">填寫收件人與收發順序</div>
        <div class="panel-body">
            
            <!-- Modal -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="top: 18%;">
                <div class="container" style="width: 500px; height: 500px">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">選擇群組</h4>
                                                                
                            </div>
                            <div class="modal-body">
                                透過部門或職位加入群組
                                <hr />
                        <asp:DropDownList ID="DropDownList1" runat="server" class="btn btn-primary dropdown-toggle"></asp:DropDownList>
                        &nbsp; &nbsp;&nbsp; &nbsp;
                        <asp:DropDownList ID="DropDownList2" runat="server" class="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
                                <br />
                                <p></p>
                                <hr />
                                自訂的群組
                                <hr />
                                <asp:GridView ID="GridView4" runat="server" CssClass="table" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="1" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="GridView4_SelectedIndexChanged" Style="line-height: 22px; width: 100%;">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);" Text="全選/取消" ToolTip="按一次全選，再按一次取消全選" />
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="序列" ReadOnly="True" Visible="False" />
                                        <asp:TemplateField HeaderText="群組名稱">
                                            <ItemTemplate>
                                                <asp:LinkButton DataField="GpName" ID="lblName" runat="server" CausesValidation="False" CommandName="Select" Text='<%# Eval("GpName") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="Btn_Insert" class="btn btn-primary" OnClick="Btn_Insert_Click" runat="server" Text="插入選取群組" />
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">關閉</button>
                            </div>

                        </div>
                    </div>
                    <!-- Modal content-->
                </div>
            </div>
            <table style="border-style: none; height: 336px">
                <tr>
                    <td runat="server" valign="top" id="Main">
                        <div style="text-align: left">
                            <asp:Label ID="Label2" runat="server" Text="輸入群組名稱："></asp:Label>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="Btn_Newgroup" runat="server" OnClick="Btn_Newgroup_Click" Text="新增至群組" />
                            &nbsp;<asp:Button ID="Btn_editgroup" runat="server" OnClick="Btn_editgroup_Click" Text="修改此群組" />
                            <br />
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <br />
                                    <button type="button" id="Btn" runat="server" data-toggle="modal" data-target="#myModal">選擇群組</button>
                                    <asp:Label ID="Lbl_GpName" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:CheckBox ID="ChB_Check" runat="server" Text="簽核時是否重新檢查金鑰" />
                                    <br />
                                    <asp:GridView ID="GridView5" class="table" runat="server" AutoGenerateColumns="False" GridLines="Horizontal">
                                        <Columns>
                                            <asp:TemplateField HeaderText="排列" ShowHeader="False" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SID" HeaderText="序列" Visible="false" />
<%--                                            <asp:TemplateField HeaderText="層級">
                                                <ItemTemplate>
                                                    <asp:TextBox DataField="Lvl" ID="Txt_Lvl" runat="server" OnTextChanged="Txt_Lvl_TextChanged" TextMode="Number" Width="50px" AutoPostBack="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="發布人編號">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_EID" Text='<%# Bind("EID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="發布人部門">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Dep" Text='<%# Bind("Department") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="發布人姓名">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Name" Text='<%# Bind("Name") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
<%--                                            <asp:TemplateField HeaderText="需簽章">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Cb_sign" runat="server" OnCheckedChanged="Cb_sign_CheckedChanged1" AutoPostBack="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:GridView>

                                    <asp:GridView ID="GridView2" class="table" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" DataKeyNames="ID" OnRowCommand="GridView2_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="排列" ShowHeader="False" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SID" HeaderText="序列" Visible="false" />
                                            <asp:TemplateField HeaderText="層級">
                                                <ItemTemplate>
                                                    <asp:TextBox DataField="Lvl" ID="Txt_Lvl" runat="server" OnTextChanged="Txt_Lvl_TextChanged" TextMode="Number" Width="50px" AutoPostBack="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="人員編號">
                                                <ItemTemplate>
                                                    <asp:TextBox DataField="EID" placeholder="請輸入員工編號或姓名" ID="Txt_EID" runat="server" AutoPostBack="True" OnTextChanged="Txt_EID_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="Lbl_Agent" runat="server" Text="代理人" ForeColor="Red" Visible="False"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="部門">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Dep" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="員工姓名">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Name" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <uc1:ucGridViewChoiceAll runat="server" ID="ucGridViewChoiceAll1" CheckBoxName="Cb_sign" HeaderText="需簽章" OnCheckedChanged="Cb_sign_CheckedChanged" AutoPostBack="True" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Cb_sign" runat="server" OnCheckedChanged="Cb_sign_CheckedChanged" AutoPostBack="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <uc1:ucGridViewChoiceAll runat="server" ID="ucGridViewChoiceAll" CheckBoxName="Cb_path" HeaderText="可察看進度" OnCheckedChanged="Cb_path_CheckedChanged" AutoPostBack="True" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Cb_path" runat="server" OnCheckedChanged="Cb_path_CheckedChanged" AutoPostBack="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <uc1:ucGridViewChoiceAll runat="server" ID="ucGridViewChoiceAll3" CheckBoxName="Cb_comment" HeaderText="是否評論" OnCheckedChanged="Cb_comment_CheckedChanged" AutoPostBack="True" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Cb_comment" runat="server" AutoPostBack="true" OnCheckedChanged="Cb_comment_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lbl_Delete" CommandName="DelData" OnClientClick="javascript:return confirm('確定刪除?')"  runat="server">刪除</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                    <asp:Button ID="Btn_Save" runat="server" OnClick="Btn_Save_Click" Text="送出" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Btn_Draft" runat="server" OnClick="Btn_Draft_Click" Text="儲存至草稿" />
                                    <asp:Label ID="Lbl_Eorr" runat="server" ForeColor="Red" Text="資料輸入不完整" Visible="False"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    &nbsp;&nbsp;
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>



</asp:Content>
