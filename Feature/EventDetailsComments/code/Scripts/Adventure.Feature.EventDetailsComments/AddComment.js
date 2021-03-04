'use strict';

function setClickHandlerAddingComment() {
    let button = $("#add-comment-button");
    if (button !== null && button !== undefined) {
        button.on("click", sendRequestAddComment);
    }
}

function sendRequestAddComment() {
    $.post(
        "/api/sitecore/Comment/Add",
        getRequestData(),
        operateResponseAddComment);
}

function getRequestData() {
    return {
        'addCommentViewModel' : {
            'authorName': $("#AuthorName").val(),
            "text": $("#Text").val(),
            "eventId": $('#EventId').val()
        }, 
        'validationMessages': {
            'AuthorNameLengthMessage': $("#AuthorNameLengthMessage").text(),
            'AuthorNameRequiredMessage': $("#AuthorNameRequiredMessage").text(),
            'CommentTextLengthMessage': $("#CommentTextLengthMessage").text(),
            'CommentTextRequiredMessage': $("#CommentTextRequiredMessage").text()
        }
    }
}

function operateResponseAddComment(data) {
    if (data !== null && data !== undefined) {
        $("#validation-container li").addClass("d-none");
        $("#success-adding-container").addClass("d-none");
        if (data.length !== 0) {
            for (let error of data) {
                $("#" + error).removeClass("d-none");
            }
        } else {
            $("#success-adding-container").removeClass("d-none");
            $("#AuthorName").val("");
            $("#Text").val("");
        }

    }
}

setClickHandlerAddingComment();