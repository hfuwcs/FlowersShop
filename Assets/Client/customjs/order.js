$(document).ready(function () {

    $('#btn-continueToOrder').on('click', function () {
        $('#btn-continueToOrder').fadeOut();
        $('#consumeProdcut_CustomerInfo').fadeIn();
        $('btn-confirmOrder').fadeIn();
    });

    $('#customerName').on('focusout', function () {
        ValidInput('#customerName', '#customerNameError', 'Họ và tên là bắt buộc.');
    });

    $('#customerPhone').on('focusout', function () {
        ValidInput('#customerPhone', '#customerPhoneError', 'Số điện thoại là bắt buộc.');
    });

    $('#customerEmail').on('focusout', function () {
        ValidInput('#customerEmail', '#customerEmailError', 'Email không hợp lệ.');
    });

    $('#customerAddress').on('focusout', function () {
        ValidInput('#customerAddress', '#customerAddressError', 'Địa chỉ chi tiết là bắt buộc.');
    });

    $('#customerProvince').on('focusout', function () {
        ValidInput('#customerProvince', '#customerProvinceError', 'Tỉnh/Thành phố là bắt buộc.');
    });

    function ValidInput(inputSelector, errorSelector, errorMessage) {
        var value = $(inputSelector).val();
        if (value === undefined || value === null || value.trim() === '') {
            $(inputSelector).addClass('is-invalid');
            $(errorSelector).text(errorMessage).show();
        } else {
            $(inputSelector).removeClass('is-invalid');
            $(errorSelector).hide();
        }
    }
});
