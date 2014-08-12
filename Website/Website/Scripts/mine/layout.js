$(function () {
    $("div#sideNavbar a").click(function () {
        // Clear all other li's active status
        $(this).parent().siblings().removeClass("active");
        // Set itself the active one
        $(this).parent("li").addClass("active");
    });
});