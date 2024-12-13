<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="X3_TERMINALINI._default" %>


<!DOCTYPE html>
<html lang="IT">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="content-language" content="it" />

    <link rel="apple-touch-icon" sizes="180x180" href="/Images/ico/apple-touch-icon.png">
    <link rel="icon" type="png" sizes="32x32" href="/Images/ico/favicon-32x32.png">
    <link rel="icon" type="png" sizes="16x16" href="/Images/ico/favicon-16x16.png">
    <link rel="manifest" href="/Images/ico/site.webmanifest">
    <link rel="mask-icon" href="/Images/ico/safari-pinned-tab.svg" color="#5bbad5">
    <meta name="msapplication-TileColor" content="#da532c">
    <meta name="theme-color" content="#ffffff">

    <title><%=_Title%> - <%= _ver%></title>

    <link href="/_include/css/jquery-ui.min.css?<%= _ver%>" rel="stylesheet" />
    <link href="/_include/css/carousel.css?<%= _ver%>" type="text/css" rel="stylesheet" />
    <link href="/_include/css/modal.css?<%= _ver%>" type="text/css" rel="stylesheet" />
    <link href="/_include/css/jquery-confirm.css?<%= _ver%>" rel="stylesheet" />
    <link href="/_include/css/bootstrap.css?<%= _ver%>" rel="stylesheet" />
    <link href="/_include/css/Custom.css?<%= _ver%>" type="text/css" rel="stylesheet" />
</head>

<body>
    <form id="frm" method="post" runat="server">
        <div id="div-login">
            <div class="container">
                <div class="row">
                    <div class="col-12 offset-sm-2 col-sm-8 offset-md-3 col-md-6 text-center">
                        <img src="/images/Logo.png" style="width:200px" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 offset-sm-2 col-sm-8 offset-md-3 col-md-6 text-center div-login-tit ">
                        <%=_Title%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 offset-sm-2 col-sm-8 offset-md-3 col-md-6">
                        <div class="div-login-main">
                            Utente<br />
                            <input type="text" class="form-control" id="login-user" name="login-user" placeholder="utente" autocomplete="off" /><br />
                            Password<br />
                            <input type="password" class="form-control" id="login-pass" name="login-pass" placeholder="password" autocomplete="off" /><br />
                            <input type="button" value="Accedi" class="form-control btn btn-primary" id="login-btt" /><br />
                            <asp:Label runat="server" ID="login_err" ForeColor="#800000"></asp:Label>
     
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 offset-sm-2 col-sm-8 offset-md-3 col-md-6 text-center div-login-foot ">
                        <a href="https://sarce.it/" target="_blank">SARCE Spa - CoC X3  -  &copy; 2024  -  <%= _ver%></a>
                    </div>
                </div>
            </div>

        </div>

    </form>

    <!-- MODAL WAIT BEGIN -->
    <div id="modal">
        <div class="wait_images"><i class="fa fa-spinner fa-spin fa-5x color_white"></i></div>
    </div>
    <!-- MODAL WAIT END -->

    <script src="/_include/js/jquery.min.js?<%= _ver%>"></script>
    <script src="/_include/js/jquery-ui.min.js?<%= _ver%>"></script>
    <script src="/_include/js/bootstrap.min.js?<%= _ver%>"></script>
    <script src="/_include/js/fontawesome.min.js?<%= _ver%>"></script>

    <script src="/_include/js/main.js?<%= _ver%>"></script>
    <script src="/_include/js/jquery-confirm.js?<%= _ver%>"></script>
    <script src="/_include/js/DataCheck.js?<%= _ver%>"></script>
    <script src="/_include/js/AutoComplete.js?<%= _ver%>"></script>
    <script>
        $("#login-btt").click(function () {
            $("#login_err").html("");
            //
            if ($("#login-user").val().trim() == "") {
                $("#login_err").html("Utente non inserito");
                $("#login-user").focus();
                return;
            }
            //
            if ($("#login-pass").val().trim() == "") {
                $("#login_err").html("Password non inserito");
                $("#login-pass").focus();
                return;
            }
            //
            $("#modal").css("display", "block");
            frm.submit();
        });



        $(function () {
            $("input[type=text]").first().focus();
        });


    </script>
</body>
</html>