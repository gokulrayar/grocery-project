<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="GroceryApp.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
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
                    <h3>Reset Password</h3>
                </center>
                <form class="page-form">
                    <div class="form-group">
                        <label class="mb-1" for="email">Email</label>
                        <input type="email" class="form-control" id="email" required>
                        <div class="invalid-feedback">
                            Please enter your valid email address.
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="mb-1" for="newpassword">New Password</label>
                        <input type="password" class="form-control" id="newpassword" required minlength="6">
                        <div class="invalid-feedback">
                            Please enter a strong password with at least 6 characters.
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="mb-1" for="confirmpassword">Confirm Password</label>
                        <input type="password" class="form-control" id="confirmpassword" required minlength="6">
                        <div class="invalid-feedback">
                            Password length should be at least 6.
                        </div>
                    </div>
                    <div class="container">
                        <div class="row">
                            <button type="button" class="btn btn-primary mt-2 form-btn" onclick="onSubmit()">
                                Change Password
                                <span class="spinner-border-sm" role="status" aria-hidden="true"></span>
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </form>

    <div id="" class="toast text-white bg-danger border-0" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="d-flex">
            <div class="toast-body">
                No user with this email is registered.
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
</style>

<script>
    let alreadyShowingMsg = false;

    function onSubmit() {
        $(".needs-validation")[0].classList.add("was-validated");
        if ($("input:invalid").length > 0) {
            return;
        }
        let newPassword = $("#newpassword")[0].value;
        let confirmPassword = $("#confirmpassword")[0].value;
        if (newPassword !== confirmPassword) {
            if (!alreadyShowingMsg) {
                alreadyShowingMsg = true;
                let captchaInvalidMsg = "<div id='custom-validation-msg' class='invalid-feedback' style='display:block;'>Passwords are not matching.</div>";
                $(captchaInvalidMsg).insertAfter($("#confirmpassword")[0]);
            }
            return;
        }
        else {
            toggleForm(true);
            const endpoint = "users/password";
            let data = {
                email: $("#email")[0].value,
                password: newPassword
            };
            const onOk = function () {
                $(".toast")[0].classList.remove("bg-danger");
                $(".toast")[0].classList.add("show", "bg-success");
                $(".toast-body")[0].innerText = "Password changed successfully. You will be redirected to the login page shortly.";
                setTimeout(() => { window.location.href = "Login" }, 10000);
            }
            const onError = function () {
                toggleForm(false);
                $(".toast")[0].classList.add("show");
            }
            send(HttpMethod.PUT, endpoint, data, onOk, onError);
        }
    }
</script>
