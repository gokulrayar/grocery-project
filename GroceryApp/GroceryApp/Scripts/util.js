function toggleForm(flag) {
    let inputs = $("input");
    for (let i = 0; i < inputs.length; i++) {
        if (!inputs[i].classList.contains('no-enable')) {
            inputs[i].disabled = flag;
        }
    }
    let buttons = $(".form-btn");
    for (let i = 0; i < buttons.length; i++) {
        buttons[i].disabled = flag;
    }
    let selects = $("select");
    for (let i = 0; i < selects.length; i++) {
        if (!selects[i].classList.contains('no-enable')) {
            selects[i].disabled = flag;
        }
    }
    let anchors = $("a");
    for (let i = 0; i < anchors.length; i++) {
        anchors[i].style.pointerEvents = flag ? "none" : "auto";
    }
    if (flag) {
        let spinner = $(".spinner-border-sm")[0];
        spinner.classList.add("spinner-border");
        let formBtn = $(".form-btn")[0];
        formBtn.classList.add("form-btn-icon-spacer");
    }
    else {
        let spinner = $(".spinner-border")[0];
        spinner.classList.remove("spinner-border");
        let formBtn = $(".form-btn")[0];
        formBtn.classList.remove("form-btn-icon-spacer");
    }
}

function checkPageRequestValidity() {
    const userId = localStorage.getItem("grocery_app_user_id");
    const accessToken = localStorage.getItem("grocery_app_access_token");
    if (!userId && !accessToken) {
        window.location.href = "Login";
    }
}