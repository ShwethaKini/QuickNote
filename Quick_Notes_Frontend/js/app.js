let url = 'https://localhost:7271/quickNotes';
let username = 'admin';
let password = 'admin';

let headers = new Headers();

headers.append('Content-Type', 'application/json');
headers.append('Authorization', 'Basic ' + btoa(username + ":" + password));
showNotes();

// if a user add a note then add it to the db
let btn = document.getElementById("btn");


btn.addEventListener("click", async function (e) {
    let addtext = document.getElementById("addtext");

    let response = await fetch(url, {
    method: 'POST',
    mode: 'cors',
    headers: headers,
    body: JSON.stringify({message: addtext.value})
})
if(response.status == 201){
    showNotes();
} else if (response.status >= 400){
    alert("Some error occured please try again after some time");
    await response.text().then(text => { console.log(text) })
}
    
})

let notes = [];
async function showNotes() {
    
    let response = await fetch(url, {
        method: 'GET',
        mode: 'cors',
        headers: headers,
        
    });
    if(response.status == 200){
    let data = await response.json();
    notes = data.items;
    }
    else if(response.status == 204){
        notes= null;
    }
    else if (response.status >= 400){
        alert("Some error occured please try again after some time");
        await response.text().then(text => { console.log(text) })
    }
    
    if (notes == null) {
        notesObj = [];
    }
    else {
        notesObj = notes;
        notesObj.forEach(x=>{
            x.createdOn = new Date(x.createdOn).toLocaleDateString('en-US');
        })
    };

    let html = "";
    notesObj.forEach(function (element, index) {
        html += `
    <div class="noteCard my-2 mx-2 card" style="width: 18rem;">
    <!-- <img src="..." class="card-img-top" alt="..."> -->
    <div class="card-body" style="background-color:rgb(168, 203, 219)">
        <h5 class="card-title">Note ${index + 1}</h5>
        <p class="card-text">${element.message}</p>
        <small style="font-size:smaller; color:cadetblue;">CreatedOn: ${element.createdOn }</small>
        <br>
        <button id="${index}_Edit" onclick="editNote('${element.id}')" class="btn btn-primary">Edit Note</button>
        <button id="${index}_Delete" onclick="deleteNote('${element.id}')" class="btn btn-primary">Delete Note</button>
    </div>
</div>
    `;
    });
    let notesElm = document.getElementById("note");
    if (notesObj.length != 0) {
        notesElm.innerHTML = html;
        notesElm.style.color="black"
        notesElm.style.textDecoration="none"
    }
    else{
        notesElm.innerHTML="sorry nothing to show please use the above box to set your own notes !!!"
        notesElm.style.color="black"
    }
};

async function deleteNote(index){
    console.log("iam deleting",index);
    let response = await fetch(`https://localhost:7271/quickNotes/${index}`, {
        method: 'DELETE',
        headers: headers,
    });
     if(response.status == 200) {
        showNotes();
     } else if (response.status >= 400 ){
        alert("Some error occured please try again after some time");
        await response.text().then(text => { console.log(text) })
    }
}

var overlayme = document.getElementById("dialog-container");
document.getElementById("confirm").onclick = function(){confirm()};
document.getElementById("cancel").onclick = function(){cancel()};
var content = document.getElementById("popup-content");
let item = null;

async function editNote(index){  
    item = notes.filter(x => {
        if(x.id == index){
            return x;
        }
    })
    overlayme.style.display = "block"; 
    content.value = item[0].message;

}

async function confirm() {
    overlayme.style.display = "none";
    let response = await fetch(`https://localhost:7271/quickNotes/${item[0].id}`, {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify({message: content.value})
    });
     if(response.status == 200) {
        showNotes();
     } else if (response.status >= 400){
        alert("Some error occured please try again after some time");
        await response.text().then(text => { console.log(text) })
    } 
       
   }

   function cancel() {
       overlayme.style.display = "none";
   }
