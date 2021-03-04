'use strict';

function setSubscribeEvent() {
    let button = $("#subscriptionButton");

    if (button !== null && button !== undefined) {
        button.on("click", sendSubscribeRequest);
    }
}

function sendSubscribeRequest() {
    $.post(
        "/api/sitecore/Mailing/Subscription",
        getSubscribeViewModelData(),
        operateResponse)
        .fail(operateFail);
}


function getSubscribeViewModelData() {
    return {
        'subscribeViewModel': {
            'Email': $("#subscriptionEmail").val(),
            'EmailValidationMessage': $("#email-validation-message")[0].innerHTML
        }
    }
}

function operateFail() {
    $("#fail").removeClass("d-none");
}

function operateResponse(data) {
    if (data !== null && data !== undefined) {
        $("#success").addClass("d-none");
        $("#fail").addClass("d-none");
        $("#email-validation-message").addClass("d-none");
        if (data.success) {
            $("#success").removeClass("d-none");
        } else {
            $("#email-validation-message").removeClass("d-none");
        }
    }
}

setSubscribeEvent();