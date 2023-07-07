const right = document.querySelector('.right')
const left = document.querySelector('.left')
const img = document.querySelectorAll('.sider-product-one-content-items')
let index = 1;
right.addEventListener("click", function () {
    index = index + 1;
    if (index > img.length - 1) {
        index = 0;
    }
    document.querySelector(".sider-product-one-content-items-content").style.right = index * 100 + "%"
}
)
left.addEventListener("click", function () {
    index = index - 1;
    if (index < 0) {
        index = img.length - 1;
    }
    document.querySelector(".sider-product-one-content-items-content").style.right = index * 100 + "%"
}
)