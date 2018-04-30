<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="row tm-item-preview tm-margin-b-xl">
        <div class="auto-style2">
            <h2 class="tm-blue-text tm-margin-b-p">我們團隊</h2>
        </div>
        <div class="col-md-6 col-sm-12 mb-md-0 mb-5">
            <div class="mr-lg-5">
                <p class="tm-margin-b-p">
                    <asp:Panel ID="Pel_Login" runat="server" Style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">
                        登入
                                    <br />
                        <br />
                        <asp:TextBox class="form-control" placeholder="請輸入帳號" ID="Txt_ID" runat="server" Width="265px"></asp:TextBox>
                        <br />
                        <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password" Width="265px"></asp:TextBox>
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="登入" class="btn btn-warning" Height="36px" Width="69px" OnClick="Btn_Login_Click" />
                        &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="註冊" class="btn btn-info" Height="36px" Width="69px" OnClick="Button2_Click" />

                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="Pel_UserInfo" runat="server" Style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1" Visible="False">

                        <asp:Label ID="Lbl_Name" runat="server"></asp:Label>
                        &nbsp;您好
                                    <br />
                        員工編號：<asp:Label ID="Lbl_Eid" runat="server"></asp:Label>
                        <br />
                        部門/職稱：<asp:Label ID="Lbl_DpAndPos" runat="server"></asp:Label>
                    </asp:Panel>
                </p>
                <br />
                <p class="tm-margin-b-p">真的嚇死人！國道警方31日上午在中山高執行交管勤務時，一輛北上聯結車疑似閃神未注意到前方管制，一時煞不住失控打滑，直直往警員衝去。還好警員反應快，短短幾秒之間，立刻拔腿狂奔，才幸運撿回一命。</p>
                <p>英國著名物理學家霍金（Stephen Hawking）葬禮將在31日下午2時在劍橋大學的大聖瑪麗教堂（St Mary the Great）舉行。因葬禮為私人形式，僅家人、朋友及同事約500人出席，教堂為葬禮將道路圍起，作為出席葬禮者的通道，大批民眾則夾道觀看送葬隊伍步入教堂。</p>
            </div>
        </div>
        <div class="col-md-6 col-sm-12 tm-highlight tm-small-pad">
            <h2 class="tm-margin-b-p">佈告欄</h2>
            <hr />
            <p class="tm-margin-b-p">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" GridLines="None" ShowHeader="False" Width="100%" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" DataKeyNames="BID" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Department" ItemStyle-Width="20%" />
                        <asp:TemplateField ItemStyle-Width="70%" ControlStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:LinkButton ID="Lb_Title" runat="server" Text='<%# Bind("BTitle") %>' CommandName="SelData"></asp:LinkButton>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:BoundField DataField="Date" DataFormatString="{0:yyyy/MM/dd}" ItemStyle-Width="10%" />
                        <asp:TemplateField ControlStyle-ForeColor="White">
                            <FooterTemplate>
                                <asp:Button ID="Button3" class="btn btn-info" runat="server" Text="More..." />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT Top 10 * FROM [Bulletin] ORDER BY [BID] DESC"></asp:SqlDataSource>
            </p>
        </div>
    </section>

    <div class="row tm-margin-b-ll">
        <article class="col-12 col-sm-12 col-md-4 col-lg-4 mb-md-0 mb-5">
            <div class="text-center tm-margin-b-30"><i class="fa tm-fa-6x fa-linode tm-blue-text"></i></div>
            <header class="tm-margin-b-30">
                <h3 class="tm-blue-text tm-h3">左</h3>
            </header>
            <p>「這可能會是我另一個累積的榮譽。這是一個很棒的時刻，一個特別的時刻。」賽後談到打破這項原本被視為不可能任務的紀錄時，詹姆斯表示紀錄總是要拿來打破的。</p>
        </article>
        <article class="col-12 col-sm-12 col-md-4 col-lg-4 mb-md-0 mb-5">
            <div class="text-center tm-margin-b-30"><i class="fa tm-fa-6x fa-telegram tm-blue-text"></i></div>
            <header class="tm-margin-b-30">
                <h3 class="tm-blue-text tm-h3">中</h3>
            </header>
            <p>天道盟正義會成員黃姓男子招募年輕男子當車手，刑事局破獲黃的詐騙集團，搜出一本手札，上頭寫著「幫派對於禮儀很重要，訓練年輕人的自尊心，隨時保持紳士風度...」</p>
        </article>
        <article class="col-12 col-sm-12 col-md-4 col-lg-4 mb-md-0 mb-5">
            <div class="text-center tm-margin-b-30"><i class="fa tm-fa-6x fa-superpowers tm-blue-text"></i></div>
            <header class="tm-margin-b-30">
                <h3 class="tm-blue-text tm-h3">右</h3>
            </header>
            <p>飛羽從大學畢業後在公費分發下來到了海濱的國小。這學校除了海濤聲之外，附近放眼望去就是工業區與農田，另外就是那一個不小的社區建築矗立在大馬路旁，除此之外就是一間又一間散落於田地上的農舍。</p>
        </article>
    </div>

    <div class="row tm-hightlight tm-margin-b-ll tm-highlight tm-small-pad">
        <article class="col-12 col-sm-12 col-md-6 col-lg-6 tm-font-thin mb-md-0 mb-5">
            <header class="tm-margin-b-30">
                <h3 class="tm-font-thin">左上框框</h3>
            </header>
            <p>從一開始的遊戲繪圖卡、專業繪圖卡，到開始布局自動駕駛、人工智慧等市場，到今年宣布朝向醫療領域、智慧城市等應用發展之餘，NVIDIA更宣布與ARM合作，預期未來將會擴大更多元的機器人應用發展</p>
        </article>
        <article class="col-12 col-sm-12 col-md-6 col-lg-6 tm-font-thin">
            <header class="tm-margin-b-30">
                <h3 class="tm-green-text tm-font-thin">右上框框</h3>
            </header>
            <p>NVIDIA在很早之前就已經開始布局機器人市場，從先前推出的Jetson系列開發板應用，讓更多人可藉由此開發套件設計機器人、無人機，甚至是機器手臂，同時藉由電腦視覺演算方式讓機器人可以「看見」物品，進而產生影像識別學習效果，並且帶動後續功能或自動化作動效果，例如讓機器手臂能正確夾取貨品、讓空拍機在飛行過程能產生自動避障效果，或是讓小型外送機器人知道如何配合GPS定位行走到指定位置。</p>
        </article>
    </div>

    <div class="row">
        <article class="col-12">
            <header class="tm-margin-b-30">
                <h3 class="tm-blue-text">標題列框架</h3>
            </header>
            <p class="tm-margin-b-p">在此之前，NVIDIA主要還是聚焦在裝置端的學習運算，因此除了讓機器人或自動駕駛車輛能藉由持續學習知道如何在正確道路轉彎、碰到障礙時自動停下，甚至藉由Isaac虛擬化平台讓機器人能以大量「分身」快速累積學習經驗。</p>
            <p>預期未來幾年內開始蓬勃發展的機器人應用，或許內部並不一定採用NVIDIA處理器產品，但背後卻可能是以NVIDIA技術加速學習運算。</p>
        </article>
    </div>


    <!-- load JS files -->
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script>     

        $(document).ready(function () {

            // Update the current year in copyright
            $('.tm-current-year').text(new Date().getFullYear());

        });

    </script>
</asp:Content>
