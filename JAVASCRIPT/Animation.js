
    function createPetal() {
        const petal = document.createElement('div');
        petal.classList.add('petal');

        // Đặt vị trí ngẫu nhiên cho cánh hoa
        petal.style.left = Math.random() * window.innerWidth + 'px';
        petal.style.animationDuration = Math.random() * 3 + 2 + 's'; // Thời gian rơi ngẫu nhiên
        petal.style.opacity = Math.random(); // Độ trong suốt ngẫu nhiên
        petal.style.transform = `rotate(${Math.random() * 360}deg)`;

        document.body.appendChild(petal);

        // Xóa cánh hoa sau khi rơi hết
        setTimeout(() => {
            petal.remove();
        }, 5000); // Cánh hoa sẽ biến mất sau 5 giây
    }

    // Tạo cánh hoa rơi mỗi 300ms
    setInterval(createPetal, 300);