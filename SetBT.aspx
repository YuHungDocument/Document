<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="SetBT.aspx.cs" Inherits="WebApplication1.SetBT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .sidenav ul {
            list-style-type: none; /* 不顯示清單符號 */
            margin: 0px;
            padding: 0px;
        }

        .sdienav ul li {
            float: left;
            background-color: #CCC; /* 每個項目的背景色 */
        }

        .sidenav ul li a {
            color: #000000;
            text-decoration: none; /* 不顯示預設底線 */
            font-size: x-large; /* 字體大小 x-large */
            display: block; /* 以區塊方式顯示 */
            line-height: 50px; /* 行高 50px，並垂直置中 */
            width: 100px; /* 寬度 100px */
            text-align: center; /* 文字文平置中 */
            font-weight: bold; /* 粗體字 */
            border-bottom: 1px dotted #CCC;
        }

            .sidenav ul li a:hover {
                color: #ff6a00
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="sidenav col-sm-2">
        <h3>參數設定</h3>
        <ul>
            <li><a href="Environmentalparameters.aspx">企業名稱設定</a></li>
            <li><a href="SetConnect.aspx">聯絡問題設定</a></li>
           <li><a href="SetBT.aspx">產品種類設定</a></li>
            <li><a href="SetNT.aspx">最新消息類別設定</a></li>
        </ul>
    </div>
     <p>
    <p>
        <h3>現有產品類別:</h3>
                 <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Tp,TID" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="800px">
             <Columns>
                <asp:CommandField  ShowEditButton="True" ButtonType="Button" />
                 <asp:TemplateField  SortExpression="TN">
                      <HeaderTemplate>
                          
                            <div style="text-align: center;">
                                <asp:Label ID="Label2" runat="server" Text="產品類別"></asp:Label>
                                 <asp:LinkButton ID="lbInsert" runat="server" Width="70px"  onclick="lbInsert_Click" Text="新增"></asp:LinkButton>
                            </div>
                            </HeaderTemplate>

                     <EditItemTemplate>
                         <asp:TextBox ID="Txt_TN" runat="server" Text='<%# Bind("TN") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <FooterTemplate>
                           <div style="text-align: center;">
                <asp:TextBox ID="tbTNFooter" runat="server" 
                Text=""></asp:TextBox>
                <asp:LinkButton ID="lbSave" runat="server" 
                onclick="lbSave_Click">儲存</asp:LinkButton>
                |
                <asp:LinkButton ID="lbCancelSave" runat="server" 
                onclick="lbCancelSave_Click">取消</asp:LinkButton>
                                </div>
            </FooterTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("TN") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField ShowHeader="False">
                       <ItemTemplate>
                           <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:return confirm('確定刪除?')" ></asp:LinkButton>
                       </ItemTemplate>
                 </asp:TemplateField>
                
             </Columns>
             <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
             <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
             <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
             <RowStyle HorizontalAlign="Center" />
             <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#F7F7F7" />
             <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
             <SortedDescendingCellStyle BackColor="#E5E5E5" />
             <SortedDescendingHeaderStyle BackColor="#242121" />
     </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" DeleteCommand="DELETE FROM [TypeGroup] WHERE [Tp] = @Tp AND [TID] = @TID" InsertCommand="INSERT INTO [TypeGroup] ([Tp], [TID], [TN]) VALUES (@Tp, @TID, @TN)" SelectCommand="SELECT * FROM [TypeGroup] WHERE ([Tp] = @Tp)And [TID]!=0" UpdateCommand="UPDATE [TypeGroup] SET [TN] = @TN WHERE [Tp] = @Tp AND [TID] = @TID">
         <DeleteParameters>
             <asp:Parameter Name="Tp" Type="String" />
             <asp:Parameter Name="TID" Type="Int32" />
         </DeleteParameters>
         <InsertParameters>
             <asp:Parameter Name="Tp" Type="String" />
             <asp:Parameter Name="TID" Type="Int32" />
             <asp:Parameter Name="TN" Type="String" />
         </InsertParameters>
         <SelectParameters>
             <asp:Parameter DefaultValue="BT" Name="Tp" Type="String" />
         </SelectParameters>
         <UpdateParameters>
             <asp:Parameter Name="TN" Type="String" />
             <asp:Parameter Name="Tp" Type="String" />
             <asp:Parameter Name="TID" Type="Int32" />
         </UpdateParameters>
     </asp:SqlDataSource>
</asp:Content>
