function pageFilter(id){
    fetch("../Data/manga_data.json")
        .then(response => response.json())
        .then(data => {
            const item = data.find(item => item.pageId == id);
            console.log("Mi");
            let html = '';
            if (item) {
                console.log("da fak?");
                item.items.forEach(subItem => {
                    html += `
                    <a href="manga_description" class="card" itemId="${subItem.itemId}" onclick="setItemId(this.getAttribute('itemId'))">
                        <img src="${subItem.img}" alt="${subItem.alt}" class="card-image">
                        <div class="title-container">
                            <h3 class="title">${subItem.title}</h3>
                        </div>
                    </a>
                    `;
                });
            } else {
                html = '<p>No data found for the selected item ID.</p>';
            }
            console.log(" a gyász.");
            document.getElementById('content').innerHTML = html;
        })
        .catch(error => console.error('Error reading JSON file:', error));
 }

 function setItemId(itemId){
    localStorage.setItem('itemId', itemId);
 }

const urlParams = new URLSearchParams(window.location.search);
const id = "0"
localStorage.setItem('pageId', id);
        
pageFilter(id);
//if(id === null){
//   loadData("Top 10 Anime");
//}
//else{
//    loadData(id);
//}