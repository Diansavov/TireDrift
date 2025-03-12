const counterUp = window.counterUp.default;

const callback = (entries) => {
    entries.forEach((entry) => {
        const el = entry.target;
        if (entry.isIntersecting && !el.classList.contains("is-visible")) {
            counterUp(el, {
                duration: 2000,
                delay: 16,
            });
            el.classList.add("is-visible");
        }
    });
};

const IO = new IntersectionObserver(callback, { threshold: 1 });

// Select all elements with the "counter" class and observe each one
document.querySelectorAll(".counter").forEach((el) => IO.observe(el));
