<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="GroceryApp.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <link href="Content/style.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/xhr.js"></script>
    <script src="Scripts/util.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
</head>
<body class="set-bg">
    <form id="form1" class="needs-validation" novalidate runat="server">
        <div class="form-container">
            <div class="m-4">
                <center>
                    <h3 class="pb-3">Registration</h3>
                </center>
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <div class="form-group">
                                <label class="mb-1" for="firstname">Firstname</label>
                                <input type="text" class="form-control" id="firstname" required>
                                <div class="invalid-feedback">
                                    Please enter your first name.
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="form-group">
                                <label class="mb-1" for="lastname">Lastname</label>
                                <input type="text" class="form-control" id="lastname" required>
                                <div class="invalid-feedback">
                                    Please enter your last name.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <div class="form-group">
                                <label class="mb-1" for="address">Address</label>
                                <input type="text" class="form-control" id="address" required>
                                <div class="invalid-feedback">
                                    Please enter your valid address.
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="form-group">
                                <label class="mb-1" for="gender">Gender</label>
                                <select class="form-select" aria-label="Default select example" id="gender" required>
                                    <option value="" selected>Select One</option>
                                    <option value="1">Male</option>
                                    <option value="2">Female</option>
                                </select>
                                <div class="invalid-feedback">
                                    Please select your gender.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <div class="form-group">
                                <label class="mb-1" for="email">Email</label>
                                <input type="email" class="form-control" id="email" required>
                                <div class="invalid-feedback">
                                    Please enter your valid email address.
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="form-group">
                                <label class="mb-1" for="password">Password</label>
                                <input type="password" class="form-control" id="password" autocomplete required minlength="6">
                                <div class="invalid-feedback">
                                    Please enter a strong password with at least 6 characters.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-6">
                            <label class="mb-1" for="role">Role</label>
                            <select class="form-select" aria-label="Default select example" id="role" required>
                                <option value="" selected>Select One</option>
                                <option value="2">User</option>
                                <option value="1">Admin</option>
                            </select>
                            <div class="invalid-feedback">
                                Please select a role.
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label class="mb-1" for="captcha">Enter Captcha</label>
                                <input type="text" class="form-control" id="captcha" required>
                                <div class="invalid-feedback">
                                    Please enter the valid captcha.
                                </div>
                            </div>
                        </div>
                        <div class="col-2">
                            <div id="captcha-img" class="form-group mt-4">
                                <!-- inject captcha -->
                            </div>
                        </div>
                        <div class="col-1 text-center">
                            <div class="form-group regenerate-captcha">
                                <i class="fa" onclick="generateCaptcha()">&#xf021;</i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <button type="button" class="btn btn-primary form-btn" onclick="onSubmit()">
                                Register
                                <span class="spinner-border-sm" role="status" aria-hidden="true"></span>
                            </button>
                        </div>
                        <div class="col text-end mt-2">
                            <a href="Login">Already registered? Login here</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="toast text-white bg-danger border-0" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="d-flex">
            <div class="toast-body">
                Something went wrong. Please try again.
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
    </div>
</body>
</html>

<style>
div.form-container {
    width: 800px;
}

.fa:hover {
    cursor: pointer;
}

.regenerate-captcha {
    margin-top: 34px;
    margin-left: -40px;
}

.spinner {
    height: 100vh;
}
</style>

<script>
    let code;

    function onSubmit() {
        $(".needs-validation")[0].classList.add("was-validated");
        if ($("input:invalid").length > 0 || $("select:invalid").length > 0) {
            return;
        }
        if (code !== $("#captcha")[0].value) {
            let captchaInvalidMsg = "<div class='invalid-feedback' style='display:block;'>Invalid captcha.</div>";
            $(captchaInvalidMsg).insertAfter($("#captcha")[0]);
            return 0;
        }
        toggleForm(true);
        const endpoint = "registration";
        let data = {
            firstName: $("#firstname")[0].value,
            lastName: $("#lastname")[0].value,
            address: $("#password")[0].value,
            gender: parseInt($("#gender")[0].value),
            role: parseInt($("#role")[0].value),
            email: $("#email")[0].value,
            password: $("#password")[0].value,
        };
        const onOk = function () {
            $(".toast")[0].classList.remove("bg-danger");
            $(".toast")[0].classList.add("show", "bg-success");
            $(".toast-body")[0].innerText = `Hi ${data.firstName}, you have registered successfully. You will be redirected to the login page shortly.`;
            setTimeout(() => { window.location.href = "Login" }, 4000);
        };
        const onError = function () {
            toggleForm(false);
            $(".toast")[0].classList.add("show");    
        }
        send(HttpMethod.POST, endpoint, data, onOk, onError);
        generateCaptcha();
    }

    function generateCaptcha() {
        let existedCaptcha = $("canvas");
        if (existedCaptcha.length > 0) {
            existedCaptcha[0].remove();
        }
        let charsArray = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@!#$%^&*";
        let captchaLength = 6;
        let captcha = [];
        for (let i = 0; i < captchaLength; i++) {
            let index = Math.floor(Math.random() * charsArray.length + 1);
            if (captcha.indexOf(charsArray[index]) == -1)
                captcha.push(charsArray[index]);
            else i--;
        }
        let canv = document.createElement("canvas");
        canv.id = "captcha-text";
        canv.width = 100;
        canv.height = 50;
        let ctx = canv.getContext("2d");
        ctx.font = "25px Georgia";
        ctx.strokeText(captcha.join(""), 0, 30);
        code = captcha.join("");
        $("#captcha-img")[0].appendChild(canv);
    }

    window.onload = generateCaptcha();
</script>