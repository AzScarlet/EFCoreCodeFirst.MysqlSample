function modalCommand(modal_id, command) {
    modal = document.getElementById(modal_id);
    if (command) {
        modal.style.display = "block";
    }
    else {
        modal.style.display = "none";
    }
}