// Set side nav bar
$(function () {
    $("div#sideNavbar li").removeClass("active");
    $("div#sideNavbar li:contains('Animation')").addClass("active");
    $("div#sideNavbar").css("width", "200px");
});