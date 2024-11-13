//SEARCH PRODUCT
$(document).ready(function () {
    // Xử lý sự kiện bấm nút tìm kiếm
    $("#btn-search").on("click", function () {
        var productName = $("#search-input").val();
        $.ajax({
            url: '/Product/SearchProduct', // URL này sẽ trỏ đến ActionResult
            type: 'POST',
            data: { ProductName: productName },
            success: function (data) {
                var tableBody = $("#product-table-body");
                tableBody.empty();
                $.each(data, function (index, item) {
                    var row = "<tr>" +
                        "<td>" + item.Name + "</td>" +
                        "<td>" + item.Price + "</td>" +
                        "<td>" + item.Description + "</td>" +
                        "<td>" + (item.Image ? "<img class='image' src='" + item.Image + "' width='200' />" : "No image available") + "</td>" +
                        "<td>" + item.Quantity + "</td>" +
                        "</tr>";
                    tableBody.append(row);
                });
            },
            error: function (xhr, status, error) {
                console.error("Error: " + error);
            }
        });
    });

    // Sử dụng event delegation cho checkbox vì form có thể được render động
    $(document).on('change', '#filterForm input[type="checkbox"]', function () {
        var selectedCategories = $('#filterForm').serialize(); // Lấy tất cả các giá trị checkbox đã chọn
        $.ajax({
            url: '/Product/SearchProductByCategory', // URL này trỏ đến Action SearchProductByCategory
            type: 'GET',
            data: selectedCategories,
            success: function (result) {
                $('#productResults').html(result); // Inject kết quả vào div kết quả
            },
            error: function () {
                alert("An error occurred while fetching products.");
            }
        });
    });
});
