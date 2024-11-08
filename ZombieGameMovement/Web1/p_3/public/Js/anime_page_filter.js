function pageFilter(){
    let content = '';
    fetch("../Data/anime_data.json")
        .then(response => response.json())
        .then(data => {

            console.log(id);
            if(id == null){
                let itemArray = [];
                let seasonArray = []
                for (let i = 0; i < data.length; i++) {


                    let item = data.find(x => x.pageId == i);
                    if (item) {
                        item.items.forEach(x => {
                            itemArray.push(x);
                        })

                        let newSeason = {
                            pageId: item.pageId,
                            pageTitle: item.pageTitle
                        }
                        seasonArray.push(newSeason);
                    }

                    else {
                        content = '<p>No data found for the selected item ID.</p>';
                    }
                }
                //Sort by title
                itemArray.sort((a, b) => a.title.localeCompare(b.title));
                //Sort by score
                //array.sort((a, b) => a.score.localeCompare(b.score));
                for (let i = 0; i < itemArray.length; i++) {
                    content += `
                    <a href="anime_description" class="card" itemId="${itemArray[i].itemId}" onclick="setItemId(this.getAttribute('itemId'))">
                    ` + 
                    //<img src="${subItem.img}" alt="${subItem.alt}" class="card-image">
                    `<div class="title-container">
                    <h3 class="title">${itemArray[i].title}</h3>
                    </div>
                    </a>
                    `;
                }

                document.getElementById('content').innerHTML = content;    
            }
            else{

                const item = data.find(x => x.pageId == id);
                
                if (item) {
                    item.items.forEach(subItem => {
                        content += `
                        <a href="anime_description" class="card" itemId="${subItem.itemId}" onclick="setItemId(this.getAttribute('itemId'))">
                        ` + 
                        //<img src="${subItem.img}" alt="${subItem.alt}" class="card-image">
                        `<div class="title-container">
                        <h3 class="title">${subItem.title}</h3>
                        </div>
                        </a>
                        `;
                    });
                }
                else {
                    content = '<p>No data found for the selected item ID.</p>';
                }
            
                document.getElementById('content').innerHTML = content;
            }
        })
        .catch(error => console.error('Error reading JSON file:', error));
 }

 function setItemId(itemId){
    localStorage.setItem('itemId', itemId);
 }

 function setPageId(itemId){
    localStorage.setItem('pageId', itemId);
 }

const id = localStorage.getItem("pageId");
//localStorage.setItem("pageId",0);
        
pageFilter();
//if(id === null){
//   loadData("Top 10 Anime");
//}
//else{
//    loadData(id);
//}