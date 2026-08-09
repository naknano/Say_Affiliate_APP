(function () {
  "use strict";

  document.addEventListener("DOMContentLoaded", function () {
    const $progressPath = $(".scrollToTop path");
    if (!$progressPath.length) return;

    const progressPath = $progressPath[0];
    let pathLength;

    try {
      pathLength = progressPath.getTotalLength();
    } catch (e) {
      console.error("Error getting path length:", e);
      return;
    }

    // Initialize path styles
    $progressPath.css({
      transition: "none",
      WebkitTransition: "none",
      strokeDasharray: pathLength + " " + pathLength,
      strokeDashoffset: pathLength
    });

    // Force browser to recalc
    progressPath.getBoundingClientRect();

    // Set up transition
    $progressPath.css({
      transition: "stroke-dashoffset 10ms linear",
      WebkitTransition: "stroke-dashoffset 10ms linear"
    });

    // Update progress on scroll
    const updateProgress = () => {
      const scroll = $(window).scrollTop();
      const height = $(document).height() - $(window).height();
      const progress = pathLength - (scroll * pathLength) / height;
      $progressPath.css("strokeDashoffset", progress);
    };

    // Throttle scroll for performance
    let ticking = false;
    $(window).on("scroll", function () {
      if (!ticking) {
        window.requestAnimationFrame(() => {
          updateProgress();
          ticking = false;
        });
        ticking = true;
      }
    });

    updateProgress();

    // Scroll-to-top button show/hide
    const offset = 50;
    const duration = 550;

    $(window).on("scroll", function () {
      if ($(this).scrollTop() > offset) {
        $(".scrollToTop").addClass("active-progress");
        $(".tmp-ready-chat").addClass("chat-visible");
      } else {
        $(".scrollToTop").removeClass("active-progress");
        $(".tmp-ready-chat").removeClass("chat-visible");
      }
    });

    // Click to scroll top
    $(".scrollToTop").on("click", function (event) {
      event.preventDefault();
      $("html, body").animate({ scrollTop: 0 }, duration);
      return false;
    });
  });
})();