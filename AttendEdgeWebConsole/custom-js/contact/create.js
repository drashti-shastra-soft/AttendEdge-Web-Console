$(document).ready(function () {
    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmCreate').submit();
        }
    });

    var notificationType = $.trim($('#NotificationType').val());
    var notificationMsg = $.trim($('#NotificationMsg').val());

    if (notificationType != null && notificationType != undefined && notificationType != '') {
        if (notificationMsg != null && notificationMsg != undefined && notificationMsg != '') {
            showToastr(notificationType, notificationMsg)
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }
});


function fnValidation() {
    var isValid = true;
    var emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;
    var firstName = $.trim($("#FirstName").val());
    var lastName = $.trim($("#LastName").val());
    var emailAddress = $.trim($("#EmailAddress").val());
    var mobileNumber = $.trim($("#MobileNumber").val());
    var roleId = $.trim($("#RoleId").val());

    // FirstName
    if (firstName == null || firstName == "" || firstName == undefined) {
        $("#FirstName").next("span").text("FirstName is required!");
        isValid = false;
    } else {
        $("#FirstName").next("span").text("");
    }

    // LastName
    if (lastName == null || lastName == "" || lastName == undefined) {
        $("#LastName").next("span").text("LastName is required!");
        isValid = false;
    } else {
        $("#LastName").next("span").text("");
    }

    // Email Address
    if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
        $("#EmailAddress").next("span").text("Email Address is required!");
        isValid = false;
    } else if (emailAddress != null && emailAddress != "" && emailAddress != undefined) {
        if (!emailRegex.test(emailAddress)) {
            $("#EmailAddress").next("span").text("Email Address is invalid!");
            isValid = false;
        } else {
            $("#EmailAddress").next("span").text("");
        }
    } else {
        $("#EmailAddress").next("span").text("");
    }

    // Mobile Number
    if (mobileNumber != null && mobileNumber != "" && mobileNumber != undefined) {
        if (mobileNumber.length != 10) {
            $("#MobileNumber").next("span").text("Please enter valid mobile number!");
            isValid = false;
        } else {
            $("#MobileNumber").next("span").text("");
        }
    }

    return isValid;
}