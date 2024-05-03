const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get('id');
const itemId = urlParams.get('itemId');
// összeraknia két paramétert majd splittelni ahogy kell. Igy csak a value-nak kell megadni és el lehet érni amit kell. very gud :p
console.log(id);
const sum = id +"|"+ itemId;
console.log(sum.split('|')[1]);


fetch("anime_data.json")
            .then(response => response.json())
            .then(data => {
                // Find the object with matching itemid
                const item = data.find(item => item.pageTitle === id);
                let html = '';
                let sidebarList = '';
                // Check if item1 is found
                if (item) {
                    item.items.forEach(subItem => {
                        sidebarList += `
                        <li><button value="${subItem.itemId}" onclick="openDescriptionPage(pageTitle.getAttribute('itemId'),this.getAttribute('itemId'))">${subItem.title}</button></li>
                        `;
                    });

                    // Find the item with id 1 within item1's items array
                    const subItem = item.items.find(subItem => subItem.itemId === parseInt(itemId));

                    // Check if subItem1 is found
                    if (subItem) {
                        console.log("Item with itemid 1 and id 1 found:");
                        console.log(subItem);
                        html += `<div>
                                    <p>${subItem.title}</p>
                                    <p>${subItem.description}</p>
                                    <p>${subItem.start}</p>
                                    <p>${subItem.end}</p>
                                    <p>${subItem.episodes}</p>
                                </div>
                                `
                    } else {
                        html += "Page not found!"
                        console.log("Item with id 1 not found within item with itemid 1.");
                    }
                } else {
                    html += "Page not found!"
                    console.log("Item with itemid 1 not found.");
                }

                // Display HTML
                
                document.getElementById("content").innerHTML = html;
                document.getElementById("sidebar-list").innerHTML = sidebarList;
            })
            .catch(error => console.error('Error reading JSON file:', error));


