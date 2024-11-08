function loadData(id,itemId){
    console.log(id +"\t" +itemId);
fetch("../Data/anime_data.json")
            .then(response => response.json())
            .then(data => {
                const item = data.find(item => item.pageId == id);
                console.log(item);

                if (item) {

                    const subItem = item.items.find(subItem => subItem.itemId == itemId);

                    if (subItem) {
                        console.log(subItem);
                                
                        document.getElementById("title").innerHTML = `<strong>${subItem.title}</strong>`;
                        document.getElementById("score").innerHTML = `<span>${subItem.score}</span>`;
                        document.getElementById("image").innerHTML = `<img src="${subItem.img}" class="desc-img" alt="${subItem.alt}">`;
                        document.getElementById("description").innerHTML += `<span>${subItem.description}</span>`;
                        // document.getElementById("episodes").innerHTML = `<span>${subItem.episodes}</span>`;
                        // document.getElementById("type").innerHTML = `<span>${subItem.type}</span>`;
                        // document.getElementById("status").innerHTML = `<span>${subItem.status}</span>`;
                        // document.getElementById("airing-date").innerHTML = `<span>${subItem.start} to ${subItem.end}</span>`;
                        // document.getElementById("producers").innerHTML = `<span>${subItem.producers}</span>`;
                        // document.getElementById("licensors").innerHTML = `<span>${subItem.licensors}</span>`;
                        // document.getElementById("studios").innerHTML = `<span>${subItem.studios}</span>`;
                        // document.getElementById("genres").innerHTML = `<span>${subItem.genres}</span>`;
                        // document.getElementById("duration").innerHTML = `<span>${subItem.duration}</span>`;
                        // document.getElementById("rating").innerHTML = `<span>${subItem.rating}</span>`;
                        // document.getElementById("video").innerHTML = `<source src="${subItem.trailer}" type="video/mp4"></source>`;
                    }
                    else {
                        document.getElementById("title").innerHTML = "Data not found!"
                        console.log("Item with id 1 not found within item with itemid 1.");
                    }
                } else {
                    document.getElementById("title").innerHTML = "Page not found!"
                    console.log("Item with itemid 1 not found.");
                }

                
            })
            .catch(error => console.error('Error reading JSON file:', error));
    }


const id = localStorage.getItem("pageId");
const itemId = localStorage.getItem("itemId");


loadData(id,itemId);