// script.js

const slides = document.querySelectorAll('.slide');
let currentSlide = 0;

function showSlide(index) {
    
    slides.forEach((slide, i) => {
        if (i === index) {
            slide.style.opacity = 1; // Set opacity to 1 for the current slide
        } else {
            slide.style.opacity = 0;
        }
    });
}

function nextSlide() {
    currentSlide = (currentSlide + 1) % slides.length;
    showSlide(currentSlide);
}

// Show the first slide immediately
showSlide(currentSlide);

// Change slide every 3 seconds (adjust as needed)
setInterval(nextSlide, 5000);
