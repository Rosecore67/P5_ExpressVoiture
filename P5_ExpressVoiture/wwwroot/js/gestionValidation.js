function confirmDelete(event, deleteUrl, message) {
    event.preventDefault(); // Empêche la soumission automatique du formulaire

    if (confirm(message)) {
        window.location.href = deleteUrl;
    }
}