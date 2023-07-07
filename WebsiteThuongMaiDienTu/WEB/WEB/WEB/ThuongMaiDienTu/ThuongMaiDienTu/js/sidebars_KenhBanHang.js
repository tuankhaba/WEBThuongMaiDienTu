let arrow = document.querySelectorAll(".arrow");
for (var i = 0; i < arrow.length; i++) {
    arrow[i].addEventListener("click", (e) => {
        let arrowParent = e.target.parentElement.parentElement;//selecting main parent of arrow
        arrowParent.classList.toggle("showMenu");
    });
}
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bx-menu");
sidebarBtn.addEventListener("click", () => {
    sidebar.classList.toggle("close");
});

var kbh_nd = document.querySelector(".text");

let link_content = document.querySelector(".iframe_link");

function LoadContent (content,name){
    link_content.src = content;
    kbh_nd.innerHTML = name;
}
