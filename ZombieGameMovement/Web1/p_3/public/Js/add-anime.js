document.getElementById("anime_form").addEventListener("submit",handleForm);

function handleForm(event) {
  event.preventDefault(); // Prevents the form from submitting

  const genres = document.getElementById("genres");
  const selectedGenres = [];

  for (const option of genres.options) {
    if (option.selected) {
      selectedGenres.push(option.value);
    }
  }

  const newItem = {
    itemId: 0,
    img: document.getElementById("img").value,
    alt: document.getElementById("title").value + " Picture",
    title: document.getElementById("title").value,
    description: document.getElementById("description").value,
    type: document.getElementById("type").value,
    episode: document.getElementById("episode").value,
    start: document.getElementById("start").value,
    end: document.getElementById("end").value,
    status: document.getElementById("status").value,
    producers: document.getElementById("producers").value,
    licensors: document.getElementById("licensors").value,
    studios: document.getElementById("studios").value,
    genres: selectedGenres,
    duration: document.getElementById("duration").value,
    rating: document.getElementById("rating").value,
    score: document.getElementById("score").value,
    trailer: "qwertz",
  };
  
  // Send the data to the server
  fetch("/submit-anime", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(newItem)
  })
  .then(response => response.json())
  .then(data => {
    console.log("Success:", data);
    alert("New item added successfully!");
  })
  .catch((error) => {
    console.error("Error:", error);
    alert("Error submitting the form.");
  });

}
