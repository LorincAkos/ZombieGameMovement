$(document).ready(function() {
    $("#anime_page").click(function() {

        window.location.href = `anime.html?id=${$("#anime_page").attr("itemID")}`;
    });

    $("#home_page").click(function() {
        window.location.href = `index.html`;
    });

    $(".anime-item").mouseenter(function(){
        $(this).css({
            width: '35%',
        });
    });

    $(".anime-item").mouseleave(function(){
        $(this).css({
            width: '30%',
        });
    });
    
});

function openDisplayPage(itemId) {
    window.location.href = `anime.html?id=${itemId}`;
}
