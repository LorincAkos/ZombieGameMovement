function seasonLoader() {
  let season = "";
  let seasonArray = [];
  fetch("../Data/anime_data.json")
    .then((response) => response.json())
    .then((data) => {
      for (let i = 0; i < data.length; i++) {
        let item = data.find((x) => x.pageId == i);
        if (item) {
          let newSeason = {
            pageId: item.pageId,
            pageTitle: item.pageTitle,
          };
          seasonArray.push(newSeason);
        } else {
          content = "<p>No data found for the selected item ID.</p>";
        }
      }

      for (let i = 0; i < seasonArray.length; i++) {
        season += `
            <a href="anime_page" class="season-button" itemId="${seasonArray[i].pageId}" onclick="setPageId(this.getAttribute('itemId'))">
                ${seasonArray[i].pageTitle}
            </a>
            `;
      }

      document.getElementById("seasons").innerHTML = season;
    });
}

seasonLoader();

var coll = document.getElementById("collapsible-button");

  coll.addEventListener("click", function() {
    this.classList.toggle("active");
    var content = this.nextElementSibling;
    if (content.style.display === "grid") {
      content.style.display = "none";
    } else {
      content.style.display = "grid";
    }
  });

