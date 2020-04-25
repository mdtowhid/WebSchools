function myFunction() {
    var x = document.getElementById("myTopnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }
}














//Codes for index page top menu
$(document).ready(function () {
    //jQury index index
    $('.trigger').click(function () {
        $('.trigger_element').slideToggle("slow");
    });

    
    //smooth scrolling functionality;
    $('#header').localScroll();

    $('#navbar').localScroll();
    $('#myTopnav').localScroll();

    var slideIndex = 0;
    var slides = document.getElementsByClassName('slides_box');
    var i;

    function showSlides() {
        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }

        slideIndex++;
        if (slideIndex > slides.length) { slideIndex = 1 }
        slides[slideIndex - 1].style.display = "block";
        setTimeout(showSlides, 10000);
    }
    showSlides();
    
});