<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Connect.aspx.cs" Inherits="WebApplication1.Connect2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="contact-form-box clearfix">
            <br />
            <a href="Home.aspx">首頁</a>/聯絡我們
            <hr />
                <div class="form-left">
                  <div class="contact-header">
                    <div class="header-info">
                      <h2>聯絡我們</h2>
                      <p>如果您有任何網站設計規劃的需求，請填寫下列表格，我們將很高興為您做訪談。</p>
                    </div>
                    <div class="form">
                      <form name="form" method="post">
                        <div class="form-group f-wrap">
                            <asp:DropDownList ID="Dp_Title" runat="server">
                                <asp:ListItem>請選擇主旨*</asp:ListItem>
                                <asp:ListItem>網頁設計建議</asp:ListItem>
                                <asp:ListItem>應徵多媒體設計師</asp:ListItem>
                                <asp:ListItem>應徵程式設計師</asp:ListItem>
                                <asp:ListItem>事務合作</asp:ListItem>
                                <asp:ListItem>其他</asp:ListItem>
                            </asp:DropDownList>
                          &nbsp;<p class="form-txt warning" style="display:none">此為必填</p>
                        </div>
                        <div class="form-group f-half">
                          <!--如未填寫請套上warning類別-->
                            <asp:TextBox ID="Txt_Name" placeholder="姓名 *" runat="server" CausesValidation="False"></asp:TextBox>
                          &nbsp;<p class="form-txt warning" style="display:none">此為必填</p>
                        </div>
                        <div class="form-group f-half">
                         <asp:TextBox ID="Txt_Company" placeholder="公司" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group f-half">
                          <asp:TextBox ID="Txt_Tel" placeholder="電話 *" runat="server"></asp:TextBox>
                          <p class="form-txt warning" style="display:none">此為必填</p>
                        </div>
                        <div class="form-group f-half">
                            <asp:TextBox ID="Txt_mail" placeholder="信箱 *" runat="server"></asp:TextBox>
                          <p class="form-txt warning" style="display:none">此為必填</p>
                        </div>
                        <div class="form-group f-wrap">
                          <asp:TextBox ID="Txt_message" placeholder="訊息 *" runat="server" Height="107px" TextMode="MultiLine" Width="643px"></asp:TextBox>
                          <p class="form-txt warning" style="display:none">此為必填</p>
                        </div>
                        <!-- <div class="form-group f-wrap" id='xcode'></div> -->
                      </form>
                    </div>
                  </div>
                  <div class="contact-btn clearfix">
                      <asp:LinkButton ID="Lb_Reset" runat="server" PostBackUrl="~/Connect.aspx">重設</asp:LinkButton>
                      <br />
                      <asp:LinkButton ID="Lb_send" runat="server" OnClick="Lb_send_Click">送出</asp:LinkButton>
                  </div>
                </div>
                <div class="form-right">
                  <div class="contact-hq">
                  </div>
                </div>
              </div>

</asp:Content>
