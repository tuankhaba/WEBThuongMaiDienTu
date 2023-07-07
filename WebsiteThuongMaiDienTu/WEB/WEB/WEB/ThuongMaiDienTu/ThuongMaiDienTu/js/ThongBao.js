let thongbao = document.getElementById('thongbao')
let boxshow = document.getElementById('boxshow')
let btn_xoa = document.getElementById('btn_xoa')
function TaoThongBao(ten, ma) {
    ma = "'" + ma + "'";
    var str = '<div class="modal fade" id="show" tabindex="-1"  aria-labelledby="exampleModalLabel" aria-hidden="true">'
    str += '<div class="modal-dialog">'
    str += '<div class="modal-content">'
    str += '<div class="modal-header">'
    str += '<h1 class="modal-title fs-5" id="exampleModalLabel">Bạn đang muốn thực hiện một hành động xóa</h1>'
    str += '<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>'
    str += '</div>'
    str += '<div class="modal-body">'
    str += 'Bạn có chắn chắn muốn xóa Voucher "' + ten + '"???'
    str += '</div>'
    str += '<div class="modal-footer">'
    str += '<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Đóng</button>'
    str += '<a href="#" onclick="XoaKM(' + ma + ')" class="btn btn-outline-danger">Đồng ý</a>';
    str += '</div>'
    str += '</div>'
    str += '</div>'
    str += '</div>'
    boxshow.innerHTML = str;
}
function ThongBao(ten,ma) {
    TaoThongBao(ten, ma);
    thongbao.click();
}
function XoaKM(ma) {
    btn_xoa.value = ma;
    btn_xoa.click();
}