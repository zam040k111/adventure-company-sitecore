'use strict';

function setSearchEventsEvent() {
    let button = $("#search-events-button");
    if (button !== null && button !== undefined) {
        button.on("click", sendRequestSearchEvents);
    }
}

function setTagsEvents() {
    var tagItems = $(".tag-item");

    for (var j = 0; j < tagItems.length; j++) {
        tagItems[j].addEventListener("click", function (e) {
            addSelectedTag(e.currentTarget.innerText, e.currentTarget.id);
            e.currentTarget.remove();
	    }, false);
    }
}

function addSelectedTag(tagName, tagId) {
    let container = $("#tag-container");
    let span = document.createElement("span");
    span.classList.add("tag");
    span.classList.add("label");
    span.classList.add("label-info");
    span.setAttribute("id", tagId);
    span.innerHTML = tagName;
    let span2 = document.createElement("span");
    span2.setAttribute("data-role", "remove");
    span2.classList.add("btn-remove");
    span2.addEventListener("click", function (e) {
        addTag(e.currentTarget.parentNode.innerText, e.currentTarget.parentNode.id);
	    e.currentTarget.parentNode.remove();
    }, false);
    span.appendChild(span2);
    container.append(span);
}

function addTag(tagName, tagId) {
    var tagsContainer = $("#tags");
    let span = document.createElement("span");
    span.classList.add("tag");
    span.classList.add("label");
    span.classList.add("label-info");
    span.classList.add("tag-item");
    span.innerHTML = tagName;
    span.setAttribute("id", tagId);
    span.addEventListener("click", function (e) {
        addSelectedTag(e.currentTarget.innerText, e.currentTarget.id);
        e.currentTarget.remove();
    }, false);
    tagsContainer.append(span);
}

function sendRequestSearchEvents() {
    $.post(
        "/api/sitecore/EventSearch/Search",
        getEventSearchRequestData(),
        operateSearchEventsResponse);
}

function getEventSearchRequestData() {
    let tagList = $("#tag-container").children();
    let tags = [];

    for (var i = 0; i < tagList.length; i++) {
        tags[i] = tagList[i].id;
    }

    return {
        'Name': $("#EventName").val(),
        'StartDate': $("#StartDate").val(),
        'Difficulty': $("#Difficulty").val(),
        'OrderBy': $("#OrderBy").val(),
        'PageNumber': $("#PageNumber").val(),
        'PageSize': $("#PageSize").val(),
        'Tags': tags
    };
}

function operateSearchEventsResponse(data) {
    if (data !== null && data !== undefined) {
        let eventsContainer = $("#events-container");
        eventsContainer.html("");
        let fontBoldClass = "font-weight-bold";
        for (let eventItem of data.FilteredItems) {

            let div = document.createElement('div');
            div.classList.add("event-list-item");

            let a = document.createElement("a");
            a.setAttribute("href", eventItem.Url);
            a.classList.add("row");

            let divName = document.createElement("div");
            divName.innerHTML = eventItem.Title;
            divName.classList.add(fontBoldClass);
            divName.classList.add("col-2");

            let divDate = document.createElement("div");
            divDate.classList.add("col-3");
            divDate.innerHTML = eventItem.StartDate;

            let divSeparator = document.createElement("div");
            divSeparator.classList.add("col-1");

            let divDifficulty = document.createElement("div");
            let spanDifficulty = document.createElement("span");
            spanDifficulty.classList.add(fontBoldClass);
            spanDifficulty.innerHTML = " Difficulty: ";
            divDifficulty.appendChild(spanDifficulty);
            divDifficulty.innerHTML += eventItem.Difficulty;
            divDifficulty.classList.add("col-2");

            let divTags = document.createElement("div");
            divTags.classList.add("col-3");
            divTags.innerHTML = " Tags: ";
            for (let tag of eventItem.Tags) {
	            divTags.innerHTML += tag + " ";
            }

            a.appendChild(divName);
            a.appendChild(divDate);
            a.appendChild(divSeparator);
            a.appendChild(divDifficulty);
            a.appendChild(divTags);

            div.append(a);
            eventsContainer.append(div);
        }

        createEventListPagination(data);
    }
}

function createEventListPagination(data) {
    if (data !== null && data !== undefined) {
        $("#pagination-items-container").html("");

        if (data.TotalPages > 1) {
            for (let i = 1; i <= data.TotalPages; i++) {
                let input = document.createElement("input");
                input.setAttribute("type", "button");

                input.classList.add("btn");
                input.classList.add("event-pagination-item");
                if (i === data.PageNumber) {
                    input.classList.add("btn-primary");
                }

                input.value = i;
                input.id = i;

                $("#pagination-items-container").append(input);
            }
        }

        setEventListPaginationItemsClicks();
    }
}

function setEventListPaginationItemsClicks() {
    let paginationItemCollection = $(".event-pagination-item");
    for (let paginationItemIndex = 0; paginationItemIndex < paginationItemCollection.length; paginationItemIndex++) {
        paginationItemCollection[paginationItemIndex].addEventListener("click", function (e) {
            changePage(paginationItemCollection[paginationItemIndex].value);

            e.stopPropagation();
        });
    }

    $("#PageSize").on("change", sendRequestSearchEvents);
}

function changePage(pageNumber) {
    $("#PageNumber").val(pageNumber);
    $(".btn-primary").removeClass("btn-primary");
    $("#" + pageNumber).addClass("btn-primary");
    sendRequestSearchEvents();
}

setSearchEventsEvent();
setEventListPaginationItemsClicks();
setTagsEvents();