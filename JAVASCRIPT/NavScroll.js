// JavaScript để thay đổi màu thanh điều hướng khi cuộn xuống
window.addEventListener('scroll', function() {
    const navbar = document.querySelector('.navbar');
    if (window.scrollY > 50) {  // Điều kiện cuộn (50px)
        navbar.classList.add('scrolled');
    } else {
        navbar.classList.remove('scrolled');
    }
});
