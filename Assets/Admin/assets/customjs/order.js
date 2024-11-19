$(document).ready(function () {
    var orderId;
    $('.btn-confirmOrder').on('click', function () {
        orderId = $(this).data('order_id');
        alert('Đã xác nhận đơn hàng ' + orderId);
        confirmOrder(orderId);
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
                    $('#successAlert').toast('show');
                }
            },
            error: function (error) {
                console.error('Lỗi khi xác nhận đơn:', error);
                alert('Có lỗi xảy ra. Vui lòng thử lại.');
            }
        });
    }
});
