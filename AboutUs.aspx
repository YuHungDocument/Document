<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="WebApplication1.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://maps.google.com/maps?file=api&v=2&key=AIzaSyCdAXSzpCp3C0kHRS9GNK2vMCMrlg1P7LQ"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="Home.aspx">首頁</a>/關於我們
    <hr />
    <div>
        <div style="text-align: center">
            <h1 style="color: #0094ff">關於我們</h1>
        </div>

        <table class="nav-justified">
            <tr>
                <td>
                    <h2 style="font-weight: bold; color: #0094ff">我們的團隊</h2>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 400px">
                    <asp:Image ID="Image2" Height="200px" Width="350px" ImageUrl="~/P/ncut.jpg" runat="server" />

                </td>
                <td>

                    <asp:Label ID="Lbl_title1" runat="server"  Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <br />
                    <asp:Label ID="Lbl_context1" runat="server"  ></asp:Label>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <br />
    <hr />
    <div>
        <table class="nav-justified">
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: right">
                    <h2 style="font-weight: bold; color: #0094ff;">使用軟體</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Lbl_title2" runat="server"  Font-Bold="True" Font-Size="X-Large" ></asp:Label>

                    <br />
                    <br />
                    <asp:Label ID="Lbl_context2"  runat="server"></asp:Label>

                </td>
                <td style="text-align: right; width: 400px">

                    <asp:Image ID="Image1" runat="server" Height="200px" Width="350px" ImageUrl="~/P/asp.net.png" />

                </td>
            </tr>
        </table>

    </div>
    <hr />

    <table class="nav-justified">
        <tr>
            <td>
                <h2 style="font-weight: bold; color: #0094ff;">如何找到我們</h2>
            </td>
            <td rowspan="2">
                <div id="mymap" style="width: 500px; height: 500px"></div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="聯絡資訊" Font-Bold="True" Font-Size="X-Large" ForeColor="#0094FF"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label6" runat="server"><p>國立勤益科技大學National Chin-Yi University of Technology</p>
                <p>地址：41170臺中市太平區坪林里中山路二段 57號 (交通資訊)</p>
                <p>No.57, Sec. 2, Zhongshan Rd., Taiping Dist., Taichung 41170, Taiwan (R.O.C.)</p>
                <p>電話: (04)23924505 (分機表)FAX:(04) 23923363 TANet 98110000 </p></asp:Label>
            </td>
        </tr>
    </table>

    <br />
    <!--以下為控制Google Maps的JavaScript-->
    <script type="text/javascript">

        //<![CDATA[
        var map = new GMap(document.getElementById("mymap"));

        //設定要顯示的控制項
        map.addControl(new GSmallMapControl());
        map.addControl(new GMapTypeControl());

        //決定你 Google 地圖的中心點位置和縮放大小
        map.setCenter(new GLatLng(24.14426, 120.73202), 16);

        //標記在 Google 地圖上的經緯度
        var point = new GLatLng(24.14426, 120.73202);
        var marker = new GMarker(point);
        map.addOverlay(marker);

        //在地圖上放置標點說明
        var html = "國立勤益科技大學";
        map.openInfoWindowHtml(map.getCenter(), html);

//]]>
    </script>

</asp:Content>
