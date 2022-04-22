// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Edit profile Modal
function showEditProfileModal() {
    const editProfileModal = document.getElementById("edit-profile-modal");
    editProfileModal.classList.add("opened");
}

function hideEditProfileModal() {
    const editProfileModal = document.getElementById("edit-profile-modal");
    editProfileModal.classList.remove("opened");
}

// deleteCommentModal
function showDeleteCommentModal(commentId) {
    const deleteCommentModal = document.getElementById("delete-comment-modal");

    const idField = deleteCommentModal.querySelector("input[name=id]");

    idField.value = commentId;

    deleteCommentModal.classList.add("opened");
}

function hideDeleteCommentModal() {
    const deleteCommentModal = document.getElementById("delete-comment-modal");
    deleteCommentModal.classList.remove("opened");
}

// Move to collection Modal

function showCollectionModal(gameId) {
    const showCollectionModal = document.getElementById("move-game-to-collection-modal");

    const idField = showCollectionModal.querySelector("input[name=id]");

    idField.value = gameId;

    showCollectionModal.classList.add("opened");
}

function hideCollectionModal() {
    const moveToCollectionModel = document.getElementById("move-game-to-collection-modal");

    moveToCollectionModel.classList.remove("opened");
}

function showRemoveFromCollectionModal(gameId) {
    const removeFromCollectionModal = document.getElementById("remove-game-from-collection-modal");

    const idField = removeFromCollectionModal.querySelector("input[name=id]");

    idField.value = gameId;

    removeFromCollectionModal.classList.add("opened");
}

function hideRemoveFromCollectionModal() {
    const removeFromCollectionModal = document.getElementById("remove-game-from-collection-modal");

    removeFromCollectionModal.classList.remove("opened");
}
