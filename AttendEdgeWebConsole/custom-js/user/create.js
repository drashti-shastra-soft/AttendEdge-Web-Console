var emailAddressRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

$(document).ready(function () {

    $('#MobileNumber').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '').substring(0, 10).replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }
        }
    });


    $('#EmailAddress').on('input', function () {
        let val = $(this).val();

        // Allow only valid email characters
        val = val.replace(/[^a-zA-Z0-9@._\-]/g, '');

        // Prevent double @ symbols
        const parts = val.split('@');
        if (parts.length > 2) {
            val = parts[0] + '@' + parts[1];
        }

        $(this).val(val);
    });

    $('#firstName').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

    $('#lastName').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

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

    $('#EmailAddress').blur(function () {
        var emailAddress = $.trim($('#EmailAddress').val());
        if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
            $("#EmailAddress").next("span").text("Email Address  is required!");
        } else if (!emailAddressRegex.test(emailAddress)) {
            $("#EmailAddress").next("span").text("Invalid email address!");
        }
        else {
            CheckEmailAddressExist(emailAddress);
        }
    });     

});

function fnValidation() {
    var isValid = true;
    var firstName = $.trim($("#FirstName").val());
    var lastName = $.trim($("#LastName").val());
    var emailAddress = $.trim($("#EmailAddress").val());
    var mobileNumber = $.trim($("#MobileNumber").val());
    var roleId = $.trim($("#RoleId").val());

    // FirstName
    if (firstName == null || firstName == "" || firstName == undefined) {
        $("#FirstName").next("span").text("First Name is required!");
        isValid = false;
    } else {
        $("#FirstName").next("span").text("");
    }

    // LastName
    if (lastName == null || lastName == "" || lastName == undefined) {
        $("#LastName").next("span").text("Last Name is required!");
        isValid = false;
    } else {
        $("#LastName").next("span").text("");
    }


    // Email Address
    if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
        $("#EmailAddress").next("span").text("Email Address  is required!");
        isValid = false;
    } else {
        $("#EmailAddress").next("span").text("");
    }

    // Mobile Number
    if (mobileNumber == null || mobileNumber == "" || mobileNumber == undefined) {
        $("#MobileNumber").next("span").text("Mobile Number is required!");
        isValid = false;
    } else {
        $("#MobileNumber").next("span").text("");
    }


    //Role
    if (roleId == null || roleId == "" || roleId == undefined) {
        $("#spnRole").text("Role is required!");
        isValid = false;
    } else {
        $("#spnRole").text("");
    }

    return isValid;
}

function CheckEmailAddressExist(emailAddress) {
    var mUser = {};
    mUser.Id = parseInt($('#Id').val());
    mUser.EmailAddress = emailAddress;

    fnHandleAjaxRequest({
        url: `/User/CheckEmailAddressExist`,
        method: 'POST',
        dataType: 'json',
        data: mUser,
        onSuccess: function (response) {
            if (response) {
                if (response.StatusCode === "Ok") {
                    if (response.IsExist === true) {
                        $('.btn-submit').removeClass('btn-submit').addClass('disabled-link');
                        $("#EmailAddress").next("span").text("This email is already linked to an account.");
                    }
                    else if (response.IsExist === false) {
                        $('.disabled-link').removeClass('disabled-link').addClass('btn-submit');
                        $("#EmailAddress").next("span").text("");
                    }
                } else {

                }
            }
        },
        onError: function () {
            showToastr("Failed to check duplicate user!");
        }
    });
}
