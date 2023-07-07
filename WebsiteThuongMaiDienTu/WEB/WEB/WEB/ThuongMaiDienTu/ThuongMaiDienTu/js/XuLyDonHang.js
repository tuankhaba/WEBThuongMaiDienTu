const Vouchers = document.querySelectorAll('.voucher')
const rdo_vc = document.querySelectorAll('.rdo_vc')
const nd_giams = document.querySelectorAll('.nd_giam')
const TimKiemVoucher = document.querySelector('#TimKiemVoucher')
TimKiemVoucher.addEventListener("input", function () {
    if (TimKiemVoucher.value.trim() == "")
        Vouchers.forEach(item => item.style.display = 'block')
    else {
        Vouchers.forEach(item => item.style.display = 'none')
        var valu = TimKiemVoucher.value.trim().toLowerCase()
        for (var i = 0; i < rdo_vc.length; i++) {
            if (rdo_vc[i].value.trim().toLowerCase().includes(valu) || nd_giams[i].innerText.trim().toLowerCase().includes(valu))
                Vouchers[i].style.display = 'block'
        }
    }
})
