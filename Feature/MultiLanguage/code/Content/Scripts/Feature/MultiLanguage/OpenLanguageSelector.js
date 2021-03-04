'use strict';

const timeoutForDisappearance = 2000;
const displayInlineBlockClass = "d-block";

$(".list-opener").on("click", openLanguageSelector);

function openLanguageSelector() {
    $("#other-languages a").addClass(displayInlineBlockClass);

    setTimeout(function () {
            $("#other-languages a").removeClass(displayInlineBlockClass);
        },
        timeoutForDisappearance);
}