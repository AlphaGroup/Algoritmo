// Set side nav bar
$(function () {
    $("div#sideNavbar li").removeClass("active");
    $("div#sideNavbar li:contains('Animation')").addClass("active");
    $("div#sideNavbar").css("width", "200px");
});

// Set time interval
$(function () {
    var $input = $("input#interval");
    // The interval has been set.
    if ($.cookie("interval")) {
        $input.val($.cookie("interval"));
    } else {
        $input.val(GetInterval());
        $.cookie("interval", $input.val(), { path: '/' });
    }
    $input.change(function () {
        $.cookie("interval", parseInt($(this).val()), { path: '/' });
    });
});