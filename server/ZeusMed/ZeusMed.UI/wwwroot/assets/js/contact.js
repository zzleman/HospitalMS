
const openMenu = document.getElementById("open-menu");
const closeMenu = document.getElementById("close-menu");
const menu = document.getElementById("menu");

openMenu.addEventListener("click", () => {
    menu.style.left = "-280px";
    menu.style.display = "block"
});

closeMenu.addEventListener("click", () => {
    menu.style.display = "none"
});