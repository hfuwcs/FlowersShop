$(document).ready(function () {
    var orderId;
    $('.btn-confirmOrder').on('click', function () {
        orderId = $(this).data('order_id');
        console.log(orderId)
        $('#confirmOrderModal').modal('show');
        
    });
    $("#confirm-order-confirm").on("click", function () {
        if (orderId) {
            confirmOrder(orderId);
        }
    });
    function confirmOrder(orderId) {
        $.ajax({
            url: '/Admin/Order/ConfirmOrder',
            type: 'POST',
            data: {
                id: orderId
            },
            success: function (response) {
                if (response) {
                    var row = $('a[data-order_id="' + orderId + '"').closet('tr');
                    row.find('td:nth-child(8)') // Chọn ô trạng thái (cột thứ 8)
                        .html('<span class="badge bg-label-success me-1">Thành công</span>'); // Cập nhật nội dung
                    $('#confirmOrderModal').modal('hide');
                    $('#successAlert').toast('show');
                }
                else {
                    $('#confirmOrderModal').modal('hide');
                    $('#failAlear').toast('show');
                }
            },
            error: function (error) {
                console.error('Lỗi khi xác nhận đơn:', error);
                alert('Có lỗi xảy ra. Vui lòng thử lại.');
            }
        });
    }

    //Hủy đơn hàng
    $('.btn-cancelOrder').on('click', function () {
        orderId = $(this).data('order_id');
        console.log(orderId)
        $('#cancelOrderModal').modal('show');

    });
    $("#cancel-order-confirm").on("click", function () {
        if (orderId) {
            cancelOrder(orderId);
        }
    });
    function cancelOrder(orderId) {
        $.ajax({
            url: '/Admin/Order/CancelOrder',
            type: 'POST',
            data: {
                id: orderId
            },
            success: function (response) {
                if (response) {
                    //var row = $('a[data-order_id="' + orderId + '"').closet('tr');
                    //row.find('td:nth-child(8)') // Chọn ô trạng thái (cột thứ 8)
                    //    .html('<span class="badge bg-label-success me-1">Thành công</span>'); // Cập nhật nội dung
                    $('#cancelOrderModal').modal('hide');
                    $('#successAlert').toast('show');
                }
                else {
                    $('#cancelOrderModal').modal('hide');
                    $('#failAlear').toast('show');
                }
            },
            error: function (error) {
                console.error('Lỗi khi xác nhận đơn:', error);
                alert('Có lỗi xảy ra. Vui lòng thử lại.');
            }
        });
    }

    //Xóa đơn hàng
    //Hủy đơn hàng
    $('.btn-cancelOrder').on('click', function () {
        orderId = $(this).data('order_id');
        console.log(orderId)
        $('#cancelOrderModal').modal('show');

    });
    $("#cancel-order-confirm").on("click", function () {
        if (orderId) {
            cancelOrder(orderId);
        }
    });
    function cancelOrder(orderId) {
        $.ajax({
            url: '/Admin/Order/CancelOrder',
            type: 'POST',
            data: {
                id: orderId
            },
            success: function (response) {
                if (response) {
                    //var row = $('a[data-order_id="' + orderId + '"').closet('tr');
                    //row.find('td:nth-child(8)') // Chọn ô trạng thái (cột thứ 8)
                    //    .html('<span class="badge bg-label-success me-1">Thành công</span>'); // Cập nhật nội dung
                    $('#cancelOrderModal').modal('hide');
                    $('#successAlert').toast('show');
                }
                else {
                    $('#cancelOrderModal').modal('hide');
                    $('#failAlear').toast('show');
                }
            },
            error: function (error) {
                console.error('Lỗi khi xác nhận đơn:', error);
                alert('Có lỗi xảy ra. Vui lòng thử lại.');
            }
        });
    }
});
