$(document).ready(function () {
    var productId;
    // Add to cart
    $(document).ready(function () {
        $('.add-to-cart').on('click', function () {
            var id = $(this).data('product_id');
            var quantity = $(this).closest('tr').find('#quantity').val();

            $.ajax({
                url: '/Cart/AddToCart',
                type: 'POST',
                data: {
                    id: id,
                    quantity: quantity
                },
                success: function (res) {
                    $('#cart-count').html(res.totalQuantity);
                    $('#cart-price').html(res.totalPrice);
                    alert("Thêm vào giỏ hàng thành công");
                },
                error: function (err) {
                    alert("Lỗi không thể thêm");
                    console.log(err);
                }
            });
        });
    });


    // Remove from cart
    $('.remove-from-cart').on('click', function () {
        var id = $(this).data('product_id');
        $.ajax({
            url: '/Cart/RemoveFromCart',
            type: 'POST',
            data: {
                id: id
            },
            success: function (res) {
                $('#product-' + id).remove();
                $('#cart-price').html(res.data.totalPrice.toLocaleString("vi-VN", { style: "currency", currency: "VND" }));
            }
        });
    });


    // Clear cart
    $('.clear-cart').on('click', function () {
        $.ajax({
            url: '/cart/clear',
            type: 'POST',
            success: function (res) {
                $('#cart-count').html(res.totalQuantity);
                $('#cart-price').html(res.totalPrice);
                $('.cart-item').remove();
            },
            error: function (err) {
                alert("Lỗi không thể thêm");
                console.log(err);
            }
        });
    });

    $('.txtQty').on('change', function () {
        var productId = $(this).data('product_id');
        var quantity = parseInt($(this).val());
        if (quantity > 0) {
            updateProduct(productId, quantity);
        } else {
            alert("Số lượng phải lớn hơn 0");
            $(this).val(1); // Reset to 1 if invalid quantity
            updateProduct(productId, 1);
        }
    });

    function decrementQuantity(productId) {
        var quantityInput = $('#quantity-' + productId);
        var quantity = parseInt(quantityInput.val());
        if (quantity > 1) {
            quantityInput.val(quantity - 1);
            quantity = quantity - 1;
        }
        updateProduct(parseInt(productId), quantity);
    }

    function incrementQuantity(productId) {
        var quantityInput = $('#quantity-' + productId);
        var quantity = parseInt(quantityInput.val());
        quantityInput.val(quantity + 1);
        quantity = quantity + 1;
        updateProduct(parseInt(productId), quantity);
    }

    function updateProduct(id, quantity) {
        $.ajax({
            url: '/Cart/UpdateCart',
            type: 'POST',
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                // Cập nhật thành tiền của sản phẩm tương ứng
                $('#item-totalPrice-' + id).html(res.data.itemPrice.toLocaleString("vi-VN", { style: "currency", currency: "VND" }));
                $('#cart-price').html(res.data.totalPrice.toLocaleString("vi-VN", { style: "currency", currency: "VND" }));
            },
            error: function (err) {
                alert("Lỗi không thể cập nhật sản phẩm");
                console.log(err);
            }
        });
    }
    // Gán các hàm vào đối tượng window để có thể truy cập từ HTML
    //P/s: Mở cửa số cho người khác nhìn vào :0
    window.decrementQuantity = decrementQuantity;
    window.incrementQuantity = incrementQuantity;
});

