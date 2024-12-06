$(document).ready(function () {
    var productId;

    //test show toast
    //không quan trọng
    $("#btn-show").on("click", function () {
        //var toast = new bootstrap.Toast($("#successAleart")[0]);
        //toast.show(); Cách 1
        $("#failAleart").toast("show"); // Cách 2
    });

    $(".edit-btn-product").on("click", function () {
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
                    $("#successAleart").toast("show"); // Hiện thông báo
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                $("#failAleart").toast("show");
            }
        });
    }
    $("#EditModalContainer").on("submit", "form", function (e) {
        e.preventDefault();

        var form = $(this)[0];
        var formData = new FormData(form); // Tạo đối tượng FormData từ form

        $.ajax({
            url: "/Admin/Product/EditProduct",
            type: "POST",
            data: formData, // Sử dụng FormData để gửi tệp
            processData: false, // Không xử lý dữ liệu, để FormData xử lý
            contentType: false, // Để browser tự xác định content type cho form data
            success: function (response) {
                if (response.success) {
                    var productId = response.product.Product_ID;
                    var newImageSrc = response.product.Image;


                    // Cập nhật thông tin sản phẩm trên trang
                    $("#product-" + productId + " td").eq(0).text(response.product.Name);
                    $("#product-" + productId + " td").eq(1).text(response.product.Price);
                    $("#product-" + productId + " td").eq(2).text(response.product.Description);
                    // Kiểm tra nếu newImageSrc không null
                    if (newImageSrc) {

                        // Tạo thẻ <img> mới
                        var newImage = $("<img>").attr("src", newImageSrc);

                        // Thay thế thẻ <span> bằng thẻ <img>
                        $("#product-" + productId + " td").eq(3).find("span").replaceWith(newImage);
                    } else {
                        // Nếu newImageSrc là null, thay thế thẻ <span> bằng thẻ <img> mặc định
                        var defaultImage = $("<img>").attr("src", "default-image.jpg");
                        $("#product-" + productId + " td").eq(3).find("span").replaceWith(defaultImage);
                    }
                    $("#product-" + productId + " td").eq(4).text(response.product.Quantity);

                    //Sau khi cập nhật thành công
                    $("#EditModal").modal("hide");
                    $("#successAleart").toast("show");
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                $("#failAleart").toast("show");
            }
        });
    });

    $("#CreateProduct").on("submit", "form", function (e) {
        e.preventDefault();

        var form = $(this)[0];
        var formData = new FormData(form); // Tạo đối tượng FormData từ form

        $.ajax({
            url: "/Admin/Product/CreateProduct",
            type: "POST",
            data: formData, // Sử dụng FormData để gửi tệp
            processData: false, // Không xử lý dữ liệu, để FormData xử lý
            contentType: false, // Để browser tự xác định content type cho form data
            success: function (response) {
                if (response.success) {
                    $("#successAleart").toast("show");
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                $("#failAleart").toast("show");
            }
        });
    });
});