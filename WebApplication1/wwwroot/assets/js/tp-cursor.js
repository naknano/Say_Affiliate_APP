(function ($) {
    "use strict";

    if ($("body").not(".is-mobile").hasClass("tp-magic-cursor")) {
        $(".tp-magnetic-item").wrap('<div class="tp-magnetic-wrap"></div>');

        if ($("a.tp-magnetic-item").length) {
            $("a.tp-magnetic-item").addClass("not-hide-cursor");
        }

        var $mouse = { x: 0, y: 0 };
        var $pos = { x: 0, y: 0 };
        var $ratio = 0.15;
        var $active = false;
        var $ball = $("#ball");

        var $ballWidth = 14;
        var $ballHeight = 14;
        var $ballScale = 1;
        var $ballOpacity = 1;
        var $ballBorderWidth = 1;

        gsap.set($ball, {
            xPercent: -50,
            yPercent: -50,
            width: $ballWidth,
            height: $ballHeight,
            borderWidth: $ballBorderWidth,
            opacity: $ballOpacity
        });

        document.addEventListener("mousemove", mouseMove);

        function mouseMove(e) {
            $mouse.x = e.clientX;
            $mouse.y = e.clientY;
        }

        gsap.ticker.add(updatePosition);

        function updatePosition() {
            if (!$active) {
                $pos.x += ($mouse.x - $pos.x) * $ratio;
                $pos.y += ($mouse.y - $pos.y) * $ratio;
                gsap.set($ball, { x: $pos.x, y: $pos.y });
            }
        }

        $(".tp-magnetic-wrap").on("mousemove", function(e) {
            parallaxCursor(e, this, 2);
            callParallax(e, this);
        });

        function callParallax(e, parent) {
            parallaxIt(e, parent, parent.querySelector(".tp-magnetic-item"), 25);
        }

        function parallaxIt(e, parent, target, movement) {
            var boundingRect = parent.getBoundingClientRect();
            var relX = e.clientX - boundingRect.left;
            var relY = e.clientY - boundingRect.top;

            gsap.to(target, {
                duration: 0.3,
                x: ((relX - boundingRect.width / 2) / boundingRect.width) * movement,
                y: ((relY - boundingRect.height / 2) / boundingRect.height) * movement,
                ease: Power2.easeOut
            });
        }

        function parallaxCursor(e, parent, movement) {
            var rect = parent.getBoundingClientRect();
            var relX = e.clientX - rect.left;
            var relY = e.clientY - rect.top;
            $pos.x = rect.left + rect.width / 2 + (relX - rect.width / 2) / movement;
            $pos.y = rect.top + rect.height / 2 + (relY - rect.height / 2) / movement;
            gsap.to($ball, { duration: 0.3, x: $pos.x, y: $pos.y });
        }

        $(".tp-magnetic-wrap").on("mouseenter", function() {
            gsap.to($ball, { duration: 0.3, scale: 2, borderWidth: 1, opacity: $ballOpacity });
            $active = true;
        }).on("mouseleave", function() {
            gsap.to($ball, { duration: 0.3, scale: $ballScale, borderWidth: $ballBorderWidth, opacity: $ballOpacity });
            gsap.to(this.querySelector(".tp-magnetic-item"), { duration: 0.3, x: 0, y: 0, clearProps:"all" });
            $active = false;
        });

        // Cursor hover view from CSS variable
        $("[data-cursor]").each(function() {
            var $this = $(this);
            var color = getComputedStyle(this).getPropertyValue('--cursor-color').trim() || '#fff';

            $this.on("mouseenter", function() {
                $("#ball").addClass("with-blur");
                $ball.append('<div class="ball-view"></div>');
                $(".ball-view").append($this.attr("data-cursor"));

                gsap.to($ball, {
                    duration: 0.3,
                    xPercent: is_rtl() ? 50 : -50,
                    yPercent: -60,
                    width: 110,
                    height: 110,
                    opacity: 1,
                    borderWidth: 0,
                    zIndex: 1,
                    backdropFilter: "blur(14px)",
                    backgroundColor: color,
                });

                gsap.to(".ball-view", { duration: 0.3, scale: 1, autoAlpha: 1 });
            }).on("mouseleave", function() {
                gsap.to($ball, {
                    duration: 0.3,
                    yPercent: -50,
                    width: $ballWidth,
                    height: $ballHeight,
                    opacity: $ballOpacity,
                    borderWidth: $ballBorderWidth,
                    backgroundColor: "#000"
                });

                gsap.to(".ball-view", { duration: 0.3, scale: 0, autoAlpha: 0, clearProps:"all" });
                $ball.find(".ball-view").remove();
            });

            $this.addClass("not-hide-cursor");
        });

        // Hide cursor on certain elements
        $("a, button").not('.cursor-hide').on("mouseenter", function() {
            gsap.to($ball, { duration: 0.3, scale: 0, opacity: 0 });
        }).on("mouseleave", function() {
            gsap.to($ball, { duration: 0.3, scale: $ballScale, opacity: $ballOpacity });
        });

        $("a").not('[target="_blank"], .cursor-hide, [href^="#"], [href^="mailto"], [href^="tel"], .lg-trigger, .tp-btn-disabled a')
            .on('click', function() {
                gsap.to($ball, { duration: 0.3, scale: 1.3, autoAlpha: 0 });
            });

        // Document enter/leave
        $(document).on("mouseleave", function() {
            gsap.to("#magic-cursor", { duration: 0.3, autoAlpha: 0 });
        }).on("mouseenter", function() {
            gsap.to("#magic-cursor", { duration: 0.3, autoAlpha: 1 });
        }).on("mousemove", function() {
            gsap.to("#magic-cursor", { duration: 0.3, autoAlpha: 1 });
        });
    }

    function is_rtl() {
        return $('html').attr('dir') == 'rtl';
    }

})(jQuery);
