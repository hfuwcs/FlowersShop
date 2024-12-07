// rating.js

// Để truy cập các sao
let stars = document.getElementsByClassName("star");
let output = document.getElementById("output");
let currentRating = 0; // Thêm biến này để lưu đánh giá hiện tại
let userLoggedIn = false; // Mặc định trạng thái đăng nhập

fetch("/Review/GetLoginStatus")
    .then(response => response.json())
    .then(data => {
        userLoggedIn = data.loggedIn;
        console.log("User logged in:", userLoggedIn); // true hoặc false
    })
    .catch(error => console.error("Error fetching login status:", error));

// Hàm để cập nhật đánh giá
function gfg(n) {
    currentRating = n; // Cập nhật giá trị đánh giá hiện tại
    remove();
    for (let i = 0; i < n; i++) {
        let cls = "";
        if (n == 1) cls = "one";
        else if (n == 2) cls = "two";
        else if (n == 3) cls = "three";
        else if (n == 4) cls = "four";
        else if (n == 5) cls = "five";
        stars[i].className = "star " + cls;
    }
    output.innerText = "Rating is: " + n + "/5";
}

// Xóa kiểu dáng được áp dụng trước đó
function remove() {
    for (let i = 0; i < stars.length; i++) {
        stars[i].className = "star";
    }
}

// Gửi đánh giá
// Gửi đánh giá
function submitReview() {
    if (!userLoggedIn) {
        alert("Bạn chưa đăng nhập!");
        window.location.href = "/User/DangNhap"; // Chuyển hướng đến trang đăng nhập
        return;
    }

    const comment = document.getElementById("comment").value;
    if (currentRating === 0) {
        document.getElementById("message").innerText = "Vui lòng chọn đánh giá!";
        return;
    }

    if (!comment.trim()) {
        document.getElementById("message").innerText = "Vui lòng nhập bình luận!";
        return;
    }

    // Gửi dữ liệu đánh giá qua API
    fetch("/Review/SubmitReview", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ rating: currentRating, comment })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert(data.message);
                document.getElementById("comment").value = "";
                gfg(0); // Reset đánh giá
            } else {
                alert(data.message || "Đã xảy ra lỗi!");
            }
        })
        .catch(error => {
            console.error("Error submitting review:", error);
            alert("Không thể gửi đánh giá.");
        });
}
