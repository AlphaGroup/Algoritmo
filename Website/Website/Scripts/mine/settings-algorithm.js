// Set nav bar
$(function () {
    $("div#sideNavbar li").removeClass("active");
    $("div#sideNavbar li:contains('Algorithm')").addClass("active");
    $("div#sideNavbar").css("width", "200px");
});

// Set radio click handler.
$(function () {
    $("div.radio :radio").click(function () {
        $.cookie("algorithm", $(this).val());
    });
});