document.addEventListener('DOMContentLoaded', function () {
    // Ouvrir le calendrier dès que l'utilisateur clique sur le champ
    document.querySelectorAll('input[type="date"]').forEach(function (element) {
        element.addEventListener('focus', function () {
            this.showPicker();
        });
    });

    // Supprimer la valeur par défaut si c'est une date très ancienne
    document.querySelectorAll('input[type="date"]').forEach(function (element) {
        if (element.value === "0001-01-01") {
            element.value = "";
        }
    });
});