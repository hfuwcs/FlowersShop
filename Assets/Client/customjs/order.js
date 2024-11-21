$(document).ready(function () {

    $('#btn-continueToOrder').on('click', function () {
        $('#btn-continueToOrder').fadeOut();
        $('#consumeProdcut_CustomerInfo').fadeIn();
        $('#btn-confirmOrder').fadeIn();
    });
    $('#samePerson').on('click', function () {
        if ($(this).is(':checked')) {
            $('#customerNameConsume').val($('#customerName').val());
            $('#customerPhoneConsume').val($('#customerPhone').val());
            $('#customerEmailConsume').val($('#customerEmail').val());
            $('#customerAddressConsume').val($('#customerAddress').val());
        } else {
            $('#customerNameConsume').val('');
            $('#customerPhoneConsume').val('');
            $('#customerEmailConsume').val('');
            $('#customerAddressConsume').val('');
        }
    });
    function OnBegin() {
        $("#btn-confirmOrder").prop("disabled", true);
        $("#btn-confirmOrder").text("Đang xử lý...");
    }

    function OnSuccess(respone) {
        if (respone.Success) {
            /*alert("Đặt hàng thành công");*/
            if (respone.Code == 1) {
                location.href = "/Order/OrderSuccess";
            }
            else {
                location.href = respone.Url;
            }
        }
    }

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

    //$('#btn-confirmOrder').on('click', function (e) {
    //    let valid = true;
    //    ValidInput('#customerName', '#customerNameError', 'Họ và tên là bắt buộc.');
    //    ValidInput('#customerPhone', '#customerPhoneError', 'Số điện thoại là bắt buộc.');
    //    ValidInput('#customerEmail', '#customerEmailError', 'Email không hợp lệ.');
    //    ValidInput('#customerAddress', '#customerAddressError', 'Địa chỉ chi tiết là bắt buộc.');

    //    // Kiểm tra lỗi
    //    $('.is-invalid').each(function () {
    //        valid = false;
    //    });

    //    if (!valid) {
    //        e.preventDefault(); // Ngăn không gửi form nếu có lỗi
    //        alert('Vui lòng kiểm tra lại thông tin!');
    //    }
    //});
    window.OnBegin = OnBegin;
    window.OnSuccess = OnSuccess;
});
