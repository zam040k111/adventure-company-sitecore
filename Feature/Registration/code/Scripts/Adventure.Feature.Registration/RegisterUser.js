'use strict';

function setRegisterUserEvent() {
    let button = $("#register-user-button");

    if (button !== null && button !== undefined) {
        button.on("click", sendRegisterUserRequest);
    }
}

function sendRegisterUserRequest() {
    $.post(
        "/api/sitecore/Registration/RegisterUser",
        getRequestData(),
        operateResponseRegisterUser)
        .fail(operateFailRegisterUser);
}


function getRequestData() {
    return {
        'newUserViewModel': {
            'UserName': $("#UserName").val(),
            'Email': $("#Email").val(),
            'Password': $("#Password").val(),
            'ConfirmPassword': $("#PasswordConfirmation").val(),
        },
        'dataSourceId': $("#DataSourceId").val()
    }
}

function operateFailRegisterUser() {
    $("#failed-register-container").removeClass("d-none");
}

function operateResponseRegisterUser(data) {
    if (data !== null && data !== undefined) {
        if (data.validation !== null && data.validation !== undefined) {
            $("#validation-container li").addClass("d-none");
            $("#success-register-container").addClass("d-none");
            $("#failed-register-container").addClass("d-none");
            if (data.validation.length !== 0) {
                for (let error of data.validation) {
                    $("#" + error).removeClass("d-none");
                }
            } else {
                if (data.success) {
                    $("#success-register-container").removeClass("d-none");
                } else {
                    $("#failed-register-container").removeClass("d-none");
                }

                if (data.emailSent) {
                    $("#email-sent-container").removeClass("d-none");
                } else {
                    $("#email-sent-container").removeClass("d-none");
                }

                $("#registration-input-list input").val("");
            }
        }
    }
}

setRegisterUserEvent();