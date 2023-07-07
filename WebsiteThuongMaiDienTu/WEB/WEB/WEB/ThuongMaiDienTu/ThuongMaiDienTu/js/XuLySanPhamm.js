
//var fulltime = new Date(sessionStorage.nam, sessionStorage.thang, sessionStorage.ngay, sessionStorage.gio, sessionStorage.phut, sessionStorage.giay);
//setInterval(function () {
//    var now = new Date().getTime();
//    var conlai = fulltime - now;
//    var days = Math.floor(conlai / (1000 * 60 * 60 * 24));
//    var hours = Math.floor(conlai / (1000 * 60 * 60));
//    var mnt = Math.floor(conlai / (1000 * 60));
//    var sc = Math.floor(conlai / (1000));
//    hours %= 24;
//    mnt %= 60;
//    sc %= 60;

//    if (days <= 0 && hours <= 0 && mnt <= 0 && sc <= 0) {
//        document.getElementById("ngay").innerHTML = 0;
//        document.getElementById("gio").innerHTML = 0;
//        document.getElementById("phut").innerHTML = 0;
//        document.getElementById("giay").innerHTML = 0;
//    }
//    else {
//        document.getElementById("ngay").innerHTML = days;
//        document.getElementById("gio").innerHTML = hours;
//        document.getElementById("phut").innerHTML = mnt;
//        document.getElementById("giay").innerHTML = sc;
//    }

//})
let sl = document.getElementById("hienthi_sl");
let gt = sl.value;
const cong = document.getElementById("cong");
const tru = document.getElementById("tru");
cong.addEventListener("click", function () {
    if ((gt + 1) > parseInt(sluong.innerText))
        sl.value = sluong.innerText;
    else {
        gt++
        sl.value = gt
    }
})
tru.addEventListener("click", function () {
    if (gt > 1) {
        gt--
    }
    sl.value = gt
})
//Bửa có sửa ở đây
sl.addEventListener("input", function () {
    if (sl.value != "") {
        let a = parseInt(sl.value)
        sl.value = (isNaN(a) || a == 0) ? 1 : (a > parseInt(sluong.innerText)) ? sluong.innerText : a;
        gt = sl.value;
        loi_sluongsp.style.display = 'none';
    }
    else
        loi_sluongsp.style.display = 'block';
})


function checknumber(vt) {
    let loai_num = "gialoai";
    let kt = document.getElementById(loai_num + vt.toString());
    let gt = document.getElementById("TenLoai" + vt.toString()).value;
    if (kt.value != "") {
        let a = parseInt(kt.value)
        kt.value = (isNaN(a) || a == 0) ? 1 : a
        document.getElementById(vt).value = a;
    }
    else {
        document.getElementById(vt).value = "";
    }
    let ll = document.getElementById("Loiloai" + vt.toString());
    if (kt.value != "" && gt != "") {
        ll.style.display = 'none';    
    }
    else {
        ll.style.display = 'block';
    }
        
}

function checknumber_sl(vt, ten, index, e) {
    let loai_num = "loai_" + ten.toString() + "sl_kc";
    let kt = document.getElementById(loai_num + vt.toString());
    let vt_doi = list_ten_loai.indexOf(ten);
    let gt = document.getElementById("loai_" + ten.toString() + "ten_kc" + vt.toString());
    if (kt.value != "") {
        let a = parseInt(kt.value)
        kt.value = (isNaN(a) || a == 0) ? 1 : a
        list_kc_loai_sl[vt_doi][list_loai_kc_vt[vt_doi][index]] = e.value;
    }
    else 
        list_kc_loai_sl[vt_doi][list_loai_kc_vt[vt_doi][index]] = "";
    let ll = document.getElementById("loiloai_" + ten.toString() + "sl_kc" + vt.toString());
    if (e.value != "" && gt.value != "") 
        ll.style.display = 'none';
    else 
        ll.style.display = 'block';
}
let sl3 = document.getElementById("hienthi_giamgia");
/*let gg = document.querySelector(".item_gg");*/
let lich = document.querySelector(".lich");
let text_gg = document.querySelector(".text_gg");
function check_gg(t) {
    if (t.value != "") {
        let a = parseInt(t.value)
        let value_gg = 0;
        if (!isNaN(a) && a != 0) {
            value_gg = (a > 101) ? 100 : a
            lich.style.display = 'block';
            text_gg.classList.add("col-6");
        }
        t.value = value_gg;
        /*gg.innerHTML = "-" + value_gg + " %";*/
    }
    else {
        /*gg.innerHTML = "0 %";*/
        loi_gg.style.display = 'none';
        timegg.value = null;
        lich.style.display = 'none';
        text_gg.classList.remove("col-6");
    }
}
sl3.addEventListener("input", function () {
    check_gg(this);
})

function getvalue(vt) {
    let gt = document.getElementById('TenLoai' + vt.toString()).value.toString();
    gt.innerText = gt.value;
}


let list_loai = document.querySelector(".list_loai")
let tb = document.getElementById('liveToastBtn')
let nd_tb = document.getElementById('ThongBao')
let option = document.querySelector('.option')
let list_loai_kich_co = document.getElementById('list_loai_kich_co')
let sl_loai = 1
let sl_box_loai = 1;
let list_sl_kich_co = [1];
let list_ten_loai = [1];
let list_box_kc = [1];
let list_kc_ten = [new Array("Kích cỡ 1")]
let list_loai_kc_vt = [[0]]
let list_kc_loai_sl = [[1]]
let list_kc_loai_ten_box = [[1]]
let show_loai = 1;


function ThemLoai() {
    //let gt = document.getElementById("kichcoloai1").querySelector("p").innerText;
    if (sl_loai + 1 <= 15) {
        sl_loai++;
        sl_box_loai++;
        let str = '<div class="loai" id="loai' + sl_box_loai.toString() + '">'
        str += '<i class="bi bi-x-circle-fill bi_xoa_loai" title="Nhấn vào đây để xóa" onclick="XoaLoai(' + sl_box_loai + ')"></i>'
        str += '<input class="w-100 btn btn_loai" id="TenLoai' + sl_box_loai.toString() + '" value="Loại ' + sl_box_loai.toString() + '" oninput="DoiTenLoai(' + sl_box_loai.toString() + ')" placeholder="Nhập loại" type="text" title="Nhập tên loại" maxlength="32" />'
        str += '<input title="Nhập giá bán" value="1" id="gialoai' + sl_box_loai.toString() + '" oninput="checknumber(' + sl_box_loai + ')" class=" them_nhap btn hienthi_sl2" type="text" placeholder="Nhập giá bán" min="1" max="1000000000" />'
        str += '<div class="loi_loai">'
        str += '<span class="demloi" style="display:none" id = "Loiloai' + sl_box_loai.toString()+'" > Không để trống!</span>'
        str += '</div ></div >'
        list_loai.innerHTML += str;
        option.innerHTML += '<option class="loai_giatri" id="' + sl_box_loai.toString() + '" value="1">Loại ' + sl_box_loai.toString() + '</option>';
        //Thêm  bảng chọn kích cở từng loại
        let str_kc = '<div class="w-100" id="kichcoloai' + sl_box_loai.toString() + '" style="display:none;">'
        str_kc += '<a class="btn btn-outline-info" style="width: 100%; color: black; font-size: 20px;" onclick="ThemKichCo(' + sl_box_loai + ')">'
        str_kc += 'Thêm kích cỡ cho loại "<span id="nutthem_loai' + sl_box_loai.toString() + '">' + sl_box_loai.toString() + '</span>" </a>'
        str_kc += '<div class=" row w-100" style="margin-left:2px;">'
        str_kc += '<center class="list_kich_co_loai' + sl_box_loai.toString() + '">'
        str_kc += '<div class="loai" id="loai' + sl_box_loai.toString() + '_kichco1">'
        str_kc += '<i class="bi bi-x-circle-fill bi_xoa_loai" title="Nhấn vào đây để xóa" onclick="XoaKichCo(' + sl_box_loai + ',1,0)"></i>'
        str_kc += '<input class=" w-100 btn btn_kichco" value="Kích cỡ 1" id="loai_' + sl_box_loai.toString() + 'ten_kc1" placeholder="Nhập kích cở" type="text" oninput="DoiTenKichCo(' + sl_box_loai + ', 0, this)" title="Nhập tên kích cỡ" maxlength="32" />'
        str_kc += '<input title="Nhập số lượng" value="1" id="loai_' + sl_box_loai.toString() + 'sl_kc1" oninput="checknumber_sl(1,' + sl_box_loai + ',0,this)" class="them_nhap btn hienthi_sl2" type="text" placeholder="Nhập số lượng" min="1" max="1000000000" />'
        str_kc += '<div class="loi_loai">'
        str_kc += '<span class="demloi" style="display:none" id="loiloai_' + sl_box_loai.toString() + 'sl_kc1"> Không để trống!</span >'
        str_kc += '</div >'
        str_kc += '</div>'
        str_kc += '</center>'
        str_kc += '</div>'
        str_kc += '</div>'
        str_kc += '</div >'
        list_loai_kich_co.innerHTML += str_kc;
        //Tương ứng vs 1 loại mưới dc tạo ra thì sẽ thêm vào 1 kích cở cho loại đó
        list_sl_kich_co.push(1);
        list_ten_loai.push(sl_box_loai);
        list_box_kc.push(1);

        list_kc_ten.push(new Array("Kích cỡ 1"));
        list_kc_loai_sl.push([1]);
        list_loai_kc_vt.push([0]);
        list_kc_loai_ten_box.push([1]);
        CapNhatLoai();
        let vt_moi = option.options[0].id;
        document.getElementById("kichcoloai" + show_loai).style.display = 'none';
        document.getElementById("kichcoloai" + vt_moi).style.display = 'block';
        show_loai = vt_moi;
        CapNhatKichCo(parseInt(vt_moi));
    }
    else {
        nd_tb.innerHTML = "Số lượng loại vượt quá quy định( tối đa 15 loại)!!!"
        tb.click()
    }
}
function CapNhatLoai() {
    for (var i = 0; i < sl_loai; i++) {
        let gt = option.options[i].id;
        document.getElementById("gialoai" + gt.toString()).value = option.options[i].value;
        document.getElementById("TenLoai" + gt.toString()).value = option.options[i].innerText;
    }
}
function ChuyenTrangKichCo() {
    let gt = option.options[option.selectedIndex].id;
    document.getElementById("kichcoloai" + (show_loai).toString()).style.display = 'none';
    document.getElementById("kichcoloai" + gt).style.display = 'block';
    show_loai = gt;
    /*CapNhatKichCo(gt);*/
}
function DoiTenLoai(vt) {
    let gt = document.getElementById("TenLoai" + vt.toString());
    if (gt.value.trim().length<=50)
        document.getElementById(vt).innerText = gt.value;
    else
        gt.value = document.getElementById(vt).innerText;
    let loai_num = "gialoai";
    let kt = document.getElementById(loai_num + vt.toString());
    document.getElementById("nutthem_loai" + vt.toString()).innerText = gt.value;
    let ll = document.getElementById("Loiloai" + vt.toString());
    if (kt.value.trim() != "" && gt.value.trim() != "") {
        ll.style.display = 'none';
    }
    else {
        ll.style.display = 'block';
    }
}
function XoaLoai(vt) {
    if (sl_loai - 1 > 0) {
        sl_loai = sl_loai - 1;
        let idx = option.selectedIndex
        let gt = option.options[idx].id;
        if (vt == gt) {
            if (idx == 0)
                idx++;
            else
                idx = 0;
            let vt_moi = option.options[idx].id;
            document.getElementById("kichcoloai" + vt_moi).style.display = 'block';
            show_loai = vt_moi;
        }
        document.getElementById("loai" + vt.toString()).remove();
        document.getElementById(vt).remove();
        document.getElementById("kichcoloai" + vt.toString()).remove();
        list_ten_loai.splice(list_ten_loai.indexOf(vt), 1);
        list_sl_kich_co.splice(list_ten_loai.indexOf(vt), 1);
    }
    else {
        nd_tb.innerHTML = "Phải có ít nhất 1 loại trong sản phẩm của bạn!!!"
        tb.click()
    }
}

function ThemKichCo(vt) {
    //Lấy list kích cở nào
    let list_kich_co = document.querySelector(".list_kich_co_loai" + vt.toString());
    //lấy vị trí indẽ của cái tên loại đc lưu trong mảng
    let vt_ten = list_ten_loai.indexOf(vt);
    if (list_sl_kich_co[vt_ten] + 1 <= 15) {
        list_sl_kich_co[vt_ten]++;
        list_box_kc[vt_ten]++;
        let str = '<div class="loai" id="loai' + vt.toString() + '_kichco' + list_box_kc[vt_ten].toString() + '">'
        str += '<i class="bi bi-x-circle-fill bi_xoa_loai" title="Nhấn vào đây để xóa" onclick = "XoaKichCo(' + vt + ',' + list_box_kc[vt_ten] + ',' + (list_box_kc[vt_ten] - 1) + ')" ></i>'
        str += '<input class=" w-100 btn btn_kichco" id="loai_' + vt.toString() + 'ten_kc' + list_box_kc[vt_ten].toString() + '" oninput="DoiTenKichCo(' + vt + ',' + (list_box_kc[vt_ten] - 1) + ',this)" value="Kích cỡ ' + list_box_kc[vt_ten].toString() + '" placeholder="Nhập kích cở" type="text" title="Nhập tên kích cỡ" maxlength="32" />'
        str += '<input title="Nhập số lượng" value="1" id="loai_' + vt.toString() + 'sl_kc' + list_box_kc[vt_ten].toString() + '" oninput="checknumber_sl(' + list_box_kc[vt_ten] + ',' + vt + ',' + (list_box_kc[vt_ten] - 1) + ',this)" class="them_nhap btn hienthi_sl2" type="text" placeholder="Nhập số lượng" min="1" max="1000000000" />'
        str += '<div class="loi_loai">'
        str += '<span class="demloi" style="display:none" id = "loiloai_' + vt.toString() + 'sl_kc' + list_box_kc[vt_ten].toString() + '" > Không để trống!</span >'
        str += '</div >'
        str += '</div >'
        list_kich_co.innerHTML += str;
        list_kc_ten[vt_ten].push("Kích cỡ " + list_box_kc[vt_ten].toString());
        list_kc_loai_sl[vt_ten].push(1);
        list_kc_loai_ten_box[vt_ten].push(list_box_kc[vt_ten]);
        list_loai_kc_vt[vt_ten].push(list_sl_kich_co[vt_ten] - 1);
        CapNhatKichCo(vt);

    }
    else {
        nd_tb.innerHTML = "Số lượng kích cỡ vượt quá quy định( tối đa 15 kích cỡ)!!!"
        tb.click()
    }
}
function DoiTenKichCo(vt, index, e) {
    let vt_doi = list_ten_loai.indexOf(vt);
    let loai_num = "loai_" + vt.toString() + "sl_kc";
    let kt = document.getElementById(loai_num + (index + 1).toString());
    if (e.value.trim().length <= 50)
        list_kc_ten[vt_doi][list_loai_kc_vt[vt_doi][index]] = e.value;
    else
        e.value = list_kc_ten[vt_doi][list_loai_kc_vt[vt_doi][index]];  /*document.getElementById("loai_" + vt.toString() + "ten_kc" + list_box_kc[vt_ten].toString()).value;*/
    let ll = document.getElementById("loiloai_" + vt.toString() + "sl_kc" + (index + 1).toString());
    if (e.value.trim() != "" && kt.value.trim() != "")
        ll.style.display = 'none';
    else 
        ll.style.display = 'block';
}
function CapNhatKichCo(vt) {
    let vt_ten = list_ten_loai.indexOf(vt);
    for (var i = 0; i < list_sl_kich_co[vt_ten] - 1; i++) {
        document.getElementById("loai_" + vt.toString() + "ten_kc" + list_kc_loai_ten_box[vt_ten][i].toString()).value = list_kc_ten[vt_ten][i].toString();
        document.getElementById("loai_" + vt.toString() + "sl_kc" + list_kc_loai_ten_box[vt_ten][i].toString()).value = list_kc_loai_sl[vt_ten][i].toString();
    }
}
function XoaKichCo(ten_loai, vt, idx) {
    let vt_ten = list_ten_loai.indexOf(ten_loai);
    if (list_sl_kich_co[vt_ten] - 1 > 0) {
        console.log(list_kc_ten[vt_ten]);
        list_sl_kich_co[vt_ten]--;
        let vt_kc = list_kc_loai_ten_box[vt_ten].indexOf(vt);
        list_kc_loai_sl[vt_ten].splice(vt_kc, 1);
        for (var i = idx; i < list_box_kc[vt_ten]; i++) {
            list_loai_kc_vt[vt_ten][i]--;
        }
        list_kc_ten[vt_ten].splice(vt_kc, 1);
        list_kc_loai_ten_box[vt_ten].splice(vt_kc, 1);
        document.getElementById("loai" + ten_loai.toString() + "_kichco" + vt.toString()).remove();
    }
    else {
        nd_tb.innerHTML = "Phải có ít nhất 1 kích cỡ cho từng loại!!!"
        tb.click()
    }
}
const toastTrigger = document.getElementById('liveToastBtn')
const toastLiveExample = document.getElementById('liveToast')
if (toastTrigger) {
    toastTrigger.addEventListener('click', () => {
        const toast = new bootstrap.Toast(toastLiveExample)

        toast.show()
    })
}
const themthuoctinh = document.getElementById('themthuoctinh')
const thongtinchitiet = document.querySelector('.thongtinchitiet')
let sl_thuoctinh = 1;
let sl_box_tt = 1;
let thuocTinh = ["Thuộc tính 1"]
let noiDung = ["Nộidung 1"]
let box_vt_ten_tt = [1];
themthuoctinh.addEventListener("click", function () {
    if (sl_thuoctinh + 1 <= 15) {
        sl_thuoctinh++;
        sl_box_tt++;
        let str = '<div class="row nd_ct" id="nd' + sl_box_tt.toString() + '">'
        str += '<div class=" bi-x-square " onclick="XoaThuocTinh(' + sl_box_tt + ')" >'
        str += '<i class="bi bi-x-square-fill"></i>'
        str += '</div >'
        str += '<div class=" col-5" >'
        str += '<input id="thuoctinh' + sl_box_tt.toString() + '" value="Thuộc tính ' + sl_box_tt.toString() + '" oninput="DoiTenThuocTinh(' + sl_box_tt + ',this)" title="Nhập thuộc tính" placeholder="Nhập thuộc tính" class=" btn thuoctinh w-100" type="text" />'
        str += '</div >'
        str += '<div class="col-7">'
        str += '<input id="noidung' + sl_box_tt.toString() + '" value="Nội dung ' + sl_box_tt.toString() + '" oninput="DoiNoiDung(' + sl_box_tt + ',this)" title="Nhập nội dung cho thuộc tính" placeholder="Nhập nội dung cho thuộc tính" class=" btn noidung w-100" type="text" />'
        str += '</div>'
        str += '<div class="loianh">'
        str += '<span id = "loi_tt' + sl_box_tt.toString() +'" style="display:none" class="loi_tt demloi" > Không được để trống nội dung và thuộc tính </span >'
        str += '</div >'
        str += '</div >'
        thongtinchitiet.innerHTML += str;
        thuocTinh.push("Thuộc tính" + sl_box_tt.toString());
        noiDung.push("Nội dung" + sl_box_tt.toString());
        box_vt_ten_tt.push(sl_box_tt);
        CapNhatThongTin();
    }
    else {
        nd_tb.innerHTML = "Số lượng thuộc tính vượt quá quy định( tối đa 15 thuộc tính)!!!"
        tb.click()
    }
})
function DoiTenThuocTinh(ten, e) {
    let index = box_vt_ten_tt.indexOf(ten);
    let nd = document.getElementById("noidung" + ten.toString());
    if (e.value.trim().length <= 50)
        thuocTinh[index] = e.value;
    else
        e.value = thuocTinh[index];
    if (nd.value.trim()!=""&& e.value.trim() != "")
        document.getElementById("loi_tt" + ten.toString()).style.display = 'none';
    else
        document.getElementById("loi_tt" + ten.toString()).style.display = 'block';

}
function DoiNoiDung(ten, e) {
    let index = box_vt_ten_tt.indexOf(ten);
    let tt = document.getElementById("thuoctinh" + ten.toString());
    if (e.value.trim().length <= 100)
        noiDung[index] = e.value;
    else
        e.value = noiDung[index];
    let ll = document.getElementById("loi_tt" + ten.toString());
    if (tt.value.trim() != "" && e.value.trim() != "")
        ll.style.display = 'none';
    else
        ll.style.display = 'block';
}
function CapNhatThongTin() {
    for (var i = 0; i < sl_thuoctinh; i++) {
        document.getElementById("thuoctinh" + box_vt_ten_tt[i].toString()).value = thuocTinh[i].toString();
        document.getElementById("noidung" + box_vt_ten_tt[i].toString()).value = noiDung[i];
    }
}
function XoaThuocTinh(ten) {
    if (sl_thuoctinh - 1 > 0) {
        let index = box_vt_ten_tt.indexOf(ten);
        sl_thuoctinh--;
        document.getElementById("nd" + ten.toString()).remove();
        noiDung.splice(index, 1);
        thuocTinh.splice(index, 1);
        box_vt_ten_tt.splice(index, 1);
    }
    else {
        nd_tb.innerText = "Phải có ít nhất một thuộc tính cho sản phẩm!!!"
        tb.click()
    }
}
let ChiTiet = document.getElementById('ChiTiet');
let NganhHang = document.getElementById('NganhHang');
let loi_nganh = document.querySelector('.loi_nganh');
let loi_ten = document.getElementById('loi_ten');
let tensp = document.getElementById('tensp');
let loi_gg = document.getElementById('loi_gg');
let timegg = document.getElementById('timegg');
let hienthi_giamgia = document.getElementById('hienthi_giamgia');
let LuuSanPham = document.getElementById('LuuSanPham');
let loaitruoc = "";
function CapNhatChiTietNganhHang(e) {
    let loainow = e.options[e.selectedIndex].value;
    ChiTiet.disabled = false;
    let k = true;
    ChiTiet.selectedIndex = 0;
    if (e.selectedIndex == 0) {
        ChiTiet.disabled = true;
        loi_nganh.style.display = 'block';
        const loaitruocs = document.querySelectorAll("." + loaitruoc.toString());
        for (var i = 0; i < loaitruocs.length; i++) {
            loaitruocs[i].style.display = 'none';
        }
        k = false;
    }
    else {
        const showLoais = document.querySelectorAll("." + loainow.toString());
        for (var i = 0; i < showLoais.length; i++) {
            showLoais[i].style.display = 'block';
        }
    }
    if (loaitruoc != "") {
        const loaitruocs = document.querySelectorAll("." + loaitruoc.toString());
        for (var i = 0; i < loaitruocs.length; i++) {
            loaitruocs[i].style.display = 'none';
        }
    }
    loaitruoc = (k) ? loainow : loaitruoc;
}
let DataSanPham = document.getElementById('DataSanPham')
function KTTen() {
    if (tensp.value == null || tensp.value.trim() == "") {
        loi_ten.innerText = "Tên sản phẩm không được để trống";
        loi_ten.style.display = 'block';
    }
    else {
        if (tensp.value.trim().length < 8) {
            loi_ten.innerText = "Tên sản phẩm phải có ít nhất 8 kí tự";
            loi_ten.style.display = 'block';
        }
        else {
            if (tensp.value.trim().length > 300) {
                loi_ten.innerText = "Tên sản phẩm chỉ tối đa 300 kí tự!!!";
                loi_ten.style.display = 'block';
            }
            else
                loi_ten.style.display = 'none';
        }
            
    }
}
function ChuyenDinhDangTG(a) {
    return a[3].toString() + a[4].toString() + "/" + a[0].toString() + a[1].toString() + "/" + a[6].toString() + a[7].toString() + a[8].toString() + a[9].toString();
}
function KTDuLieu() {
    let sls = document.querySelectorAll('.demloi')
    if (ChiTiet.selectedIndex != 0 && NganhHang.selectedIndex != 0)
        loi_nganh.style.display = 'none';
    else
        loi_nganh.style.display = 'block';
    if (sl_anh == 0)
        loi_anh.style.display = 'block';
    KTTen();
    if (hienthi_giamgia.value == null || hienthi_giamgia.value > 0) {
        if (timegg.value == null || timegg.value == "") {
            loi_gg.innerText = "Vui lòng chọn ngày hết hạn";
            loi_gg.style.display = 'block';
        }
        else {
            let today = new Date();
            let ngay = today.getDate();
            let thang = today.getMonth() + 1;
            let nam = today.getFullYear();
            let t = (thang > 9 ? thang.toString() : "0" + thang.toString()) + "/" + (ngay > 9 ? ngay.toString() : "0" + ngay.toString()) + "/" + nam.toString();
            if (new Date(ChuyenDinhDangTG(timegg.value)) <= new Date(t)) {
                loi_gg.innerText = "Ngày hết hạn phải lớn hơn ngày hệ thống";
                loi_gg.style.display = 'block';
            }
            else {
                loi_gg.style.display = 'none';
            }
        }
    }
    let mota = document.querySelector('.mota');
    let loi_mota = document.getElementById('loi_mota');
    if (mota.value == null || mota.value.trim() == "") {
        loi_mota.innerText = "Mô tả sản phẩm không được để trống";
        loi_mota.style.display = 'block';
    }
    else {
        if (mota.value.trim().length < 1000) {
            loi_mota.innerText = "Nhập tối thiểu 1000 ký tự";
            loi_mota.style.display = 'block';
        }
        else
            loi_mota.style.display = 'none';
    }
    for (var i = 0; i < sls.length; i++)
        if (sls[i].style.display !== 'none') {
            nd_tb.innerHTML = "Bạn vẫn chưa nhập đủ dữ liệu, vui lòng kiểm tra lại!!!"
            tb.click();
            return;
        }
    let str = "";
    let anhsps = document.querySelectorAll('.anhsp');
    str += '<input  name="SoLuongAnh" hidden value="' + sl_anh + '" />';
    str += '<input  name="SoLuongLoai" hidden value="' + sl_loai + '" />';
    for (var i = 0; i < sl_anh; i++) {
        str += '<input  name="Anh' + (i + 1).toString() + '" hidden  value="' + anhsps[i].src + '" />';
    }
    for (var i = 0; i < sl_loai; i++) {
        str += '<input  name="TenLoai' + (i + 1).toString() + '" hidden  value="' + option[i].innerText + '" />';
        str += '<input  name="GiaLoai' + (i + 1).toString() + '" hidden value="' + option[i].value + '" />';
        str += '<input  name="SoLuongKichCoLoai' + (i + 1).toString() + '" hidden value="' + list_sl_kich_co[i] + '" />';
        for (var j = 0; j < list_sl_kich_co[i]; j++) {
            str += '<input hidden name="Loai' + (i + 1).toString() + 'TenKichCo' + (j + 1).toString() + '"  value="' + list_kc_ten[i][j] + '" />';
            str += '<input hidden name="Loai' + (i + 1).toString() + 'SLKichCo' + (j + 1).toString() + '"  value="' + list_kc_loai_sl[i][j] + '" />';
        }
    }
    str += '<input hidden name="SlThuocTinh"  value="' + sl_thuoctinh + '" />';
    for (var i = 0; i < sl_thuoctinh; i++) {
        str += '<input hidden name="ThuocTinh' + (i + 1).toString() + '"  value="' + thuocTinh[i] + '" />';
        str += '<input hidden name="NoiDung' + (i + 1).toString() + '"  value="' + noiDung[i] + '" />';
    }
    DataSanPham.innerHTML += str;
    LuuSanPham.click();
}
function KTCT(e) {
    if (e.selectedIndex != 0)
        loi_nganh.style.display = 'none';
    else
        loi_nganh.style.display = 'block';
}
