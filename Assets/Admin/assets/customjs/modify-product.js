$(document).ready(function () {
    var productId;

    $(".edit-btn").on("click", function () {
        var url = "/Admin/Product/EditProduct";
        productId = $(this).data("product_id");
        // Gửi yêu cầu GET để lấy dữ liệu sản phẩm
        $.get(url, { id: productId }, function (data) {
            // Chèn nội dung modal vào container
            $("#EditModalContainer").html(data);
            $("#EditModal").modal("show");
        });
    });
    $("#edit-confirm").on("click", function () {
        if (productId) {
            editProduct(productId);
        }
    });

    $(".delete-btn").on("click", function () {
        productId = $(this).data("product_id");
        $("#DeleteModal").modal("show");
    });

    $("#delete-confirm").on("click", function () {
        if (productId) {
            deleteProduct(productId);
        }
    });

    function deleteProduct(id) {
        $.ajax({
            url: "/Admin/Product/DeleteProduct",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ productid: id }), // Truyền đúng key và id
            success: function (response) {
                if (response) {
                    $("#product-" + id).remove(); // Xóa hàng sản phẩm
                    $("#DeleteModal").modal("hide"); // Đóng modal
                    $("#successAlert").show(); // Hiện thông báo
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                alert("Đã xảy ra lỗi khi xóa sản phẩm!");
            }
        });
    }
    $("#EditModalContainer").on("submit", "form", function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Admin/Product/EditProduct",
            type: "POST",
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    //Load lại dữ liệu các thông tin sản phẩm
                    $("#product-" + productId + " td").eq(0).text(response.product.Name);
                    $("#product-" + productId + " td").eq(1).text(response.product.Price);
                    $("#product-" + productId + " td").eq(2).text(response.product.Description);
                    $("#product-" + productId + " td").eq(4).text(response.product.Quantity);
                    $("#EditModal").modal("hide");
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                alert("Đã xảy ra lỗi khi cập nhật sản phẩm!");
            }
        });
    });
});