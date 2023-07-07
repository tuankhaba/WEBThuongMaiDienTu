let diachicuthe = document.getElementById('diachicuthe');
let ChonQuan = document.getElementById('ChonQuan')
let ChonXa = document.getElementById('ChonXa');
let maTinh = "";
function CapNhatQuan(e) {
    let MaTinhNow = e.options[e.selectedIndex].value;
    let k = true;
    ChonQuan.selectedIndex = 0;
    ChonQuan.disabled = false;
    ChonXa.disabled = true;
    if (diachicuthe != null)
        diachicuthe.disabled = true;
    ChonXa.selectedIndex = 0;
    if (e.selectedIndex == 0) {
        ChonQuan.disabled = true;        
        k = false;
    }
    else {
        const showQuans = document.querySelectorAll(".Tinh" + MaTinhNow.toString());
        for (var i = 0; i < showQuans.length; i++) {
            showQuans[i].style.display = 'block';
        }
    }
    if (maTinh != "") {
        const QuanTruocs = document.querySelectorAll(".Tinh" + maTinh.toString());
        for (var i = 0; i < QuanTruocs.length; i++) {
            QuanTruocs[i].style.display = 'none';
        }
    }
    maTinh = (k) ? MaTinhNow : "";
    
}
let XaTruocs = null;
function CapNhatXa(e) {
    let MaQuanNow = e.options[e.selectedIndex].value;
    ChonXa.disabled = false;
    let k = true;
    ChonXa.selectedIndex = 0;
    if (diachicuthe != null)
        diachicuthe.disabled = true;
    let showXas = null;
    if (e.selectedIndex == 0) {
        diachicuthe.disabled = true;        
        ChonXa.disabled = true;
        for (var i = 0; i < XaTruocs.length; i++) {
            XaTruocs[i].style.display = 'none';
        }
        k = false;
    }
    else {
        showXas = document.querySelectorAll(".Quan" + MaQuanNow.toString());
        for (var i = 0; i < showXas.length; i++) {
            showXas[i].style.display = 'block';            
        }
    }
    if (XaTruocs != null) {
        for (var i = 0; i < XaTruocs.length; i++) {
            XaTruocs[i].style.display = 'none';
        }
    }
    console.log(XaTruocs)
    XaTruocs = (k) ? showXas : XaTruocs;
}
function HienDiaChiCuThe(e) {
    if (e.selectedIndex == 0) {
        diachicuthe.value = null;
        diachicuthe.disabled = true;
    }
    else
        diachicuthe.disabled = false;
}

let loidls = document.querySelectorAll('.loidl');
let HoVaTen = document.getElementById('HoVaTen');
let SoDienThoai = document.getElementById('SoDienThoai');
let KT = document.getElementById('KT');
let HuyLuuDiaChi = document.getElementById('HuyLuuDiaChi');
let ThemDiaChiMoi = document.getElementById('ThemDiaChiMoi');
HuyLuuDiaChi.addEventListener("click", function(){
    loidls.forEach(item => item.innerHTML = "");
})
KT.addEventListener("click", function () {
    KiemTraTT();
})
function KiemTraTT() {
    let check = true;
    if (HoVaTen.value == null || HoVaTen.value.trim().length == 0) {
        check = false;
        loidls[0].innerHTML = "Họ và tên không được để trống!";
    }
    else {
        loidls[0].innerHTML = "";
    }
    if (SoDienThoai.value == null || SoDienThoai.value.trim().length == 0) {
        check = false;
        loidls[1].innerHTML = "Số điện thoại không được để trống!";
    }
    else {
        if (SoDienThoai.value.trim().length < 10) {
            loidls[1].innerHTML = "Số điện thoại sai định dạng!";
            check = false;
        }

        else {
            loidls[1].innerHTML = "";
        }
    }
    if (ChonXa.disabled == true || ChonXa.selectedIndex == 0) {
        loidls[2].innerHTML = "Vui lòng chọn đầy đủ địa chỉ!!!"
        check = false;
    }
    else {
        loidls[2].innerHTML = "";
    }
    if (diachicuthe.value == null || diachicuthe.value.trim().length == 0) {
        loidls[3].innerHTML = "Vui lòng nhập địa chỉ cụ thể!!!"
        check = false;
    }
    else {
        loidls[3].innerHTML = "";
    }
    if (check) {
        ThemDiaChiMoi.click();
    }
}