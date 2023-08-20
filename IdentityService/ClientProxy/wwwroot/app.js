function log() {
    document.getElementById("results").innerText = "";

    Array.prototype.forEach.call(arguments, function (msg) {
        if (typeof msg !== "undefined") {
            if (msg instanceof Error) {
                msg = "Error: " + msg.message;
            } else if (typeof msg !== "string") {
                msg = JSON.stringify(msg, null, 2);
            }
            document.getElementById("results").innerText += msg + "\r\n";
        }
    });
}

let userClaims = null;

(async function () {
    var req = new Request("/bff/user", {
        headers: new Headers({
            "X-CSRF": "1",
        }),
    });

    try {
        var resp = await fetch(req);
        if (resp.ok) {
            userClaims = await resp.json();

            log("user logged in", userClaims);
        } else if (resp.status === 401) {
            log("user not logged in");
        }
    } catch (e) {
        log("error checking user status");
    }
})();

async function GetUser() {
    var req = new Request("/bff/user", {
        headers: new Headers({
            "X-CSRF": "1",
        }),
    });

    try {
        var resp = await fetch(req);
        if (resp.ok) {
            userClaims = await resp.json();

            log("user logged in", userClaims);
        } else if (resp.status === 401) {
            log("user not logged in");
        }
    } catch (e) {
        log("error checking user status");
    }
}

async function GetUserOIDC() {
    var req = new Request("https://localhost:8006/connect/userinfo", {
        headers: new Headers({
            "X-CSRF": "1",
        }),
    });

    try {
        var resp = await fetch(req);
        if (resp.ok) {
            userClaims = await resp.json();

            log("user logged in", userClaims);
        } else if (resp.status === 401) {
            log("user not logged in");
        }
    } catch (e) {
        log("error checking user status");
    }
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("user").addEventListener("click", GetUser, false);
document.getElementById("useroidc").addEventListener("click", GetUserOIDC, false);
document.getElementById("register").addEventListener("click", register, false);
document.getElementById("local").addEventListener("click", localApi, false);
document.getElementById("remote").addEventListener("click", remoteApi, false);
document.getElementById("logout").addEventListener("click", logout, false);

function login() {
    window.location = "/bff/login";
}

function register() {
    window.location = "https://localhost:8006/Account/Register?returnUrl=https://localhost:8007/signin-oidc";
}

function logout() {
    if (userClaims) {
        var logoutUrl = userClaims.find(
            (claim) => claim.type === "bff:logout_url"
        ).value;
        window.location = logoutUrl;
    } else {
        window.location = "/bff/logout";
    }
}

async function localApi() {
}

async function remoteApi() {
    var req = new Request("/gateway/moviess", {
        headers: new Headers({
            "X-CSRF": "1",
            "Short-Token": "ISbBGdwe5AuB5DW7cgFD4Q==",
        }),
    });

    try {
        var resp = await fetch(req);

        let data;
        if (resp.ok) {
            data = await resp.json();
        }
        log("Remote API Result: " + resp.status, data);
    } catch (e) {
        log("error calling remote API");
    }
}