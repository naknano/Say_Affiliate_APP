/***************************************************
==================== JS INDEX ======================
****************************************************
01. PreLoader Js
02. Nice Select Js
03. mobile menu Js
04. Sticky Header Js
05. Sidebar Js
06. Search Js
07. Common Js
08. Smooth Scroll Js
10. Masonary Js
11. magnificPopup img view
12. Counter 
13. button hover animation
14. webgl images hover animation
15. portfolio animation start
16. hover reveal start
17. hover image-wrapper
18. portfolio panel
19. section-triger-slicer
20. Button Move Animation
21. tp-fixed-title-wrap
22. Counter Animation
23. Portfolio Animation
24. backToTop
25. tp-service-pp-panel
26. Text Invert With Scroll 
27. tp-btn-trigger
28. zoom in
29. scroll-scale-up-img
30. tp-custom-accordion
31. service__item-8
32. portfolio-item-hover
34. Split text into characters using SplitType
35. scrool-rotate-img
36. scroll with moving text
37. scale animation
38. has_fade_anim
39. tp-skill-pb-panel
40. tp-text-perspective
41. fade-class-active
42. has-scale-image
43. Function Showcase Snap Slider
44. text-scale-anim
45. tp-hero-md-col-custom
46. tp-funfact-panel
47. text bounce animation
48. rotate-text-anim
49. atropos-component-active
50. tp-text-revel-anim
51. Text Effect Animation
52. tp-video-img-wrap
53. tp-awards-vp-move-thumbs-wrapper
54. tp-service-vp-item
55. tp-vimeo-video-init
56. tp-vimeo-video-perspective
57. tp-portfolio-revealing-slide
58. perspective-slider animation
59. tp-port-slider-title
60. tp-porfolio-10-title-wrap
61. portfolio item pin js
62. Password Toggle Js
63. E-commerce plus minus js  
64. brand img animation
65. tp-map-pin
66. price toggle animation
67. button effect
68. hover reveal start
69. watter effact here
70. ar-service-item
71. ar-scroll-image
72. cnt-portfolio-ptb
73. tp-gsap-bg
****************************************************/

(function ($) {
	"use strict";

	var windowOn = $(window);

	// Get Device width
	let device_width = window.innerWidth;

	////////////////////////////////////////////////////
	// 01. PreLoader Js

	windowOn.on('load', function () {
		var body = $('body');
		body.addClass('loaded');
		setTimeout(function () {
			body.removeClass('loaded');
		}, 1500);
	});
	
	document.addEventListener("DOMContentLoaded", () => {
		const svg = document.getElementById("svg");
		if (!svg) return;

		const tls = gsap.timeline();
		const curve = "M0 502S175 272 500 272s500 230 500 230V0H0Z";
		const flat = "M0 2S175 1 500 1s500 1 500 1V0H0Z";

		// Loader heading text
		if (document.querySelector(".loader-wrap-heading")) {
			tls.to(".loader-wrap-heading .load-text , .loader-wrap-heading .cont", {
				delay: 0.5,
				y: -100,
				opacity: 0,
			});
		}

		// SVG animation
		tls.to(svg, {
			duration: 0.5,
			attr: { d: curve },
			ease: "power2.in",
		}).to(svg, {
			duration: 0.5,
			attr: { d: flat },
			ease: "power2.out",
		});

		// Loader wrap
		if (document.querySelector(".loader-wrap")) {
		tls.to(".loader-wrap", { y: -1500 })
			.to(".loader-wrap", { zIndex: -1, display: "none" });
		}

		// Pre-header animation (safe check)
		const preHeader = document.querySelector(".pre-header");
		if (preHeader) {
		tls.from(preHeader, { y: 200 }, "-=1.5");

		const preHeaderCont = preHeader.querySelector(".containers");
			if (preHeaderCont) {
				tls.from(preHeaderCont, {
					y: 40,
					opacity: 0,
					delay: 0.1,
				}, "-=1.5");
			}
		}
	});



	////////////////////////////////////////////////////
	// 02. Nice Select Js
	$('.tp-select').niceSelect();


	////////////////////////////////////////////////////
	// 03. mobile menu Js
	var tpMenuWrap = $('.tp-mobile-menu-active > ul').clone();
	var tpSideMenu = $('.tp-offcanvas-menu nav');
	tpSideMenu.append(tpMenuWrap);
	if ($(tpSideMenu).find('.tp-submenu, .mega-menu').length != 0) {
		$(tpSideMenu).find('.tp-submenu, .mega-menu').parent().append('<button class="tp-menu-close"><i class="far fa-chevron-right"></i></button>');
	}
	var sideMenuList = $('.tp-offcanvas-menu nav > ul > li button.tp-menu-close, .tp-offcanvas-menu nav > ul li.has-dropdown > a, .tp-offcanvas-menu nav > ul li.has-dropdown > ul > li.menu-item-has-children > a');
	$(sideMenuList).on('click', function (e) {
		e.preventDefault();
		if (!($(this).parent().hasClass('active'))) {
			$(this).parent().addClass('active');
			$(this).siblings('.tp-submenu, .mega-menu').slideDown();
		} else {
			$(this).siblings('.tp-submenu, .mega-menu').slideUp();
			$(this).parent().removeClass('active');
		}
	});


	///////////////////////////////////////////////////
	// 04. Sticky Header Js
	$(window).on('scroll', function () {
		let scroll = $(window).scrollTop();
		if (scroll < 20) {
			$("#header-sticky").removeClass("header-sticky");
		} else {
			$("#header-sticky").addClass("header-sticky");
		}
	});


	////////////////////////////////////////////////////
	// 05. Sidebar Js
	$(".tp-menu-bar").on("click", function () {
		$(".tp-offcanvas").addClass("opened");
		$(".body-overlay").addClass("apply");
	});
	$(".close-btn").on("click", function () {
		$(".tp-offcanvas").removeClass("opened");
		$(".body-overlay").removeClass("apply");
	});
	$(".body-overlay").on("click", function () {
		$(".tp-offcanvas").removeClass("opened");
		$(".body-overlay").removeClass("apply");
	});
	$(".tp-offcanvas-open-btn").on("click", function () {
		$(".tp-offcanvas-2-area, .body-overlay").addClass("opened");
	});

	// 15. overlay not working js //
	$('.tp-offcanvas-open-btn').on('click', function () {
		const hasOffcanvas2 = $('.tp-offcanvas-2-area').length;
		$('.body-overlay').toggleClass('opened', !hasOffcanvas2);
	});
	  


	////////////////////////////////////////////////////
	// 06. Search Js
	$(".tp-search-click").on("click", function () {
		$(".tp-search-form-toggle,.tp-search-body-overlay").addClass("active");
	});

	$(".tp-search-close,.tp-search-body-overlay").on("click", function () {
		$(".tp-search-form-toggle,.tp-search-body-overlay").removeClass("active");
	});

	$(".cartmini-open-btn").on("click", function () {
		$(".cartmini__area").addClass("cartmini-opened");
		$(".body-overlay").addClass("apply");
	});
  
	$(".cartmini-close-btn").on("click", function () {
		$(".cartmini__area").removeClass("apply cartmini-opened");
		$(".body-overlay").removeClass("apply");
	});

	// 14. Close Handlers //
	$(".cartmini-close-btn, .body-overlay, .tp-offcanvas-2-close-btn").on("click", function () {
		$(".tp-search-area, .cartmini__area, .tp-offcanvas-2-area").removeClass("opened cartmini-opened");
		$(".body-overlay").removeClass("opened");
	});

	// addClass removeClass //	
	$('.tp-service-vp-wrap .tp-service-vp-item').on('mouseenter', function(){
		$(this).addClass('active').parent().siblings().find('.tp-service-vp-item').removeClass('active')
	}).on('mouseleave', function(){
		$(this).addClass('active').parent().siblings().find('.tp-service-vp-item').addClass('active')
	})

	$('.tp-offcanvas-2-area .tp-offcanvas-menu > nav > ul > li').on("mouseenter", function () {
		$(this).addClass('is-active').siblings().removeClass('is-active');
	}).on("mouseleave", function () {
		$(this).siblings().addClass('is-active');
	});


	////////////////////////////////////////////////////
	// 07. Common Js
	$("[data-background").each(function () {
		$(this).css("background-image", "url( " + $(this).attr("data-background") + "  )");
	});

	$("[data-width]").each(function () {
		$(this).css("width", $(this).attr("data-width"));
	});

	$("[data-bg-color]").each(function () {
		$(this).css("background-color", $(this).attr("data-bg-color"));
	});

	////////////////////////////////////////////////////
	// 08. Smooth Scroll Js
	gsap.registerPlugin(ScrollTrigger, ScrollSmoother, ScrollToPlugin);
	if($('#smooth-wrapper').length && $('#smooth-content').length){
		ScrollSmoother.create({
			smooth: 1.35,
			effects: true,
			smoothTouch: .1,
			ignoreMobileResize: false
		});
		ScrollTrigger.refresh(true);
	}


	// Smooth btn Scroll Js
	function smoothSctoll() {
        $('.tp-smooth').on('click', function (event) {
            var target = $(this.getAttribute('href'));
            if (target.length) {
                event.preventDefault();
                $('html, body').stop().animate({
                    scrollTop: target.offset().top - -60
                }, 1500);
            }
        });
    }
    smoothSctoll();

	////////////////////////////////////////////////////
	// 10. Masonary Js
	$('.grid').imagesLoaded(function () {
		// init Isotope
		var $grid = $('.grid').isotope({
			itemSelector: '.grid-item',
			percentPosition: true,
			masonry: {
				// use outer width of grid-sizer for columnWidth
				columnWidth: '.grid-item',
			}
		});


		// filter items on button click
		$('.masonary-menu').on('click', 'button', function () {
			var filterValue = $(this).attr('data-filter');
			$grid.isotope({ filter: filterValue });
		});

		//for menu active class
		$('.masonary-menu button').on('click', function (event) {
			$(this).siblings('.active').removeClass('active');
			$(this).addClass('active');
			event.preventDefault();
		});

	});

	////////////////////////////////////////////////////
	// 11. magnificPopup img view
	$('.popup-image').magnificPopup({
		type: 'image',
		gallery: {
			enabled: true
		}
	});

	$(".popup-video").magnificPopup({
		type: "iframe",
	});

	////////////////////////////////////////////////////
	// 12. Counter Js
	new PureCounter();
	new PureCounter({
		filesizing: true,
		selector: ".filesizecount",
		pulse: 2,
	});

	////////////////////////////////////////////////////
	// 13. button hover animation
	$('.tp-btn-rounded').on('mouseenter', function (e) {
		var x = e.pageX - $(this).offset().left;
		var y = e.pageY - $(this).offset().top;

		$(this).find('.tp-btn-circle-dot').css({
			top: y,
			left: x
		});
	});

	////////////////////////////////////////////////////
	// 14. webgl images hover animation
	if ($('.tp--hover-item').length) {
		let hoverAnimation__do = function (t, n) {
			let a = new hoverEffect({
				parent: t.get(0),
				intensity: t.data("intensity") || void 0,
				speedIn: t.data("speedin") || void 0,
				speedOut: t.data("speedout") || void 0,
				easing: t.data("easing") || void 0,
				hover: t.data("hover") || void 0,
				image1: n.eq(0).attr("src"),
				image2: n.eq(0).attr("src"),
				displacementImage: t.data("displacement"),
				imagesRatio: n[0].height / n[0].width,
				hover: !1
			});
			t.closest(".tp--hover-item").on("mouseenter", function () {
				a.next()
			}).on("mouseleave", function () {
				a.previous()
			})
		}
		let hoverAnimation = function () {
			$(".tp--hover-img").each(function () {
				let n = $(this);
				let e = n.find("img");
				let i = e.eq(0);
				i[0].complete ? hoverAnimation__do(n, e) : i.on("load", function () {
					hoverAnimation__do(n, e)
				})
			})
		}
		hoverAnimation();
	}

	////////////////////////////////////////////////////
	// 15. portfolio animation start

	if ($('.tp-item-anime').length > 0) {
		gsap.set('.tp-item-anime.marque', {
			x: '25%'
		});
		gsap.timeline({
			scrollTrigger: {
				trigger: '.tp-item-anime-area ',
				start: '-1000 10%',
				end: 'bottom 20%',
				scrub: true,
				invalidateOnRefresh: true
			}
		})
		.to('.tp-item-anime.marque ', {
			x: '-100%'
		});
	}

	if ($('.tp-item-anime-md').length > 0) {
		gsap.set('.tp-item-anime-md.marque', {
			x: '2%'
		});
		gsap.timeline({
			scrollTrigger: {
				trigger: '.tp-item-anime-area-md ',
				start: '-100 20%',
				end: 'bottom 20%',
				scrub: true,
				invalidateOnRefresh: true
			}
		})
		.to('.tp-item-anime-md.marque ', {
			x: '-10%'
		});
	}

	function moving_text(){
		if ($('.moving-text').length > 0) {
			gsap.utils.toArray('.moving-text').forEach((section, index) => {
				const w = section.querySelector('.wrapper-text');
				const [x, xEnd] = (index % 2) ? [(section.offsetWidth - w.scrollWidth), 0] : [0, section.offsetWidth - w.scrollWidth];
				gsap.fromTo(w, { x }, {
					x: xEnd,
					scrollTrigger: {
						trigger: section,
						scrub: 0.1,
					}
				});
			});
		}
	}
	$(function(){
		moving_text();
	});

	////////////////////////////////////////////////////
	// 16. hover reveal start
	const hoverItem = document.querySelectorAll(".tp-reveal-item");
	function moveImage(e, hoverItem, index) {
		const item = hoverItem.getBoundingClientRect();
		const x = e.clientX - item.x;
		const y = e.clientY - item.y;
		if (hoverItem.children[index]) {
			hoverItem.children[index].style.transform = `translate(${x}px, ${y}px)`;
		}
	}
	hoverItem.forEach((item, i) => {
		$(item).on("mousemove", (e) => {
			setInterval(moveImage(e, item, 1), 50);
		});
	});

	////////////////////////////////////////////////////
	// 17. hover image-wrapper
	const imageWrapper = document.querySelector(".image-wrapper");
	const imageSlider = document.querySelector(".image-slider");
	const projects = gsap.utils.toArray(".projects");

	if (imageWrapper && imageSlider && projects.length > 0) {

	$('[data-index-number]').each((index, el) => {
		$(el).on("mouseenter", () => {
			gsap.to(imageWrapper, {
				opacity: 1,
				duration: 0.5,
			});
		});

		$(el).on("mouseleave", () => {
			gsap.to(imageWrapper, {
				opacity: 0,
				duration: 0.5,
			});
		});

		const projectCount = imageSlider.children.length;
		const movePercent = 100 / projectCount;
		$(el).on("mousemove", function(){
			const indexNumber = $(this).data('index-number');
			gsap.to(imageSlider, {
				y: -(movePercent * indexNumber) + '%',
				duration: 0.6,
				ease: "power2.out"
			});
		});
	})

	$(document).on("mousemove", ".tp-service-wd", function(event){
		const parentOffset = $(this).offset();
		const relativeX = event.pageX - parentOffset.left;
		const relativeY = event.pageY - parentOffset.top;

		const offsetX = -200;
		const offsetY = 0; 

		gsap.to(imageWrapper, {
			x: relativeX - offsetX,
			y: relativeY - offsetY,
			duration: 0.5,
			ease: "power2.out"
		});
	});
	}

	////////////////////////////////////////////////////
	// 18. portfolio panel
	let tl = gsap.timeline();
	let pr = gsap.matchMedia();
	pr.add("(min-width: 767px)", () => {
		let otherSections = document.querySelectorAll('.des-portfolio-panel')
		otherSections.forEach((section, index) => {
			gsap.set(otherSections, {
				scale: 1,
			});
			tl.to(section, {
				scale: .8,
				scrollTrigger: {
					trigger: section,
					pin: section,
					scrub: 1,
					start: 'top 0',
					end: "bottom 60%",
					endTrigger: '.des-portfolio-wrap',
					pinSpacing: false,
					markers: false,
				},
			})
		})
	});

	
	////////////////////////////////////////////////////
	// 19. section-triger-slicer
	gsap.registerPlugin(ScrollTrigger);

	const triggerSlices = [...document.querySelectorAll('.section-triger')];

	triggerSlices.forEach((section) => {
	const slices = section.querySelectorAll(".uncover_slice");
	const image = section.querySelector(".myimg");

	const tl = gsap.timeline({
		scrollTrigger: {
		trigger: section,
		start: "50% bottom",
		markers: false,
		}
	});

	tl.to(slices, { 
		height: 0,  
		ease: 'power6.inOut',
		duration: 0.6,
		stagger: { each: 0.3 }
	}, 'start')
	.to(image, {
		scale: 1.3,
		duration: 1.5,
		ease: 'power6.inOut'
	}, 'start');
	});

  	/////////////////////////////////////////////////////
  	// 20. Button Move Animation
	const all_btn = gsap.utils.toArray(".btn_wrapper, #btn_wrapper");
	const all_btn_circle = gsap.utils.toArray(".btn-item");

	if (all_btn.length && all_btn_circle.length) {
		all_btn.forEach((btn, i) => {
			const circle = all_btn_circle[i];

			// Mouse move = parallax effect
			$(btn).on("mousemove", function (e) {
			const $this = $(this);
			const relX = e.pageX - $this.offset().left;
			const relY = e.pageY - $this.offset().top;
				gsap.to(circle, {
					duration: 0.5,
					x: ((relX - $this.width() / 2) / $this.width()) * 80,
					y: ((relY - $this.height() / 2) / $this.height()) * 80,
					ease: "power2.out",
				});
			});

			// Mouse leave = reset effect
			$(btn).on("mouseleave", function () {
				gsap.to(circle, {
					duration: 0.5,
					x: 0,
					y: 0,
					ease: "power2.out",
				});
			});
		});
	}


  /////////////////////////////////////////////////////
  // 21.tp-fixed-title-wrap
	let pc = gsap.matchMedia();
	pc.add("(min-width: 992px)", () => {
		if ($('.tp-fixed-title-wrap').length > 0) {
			let project_text = gsap.timeline({
				scrollTrigger: {
					trigger: ".tp-fixed-title-wrap",
					start: 'top center-=450',
					end: "bottom 70%",
					pin: ".tp-fixed-title",
					markers: false,
					pinSpacing: false,
					scrub: 1,
				}
			})
		}	
	});

	/////////////////////////////////////////////////////
	// 22. Counter Animation
	const bounceItems = gsap.utils.toArray(".bounce_animation .bounce__anim");

	if (bounceItems.length > 0) {
	// Initial state
	gsap.set(bounceItems, { y: -100, opacity: 0 });

	if (device_width < 1023) {
		// Mobile / Tablet
		bounceItems.forEach((item) => {
		let counterTl = gsap.timeline({
			scrollTrigger: {
			trigger: item,
			start: "top center+=200",
			}
		});

		counterTl.to(item, {
			y: 0,
			opacity: 1,
			ease: "bounce",
			duration: 1.5,
		});
		});
	} else {
		// Desktop
		gsap.to(bounceItems, {
		scrollTrigger: {
			trigger: ".bounce_animation",
			start: "top center+=300",
		},
		y: 0,
		opacity: 1,
		ease: "bounce",
		duration: 1.5,
		stagger: {
			each: 0.3,
		}
		});
	}
	}


	/////////////////////////////////////////////////////
	// 23. Portfolio Animation
	if (device_width > 767) {
		const portfolioArea = document.querySelector(".portfolio__area");
		const portfolioText = document.querySelector(".portfolio__text");

		if (portfolioArea && portfolioText) {
			let portfolioline = gsap.timeline({
				scrollTrigger: {
					trigger: portfolioArea,
					start: "top center-=200",
					pin: portfolioText,
					end: "bottom bottom+=10",
					markers: false,
					pinSpacing: false,
					scrub: 1,
				}
			});

			portfolioline.to(portfolioText, { scale: 3, duration: 1 });
			portfolioline.to(portfolioText, { scale: 3, duration: 1 });
			portfolioline.to(portfolioText, { scale: 1, duration: 1 }, "+=2");

			gsap.to(portfolioText, {
			scrollTrigger: {
				trigger: portfolioArea,
				start: "top center-=100",
				end: "bottom bottom+=10",
				scrub: 1
			},
			opacity: 0
			});
		}
	}

	// portfolio__item animation (safe check)
	let portfolioLists = gsap.utils.toArray(".portfolio__item");
	if (portfolioLists.length > 0) {
		portfolioLists.forEach((portfolio) => {
			gsap.set(portfolio, {
				opacity: 0.7,
				transform: "perspective(4000px) translate3d(0px, 0px, 0px) rotateX(90deg) scale(0.5, 0.5)"
			});

			let t1 = gsap.timeline();
			t1.set(portfolio, { position: "relative" });

			t1.to(portfolio, {
			scrollTrigger: {
				trigger: portfolio,
				scrub: 2,
				start: "top bottom+=100",
				end: "bottom center",
				markers: false
			},
				scale: 1,
				rotateX: 0,
				opacity: 1,
				duration: 1.5
			});
		});
	}

  
 	/////////////////////////////////////////////////////
  // 24. backToTop
	let windowHeight = 0;
	let documentHeight = 0;
	function updateDimensions() {
		windowHeight = window.innerHeight;
		documentHeight = document.documentElement.scrollHeight - windowHeight;
	}
	updateDimensions();
	$(window).on('resize', updateDimensions);

	let $box = $(".scrollToTop");
	if ($box.length) {
		let $water = $box.find(".water");

		$(window).on('scroll', function() {
			let scrollPosition = $(window).scrollTop();
			let percent = Math.min(
				Math.floor((scrollPosition / documentHeight) * 100),
				100
			);

			$water.css("transform", "translate(0," + (100 - percent) + "%)");

			if (scrollPosition >= 200) {
				$box.fadeIn();
			} else {
				$box.fadeOut();
			}
		});

		// Scroll to top
		$box.on('click', function() {
			$('html, body').animate({ scrollTop: 0 }, 'smooth');
		});
	}


 	/////////////////////////////////////////////////////
  // 24. panel pin section
	pr.add("(min-width: 1199px)", () => {
		let tl = gsap.timeline();
		let panels = document.querySelectorAll('.tp-panel-pin')
		panels.forEach((section, index) => {
			tl.to(section, {
				scrollTrigger: {
					trigger: section,
					pin: section,
					scrub: 1,
					start: 'top 10%',
					end: "bottom 99%",
					endTrigger: '.tp-panel-pin-area',
					pinSpacing: false,
					markers: false,
				},
			})
		})
	});

	/////////////////////////////////////////////////////
  	// 25. tp-service-pp-panel
	let sv = gsap.matchMedia();
	sv.add("(min-width: 1199px)", () => {
		let tl = gsap.timeline();
		let projectpanels = document.querySelectorAll('.tp-service-pp-panel');
		let baseOffset = 130;
		let offsetIncrement = 130;

		projectpanels.forEach((section, index) => {
			let topOffset = baseOffset + (index * offsetIncrement);
			tl.to(section, {
				scrollTrigger: {
					trigger: section,
					pin: section,
					scrub: 1,
					start: `top ${topOffset}px`,
					end: "bottom 120%",
					endTrigger: '.tp-service-pp-pin',
					pinSpacing: false,
					markers: false,
				},
			});
		});
	});

	/////////////////////////////////////////////////////
  	// 26. Text Invert With Scroll 
	function tp_text_invert(){
		const split = new SplitText(".tp_text_invert", { type: "lines" });
		split.lines.forEach((target) => {
			gsap.to(target, {
				backgroundPositionX: 0,
				ease: "none",
				scrollTrigger: {
					trigger: target,
					scrub: 1,
					start: 'top 85%',
					end: "bottom center"
				}
			});
		});	
	}
	$(function(){
		tp_text_invert();
	});


	///////////////////////////////////////////////////
	// 27. tp-btn-trigger
	if ($('.tp-btn-trigger').length > 0) {

		gsap.set(".tp-btn-bounce", { y: -70, opacity: 0 });
		var mybtn = gsap.utils.toArray(".tp-btn-bounce");
		mybtn.forEach((btn) => {
			var $this = $(btn);
			gsap.to(btn, {
				scrollTrigger: {
					trigger: $this.closest('.tp-btn-trigger'),
					start: "top center",
					markers: false
				},
				duration: 1,
				ease: "bounce.out",
				y: 0,
				opacity: 1,
			})
		});
	}

	///////////////////////////////////////////////////
	// 28. zoom in
	$(".anim-zoomin").each(function() {

		$(this).wrap('<div class="anim-zoomin-wrap"></div>');

		$(".anim-zoomin-wrap").css({ "overflow": "hidden" })

		let $this = $(this);
		let $asiWrap = $this.parents(".anim-zoomin-wrap");

		let tp_ZoomIn = gsap.timeline({
			scrollTrigger: {
				trigger: $asiWrap,
				start: "top 100%",
				markers: false,
			}
		});
		tp_ZoomIn.from($this, { duration: 2, autoAlpha: 0, scale: 1.4, ease: Power2.easeOut, clearProps:"all" });

	});

	///////////////////////////////////////////////////
	// 29. scroll-scale-up-img
	document.querySelectorAll(".scale-up-img").forEach((section) => {
		let tl = gsap.timeline({
		  scrollTrigger: {
			trigger: section,
			start: "top bottom",
			end: "bottom center",
			scrub: 1,
			markers: false,
		  },
		});
	  
		tl.to(section.querySelector(".scale-up"), {
		  scale: 1.15,
		  duration: 1,
		});
	  });

	///////////////////////////////////////////////////
	// 30. tp-custom-accordion
	$('.tp-custom-accordion .accordion-item').on("click", function(){
		$(this).addClass('tp-faq-active').siblings().removeClass('tp-faq-active');
	}); 

	///////////////////////////////////////////////////
	// 31. service__item-8
	$('.service__item-8').on("mouseenter", function(){
		$(this).addClass('active').siblings().removeClass('active');
		$('#service-bg-img').removeClass().addClass($(this).attr('rel'));
	});  

	///////////////////////////////////////////////////
	// 32. portfolio-item-hover

	/*=============================================
		=       Service       =
	=============================================*/
	document.addEventListener("DOMContentLoaded", function () {
		let serviceItems = document.querySelectorAll(".portfolio-item");
		let images = document.querySelectorAll(".hover-image");
	
		serviceItems.forEach((item, index) => {
			item.addEventListener("mouseenter", function () {
				serviceItems.forEach((el) => el.classList.remove("active"));
				images.forEach((img) => img.classList.remove("active"));
				this.classList.add("active");
				images[index].classList.add("active");
			});
		});
	});

  /////////////////////////////////////////////////////
  // 34 .Split text into characters using SplitType
    const text = new SplitType('.text', { types: 'chars' });
    const length = text.chars.length;

    text.chars.forEach((char, index) => {
        $(char).css('font-weight', (index + 1) * 0.1 * 600);

        $(char).on('mouseover', function () {
          	animate(index);
        });
    });

    function calculate(index, length) {
        const positions = Array.from(Array(length).keys());
        const left = positions.slice(0, index - 1);
        const right = positions.slice(index);
        return { left, right };
    }

    function weightByIndex(index) {
        return (index + 1) * 70;
    }

    function animate(index) {
        const { left, right } = calculate(index + 1, length);
        const tl = gsap.timeline();

        tl.to(text.chars[index], {
			fontWeight: weightByIndex(0),
        }, "way");

        text.chars.slice(0, index - 1).reverse().forEach((char, i) => {
			tl.to(char, {
				fontWeight: weightByIndex(i),
			}, "way");
        });

        text.chars.slice(index).forEach((char, i) => {
			tl.to(char, {
				fontWeight: weightByIndex(i),
			}, "way");
        });
    }

  /////////////////////////////////////////////////////
  // 35 .scrool-rotate-img

	$(".scrool-rotate-img").each(function () {
		gsap.to(this, {
			rotation: 720,
			ease: "power2.out",
			duration: 1,
			scrollTrigger: {
				trigger: this,
				start: "top bottom",
				end: "bottom top",
				scrub: 1.5,
			}
		});
	});

  /////////////////////////////////////////////////////
  // 36 .scroll with moving text
	function moving_text() {
		if ($('.moving-text').length > 0) {
			gsap.utils.toArray('.moving-text').forEach((section, index) => {
				const w = section.querySelector('.wrapper-text');
				const [x, xEnd] = (index % 2) ? [(section.offsetWidth - w.scrollWidth), 0] : [0, section.offsetWidth - w.scrollWidth];
				gsap.fromTo(w, { x }, {
					x: xEnd,
					scrollTrigger: {
						trigger: section,
						scrub: 0.1,
					}
				});
			});
		}
	}
	$(window).on('load', function() {
		moving_text();
	});

  /////////////////////////////////////////////////////
  // 37 .scale animation

    const scaleImage = document.querySelectorAll(".mil-scale-img");

    scaleImage.forEach((section) => {
        var value1 = section.getAttribute("data-value-1");
        var value2 = section.getAttribute("data-value-2");

        if (window.innerWidth < 1200) {
            value1 = Math.max(.95, value1);
        }

        gsap.fromTo(section, {
            ease: 'sine',
            scale: value1,
        }, {
            scale: value2,
            scrollTrigger: {
                trigger: section,
                scrub: true,
                toggleActions: 'play none none reverse',
            }
        });
    });


	///////////////////////////////////////////////////
	// 38. has_fade_anim
	let fadeArray_items = document.querySelectorAll(".has_fade_anim");
	if (fadeArray_items.length > 0) {
		let t = gsap.utils.toArray(".has_fade_anim");
		t.forEach((t, a) => {
			var e = "bottom",
				r = 1,
				o = 1.15,
				i = 50,
				s = 0.15,
				d = "power2.out";
			t.getAttribute("data-fade-offset") && (i = t.getAttribute("data-fade-offset")),
				t.getAttribute("data-duration") && (o = t.getAttribute("data-duration")),
				t.getAttribute("data-fade-from") && (e = t.getAttribute("data-fade-from")),
				t.getAttribute("data-on-scroll") && (r = t.getAttribute("data-on-scroll")),
				t.getAttribute("data-delay") && (s = t.getAttribute("data-delay")),
				t.getAttribute("data-ease") && (d = t.getAttribute("data-ease"));
			let g = { opacity: 0, ease: d, duration: o, delay: s };
			"top" == e && (g.y = -i), "left" == e && (g.x = -i), "bottom" == e && (g.y = i), "right" == e && (g.x = i), 1 == r && (g.scrollTrigger = { trigger: t, start: "top 85%" }), gsap.from(t, g);
		});
	}

	///////////////////////////////////////////////////
	// 39. tp-skill-pb-panel
	let pp = gsap.matchMedia();
	pp.add("(min-width: 1200px)", () => {
		if ($('.tp-skill-pb-panel-wrap').length) {
			let sections = gsap.utils.toArray(".tp-skill-pb-panel");
				gsap.to(sections, {
				xPercent: -100 * (sections.length - 1),
				ease: "none",
				scrollTrigger: {
					start: "top 0px",
					trigger: ".tp-skill-pb-panel-wrap",
					pin: true,
					scrub: 1,
					end: () => "+=" + document.querySelector(".tp-skill-pb-panel-wrap").offsetWidth
				}
			});
		}
	});

	///////////////////////////////////////////////////
	// 40. tp-text-perspective

	gsap.utils.toArray(".tp-text-perspective").forEach(splitTextLine => {
		const delay_value = parseFloat(splitTextLine.getAttribute("data-delay") || 0.5);
		const tl = gsap.timeline({
			scrollTrigger: {
				trigger: splitTextLine,
				start: 'top 85%',
				duration: 1.5,
				scrub: false,
				markers: false,
				toggleActions: 'play none none none'
			}
		});
		const itemSplitted = new SplitText(splitTextLine, { type: "lines" });
		gsap.set(splitTextLine, { perspective: 400 });
		itemSplitted.split({ type: "lines" });

		tl.from(itemSplitted.lines, {
			duration: 1,
			delay: delay_value,
			opacity: 0,
			rotationX: -80,
			force3D: true,
			transformOrigin: "top center -50",
			stagger: 0.1
		});
	});

	///////////////////////////////////////////////////
	// 41. fade-class-active
	if ($(".tp_fade_anim").length > 0) {
		gsap.utils.toArray(".tp_fade_anim").forEach((item) => {
			let tp_fade_offset = item.getAttribute("data-fade-offset") || 40,
				tp_duration_value = item.getAttribute("data-duration") || 0.75,
				tp_fade_direction = item.getAttribute("data-fade-from") || "bottom",
				tp_onscroll_value = item.getAttribute("data-on-scroll") || 1,
				tp_delay_value = item.getAttribute("data-delay") || 0.15,
				tp_ease_value = item.getAttribute("data-ease") || "power2.out",
				tp_anim_setting = {
					opacity: 0,
					ease: tp_ease_value,
					duration: tp_duration_value,
					delay: tp_delay_value,
					x: (tp_fade_direction == "left" ? -tp_fade_offset : (tp_fade_direction == "right" ? tp_fade_offset : 0)),
					y: (tp_fade_direction == "top" ? -tp_fade_offset : (tp_fade_direction == "bottom" ? tp_fade_offset : 0)),
			    };
			if (tp_onscroll_value == 1) {
				tp_anim_setting.scrollTrigger = {
					trigger: item,
					start: 'top 85%',
				};
			}
			gsap.from(item, tp_anim_setting);
		});
	}

	///////////////////////////////////////////////////
	// 42. has-scale-image
	if ($('.has-scale-image').length > 0) {
		$('.has-scale-image').each(function() {   
			var imgSrc = $(this).attr('data-img');
			var bgColor = $(this).attr('data-bgcolor');
			var hiddenImageDiv = $('<div class="hidden-image"></div>');
			hiddenImageDiv.css({
				'background-image': 'url(' + imgSrc + ')',
				'background-color': bgColor // AdaugÄ culoarea de fundal
			});
			$(this).append(hiddenImageDiv);
			
			gsap.to(this, {
				opacity: 1,
				scale:1,
				duration: 0.7, 
				stagger:0.5,
				ease: "back.out(4)",
				scrollTrigger: {
					trigger: this, 
					start: "top 90%", 						
				}
			});
			
			$(this).on('mouseenter', function() {
				gsap.to(this, {
					duration: 0.3, 
					borderRadius: '2px', 
					scale: 3,
					boxShadow: '0 2px 8px rgba(0, 0, 0, 0.3)' // Add box shadow here
				});
			}).on('mouseleave', function() {	
				gsap.to(this, {
					duration: 0.3, 
					borderRadius: '6px', 
					scale: 1,
					boxShadow: 'none' // Reset box shadow on mouse leave
				});
			});
			
		});
	}

	///////////////////////////////////////////////////
	// 43. Function Showcase Snap Slider

	function isMobile() {
		return /Mobi|Android|iPhone|iPad|iPod/i.test(navigator.userAgent);
	}

	document.addEventListener("DOMContentLoaded", function() {
		ShowcaseSnapSlider();
	});

	function ShowcaseSnapSlider() {
		
		if( $('.tp-snap-slider-holder').length > 0 ){
			
			// Selectors and utilities
			const snapSliderHolder = document.querySelector(".tp-snap-slider-holder");
			const snapSlides = gsap.utils.toArray(".tp-snap-slide");
			const snapSlidesImgMask = gsap.utils.toArray(".tp-snap-slide .img-mask");
			const snapCaptionWrapper = document.querySelector(".tp-snap-slider-captions");
			const snapCaptions = gsap.utils.toArray(".tp-snap-slide-caption");
			const snapThumbsWrapper = document.querySelector(".tp-snap-slider-thumbs");
			const snapThumbs = gsap.utils.toArray(".thumb-slide");
			
			
			gsap.fromTo(snapSlidesImgMask,
				{ opacity: 0.1},
				{
					duration: 1,
					opacity: 1,
					ease: "sine.out",
					scrollTrigger: {
						trigger: snapSliderHolder,
						start: 'top 100%',
						end: '+=100%',
						scrub: true,
					},
				}
			);
			
			
			gsap.fromTo(snapSlidesImgMask,
				{ opacity: 1},
				{
					duration: 1,
					opacity: 0.1,
					ease: "sine.out",
					scrollTrigger: {
						trigger: snapSliderHolder,
						start: 'bottom 100%',
						end: '+=100%',
						scrub: true,
					},
				}
			);
		
			// Pin and animate thumbnails
			ScrollTrigger.create({
				trigger: snapSlides,
				start: "top top",
				end: () => "+=" + innerHeight * (snapSlides.length - 1),
				pin: snapThumbsWrapper,
				scrub: true,
			});
		
			gsap.fromTo(
				snapThumbs,
				{ y: 0 },
				{
					y: -snapThumbs[0].offsetHeight * (snapThumbs.length - 1),
					scrollTrigger: {
						trigger: snapSliderHolder,
						scrub: true,
						start: "top top",
						end: "+=" + innerHeight * (snapSlides.length - 1),
					},
					ease: "none",
				}
			);
		
			// Pin and animate captions
			ScrollTrigger.create({
				trigger: snapCaptionWrapper,
				start: "top top",
				end: () => "+=" + innerHeight * (snapSlides.length - 1),
				pin: true,
				scrub: true,
			});
		
			gsap.fromTo(
				snapCaptions,
				{ y: 0 },
				{
					y: -snapCaptions[0].offsetHeight * (snapCaptions.length - 1),
					scrollTrigger: {
						trigger: snapSliderHolder,
						scrub: true,
						start: "top top",
						end: "+=" + innerHeight * (snapSlides.length - 1),
					},
					ease: "none",
				}
			);

		
			// Animate image transitions within slides
			snapSlides.forEach((slide, i) => {
				const imageWrappers = slide.querySelectorAll(".img-mask");
				const isLastSlide = i === snapSlides.length - 1;
				const isFirstSlide = i === 0;
		
				gsap.fromTo(
					imageWrappers,
					{ y: isFirstSlide ? 0 : -window.innerHeight },
					{
						y: isLastSlide ? 0 : window.innerHeight,
						scrollTrigger: {
							trigger: slide,
							scrub: true,
							start: isFirstSlide ? "top top" : "top bottom",
							end: isLastSlide ? "top top" : undefined,
						},
						ease: "none",
					}
				);
			});
		}		
	}
	

	///////////////////////////////////////////////////
	// 44. text-scale-anim

	const headings = document.querySelectorAll('.text-scale-anim');

    headings.forEach(heading => {
        const textNodes = [];

        heading.childNodes.forEach(node => {
            if (node.nodeType === Node.TEXT_NODE) {
                node.textContent.split(' ').forEach((word, index, array) => {
                    const wordSpan = document.createElement('span');
                    wordSpan.classList.add('tp-word-span');
                    word.split('').forEach(letter => {
                        const letterSpan = document.createElement('span');
                        letterSpan.classList.add('tp-letter-span');
                        letterSpan.textContent = letter;
                        wordSpan.appendChild(letterSpan);
                    });
                    textNodes.push(wordSpan);
                    if (index < array.length - 1) {
                        textNodes.push(document.createTextNode(' '));
                    }
                });
            } else if (node.nodeType === Node.ELEMENT_NODE) {
                textNodes.push(node.cloneNode(true));
            }
        });

        heading.innerHTML = '';
        textNodes.forEach(node => heading.appendChild(node));

        const letters = heading.querySelectorAll('.tp-letter-span');
        letters.forEach(letter => {
            $(letter).on('mouseenter', () => {
                gsap.to(letter, {
                    scaleY: 1.3,
                    y: '-14%',
                    duration: 0.2,
                    ease: 'sine'
                });
            });

            $(letter).on('mouseleave', () => {
                gsap.to(letter, {
                    scaleY: 1,
                    y: '0%',
                    duration: 0.2,
                    ease: 'sine'
                });
            });
        });
    });

	const headings_bottom = document.querySelectorAll('.text-scale-anim-bottom');

    headings_bottom.forEach(heading => {
        const textNodes = [];

        heading.childNodes.forEach(node => {
            if (node.nodeType === Node.TEXT_NODE) {
                node.textContent.split(' ').forEach((word, index, array) => {
                    const wordSpan = document.createElement('span');
                    wordSpan.classList.add('tp-word-span');
                    word.split('').forEach(letter => {
                        const letterSpan = document.createElement('span');
                        letterSpan.classList.add('tp-letter-span');
                        letterSpan.textContent = letter;
                        wordSpan.appendChild(letterSpan);
                    });
                    textNodes.push(wordSpan);
                    if (index < array.length - 1) {
                        textNodes.push(document.createTextNode(' '));
                    }
                });
            } else if (node.nodeType === Node.ELEMENT_NODE) {
                textNodes.push(node.cloneNode(true));
            }
        });

        heading.innerHTML = '';
        textNodes.forEach(node => heading.appendChild(node));

        const letters = heading.querySelectorAll('.tp-letter-span');
        letters.forEach(letter => {
            $(letter).on('mouseenter', () => {
                gsap.to(letter, {
                    scaleY: 1.3,
                    y: '14%',
                    duration: 0.2,
                    ease: 'sine'
                });
            });

            $(letter).on('mouseleave', () => {
                gsap.to(letter, {
                    scaleY: 1,
                    y: '0%',
                    duration: 0.2,
                    ease: 'sine'
                });
            });
        });
    });


	///////////////////////////////////////////////////
	// 45. tp-hero-md-col-custom
	$('.tp-hero-md-col-custom').on('mouseenter', function () {
		$(this).addClass('active').siblings().removeClass('active');
	})

	///////////////////////////////////////////////////
	// 46. tp-funfact-panel

	let fp = gsap.matchMedia();
	fp.add("(min-width: 1200px)", () => {
		if ($('.tp-funfact-panel-wrap').length) {
			let sections = gsap.utils.toArray(".tp-funfact-panel");
				gsap.to(sections, {
				xPercent: -100 * (sections.length - 1),
				ease: "none",
				scrollTrigger: {
					start: "top 70px",
					trigger: ".tp-funfact-panel-wrap",
					pin: true,
					scrub: 1,
					end: () => "+=" + document.querySelector(".tp-funfact-panel-wrap").offsetWidth
				}
			});
		}
	});

	///////////////////////////////////////////////////
	// 47. text bounce animation
	if ($('.tp-text-bounce-trigger').length > 0) {
		gsap.set(".tp-text-bounce", { y: 100, opacity: 0 });
		let mybtn = gsap.utils.toArray(".tp-text-bounce");
		mybtn.forEach((btn) => {
			let $this = $(btn);
			let tp_delay_value = $this[0].getAttribute("data-delay") || 0.15;
			gsap.to(btn, {
				scrollTrigger: {
					trigger: $this.closest('.tp-text-bounce-trigger'),
					start: "top center",
					markers: false
				},
				duration: 1,
				delay: parseFloat(tp_delay_value),
				ease: "back.out(1.7)",
				y: 0,
				opacity: 1,
			});
		});
	}

	/////////////////////////////////////////////////////
	// 48. rotate-text-anim

	const rotateText = document.querySelector(".rotate-text-anim");

		if (rotateText) {
		// SplitText apply only if element exists
		let headingTitle = new SplitText(rotateText, { type: "chars" });
		let headingChars = headingTitle.chars;

		let tHero = gsap.timeline({
			scrollTrigger: {
			trigger: rotateText,
			start: "top 80%",
			toggleActions: "play none none none",
			}
		});

		tHero.from(headingChars, {
			rotate: 20,
			ease: "back.out",
			opacity: 0,
			duration: 1,
			stagger: 0.1
		});
	}



	/////////////////////////////////////////////////////
	// 49. atropos-component-active
	class AtroposComponent extends HTMLElement {
		constructor() {
			super();
		}
		connectedCallback() {
		this.atropos = new Atropos({
			el: this.querySelector('.my-atropos'),
				onEnter() {},
				onLeave() {},
				onRotate(x, y) {}
			});
		}
			disconnectedCallback() {
			this.atropos.destroy();
		}
	}
	customElements.define('atropos-component', AtroposComponent);

	/////////////////////////////////////////////////////
	// 50. tp-text-revel-anim
	const td_anim_reveal = document.querySelectorAll(".tp-text-revel-anim");
	td_anim_reveal.forEach(areveal => {
		const getAttributeValue = (attr, defaultValue) => areveal.getAttribute(attr) || defaultValue;
		const duration_value = Number(getAttributeValue("data-duration", 1));
		const onscroll_value = Number(getAttributeValue("data-on-scroll", 1));
		const stagger_value = Number(getAttributeValue("data-stagger", 0.02));
		const data_delay = Number(getAttributeValue("data-delay", 0.08));
		const ease_value = getAttributeValue("data-ease", "circ.out");

		areveal.split = new SplitText(areveal, { type: "lines,words,chars", linesClass: "tp-revel-line" });
		const animationProps = {
			duration: duration_value,
			delay: data_delay,
			ease: ease_value,
			y: 40,
			stagger: stagger_value,
			opacity: 0,
		};

		if (onscroll_value === 1) {
			areveal.anim = gsap.from(areveal.split.chars, {
				scrollTrigger: {
					trigger: areveal,
					start: 'top 85%',
				},
				...animationProps,
			});
		} else {
			areveal.anim = gsap.from(areveal.split.chars, animationProps);
		}
	});


	/////////////////////////////////////////////////////
	// 51. Text Effect Animation
	if ($(".text-anim").length) {
		let staggerAmount = 0.03,
		translateXValue = 20,
		delayValue = 0.1,
		easeType = "power2.out",
		animatedTextElements = document.querySelectorAll(".text-anim");

		animatedTextElements.forEach((element) => {
			let animationSplitText = new SplitText(element, { type: "chars, words" });
			gsap.from(animationSplitText.chars, {
				duration: 1,
				delay: delayValue,
				x: translateXValue,
				autoAlpha: 0,
				stagger: staggerAmount,
				ease: easeType,
				scrollTrigger: { trigger: element, start: "top 85%" },
			});
		});
	}


	////////////////////////////////////////////////////
	// 52. tp-video-img-wrap
	if ($('.tp-video-brand-img-wrap-2').length > 0) {
		let ms = gsap.matchMedia();
		ms.add("(min-width: 768px)", () => {
			gsap.fromTo("#video video", 
				{
					scale: 0.14,     
					y: -334.66,                 
					borderRadius: '50rem'        
				}, 
				{
					scale: 1,   
					y: 0,  
					ease: "power2.out",
					borderRadius: '0rem',
					scrollTrigger: {
						trigger: "#video",
						start: "top 80%",      
						end: "top 20%",
						scrub: true,    
					}
				}
			);
		});
	}

	////////////////////////////////////////////////////
	// 53. tp-awards-vp-move-thumbs-wrapper
	
	if ($('.tp-awards-vp-move-thumbs-wrapper').length > 0) {
		if (!isMobile()) {
			const moveThumbsWrapper = document.querySelector('.tp-awards-vp-move-thumbs-wrapper');
			const startThumbsCaption = document.querySelector('.tp-awards-vp-start-thumbs-caption');
			const moveThumbsParent = document.querySelectorAll('.tp-awards-vp-start-thumbs-wrapper .tp-awards-vp-start-move-thumb');
			const moveThumbs = document.querySelectorAll('.tp-awards-vp-start-thumbs-wrapper .tp-awards-vp-move-thumb-inner');
			const overlappingThumbs = document.querySelectorAll('.tp-awards-vp-end-thumbs-wrapper .tp-awards-vp-end-move-thumb');

			function animateElements(moveThumbs, overlappingThumbs, moveThumbsParent) {
				moveThumbs.forEach((moveThumb, index) => {
					const state = Flip.getState(moveThumb);
					overlappingThumbs[index].appendChild(moveThumb);

					const moveAnimation = Flip.from(state, {
						duration: 1,
						ease: 'power4.inOut',
					});

					const startOffset = moveThumbsParent[index].dataset.start;
					const endOffset = moveThumbsParent[index].dataset.stop;

					ScrollTrigger.create({
						trigger: moveThumbsParent[index],
						start: startOffset,
						end: endOffset,
						scrub: true,
						animation: moveAnimation,
					});
				});

				//  Only run if element exists
				if (startThumbsCaption) {
					gsap.to(startThumbsCaption, {
						scrollTrigger: {
							trigger: startThumbsCaption,
							start: () => {
								const startPin = (window.innerHeight - startThumbsCaption.offsetHeight) / 2;
								return "top +=" + startPin;
							},
							end: () => {
								return "+=" + window.innerHeight;
							},
							pin: true,
							pinSpacing: false,
							scrub: true,
						},
						opacity: 0,
						ease: "power1.inOut",
					});
				}
			}

			animateElements(
				Array.from(moveThumbs),
				Array.from(overlappingThumbs),
				Array.from(moveThumbsParent)
			);
		}
	}
				

	////////////////////////////////////////////////////
	// 54. tp-service-vp-item
	const aw = gsap.matchMedia();
	aw.add("(min-width: 991px)", () => {
		const awardItems = document.querySelectorAll('.tp-service-vp-item');
		awardItems.forEach(function(div){
			div.addEventListener('mouseenter', function() {
				gsap.to(div, {
					width: '100%',
					duration: 2,
					ease: 'expo.out'
				});
			});
			div.addEventListener('mouseleave', function() {
				gsap.to(div, {
					width: '73%',
					duration: 2,
					ease: 'expo.out'
				});
			});
		})
	});


	////////////////////////////////////////////////////
	// 55. tp-vimeo-video-init
	const vimeoMap = new Map();

	windowOn.on("load resize", function () {
		resizeVideoContainers();
	});
	
	function resizeVideoContainers() {
		$(".video-container").each(function () {
			let container = $(this),
				iframe = container.find("iframe"),
				containerWidth = container.outerWidth(),
				containerHeight = container.outerHeight(),
				newHeight;

			if (containerWidth / containerHeight < 16 / 9) {
				// container is taller (narrow width relative to height)
				newHeight = containerHeight;
				iframe.css({
					width: "300%", 
					height: newHeight + "px"
				});
			} else {
				// container is wider or equal ratio
				newHeight = containerWidth / (16 / 9);
				iframe.css({
					width: 3 * containerWidth + "px",  // 3 times container width
					height: newHeight + "px"
				});
			}
		});
	}

	$("body").on("mouseenter", ".hover-play", function () {
		const $container = $(this);
		const $vimeo = $container.find('iframe[src*="vimeo.com"]');
		if ($vimeo.length) {
			const el = $vimeo[0];

			function safePlay(player) {
				if ($container.is(":hover")) {
					player.play().catch(err => {
						if (err.name === "PlayInterrupted") {
							console.log("Vimeo play interrupted – likely due to rapid hover toggle. Ignored.");
						} else {
							console.error("Vimeo play error:", err);
						}
					});
				}
			}

			if (typeof Vimeo === "undefined") {
				if (!window.vimeoLoadedOnce) {
					window.vimeoLoadedOnce = true;
					const script = document.createElement("script");
					script.src = "https://player.vimeo.com/api/player.js";
					script.onload = () => {
						initVimeoPlayer($vimeo, safePlay);
					};
					document.body.appendChild(script);
				}
			} else {
				const entry = vimeoMap.get(el);
				if (entry && entry.ready) {
					safePlay(entry.player);
				} else {
					initVimeoPlayer($vimeo, safePlay);
				}
			}
		}
	});

	$("body").on("mouseleave", ".hover-play", function () {
		const $vimeo = $(this).find('iframe[src*="vimeo.com"]');
		if ($vimeo.length) {
			const entry = vimeoMap.get($vimeo[0]);
			if (entry && entry.ready) {
				setTimeout(() => {
					entry.player.pause();
				}, 100);
			}
		}
	});

	// Updated initVimeoPlayer function to accept callback
	function initVimeoPlayer($iframe, onReadyCallback = null) {
		const el = $iframe[0];
		if (!vimeoMap.has(el)) {
			const player = new Vimeo.Player(el);
			player.ready()
				.then(() => {
					vimeoMap.set(el, { player, ready: true });
					if (typeof onReadyCallback === "function") {
						onReadyCallback(player);
					}
				})
				.catch(err => console.error("Vimeo init error:", err));
			vimeoMap.set(el, { player, ready: false });
		}
	}

	////////////////////////////////////////////////////
	// 56. tp-vimeo-video-perspective
	
	document.addEventListener("DOMContentLoaded", function() {
		const $projects = $(".project-item.project-style-3.hover-play");

		if ($projects.length === 0) return;

		$projects.css({
			perspective: "1500px",
			"transform-style": "preserve-3d",
			overflow: "visible",
		});

		function updateTransform() {
			if (window.innerWidth < 1024) {
			$projects.each(function () {
				const $inner = $(this).find(".project-item-inner");
				$inner.css({ transform: "none", transition: "none" });
				const $img = $inner.find(".tp-portfolio-vp-post-thumbnail img");
				$img.css({ transform: "none", transition: "none" });
			});
			} else {
			const windowHeight = window.innerHeight;
			$projects.each(function () {
				const $project = $(this);
				const $inner = $project.find(".project-item-inner");

				if ($inner.length === 0) return;

				const rect = this.getBoundingClientRect();
				let percent = (rect.top + rect.height / 2 - windowHeight / 2) / (windowHeight / 2);
				percent = Math.max(-1, Math.min(percent, 1));

				const rotateX = 30 * -percent + 0.756;
				const scale = (0.996976 - 0.105 * Math.abs(percent)).toFixed(6);

				$inner.css({
				transform: `rotateX(${rotateX.toFixed(3)}deg) scale3d(${scale}, ${scale}, 1)`,
				transition:
					"transform 0.6s cubic-bezier(0.25, 1, 0.5, 1), opacity 0.6s cubic-bezier(0.25, 1, 0.5, 1)",
				"transform-style": "preserve-3d",
				"will-change": "transform, opacity",
				});

				const $img = $inner.find(".tp-portfolio-vp-post-thumbnail img");
				if ($img.length) {
				const translateY = 20 * percent;
				$img.css({
					transform: `translateY(${translateY.toFixed(2)}px)`,
					transition: "transform 0.6s cubic-bezier(0.25, 1, 0.5, 1)",
					"will-change": "transform",
				});
				}
			});
			}
		}

		let ticking = false;

		updateTransform();

		$(window).on("scroll resize", function () {
			if (!ticking) {
			window.requestAnimationFrame(() => {
				updateTransform();
				ticking = false;
			});
			ticking = true;
			}
		});
	});

	////////////////////////////////////////////////////
	// 57. tp-portfolio-revealing-slide

	(function () {
		var $slides = document.querySelectorAll('.tp-portfolio-revealing-slide');
		var $controls = document.querySelectorAll('.tp-portfolio-revealing-slider-control');
		var numOfSlides = $slides.length;
		var slidingAT = 1300;
		var slidingBlocked = false;

		[].slice.call($slides).forEach(function ($el, index) {
			var i = index + 1;
			$el.classList.add('tp-portfolio-revealing-slide-' + i);
			$el.dataset.slide = i;
		});

		if (!$slides.length) return;
		$slides[0].classList.add('s-active');

		[].slice.call($controls).forEach(function ($el) {
			$el.addEventListener('click', controlClickHandler);
		});

		// Scroll support
		window.addEventListener('wheel', function (e) {
			if (slidingBlocked) return;

			if (e.deltaY > 0) {
				changeSlide(true);
			} else if (e.deltaY < 0) {
				changeSlide(false);
			}
		});

		function controlClickHandler() {
			if (slidingBlocked) return;
			var isRight = this.classList.contains('m-right');
			changeSlide(isRight, this);
		}

		function changeSlide(isRight, $controlEl) {
			slidingBlocked = true;

			var $curActive = document.querySelector('.tp-portfolio-revealing-slide.s-active');
			if (!$curActive) {
				console.warn('No active slide found.');
				slidingBlocked = false;
				return;
			}

			var index = +$curActive.dataset.slide;
			isRight ? index++ : index--;
			if (index < 1) index = numOfSlides;
			if (index > numOfSlides) index = 1;
			var $newActive = document.querySelector('.tp-portfolio-revealing-slide-' + index);

			if ($controlEl) {
				$controlEl.classList.add('a-rotation');
			}

			$curActive.classList.remove('s-active', 's-active-prev');
			document.querySelector('.tp-portfolio-revealing-slide.s-prev')?.classList.remove('s-prev');

			$newActive.classList.add('s-active');
			if (!isRight) $newActive.classList.add('s-active-prev');

			var prevIndex = index - 1;
			if (prevIndex < 1) prevIndex = numOfSlides;
			document.querySelector('.tp-portfolio-revealing-slide-' + prevIndex).classList.add('s-prev');

			var direction = isRight ? 1 : -1;
			var currentHeading = $curActive.querySelector('.tp-portfolio-revealing-slide-heading');
			var nextHeading = $newActive.querySelector('.tp-portfolio-revealing-slide-heading');

			if (currentHeading && nextHeading) {
				gsap.timeline()
					.to(currentHeading, {
						scaleX: 2,
						xPercent: 20 * direction,
						duration: 1,
						ease: 'power3.inOut'
					}, 0)
					.fromTo(nextHeading, {
						scaleX: 2,
						xPercent: -10 * direction
					}, {
						scaleX: 1,
						xPercent: 0,
						duration: 1,
						ease: 'power3.inOut'
				}, 0);
			}

			setTimeout(function () {
				if ($controlEl) $controlEl.classList.remove('a-rotation');
				slidingBlocked = false;
			}, slidingAT * 0.75);
		}
	})();

	////////////////////////////////////////////////////
	// 58. perspective-slider animation

	function perspective() {
        if ($('.tp-perspective-slider').length) {

            gsap.set('.tp-perspective-slider .tp-perspective-main .tp-perspective-inner', { perspective: 60 });

            $('.tp-perspective-slider .tp-perspective-main .tp-perspective-inner .tp-perspective-image').each(function () {
                var slide = $(this);

                gsap.fromTo(this, {
                    scaleX: 1,
                    z: '0vh'
                }, {
                    scaleX: 1,
                    z: '-2vh',
                    scrollTrigger: {
                        trigger: slide,
                        start: "top+=150px bottom",
                        end: "bottom top",
                        immediateRender: false,
                        scrub: 0.1,
                    }
                });
            });

        }
    }
    perspective()

	////////////////////////////////////////////////////
	// 59. ttp-port-slider-title

	$('.tp-port-slider-title').on("mouseenter", function () {
		$('#tp-port-slider-wrap').removeClass().addClass($(this).attr('rel'));
		$(this).addClass('active').siblings().removeClass('active');
	});

	////////////////////////////////////////////////////
	// 60. tp-porfolio-10-title-wrap
	$('.tp-porfolio-10-title-wrap > ul > li').on('mouseenter', function(){
		$(this).siblings().removeClass('active');
		const rel = $(this).attr('rel');
		$(this).addClass('active');
		$('#tp-porfolio-10-bg-box').removeClass().addClass(rel);
	})

	////////////////////////////////////////////////////
	// 61. portfolio item pin js 
	let pi = gsap.matchMedia();
	pi.add("(min-width: 1200px)", () => {
		document.querySelectorAll('.tp-pd-3-portfolio-item-wrap').forEach((group) => {
			const panels = group.querySelectorAll('.tp-pd-3-portfolio-item');
			const pinTarget = group.querySelector('.tp-pd-3-content-pin');
			panels.forEach((section) => {
				ScrollTrigger.create({
					trigger: section,
					pin: pinTarget,
					start: 'top 20%',
					end: 'bottom center',
					scrub: 1,
					pinSpacing: false,
					markers: false,
				});
			});
		});
	});

	////////////////////////////////////////////////////
	// 62. Password Toggle Js 
	if ($('#password-show-toggle').length > 0) {
		var btn = document.getElementById('password-show-toggle');
		btn.addEventListener('click', function (e) {
			var inputType = document.getElementById('tp_password');
			var openEye = document.getElementById('open-eye');
			var closeEye = document.getElementById('close-eye');

			if (inputType.type === "password") {
				inputType.type = "text";
				openEye.style.display = 'block';
				closeEye.style.display = 'none';
			} else {
				inputType.type = "password";
				openEye.style.display = 'none';
				closeEye.style.display = 'block';
			}
		});
	}

	////////////////////////////////////////////////////
	// 63. E-commerce plus minus js
	function tp_ecommerce() {
		$('.tp-cart-minus').on('click', function () {
		  const $input = $(this).siblings('input');
		  let count = Number($input.val()) - 1;
		  $input.val(count < 1 ? 1 : count).change();
		});
	  
		$('.tp-cart-plus').on('click', function () {
		  const $input = $(this).siblings('input');
		  $input.val(Number($input.val()) + 1).change();
		});
	  
		$('.tp-color-variation-btn').on('click', function () {
		  $(this).addClass('active').siblings().removeClass('active');
		});

		//  tpReturnCustomerLoginForm //
		$('.tp-checkout-login-form-reveal-btn').on('click', function () {
			$('#tpReturnCustomerLoginForm').slideToggle(400);
		  });
		
		//  Show Coupon Toggle Js //
		$('.tp-checkout-coupon-form-reveal-btn').on('click', function () {
		$('#tpCheckoutCouponForm').slideToggle(400);
		});
	
		// Create An Account Toggle Js //
		$('#cbox').on('click', function () {
			$('#cbox_info').slideToggle(900);
		});
	
		// Shipping Box Toggle Js //
		$('#ship-box').on('click', function () {
			$('#ship-box-info').slideToggle(1000);
		});
	}
	tp_ecommerce();

	////////////////////////////////////////////////////
	// 64. brand img animation
	if ($('.tp-brand-inner-area').length) {
	gsap.set(".tp-brand-inner-item img", { scale: 1.3, opacity: 0 });
		gsap.to(".tp-brand-inner-item img", {
			scale: 1,
			opacity: 1,
			duration: 1.5,
			ease: "bounce.out",
			stagger: {
				each: 0.1
			},
			scrollTrigger: {
				trigger: '.tp-brand-inner-area',
				start: "top 100%"
			}
		})
	}

	////////////////////////////////////////////////////
	// 65. tp-map-pin
	document.querySelectorAll('.tp-map-pin').forEach(pin => {
		pin.addEventListener('mouseenter', function () {
			document.querySelectorAll('.tp-map-pin.active').forEach(activePin => {
			activePin.classList.remove('active');
			});
			this.classList.add('active');
		});
	});

	// update-js-here

	////////////////////////////////////////////////////
	// 66. price toggle animation
	function tabtable_active_1() {
		const $elements = {
			monthly: $("#al-nav-monthly"),
			yearly: $("#al-nav-yearly"),
			switcher: $("#al-switcher-input"),
			tabMonthly: $("#al-tab-monthly"),
			tabYearly: $("#al-tab-yearly")
		};

		const setActive = isMonthly => {
			$elements.switcher.prop("checked", isMonthly);
			$elements.monthly.toggleClass("is-active", isMonthly);
			$elements.yearly.toggleClass("is-active", !isMonthly);
			$elements.tabMonthly.toggleClass("al-tab-hide", !isMonthly);
			$elements.tabYearly.toggleClass("al-tab-hide", isMonthly);
		};

		[$elements.monthly, $elements.yearly].forEach($el =>
			$el.on("click", () => setActive($el.is($elements.monthly)))
		);

		$elements.switcher.on("click", () =>
			setActive(!$elements.monthly.hasClass("is-active"))
		);
	}
	if ($("#al-nav-monthly").length) tabtable_active_1();

	////////////////////////////////////////////////////
	// 67. button effect

	var mouse = { x: 0, y: 0 };
	var pos = { x: 0, y: 0 };
	var ratio = 0.65;
	var active = false;

	var allParalax = document.querySelectorAll('.parallax-wrap');

	allParalax.forEach(function (e) {
		e.addEventListener("mousemove", mouseMoveBtn);
	})

	function mouseMoveBtn(e) {
		var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
		mouse.x = e.pageX;
		mouse.y = e.pageY - scrollTop;

	}
	gsap.ticker.add(updatePosition);

	$(".parallax-wrap").mouseenter(function (e) {
		gsap.to(this, { duration: 0.3, scale: 2 });
		gsap.to($(this).children(), { duration: 0.3, scale: 0.5 });
		active = true;
	});

	$(".parallax-wrap").mouseleave(function (e) {
		gsap.to(this, { duration: 0.3, scale: 1 });
		gsap.to($(this).children(), { duration: 0.3, scale: 1, x: 0, y: 0 });
		active = false;
	});

	function updatePosition() {
		pos.x += (mouse.x - pos.x) * ratio;
		pos.y += (mouse.y - pos.y) * ratio;

	}


	$(".parallax-wrap").mousemove(function (e) {
		parallaxCursorBtn(e, this, 2);
		callParallaxBtn(e, this);
	});

	function callParallaxBtn(e, parent) {
		parallaxItBtn(e, parent, parent.querySelector(".parallax-element"), 20);
	}

	function parallaxItBtn(e, parent, target, movement) {
		var boundingRect = parent.getBoundingClientRect();
		var relX = e.pageX - boundingRect.left;
		var relY = e.pageY - boundingRect.top;
		var scrollTop = window.pageYOffset || document.documentElement.scrollTop;

		gsap.to(target, {
			duration: 0.3,
			x: (relX - boundingRect.width / 2) / boundingRect.width * movement,
			y: (relY - boundingRect.height / 2 - scrollTop) / boundingRect.height * movement,
			ease: Power2.easeOut
		});
	}

	function parallaxCursorBtn(e, parent, movement) {
		var rect = parent.getBoundingClientRect();
		var relX = e.pageX - rect.left;
		var relY = e.pageY - rect.top;
		var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
		pos.x = rect.left + rect.width / 2 + (relX - rect.width / 2) / movement;
		pos.y = rect.top + rect.height / 2 + (relY - rect.height / 2 - scrollTop) / movement;
	}


	var moveSection = $(".tp-mouse-move-section");
	var moveSectionElements = $(".tp-mouse-move-element");

	if (moveSection && moveSectionElements) {
		moveSection.on("mousemove", function (e) {
			var x = e.clientX
			var y = e.clientY
			let viewportWidth = window.innerWidth;
			let viewportHeight = window.innerHeight;
			let center = viewportWidth / 2
			let centerHeight = innerHeight / 2

			if (x > center) {
				gsap.to(moveSectionElements, {
					x: 20,
					duration: 2,
					ease: "power4.out"
				})
			}
			else {
				gsap.to(moveSectionElements, {
					x: -20,
					scale: 1.1,
					duration: 2,
					ease: "power4.out"
				})
			}
			if (y > centerHeight) {
				gsap.to(moveSectionElements, {
					y: 5,
					duration: 2,
					ease: "power4.out"
				})
			}
			else {
				gsap.to(moveSectionElements, {
					y: -5,
					duration: 2,
					ease: "power4.out"
				})
			}
		});
	}

	////////////////////////////////////////////////////
	// 68. hover reveal start

	$('.al-project-pg-2-item').on("mouseenter", function () {
		$(this).addClass('active').siblings().removeClass('active');
	});

	$('.al-project-pg-2-item').on("mouseleave", function () {
		$('.al-project-pg-2-item').removeClass('active').addClass('active');
	});

	////////////////////////////////////////////////////
	// 69. watter effact here
	if ($(".ripple-image").length > 0) {
		imagesLoaded(".ripple-image img", { background: true }, () => {
			$(".ripple-image").each(function () {
				var $container = $(this);
				var $img = $container.find("img").first();
				var imgURL = $img.attr("src");
				$container.css({
					"background-image": "url(" + imgURL + ")",
					"background-size": "cover",
					"background-position": "center center"
				});
				$container.ripples({
					resolution: 400,
					perturbance: 0.03,
					imageUrl: imgURL
				});
				$img.hide();
			});
		});
	}

	// update-2-all-js-here


	////////////////////////////////////////////////////
	// 70. ar-service-item
	$('.ar-service-item').on('mouseenter', function () {
		$(this).addClass('active').siblings('.ar-service-item').removeClass('active');
	});

	////////////////////////////////////////////////////
	// 71. ar-scroll-image
	if ($('.ar-scroll-image').length > 0) {
		gsap.to(".ar-scroll-image", {
			xPercent: -10,
			scrollTrigger: {
				trigger: ".ar-banner-shape",
				start: "top bottom",
				end: "bottom top",
				scrub: true, 
			}
		});
	}

	////////////////////////////////////////////////////
	// 72. cnt-portfolio-ptb

	if ($('.cnt-portfolio-ptb').length) {
		let mm = gsap.matchMedia();
	
		mm.add("(min-width: 992px)", () => {
		// Common ScrollTrigger options
		const baseOptions = {
			scrub: 1,
			start: 'top 0%',
			end: 'bottom 80%',
			endTrigger: '.cnt-portfolio-ptb',
			pinSpacing: false,
			markers: false,
		};
	
		// For px-step-item
		document.querySelectorAll('.cnt-portfolio-video-wrapper').forEach((item, i) => {
			gsap.to(item, {
			scrollTrigger: {
				trigger: item,
				pin: item,
				...baseOptions,
			}
			});
		});
	
		// For px-step-card with left/right rotation
		document.querySelectorAll('.cnt-portfolio-video-card').forEach((card, i) => {
			let rotateValue = i % 2 === 0 ? -5 : 5;
	
			gsap.to(card, {
			rotate: rotateValue,
				scrollTrigger: {
					trigger: card,
					pin: card,
					...baseOptions,
					start: 'top 10%',
					end: 'bottom 110%'
				}
				});
			});
		});
	}

	////////////////////////////////////////////////////
	// 73. tp-gsap-bg
	if ($('.tp-gsap-bg').length > 0) {
		gsap.set(".tp-gsap-bg", { scaleX: 1 });
		let mm = gsap.matchMedia();
		mm.add("(min-width:1400px)", () => {
			gsap.to(".tp-gsap-bg", {
				scrollTrigger: {
					trigger: ".tp-gsap-bg",
					scrub: 0.2, 
					start: "top 90%", 
					end: "bottom 100%",
				},
				scaleX: 0.85,
				borderRadius: "60px",
				transformOrigin: "center center",
				ease: "none",
			});
		});
	}
	if ($('.tp-gsap-image').length > 0) {
		let mm = gsap.matchMedia();
		mm.add("(min-width:1200px)", () => {
			gsap.to(".tp-gsap-image", {
				scrollTrigger: {
					trigger: ".tp-gsap-image",
					scrub: 0.3,
					start: "top 95%",
					end: "bottom 100%",
				},
				borderRadius: "400px",
				transformOrigin: "center center",
				ease: "none",
			});
		});
	}


})(jQuery);

