$(document).ready(function () {
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