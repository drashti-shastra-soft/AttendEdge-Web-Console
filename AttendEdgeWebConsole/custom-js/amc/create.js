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

    $('#Email').on('input', function () {
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

    $('#Name').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

    $('#ContactPerson').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

    

    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmCreate').submit();
        }
    });

    var today = new Date().toISOString().split('T')[0];
    $('input[name="ContractStartDate"]').val(today);
    $('input[name="ContractEndDate"]').val(today);

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
    var name = $.trim($("#Name").val());
    var mobileNumber = $.trim($("#MobileNumber").val());
    var emailAddress = $.trim($("#EmailAddress").val());
    var contactPerson = $.trim($("#ContactPerson").val());
    var startDate = $.trim($("#ContractStartDate").val());
    var endDate = $.trim($("#ContractEndDate").val());
    var servicesCovered = $.trim($("#ServicesCovered").val());

    // Name validation
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    // Contract Start Date
    if (startDate == null || startDate == "" || startDate == undefined) {
        $("#ContractStartDate").next("span").text("Start Date is required!");
        isValid = false;
    } else {
        $("#ContractStartDate").next("span").text("");
    }

    // Contract End Date
    if (endDate == null || endDate == "" || endDate == undefined) {
        $("#ContractEndDate").next("span").text("End Date is required!");
        isValid = false;
    } else {
        $("#ContractEndDate").next("span").text("");
    }

    //ContactPerson
    if (contactPerson == null || contactPerson == "" || contactPerson == undefined) {
        $("#ContactPerson").next("span").text("Contact Person Date is required!");
        isValid = false;
    } else {
        $("#ContactPerson").next("span").text("");
    }


    //services Covered 
    if (servicesCovered == null || servicesCovered == "" || servicesCovered == undefined) {
        $("#ServicesCovered").next("span").text("Services Covered is required!");
        isValid = false;
    } else {
        $("#ServicesCovered").next("span").text("");
    }

    // Start Date should not be after End Date
    if (startDate == null || startDate == "" || startDate == undefined) {
        $("#ContractStartDate").next("span").text("Contract Start Date Covered is required!");
        isValid = false;
    } else {
        $("#ContractStartDate").next("span").text("");
    }


     //Email Address
    if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
        $("#EmailAddress").next("span").text("Email Address  is required!");
        isValid = false;
    } else {
        $("#EmailAddress").next("span").text("");
    }

    //if (email == null || email == "" || email == undefined) {
    //    $("#Email").next("span").text("Email Address is required!");
    //    isValid = false;
    //} else if (email != null && email != "" && email != undefined) {
    //    if (!emailRegex.test(email)) {
    //        $("#Email").next("span").text("Email Address is invalid!");
    //        isValid = false;
    //    } else {
    //        $("#Email").next("span").text("");
    //    }
    //} else {
    //    $("#Email").next("span").text("");
    //}

    if (mobileNumber == null || mobileNumber == "" || mobileNumber == undefined) {
        $("#MobileNumber").next("span").text("Mobile Number is required!");
        isValid = false;
    } else {
        $("#MobileNumber").next("span").text("");
    }


    return isValid;
}