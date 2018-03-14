<%@ Page Language="C#"  MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="WebApplication1.ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
//CharMode函數
//測試某個字元是屬於哪一類.
function CharMode(iN){
if (iN>=48 && iN <=57) //數位
return 1;
if (iN>=65 && iN <=90) //大寫字母
return 2;
if (iN>=97 && iN <=122) //小寫
return 4;
else
return 8; //特殊字元
}
//bitTotal函數
//計算出當前密碼當中一共有多少種模式
function bitTotal(num){
modes=0;
for (i=0;i<4;i++){
if (num & 1) modes++;
num>>>=1;
}
return modes;
}
//checkStrong函數
//返回密碼的強度級別
    function checkStrong(sPW) {
        if (sPW.length <= 4)
            return 0; //密碼太短
        Modes = 0;
        for (i = 0; i < sPW.length; i++) {
            //測試每一個字元的類別並統計一共有多少種模式.
            Modes |= CharMode(sPW.charCodeAt(i));
        }
        return bitTotal(Modes);
    }

    //pwStrength函數
    //當使用者放開鍵盤或密碼輸入框失去焦點時,根據不同的級別顯示不同的顏色
    function pwStrength(pwd) {
        O_color = "#e0f0ff";
        L_color = "#FF0000";
        M_color = "#FF9900";
        H_color = "#33CC00";
        if (pwd == null || pwd == '') {
            Lcolor = Mcolor = Hcolor = O_color;
        }
        else {
            S_level = checkStrong(pwd);
            switch (S_level) {
                case 0:
                    Lcolor = Mcolor = Hcolor = O_color;
                case 1:
                    Lcolor = L_color;
                    Mcolor = Hcolor = O_color;
                    break;
                case 2:
                    Lcolor = Mcolor = M_color;
                    Hcolor = O_color;
                    break;
                default:
                    Lcolor = Mcolor = Hcolor = H_color;
            }
        }
        document.getElementById("strength_L").style.background = Lcolor;
        document.getElementById("strength_M").style.background = Mcolor;
        document.getElementById("strength_H").style.background = Hcolor;
        return;
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="Lbl_ID" runat="server"  Visible="False"></asp:Label>
    <h3>輸入新密碼</h3>
  <div class="auto-style4">
                    <div class="container">
                    <div style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">
輸入新密碼:<asp:TextBox class="form-control" Width="265px" ID="Txt_Pwd" runat="server" onKeyUp=pwStrength(this.value) onBlur=pwStrength(this.value) ></asp:TextBox>
                        <br />
                        <br />
確認新密碼:<asp:TextBox ID="Txt_ConfirmPwd" runat="server" class="form-control" Width="265px"></asp:TextBox>
                        <br />
密碼強度:
<table border="1" cellpadding="1" borderColorDark="#fdfeff" borderColorLight="#99ccff" cellspacing="1" style="width: 200px; display: inline; background-colore0f0ff">
<tr>
<td id="strength_L" style="width: 100px; height: 19px; text-align: center;" >
弱</td>
<td id="strength_M" style="width: 100px; height: 19px; text-align: center">
中</td>
<td id="strength_H" style="width: 100px; height: 19px; text-align: center" >
強</td>
</tr>
</table>
     <br />
     <br />
     <asp:Button ID="Button1" runat="server" Text="變更密碼" class="btn btn-warning" Height="36px" Width="81px" OnClick="Btn_ChangePwd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="back" runat="server" class="btn btn-default" Text="返回" OnClick="back_Click" />
</div>
                        </div>
      </div>
     </asp:Content>