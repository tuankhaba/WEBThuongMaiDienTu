const num = document.querySelector('.phone')
function CheckNumber() {
    var x = num.value;
    if (isNaN(x)) {
        num.style.borderColor = 'red';
    } else
        num.style.borderColor = 'green';
}

let gt = "";

function CheckPhone(e) {
    if (e.value != "") {
        let tam = e.value
        let a = parseInt(e.value)
        gt = (isNaN(a)) ? gt : tam;
        e.value = gt;
    }

}
