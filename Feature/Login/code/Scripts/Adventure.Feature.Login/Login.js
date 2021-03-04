'use strict';

var LoginButtonText = null;
var LogoutButtonText = null;
var HideFormButtonText = null;

function setLoginEvent() {
	let button = $("#login-user-button");
	let userName = $("#user-name");

    if (button !== null && button !== undefined) {
        button.on("click", sendLoginRequest);
	}
	if (userName !== null && userName !== undefined) {
		userName.attr("href", "/AccountHeader/UserProfile");
	}

    LoginButtonText = $("#login-button-text").val();
    LogoutButtonText = $("#logout-button-text").val();
	HideFormButtonText = $("#hide-form-button-text").val();
}

function setLoginLogoutHeaderEvent() {
	hideLoginForm();
	let userName = getUserName();
	let button = $("#login-logout-button");

	if (button !== null && button !== undefined) {
		if (userName.length > 0) {
			button.text(LogoutButtonText);
			button.on("click", sendLogoutRequest);
			button.attr("type", "submit");
		} else {
			button.text(LoginButtonText);
			button.on("click", showLoginForm);
			button.attr("type", "button");
		}
		$("#user-name").text(userName);
	}
}

function getUserName() {
	return $.ajax({
		type: "GET",
		url: "/api/sitecore/Login/GetUserName",
		async: false
	}).responseText;
}

function sendLogoutRequest() {
	$.get("/api/sitecore/Login/Logout");
	hideLoginForm();
	$("#user-name").text("");
}

function showLoginForm() {
	$("#login-form").removeClass("d-none");
	$("#login-logout-button").text(HideFormButtonText);
	$("#login-logout-button").on("click", hideLoginForm);
}

function hideLoginForm() {
	$("#login-form").addClass("d-none");
	$("#login-logout-button").text(LoginButtonText);
	$("#login-logout-button").on("click", showLoginForm);
}

function sendLoginRequest() {
    $.post(
        "/api/sitecore/Login/Login",
        getLoginViewModelData(),
        operateResponseLogin);
}

function getLoginViewModelData() {
	let checked = false;
	if ($('#still-logged-in').is(":checked")) {
		checked = true;
	}
	return {
        'loginViewModel': {
            'UserName': $("#userName").val(),
			'Password': $("#password").val(),
			'AfterLoginRedirectTo': $("#redirection").val(),
			'StillLoggedIn': checked
        }
    }
}

function operateResponseLogin(data) {
	if (data !== null && data !== undefined) {
		$("#login-result-message").addClass("d-none");
		if (data.success) {
			setLoginLogoutHeaderEvent();
			window.location.href = data.redirect;
		} else {
			$("#login-result-message").text(data.message);
			$("#login-result-message").removeClass("d-none");
		}
	}
}

setLoginEvent();
setLoginLogoutHeaderEvent();