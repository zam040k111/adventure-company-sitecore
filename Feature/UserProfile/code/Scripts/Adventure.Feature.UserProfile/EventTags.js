'use strict';

function setTagsEvents() {
	var tagItems = $(".tag-item");
	var selectedItems = $(".selected-item");

	for (var j = 0; j < tagItems.length; j++) {
		tagItems[j].addEventListener("click", function (e) {
			addSelectedTag(e.currentTarget.innerText, e.currentTarget.id);
			e.currentTarget.remove();
		}, false);
	}

	for (var j = 0; j < selectedItems.length; j++) {
		selectedItems[j].addEventListener("click", function (e) {
			addUnselectedTag(e.currentTarget.innerText, e.currentTarget.id);
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
	span.classList.add("selected-item");
	span.setAttribute("id", tagId);
	span.innerHTML = tagName;
	let span2 = document.createElement("span");
	span2.setAttribute("data-role", "remove");
	span2.classList.add("btn-remove");
	span2.addEventListener("click", function (e) {
		addUnselectedTag(e.currentTarget.parentNode.innerText, e.currentTarget.parentNode.id);
		e.currentTarget.parentNode.remove();
	}, false);
	span.appendChild(span2);
	container.append(span);
}

function addUnselectedTag(tagName, tagId) {
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

setTagsEvents();
