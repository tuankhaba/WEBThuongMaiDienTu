function showPass(index) {
    let mk = document.querySelector(".pass" + index);
    let show = document.querySelector(".show" + index);
    let ushow = document.querySelector(".ushow" + index);
    if (mk.type === 'password') {
        mk.setAttribute('type', 'text');
        show.style.display = 'none';
        ushow.style.display = 'block';
    }
    else {
        mk.setAttribute('type', 'password');
        ushow.style.display = 'none';
        show.style.display = 'block';
        
    }

}
let pass2 = document.querySelector('.pass2')
let pass3 = document.querySelector('.pass3')
let mkmoinhaplai = document.querySelector('.mkmoinhaplai')
let loinhacmk = document.querySelector('.loinhacmk')
let ltd = document.querySelector('.ltd')

pass2.addEventListener("input", function () {
    if (pass2.value.trim().length < 8) {
        loinhacmk.style.color = "red";
    }
    else {
        loinhacmk.style.color = "#888888";
    }
})
pass3.addEventListener("input", function () {
    if (pass2.value != this.value) {
        mkmoinhaplai.innerText = "Mật khẩu nhập lại chưa chính xác hoặc chưa đủ điều kiện!";
        mkmoinhaplai.style.color = "red";
        ltd.disabled = true;
    }
    else
        if (this.value.trim().length >= 8)
    {
        mkmoinhaplai.innerText = "Mật khẩu trùng khớp!";
        mkmoinhaplai.style.color = "green";
        ltd.disabled = false;
    }
})


let check_sdt = document.querySelector('.check_sdt')
let check_email = document.querySelector('.check_email')
let DangKy = document.getElementById('DANGKY')
let KT = document.querySelector('.ltd')
let sdt = document.querySelector('.dienthoai')
let em = document.querySelector('.em')
let hoten = document.querySelector('.hoten')
let check_hoten = document.querySelector('.check_hoten')

let ttk = document.querySelector('.ttk')
let check_ttk = document.querySelector('.check_ttk')
KT.addEventListener("click", function () {
    let check = true;
    if (ttk.value.trim().indexOf(' ') >= 0) {
        check_ttk.innerText = "Tên tài khoản không được chứa khoảng trắng!!!";
        check = false;
    }
    else {
        check_ttk.innerText = "";
    }
    if (sdt.value == null || sdt.value.trim() == "") {
        check_sdt.innerText = "Số điện thoại không được bỏ trống!";
        check = false;
    }
    else {
        if (sdt.value.trim().length < 10) {
            check_sdt.innerText = "Số điện thoại không đúng định dạng";
            check = false;
        }
        else
            check_sdt.innerText = "";
    }
    if (em.value == null || em.value == "") {
        check = false;
        check_email.innerText = "Email không được bỏ trống";

    }
    else {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(em.value)) {
            check = false;
            check_email.innerText = "Email không đúng định dạng";
        }
        else
            check_email.innerText = " "
    }
    if (hoten.value == null || hoten.value.trim() == "") {
        check_hoten.innerText = "Họ và tên không được để trống!";
        check = false;
    }
    else {
        check_hoten.innerText = "";
    }
    if (check)
        DangKy.click();
})