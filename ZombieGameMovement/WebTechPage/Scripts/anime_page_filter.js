const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get('id');
console.log(id);

// Load JSON data
fetch('data.json')
    .then(response => response.json())
    .then(data => {
        // Find item with matching ID
        const item = data.find(item => item.id === parseInt(id));

        // Build HTML to display item
        let html = `<ul><li>Name: ${item.name}</li><li>Age: ${item.age}</li><li>City: ${item.city}</li></ul>`;

        // Display HTML
        document.getElementById('output').innerHTML = html;
    })
    .catch(error => console.error('Error reading JSON file:', error));


