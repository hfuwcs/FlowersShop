$(document).ready(function () {
    var discountId;
    $(".edit-btn-discountCode").on("click", function () {
        var url = "/Admin/Discount/EditDiscountCode";
        discountId = $(this).data("discount_id");
        $.get(url, { id: discountId }, function (data) {
            $("#EditDiscountCodeContainer").html(data);
            $("#EditDiscountCode").modal("show");
        });
    });
    //too freaking lazy too freaking tired to do this ajaxxxxxxxxxx
    //$("#EditDiscountCodeContainer").on("submit", "form", function (e) {
    //    e.preventDefault();

    //    var form = $(this)[0];
    //    var formData = new FormData(form); // Tạo đối tượng FormData từ form

    //    $.ajax({
    //        url: "/Admin/Discount/EditDiscountCode",
    //        type: "POST",
    //        data: formData, // Sử dụng FormData để gửi tệp
    //        processData: false, // Không xử lý dữ liệu, để FormData xử lý
    //        contentType: false, // Để browser tự xác định content type cho form data
    //        success: function (response) {
    //            if (response.success) {
    //                $("#EditDiscountCode").modal("hide");
    //                $("#successAleart").toast("show");
    //            }
    //        },
    //        error: function (xhr, status, error) {
    //            console.log(error);
    //            $("#failAleart").toast("show");
    //        }
    //    });
    //});
    //$("#DeleteDiscountCode").on("submit", function (e) {
    //    //e.preventDefault();
    //    var discountId = $(this).data("discount_id");
    //    var url = "/Admin/Discount/DeleteDiscountCode";
    //    $.post(url, { id: discountId }, function (data) {
    //        if (data.success) {
    //            $("#DeleteDiscountCode").modal("hide");
    //            $("#successAleart").toast("show");
    //        }
    //        else {
    //            $("#failAleart").toast("show");
    //        }
    //    });
    //});
    $(".delete-btn-discountCode").on("click", function () {
        discountId = $(this).data("discount_id");
        $("#DeleteDiscountCode").modal("show");
    });
    $("#delete-discountCode-confirm").on("click", function () {
        if (discountId) {
            deleteProduct(discountId);
        }
    });

    function deleteProduct(id) {
        $.ajax({
            url: "/Admin/Discount/DeleteDiscountCode",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ id: id }), // Truyền đúng key và id
            success: function (response) {
                if (response) {
                    $("#discount-" + id).remove(); // Xóa hàng sản phẩm
                    $("#DeleteDiscountCode").modal("hide"); // Đóng modal
                    $("#successAleart").toast("show"); // Hiện thông báo
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                $("#failAleart").toast("show");
            }
        });
    }
});