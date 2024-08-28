    // Gestion de la disponibilité et de la date de disponibilité
    var estDisponibleSelect = document.querySelector('select[name="EstDisponible"]');
    var dateDisponibiliteInput = document.querySelector('input[name="DateDisponibiliteVente"]');

    function toggleDateDisponibiliteValidation() {
        if (estDisponibleSelect.value === "false") {
            dateDisponibiliteInput.removeAttribute('required');
            dateDisponibiliteInput.setAttribute('disabled', 'disabled');
        } else {
            dateDisponibiliteInput.setAttribute('required', 'required');
            dateDisponibiliteInput.removeAttribute('disabled');
        }
    }

    estDisponibleSelect.addEventListener('change', toggleDateDisponibiliteValidation);
    toggleDateDisponibiliteValidation(); // Initial call to set the correct state

    // Prévisualisation de l'image
    function previewImage(event) {
        var reader = new FileReader();
        reader.onload = function () {
            var output = document.getElementById('imagePreview');
            output.src = reader.result;
            output.style.display = 'block';
        };
        reader.readAsDataURL(event.target.files[0]);
    }

    document.querySelector('input[type="file"]').addEventListener('change', previewImage);
});
