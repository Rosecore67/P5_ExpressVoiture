function confirmDelete(deleteUrl, message) {
    if (confirm(message)) {
        window.location.href = deleteUrl;
    }
}