// Add click handler to side-navbar
$(function () {
    $("div#sideNavbar a").click(function () {
        // Clear all other li's active status
        $(this).parent().siblings().removeClass("active");
        // Set itself the active one
        $(this).parent("li").addClass("active");
    });
});

// Add mouse over and out handlers
$(function () {
    $("div#sideNavbar").hover(
        function () {
            // For mouse enter
            $(this).animate({ "width": "200px" }, 500);
        },
        function () {
            // For mouse out
            $(this).animate({ "width": "40px" }, 500);
        });
});
