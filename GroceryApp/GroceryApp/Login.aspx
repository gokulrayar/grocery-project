<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GroceryApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Content/style.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/xhr.js"></script>
    <script src="Scripts/util.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
</head>
<body class="set-bg">
    <form id="form1" class="needs-validation" novalidate runat="server">
        <div class="form-container shadow-lg">
            <div class="m-4">
                <center>
                    <h3>Login</h3>
                </center>
                <div class="form-group">
                    <label class="mb-1" for="email">Email</label>
                    <input type="email" class="form-control" id="email" autocomplete required>
                    <div class="invalid-feedback">
                        Please enter your valid email address.
                    </div>
                </div>
                <div class="form-group">
                    <label class="mb-1" for="password">Password</label>
                    <input type="password" class="form-control" id="password" autocomplete required minlength="6">
                    <div class="invalid-feedback">
                        Please enter your valid password.
                    </div>
                </div>
                <div class="container">
                    <div class="row mt-4">
                        <div class="col-8 ps-0">
                            <div class="row mb-1">
                                <small><a href="ResetPassword">Forgot Password</a></small>
                            </div>
                            <div class="row">
                                <small><a href="Registration">New User? Register here</a></small>
                            </div>
                        </div>
                        <div class="col-4 pe-0">
                            <button type="button" class="btn btn-primary form-btn" onclick="onSubmit()">
                                Login
                                <span class="spinner-border-sm" role="status" aria-hidden="true"></span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="toast text-white bg-danger border-0" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="d-flex">
            <div class="toast-body">
                Wrong username or password is given.
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
    </div>
</body>
</html>

<style>
div.form-container {
    width: 400px;
}

.form-btn {
    margin-left: 35px;
    margin-top: 6px;
}

.form-btn-icon-spacer {
    margin-left: 17px !important;
}
</style>

<script>
    function onSubmit() {
        $(".needs-validation")[0].classList.add("was-validated");
        if ($("input:invalid").length > 0) {
            return;
        }
        toggleForm(true);
        const endpoint = "authentication/login";
        let data = {
            email: $("#email")[0].value,
            password: $("#password")[0].value
        };
        const onOk = function (response) {
            localStorage.setItem("grocery_app_access_token", response.data.accessToken);
            localStorage.setItem("grocery_app_user_id", response.data.userId);
            $(".toast")[0].classList.remove("bg-danger");
            $(".toast")[0].classList.add("show", "bg-success");
            $(".toast-body")[0].innerText = "Logged in successfully. You will be redirected to the welcome page shortly.";
            setTimeout(() => { window.location.href = "Welcome" }, 5000);
        };
        const onError = function (response) {
            toggleForm(false);
            $(".toast")[0].classList.add('show');
        };
        send(HttpMethod.POST, endpoint, data, onOk, onError);
    }
</script>
