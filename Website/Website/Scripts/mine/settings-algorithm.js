$(function() {
    $("div#sideNavbar li").removeClass("active");
    $("div#sideNavbar li:contains('Algorithm')").addClass("active");
    $("div#sideNavbar").css("width", "200px");
});