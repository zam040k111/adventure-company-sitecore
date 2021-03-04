'use strict';

function setUserProfileEvent() {
	let button = $("#user-profile-button");
	let formSubmitButton = $("#submit-file");
	let image = $("#input-file");

	if (button !== null && button !== undefined) {
		button.on("click", sendUserProfileRequest);
	}
	if (formSubmitButton !== null && formSubmitButton !== undefined) {
		formSubmitButton.on("click", sendUploadRequest);
	}
	if (image !== null && image !== undefined) {
		image.change(function () { changeImage(this); });
	}
}

function changeImage(input) {
	if (input.files && input.files[0]) {
		var reader = new FileReader();

		reader.onload = function (e) {
			$('#user-photo').attr('src', e.target.result);
		}

		reader.readAsDataURL(input.files[0]);
	}
}

function reqListener() {
	operateResponseUserProfile(JSON.parse(this.response));
}

function sendUploadRequest() {
	var form = document.forms.namedItem("file-form");
	var oData = new FormData(form);

	var oReq = new XMLHttpRequest();
	oReq.onload = reqListener;
	oReq.open("POST", "/api/sitecore/UserProfile/UploadPhoto", true);

	oReq.send(oData);
}

function sendUserProfileRequest() {
	$.post(
		"/api/sitecore/UserProfile/ProfilePage",
		getUserProfileData(),
		operateResponseUserProfile);
}

function getUserProfileData() {
	let tagList = $("#tag-container").children();
	let tags = [];

	for (var i = 0; i < tagList.length; i++) {
		tags[i] = tagList[i].innerText;
	}

	return {
		'userProfile': {
			'FirstName': $("#FirstName").val(),
			'LastName': $("#LastName").val(),
			'Phone': $("#Phone").val(),
			'Title': $("#Title").val(),
			'Email': $("#Email").val(),
			'OldPassword': $("#OldPassword").val(),
			'Password': $("#Password").val(),
			'ConfirmPassword': $("#ConfirmPassword").val(),
			'SelectedTags': tags
		}
	}
}

function operateResponseUserProfile(data) {
	if (data !== null && data !== undefined) {
		if (data.validation !== null && data.validation !== undefined) {
			$(".validation-messages li").addClass("d-none");
			$("#success").addClass("d-none");
			$("#failed").addClass("d-none");
			if (data.validation.length !== 0) {
				for (let error of data.validation) {
					document.getElementById(error).classList.remove("d-none");
				}
			} else {
				if (data.success) {
					$("#success").removeClass("d-none");
				} else {
					$("#failed").removeClass("d-none");
				}
			}
		}
	}
}

setUserProfileEvent();