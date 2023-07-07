let loi_gg = document.getElementById('loi_gg');
function check_gg(t) {
    if (t.value != "") {
        let a = parseInt(t.value)
        let value_gg = 1;
        if (!isNaN(a) && a != 0) {
            value_gg = (a > 101) ? 100 : a
            loi_gg.style.display = 'none';
        }
        t.value = value_gg;
    }
    else {
        loi_gg.style.display = 'block';
    }
}
function check_number(t,idname) {
    if (t.value != "") {
        let a = parseInt(t.value)
        let value_gg = 1;
        if (!isNaN(a) && a != 0) {
            value_gg = a;
            document.getElementById(idname).style.display = 'none';
        }
        t.value = value_gg;
    }
    else {
        document.getElementById(idname).style.display = 'block';
    }
}
function check_trong(t, idname1, idname2,sl) {
    let nd = document.getElementById(idname1);
    let nd2 = document.getElementById(idname2);

    if (t.value.trim() == null || t.value.trim() == "") {
        nd2.style.display = 'none';
        nd.style.display = 'block';
    }
    else {
        if (t.value.trim().length < sl) {
            nd2.style.display = 'block';
            nd.style.display = 'none';
        }
        else {
            nd.style.display = 'none';
            nd2.style.display = 'none';
        }

    }

}
function check_khoangtrang(t, idname1) {
    let nd = document.getElementById(idname1);
    if (t.value.trim() == null || t.value.trim() == "") {
        nd.innerText = 'Mã giảm giá không được để trống';
        nd.style.display = 'block';
    }
    else {
        if (t.value.trim().length < 4) {
            nd.innerText = 'Mã giảm giá phải có tối thiểu 4 chữ cái';
            nd.style.display = 'block';
        }
        else {
            if (t.value.trim().indexOf(' ') >= 0) {
                nd.innerText = "Mã giảm giá không chứa ký tự khoảng trắng";
                nd.style.display = 'block';
            }
            else
                nd.style.display = 'none';
        }

    }

}

let NgayKetThuc = document.getElementById('NgayKetThuc')
let NgayBatDau = document.getElementById('NgayBatDau')
let loi_ngay = document.getElementById('loi_ngay')
NgayKetThuc.addEventListener('input', function() {
    KTThoiHanHopLe(NgayBatDau, NgayKetThuc, loi_ngay);
})
NgayBatDau.addEventListener('input', function () {
    KTThoiHanHopLe(NgayBatDau, NgayKetThuc, loi_ngay);
})
function ChuyenDinhDangTG(a) {
    return a[0].toString() + a[1].toString() + "/" + a[3].toString() + a[4].toString() + "/" + a[6].toString() + a[7].toString() + a[8].toString() + a[9].toString();
}
function KTThoiHanHopLe(bd, kt, loi) {
    let today = new Date();
    let ngay = today.getDate();
    let thang = today.getMonth() + 1;
    let nam = today.getFullYear();
    let t = ((ngay > 9 ? ngay.toString() : "0" + ngay.toString()) + "/" + thang > 9 ? thang.toString() : "0" + thang.toString()) + "/" + nam.toString();   
    let ktt = ChuyenDinhDangTG(kt.value);
    let check = 0;
    if (bd.value.trim() != null && bd.value != "") {
        check = 1;
        let bdd = ChuyenDinhDangTG(bd.value);
        if (new Date(ktt) <= new Date(bdd)) {
            loi.innerText = "Ngày bắt đầu không được lớn hơn ngày kết thúc";
            loi.style.display = 'block';
        }
        else
            loi.style.display = 'none';
    }
    if (new Date(ktt) <= new Date(t)) {
        loi.innerText = "Ngày kết thúc phải lớn hơn ngày hiện tại";
        loi.style.display = 'block';
    }
    else
        if (check == 0)
            loi.style.display = 'none';
}
let KTDuLieu = document.getElementById('KTDuLieu');
let tenkm = document.getElementById('tenkm');
let makm = document.getElementById('makm');
let tylegg = document.getElementById('tylegg');
let donhangTT = document.getElementById('donhangTT');
let muctoida = document.getElementById('muctoida');
let luot = document.getElementById('luot');
let loinhaps = document.querySelectorAll('.loinhap');
let liveToastBtn = document.getElementById('liveToastBtn');
let ThongBao = document.getElementById('ThongBao');
let XacNhan = document.getElementById('XacNhan');

KTDuLieu.addEventListener('click', function () {
    check_trong(tenkm, 'loi_trongten', 'loi_slkt_ten', 8)
    check_khoangtrang(makm, 'loi_trongma');
    if (NgayKetThuc.value == null || NgayKetThuc.value.trim() == "") {
        loi_ngay.innerText = "Ngày kết thúc không được để trống";
        loi_ngay.style.display = 'block';
    }
    check_gg(tylegg)
    check_number(donhangTT, 'loi_gt')
    check_number(luot, 'loi_luotdung')
    check_number(muctoida, 'loi_hanmuc')
    for (var i = 0; i < loinhaps.length; i++) {
        if (loinhaps[i].style.display !== 'none') {
            ThongBao.innerText = "Bạn vẫn chưa nhập đủ dữ liệu, vui lòng kiểm tra lại!!!"
            liveToastBtn.click();
            console.log(loinhaps[i]);
            return;
        }
    }
    XacNhan.click();
})

const toastTrigger = document.getElementById('liveToastBtn')
const toastLiveExample = document.getElementById('liveToast')
if (toastTrigger) {
    toastTrigger.addEventListener('click', () => {
        const toast = new bootstrap.Toast(toastLiveExample)
        toast.show()
    })
}