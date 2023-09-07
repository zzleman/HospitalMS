// Function to update a counter element
function updateCounter(counterElement, target, duration) {
    const increment = (target / duration) * 50; // 50 is an arbitrary multiplier to control the speed

    let currentCount = 0;
    const interval = setInterval(() => {
        currentCount += increment;
        if (currentCount >= target) {
            currentCount = target;
            clearInterval(interval);
        }
        counterElement.innerText = Math.ceil(currentCount);
    }, 50); // 50 milliseconds interval for smoother counting
}

// DOM Element's
const akhiValues = document.querySelectorAll('.akhiValue');
const counterArea = document.querySelector('.counterArea'); // Change this selector to match the section you want to trigger the animation

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

// Function to check if an element is in the viewport
function isInViewport(el) {
    const rect = el.getBoundingClientRect();
    return rect.top < window.innerHeight && rect.bottom >= 0;
}

// Add an event listener to window for scroll events
window.addEventListener('scroll', startAnimationOnScroll);

// Initial check when the page loads
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
    520: {
      slidesPerView: 2,
    },
    1000: {
      slidesPerView: 3,
    },
  },
});