let gb = document.getElementById('gb')
let giagoc = document.getElementById('giagoc')
let mucgiam = document.getElementById('mucgiam')
let truoc = document.getElementById('listkc0');
let sluong = document.getElementById('sluong');
let nhom_kichco = document.querySelectorAll('.nhom_kichco')
let conhang = document.getElementById('conhang')
let hethang = document.getElementById('hethang')
function DoiGia(t, i, gia) {
    giagoc.innerText = gia.toLocaleString('de-DE') + ' đ';
    gb.innerText = parseInt(gia - (gia / 100 * mucgiam.innerText)).toLocaleString('de-DE') + ' đ'
    truoc.style.display = 'none'
    truoc = document.getElementById("listkc" + i.toString());
    truoc.style.display = 'block'
    sluong.innerText = truoc.title;
    nhom_kichco.forEach(item => item.checked = false)
    if (truoc.title == 0) {
        hethang.style.display = 'block';
        conhang.style.display = 'none';
        sl.value = 0;
        gt = 0;
        sl.disabled = true;
    }
    else {
        conhang.style.display = 'block';
        hethang.style.display = 'none';
        sl.value = 1;
        gt = 1;
        sl.disabled = false;
    }
    loi_loaisp.style.display = 'none';
}
function DoiSoLuong(sll) {
    sluong.innerText = sll;
    if (sll == 0) {
        hethang.style.display = 'block';
        conhang.style.display = 'none';
        sl.value = 0;
        gt = 0;
        sl.disabled = true;
    }
    else {
        conhang.style.display = 'block';
        hethang.style.display = 'none';
        sluong.innerText = sll
        sl.value = 1;
        gt = 1;
        sl.disabled = false;
    }
    loi_kichcosp.style.display = 'none';
}
let loc_danhgia = document.querySelectorAll('.loc_danhgia')
let sao = document.querySelectorAll('.sao')
for (var i = 0; i < loc_danhgia.length; i++) {
    let saochk = sao[i]
    loc_danhgia[i].addEventListener("click", function () {
        document.querySelector('.sao.bi-star-fill').classList.add('bi-star');
        document.querySelector('.sao.bi-star-fill').classList.remove('bi-star-fill');
        console.log(saochk);
        saochk.classList.remove('bi-star');
        saochk.classList.add('bi-star-fill');
    })
}


let ThemVaoGioHang = document.getElementById('ThemVaoGioHang')
let MuaNgay = document.getElementById('MuaNgay')
let Click_thiet = document.getElementById('Click_thiet')
let btn_check_loai = document.querySelectorAll('.btn_check_loai')
let loi_loaisp = document.querySelector('#loi_loaisp')
let loi_sluongsp = document.querySelector('#loi_sluongsp')
let loi_kichcosp = document.querySelector('#loi_kichcosp')
hienthi_sl.value = 1;
ThemVaoGioHang.addEventListener('click', function () {
    if (KiemTraDuLieuHopLe()) {
        Click_thiet.value = 1;
        Click_thiet.click();
    }
})
MuaNgay.addEventListener('click', function () {
    if (KiemTraDuLieuHopLe()) {
        Click_thiet.value = 0;
        Click_thiet.click();
    }
})
function KiemTraDuLieuHopLe() {
    let check = true;
    if (KtLoaiCheck() == false)
        loi_loaisp.style.display = 'block';
    if (KtKichCoCheck() == false) {
        loi_kichcosp.style.display = 'block';
        check = false;
    }
    if (hienthi_sl.value == null || hienthi_sl.value == "") {
        loi_sluongsp.style.display = 'block';
        check = false;
    }
    else
        loi_sluongsp.style.display = 'none';
    return check;
}
function KtLoaiCheck() {
    for (var i = 0; i < btn_check_loai.length; i++) {
        if (btn_check_loai[i].checked == true)
            return true;
    }
    return false;
}

function KtKichCoCheck() {
    for (var i = 0; i < nhom_kichco.length; i++) {
        if (nhom_kichco[i].checked == true)
            return true;
    }
    return false;
}