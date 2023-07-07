const dropArea = document.querySelector('.drag-area');
const dropText = document.querySelector('.keotha');
const button = document.querySelector('.chonfile');
const input = document.querySelector('.ip');
const thaotac = document.querySelector('.thaotac_load');
const loi_anh = document.getElementById('loi_anh');
let id_auto = 1;
let sl_anh = 0;
const anhsps = document.querySelectorAll('.anhsp');
let linksrc = "";
let elm = null;
//anhsps.forEach(anh => {
//    anh.addEventListener('drop', function () {
//        console.log("vô");
//        console.log(linksrc);
//        elm.src = this.src;
//        this.src = linksrc;
//    })
//    anh.addEventListener('dragover', e => {
//        e.preventDefault();
//    })
//    anh.addEventListener('drag', function () {
//        linksrc = this.src;
//        elm = this;
//    })
//})

function f_dragover(e) {
    e.preventDefault();
}
function f_drop(e) {
    elm.src = e.src;
    e.src = linksrc;
}
function f_drag(e) {
    linksrc = e.src;
    elm = e;
}
button.addEventListener('click', function () {
    input.click();
})
input.addEventListener('change', function () {
    const fl = this.files[0];
    showFile(fl)
})
dropArea.addEventListener('dragover', function (envent) {
    envent.preventDefault();
    dropText.textContent = "Thả để tải ảnh lên"
})
dropArea.addEventListener('dragleave', function (envent) {
    envent.preventDefault();
    dropText.textContent = "Kéo và thả để tải ảnh lên"
})
dropArea.addEventListener('drop', function (envent) {
    envent.preventDefault();
    const file = envent.dataTransfer.files[0];
    showFile(file);
})
function showFile(file) {
    const fileType = file.type;
    const validExtensions = ['image/jpeg', 'image/jpg', 'image/png'];
    if (validExtensions.includes(fileType)) {
        const fileReader = new FileReader();
        fileReader.onload = function () {
            if (sl_anh + 1 <= 10) {
                sl_anh++;
                loi_anh.style.display = 'none';
                const fileUrl = fileReader.result;
                let str = '<div class="img_them col-4" id="anh_mh' + id_auto.toString() + '">'
                str += '<img draggable="true" ondragover="f_dragover(event)" ondrag="f_drag(this)" ondrop="f_drop(this)" class="w-100 h-100 anhsp" src = "' + fileUrl + '" />'
                str += '<div class="img_xoa bg-info" onclick="Xoa(' + id_auto.toString() + ')">'
                str += '<i class="bi bi-trash-fill"></i></div></div >'
                thaotac.innerHTML += str;
                id_auto++;
            }
            else {
                nd_tb.innerHTML = "Số lượng ảnh vượt quá quy định (tối đa 10 ảnh)!!!"
                tb.click()
            }
            }
        fileReader.readAsDataURL(file);
        dropText.innerHTML = "Kéo và thả để tải ảnh lên";
    } else {
        document.getElementById('liveToastBtn').click();
        document.getElementById('ThongBao').innerHTML = "Đây không phải là 1 file ảnh";
        dropText.textContent = "Kéo và thả để tải ảnh lên";
    }
}
function Xoa(id_name) {
    document.getElementById("anh_mh" + id_name.toString()).remove();
    sl_anh--;
    if (sl_anh == 0) {
        loi_anh.style.display = 'block';
    }
}
