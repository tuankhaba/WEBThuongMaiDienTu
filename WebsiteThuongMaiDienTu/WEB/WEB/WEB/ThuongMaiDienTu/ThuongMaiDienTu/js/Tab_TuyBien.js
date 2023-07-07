const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

const tabs = $$(".tab-itemm");
const panes = $$(".tab-panee");

const tabActive = $(".tab-itemm.activee");
const line = $(".tabss .line");

line.style.left = tabActive.offsetLeft + "px";
line.style.width = tabActive.offsetWidth + "px";


tabs.forEach((tab, index) => {
    const pane = panes[index];

    tab.onclick = function () {
        $(".tab-itemm.activee").classList.remove("activee");
        $(".tab-panee.activee").classList.remove("activee");

        line.style.left = this.offsetLeft + "px";
        line.style.width = this.offsetWidth + "px";

        this.classList.add("activee");
        pane.classList.add("activee");
    };
});