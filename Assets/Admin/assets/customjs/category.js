$(document).ready(function () {
    var categoryId;
    var categoryType;
    $("#addCategoryForm").on("submit", function (e) {
        e.preventDefault();
        const name = $("#categoryNameInput").val();
        const id = $("input[name='category_id']:checked").val();
        if (name && id) {
            addCategory(name, id);
        }
        else {
            console.log("Lỗi nhập liệu")
            $("#failAleart").toast("show");
        }
    });

    function addCategory(name, id) {
        $.ajax({
            url: "/Admin/Category/AddCategory",
            type: "POST",
            data: { categoryNameInput: name, category_id: id },
            success: function (response) {
                if (response) {
                    $("#successAleart").toast("show");
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                $("#failAleart").toast("show");
            }
        });
    }
    $('.btn-delete-category').on("click", function () {
        categoryId = $(this).data("category_id");
        categoryType = $(this).data("category_type");
        $("#DeleteModal").modal("show");
        console.log(categoryType)
        console.log(categoryId)
    });
    $("#delete-category-confirm").on("click", function () {
        if(categoryType && categoryId) {
            deleteCategory(categoryType, categoryId);
        }
    });

    function deleteCategory(categoryType, categoryId) {
        $.ajax({
            url: "/Admin/Category/DeleteCategory",
            type: "POST",
            data: { categoryType: categoryType, category_id: categoryId },
            success: function (response) {
                if (response.success) {
                    $("#successAleart").toast("show");
                    $("#DeleteModal").modal("hide");
                }
                else {
                    alert(response.message);
                    $("#failAleart").toast("show");
                    $("#DeleteModal").modal("hide");
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                $("#failAleart").toast("show");
                alert(message);
            }
        });
    }

    //$("#colorEditForm").on("submit", function (e) {
    //    e.preventDefault();
    //    const name = $("#categoryNameInput").val();
    //    const id = $("input[name='category_id']:checked").val();
    //    if (name && id) {
    //        addCategory(name, id);
    //    }
    //    else {
    //        console.log("Lỗi nhập liệu")
    //        $("#failAleart").toast("show");
    //    }
    //});

    //function addCategory(name, id) {
    //    $.ajax({
    //        url: "/Admin/Category/AddCategory",
    //        type: "POST",
    //        data: { categoryNameInput: name, category_id: id },
    //        success: function (response) {
    //            if (response) {
    //                $("#successAleart").toast("show");
    //            }
    //        },
    //        error: function (xhr, status, error) {
    //            console.log(error);
    //            $("#failAleart").toast("show");
    //        }
    //    });
    //}
});