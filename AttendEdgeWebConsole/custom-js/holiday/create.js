$(document).ready(function () {
    $('#Name').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

    $('#description').on('input', function () {
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

function fnValidation() {
    var isValid = true;
    var name = $.trim($("#Name").val());
    var date = $.trim($("#Date").val());
    var dateRegex = /^\d{4}-\d{2}-\d{2}$/;

    // Name

    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    //Date date
    if (date == null || date == "" || date == undefined) {
        $("#Date").next("span").text("Date is required!");
        isValid = false;
    } else {
        if (!dateRegex.test(date)) {
            $("#Date").next("span").text("Invalid date format! Use YYYY-MM-DD.");
            isValid = false;
        } else {
            $("#Date").next("span").text("");
        }
    }

    return isValid;
}