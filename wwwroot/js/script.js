function showNotesInterface() {
    var notesInterface = document.getElementById("note-interface");
    var newNoteInterface = document.getElementById("new-note");
    if(notesInterface.style.display === "none") {
        notesInterface.style.display = "inline-block";
    } else {
        notesInterface.style.display = "none";
        if(newNoteInterface.style.display !== "none") {
            newNoteInterface.style.display = "none";
        }
    }
}

function showNewNoteInterface() {
    var newNoteInterface = document.getElementById("new-note");
    if(newNoteInterface.style.display === "none") {
        newNoteInterface.style.display = "inline-block";
    } else {
        newNoteInterface.style.display = "none";
    }
}

function clearNewNoteInfo() {
    var newNoteInterface = document.getElementById("new-note");

    document.getElementsByName("Title")[0].value = "";
    document.getElementsByName("Description")[0].value = "";
    newNoteInterface.style.display = "none";
}

var id = null;
var userId = null;

function openUpdateNoteInterface(button) {
    id = button.parentNode.getAttribute("valueA");
    userId = button.parentNode.getAttribute("valueB");

    console.log(id);
    console.log(userId);
    
    var note = document.querySelector(".note", `valueA=${id}`);
    var title = note.querySelector(".note-title").innerHTML;
    var description = note.querySelector(".note-body").innerHTML;

    var newNoteInterface = document.getElementById("new-note");
    if(newNoteInterface.style.display === "none") {
        newNoteInterface.style.display = "inline-block";
    }

    document.querySelector('textarea[name="Title"]').value = title;
    document.querySelector('textarea[name="Description"]').value = description;
}

const slidesModal = document.getElementById("main-slideshow");
const modalBackground = document.getElementById("dialog-background");

function showSlides() {
    slidesModal.showModal();
    modalBackground.style.display = "inline-block";
}

function closeSlides() {
    slidesModal.close();
    modalBackground.style.display = "none";
}