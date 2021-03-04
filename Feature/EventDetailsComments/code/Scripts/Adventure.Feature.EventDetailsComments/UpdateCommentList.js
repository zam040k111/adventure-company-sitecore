'use strict';

const timeout = 1000;

function setClickHandlerUpdatingCommentList() {
    let button = $("#add-comment-button");
    if (button !== null && button !== undefined) {
        button.on("click", sendRequestUpdateCommentList);
    }
}

function sendRequestUpdateCommentList() {
    setTimeout(function() {
            $.get(
                "/api/sitecore/Comment/GetDisplayed",
                {
                    commentsCountToDisplay: $("#CommentsCount").val(),
                    eventId: $("#EventId").val()
                }).done(operateResponseUpdateCommentList);
        },
        timeout);
}

function operateResponseUpdateCommentList(data) {
    if (data !== null && data !== undefined) {
        let commentContainer = $("#comment-container");
        commentContainer.html("");
        for (let comment of data) {
            let nameDateDive = document.createElement("div");
            let textDiv = document.createElement("div");

            let nameSpan = document.createElement("span");
            nameSpan.classList.add("font-weight-bold");
            nameSpan.innerHTML = comment.AuthorName;

            nameDateDive.append(nameSpan);
            nameDateDive.innerHTML += " - " + comment.DateAdded;

            textDiv.innerHTML = comment.Text;

            commentContainer.append(nameDateDive);
            commentContainer.append(textDiv);
        }
    }
}

setClickHandlerUpdatingCommentList();