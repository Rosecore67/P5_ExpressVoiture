function confirmDelete(deleteUrl) {
    if (confirm("Êtes-vous sûr de vouloir supprimer cette réparation ?")) {
        window.location.href = deleteUrl;
    }
}