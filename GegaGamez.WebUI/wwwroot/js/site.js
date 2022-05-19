// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const modalOpenedClass = "opened";

// Edit profile Modal
function showEditProfileModal() {
    const editProfileModal = document.getElementById("edit-profile-modal");
    editProfileModal.classList.add(modalOpenedClass);
}

function hideEditProfileModal() {
    const editProfileModal = document.getElementById("edit-profile-modal");
    editProfileModal.classList.remove(modalOpenedClass);
}

// deleteCommentModal
function showDeleteCommentModal(commentId) {
    const deleteCommentModal = document.getElementById("delete-comment-modal");

    const idField = deleteCommentModal.querySelector("input[name=id]");

    idField.value = commentId;

    deleteCommentModal.classList.add(modalOpenedClass);
}

function hideDeleteCommentModal() {
    const deleteCommentModal = document.getElementById("delete-comment-modal");
    deleteCommentModal.classList.remove(modalOpenedClass);
}

// Move to collection Modal

function showCollectionModal(gameId=null) {
    const showCollectionModal = document.getElementById("move-game-to-collection-modal");

    if (gameId != null) {
        const idField = showCollectionModal.querySelector("input[name=id]");

        idField.value = gameId;
    }

    showCollectionModal.classList.add(modalOpenedClass);
}

function hideCollectionModal() {
    const moveToCollectionModel = document.getElementById("move-game-to-collection-modal");

    moveToCollectionModel.classList.remove(modalOpenedClass);
}

function showRemoveFromCollectionModal(gameId) {
    const removeFromCollectionModal = document.getElementById("remove-game-from-collection-modal");

    const idField = removeFromCollectionModal.querySelector("input[name=id]");

    idField.value = gameId;

    removeFromCollectionModal.classList.add(modalOpenedClass);
}

function hideRemoveFromCollectionModal() {
    const removeFromCollectionModal = document.getElementById("remove-game-from-collection-modal");

    removeFromCollectionModal.classList.remove(modalOpenedClass);
}

function showRateGameModal(gameId = null) {
    const rateGameModal = document.getElementById("rate-game-modal");
    if (gameId != null) {
        const idField = rateGameModal.querySelector("input[name=id]");
        idField.value = gameId;
    }

    rateGameModal.classList.add(modalOpenedClass);
}

function hideRateGameModal() {
    const rateGameModal = document.getElementById("rate-game-modal");

    rateGameModal.classList.remove(modalOpenedClass);
}
