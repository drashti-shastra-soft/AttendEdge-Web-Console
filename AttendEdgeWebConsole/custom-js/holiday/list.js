$(function () {
    $(document).off('click', '.add-holiday').on('click', '.add-holiday', fnCreate);

    $(document).off('click', '.btn-edit').on('click', '.btn-edit', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        if (id) fnGet(id);
    });

    $(document).off('click', '.icon-delete').on('click', '.icon-delete', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        if (id) fnDelete(id);
    });

    // Reset pagination and submit the form on Enter key in search fields
    $(document).off('keyup', '.search-text').on('keyup', '.search-text', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            resetPagination();
            $('#frmList').trigger('submit');
        }
    });

    // Submit form when page size filter changes
    $(document).off('change', '.search-dropdown').on('change', '.search-dropdown', function () {
        $('#frmList').trigger('submit');
    });

    $(document).off('change', '.search-pagination').on('change', '.search-pagination', function () {
        $('#frmList').trigger('submit');
    })


    $(document).off('click', '.navigation').on('click', '.navigation', function (e) {
        e.preventDefault();
        var direction = $(this).data('direction').trim();
        if (direction != null && direction != undefined && direction != '') {
            triggerPageNavigation(direction);
        }
        $('#frmList').trigger('submit');
    });
});

function fnGet(id) {
    fnHandleAjaxRequest({
        url: `/Holiday/Get/${id}`,
        onSuccess: function (html) {
            $('#divCreate').html(html);
            $('#modal-holiday').modal('show');
        }
    });
}

function fnCreate() {
    fnHandleAjaxRequest({
        url: '/Holiday/Create',
        onSuccess: function (html) {
            $('#divCreate').html(html);
            $('#modal-holiday').modal('show');
        },
        onError: function () {
            showToast("Failed to load form.");
        }
    });
}

function fnDelete(id) {
    fnHandleAjaxRequest({
        url: `/Holiday/Delete/${id}`,
        dataType: 'json',
        onSuccess: function (response) {
            if (response) {
                showToastr(response.NotificationType, response.NotificationMsg);
                $('#frmList').submit();
            }
        },
        onError: function () {
            showToast("Failed to delete user.");
        }
    });
}

function triggerPageNavigation(btn) {
    const config = window.paginationConfig;
    const currentSkip = parseInt($('#Pagination_Skip').val()) || 0;

    let newSkip = currentSkip;

    switch (btn) {
        case "First":
            if (config.currentPage > 1) newSkip = 0;
            break;

        case "Next":
            if (config.currentPage < config.totalPage) newSkip += config.pageSize;
            break;

        case "Previous":
            if (currentSkip >= config.pageSize) newSkip -= config.pageSize;
            break;

        case "Last":
            if (config.currentPage < config.totalPage) newSkip = (config.totalPage - 1) * config.pageSize;
            break;

        default:
            return;
    }

    if (newSkip !== currentSkip || btn === "First") {
        $('#Pagination_Skip').val(newSkip);
        $('#frmList').submit();
    }
}

$(document).ready(function () {
    if ($('#SearchCriteria_Date').val() == '01/01/0001' || $('#SearchCriteria_Date').val() == '1/1/0001') {
        $('#SearchCriteria_Date').val('');
    }

    if ($('#SearchCriteria_StartTime').val() == '12:00 AM') {
        $('#SearchCriteria_StartTime').val('');
    }

    if ($('#SearchCriteria_EndTime').val() == '12:00 AM') {
        $('#SearchCriteria_EndTime').val('');
    }

    if ($('#SearchCriteria_Module').val() == '0') {
        $('#SearchCriteria_Module').val('');
    }
});

