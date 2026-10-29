$(document).ready(function () {

    $('#leaveType').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

    $('#Reason').on('input', function () {
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
            showToastr(notificationType, notificationMsg);
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }
});

function fnValidation()
{
    debugger;
    var isValid = true;
    // Get trimmed values
    var leaveType = $.trim($("#LeaveType").val());
    var reason = $.trim($("#Reason").val());
    var startDate = $.trim($("#StartDate").val());
    var endDate = $.trim($("#EndDate").val());


    //leaveType
    if (leaveType == null || leaveType == "" || leaveType == undefined) {
        $("#LeaveType").next("span").text("Leave Type is required!");
        isValid = false;
    } else {
        $("#LeaveType").next("span").text("");
    }



    //reason
    if (reason == null || reason == "" || reason == undefined) {
        $("#Reason").next("span").text("Reason is required!");
        isValid = false;
    } else {
        $("#Reason").next("span").text("");
    }

    // Start Date
    if (startDate == null || startDate == "" || startDate == undefined) {
        $("#StartDate").next("span").text("Start Date is required!");
        isValid = false;
    } else {
        $("#StartDate").next("span").text("");
    }

    // End Date
    if (endDate == null || endDate == "" || endDate == undefined) {
        $("#EndDate").next("span").text("End Date is required!");
        isValid = false;
    } else {
        $("#EndDate").next("span").text("");
    }

    // Start Date should not be after End Date
    if (startDate && endDate && new Date(startDate) > new Date(endDate)) {
        $("#EndDate").next("span").text("End Date cannot be before Start Date!");
        isValid = false;
    }

    return isValid;
}

$(function () {
    var now = new Date();

    // Format date and datetime values
    var dateStr = now.toISOString().split('T')[0];
    var datetimeStr = now.toISOString().slice(0, 16);

    // Set values to corresponding input fields using IDs or name attributes
    $('input[name="StartDate"]').val(dateStr);
    $('input[name="EndDate"]').val(dateStr);
    $('input[name="RequestedAt"]').val(datetimeStr);
    $('input[name="ProcessedAt"]').val(datetimeStr);
});

$(document).ready(function () {
    const $checkbox = $('input[name="IsHalfDay"]');
    const $halfTypeCol = $checkbox.closest('.row').find('.col').eq(1); // Second column

    toggleHalfType($checkbox.is(':checked'));

    $checkbox.change(function () {
        toggleHalfType($(this).is(':checked'));
    });

    function toggleHalfType(isChecked) {
        if (isChecked) {
            $halfTypeCol.show();
        } else {
            $halfTypeCol.hide();
        }
    }
});