
function updateCounter(counterElement, target, duration) {
    const increment = (target / duration) * 50; 

    let currentCount = 0;
    const interval = setInterval(() => {
        currentCount += increment;
        if (currentCount >= target) {
            currentCount = target;
            clearInterval(interval);
        }
        counterElement.innerText = Math.ceil(currentCount);
    }, 50); 
}

// DOM Element's
const akhiValues = document.querySelectorAll('.akhiValue');
const counterArea = document.querySelector('.counterArea'); 

let animationStarted = false;

function startAnimationOnScroll() {
    if (!animationStarted && isInViewport(counterArea)) {
        akhiValues.forEach((akhiValue) => {
            const target = +akhiValue.getAttribute('data-target');
            updateCounter(akhiValue, target, 3000); // Adjust the duration (5000 milliseconds = 5 seconds) as needed
        });

        animationStarted = true; // Prevent starting the animation again
    }
}

function isInViewport(el) {
    const rect = el.getBoundingClientRect();
    return rect.top < window.innerHeight && rect.bottom >= 0;
}

window.addEventListener('scroll', startAnimationOnScroll);

startAnimationOnScroll();




var swiper = new Swiper(".slide-content", {
  slidesPerView: 3,
  spaceBetween: 10,
  sliderPerGroup: 3,
  loop: true,
  centerSlide: "true",
  fade: "true",
  grabCursor: "true",
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },

  breakpoints: {
    0: {
      slidesPerView: 1,
    },
    768: {
      slidesPerView: 2,
    },
    1000: {
      slidesPerView: 3,
    },
  },
});

const openMenu = document.getElementById("open-menu");
        const closeMenu = document.getElementById("close-menu");
        const menu = document.getElementById("menu");

        openMenu.addEventListener("click", () => {
            menu.style.left = "-280px";
            menu.style.display="block"
        });

        closeMenu.addEventListener("click", () => {
            menu.style.display="none"
        });