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
        var id = $(this).data('id');
        $.ajax({
            url: '/cart/remove',
            type: 'POST',
            data: {
                id: id
            },
            success: function (res) {
                $('#cart-count').html(res.totalQuantity);
                $('#cart-price').html(res.totalPrice);
                $('#cart-item-' + id).remove();
            }
        });
    });

    // Update cart
    //$('.update-cart').on('click', function () {
    //    var id = $(this).data('product_id');
    //    var quantity = $('#quantity').val();
    //    updateProduct(id, quantity);
    //})

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
                $('#cart-count').html(res.totalQuantity);
                $('#cart-price').html(res.totalPrice);
                $('#cart-item-price-' + id).html(res.itemPrice);
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

