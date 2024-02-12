let StatusCode = {
    OK: 200,
    UNAUTHORIZED: 401
}

let HttpMethod = {
    GET: "GET",
    POST: "POST",
    PUT: "PUT",
    PATCH: "PATCH",
    DELETE: "DELETE"
}

function getDetails() {
    const userId = localStorage.getItem("grocery_app_user_id");
    const accessToken = localStorage.getItem("grocery_app_access_token");
    return { userId, accessToken };
}

function send(method, endpoint, data, onOk, onError) {
    const baseUrl = "http://groceryapp.api.com/";
    const { accessToken } = getDetails();
    let address = baseUrl + endpoint;
    data = JSON.stringify(data);
    const xhr = new XMLHttpRequest();
    xhr.open(method, address, true);
    xhr.onload = function () {
        if (xhr.status == StatusCode.UNAUTHORIZED) {
            localStorage.removeItem("grocery_app_access_token");
            localStorage.removeItem("grocery_app_user_id");
            window.location.href = "Login";
        }
        xhr.status == StatusCode.OK ? onOk(JSON.parse(xhr.response)) : onError(JSON.parse(xhr.response), xhr.status);
    };
    if (accessToken && accessToken.length > 0) {
        xhr.setRequestHeader("Authorization", `Bearer ${accessToken}`);
    }
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(data);
}