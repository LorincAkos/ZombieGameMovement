$(document).ready(function() {
    $("#anime_page").click(function() {

        window.location.href = `anime.html?id=${$("#anime_page").attr("value")}`;
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

    $(".card").click(function() {
        console.log(1);
    });
    
});

function openDisplayPage(itemId) {
    window.location.href = `anime.html?id=${itemId}`;
}

function openDescriptionPage(id,itemId) {
    window.location.href = `description.html?id=${id}&itemId=${itemId}`;
}

