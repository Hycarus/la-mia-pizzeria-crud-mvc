//document.addEventListener("DOMContentLoaded", function () {
//    const buttons = document.querySelectorAll(".cancel-button");
//    buttons.forEach((button) => {
//        button.addEventListener("click", (event) => {
//            const dataId = button.getAttribute("data-id");
//            const dataName = button.getAttribute("data-name");

//            const modal = document.getElementById("deleteModal");
//            const bootstrapModal = new bootstrap.Modal(modal);
//            bootstrapModal.show();

//            const title = document.getElementById("modal-item-title");
//            title.textContent = dataName;

//            const form = document.getElementById("deleteForm");
//            form.setAttribute("action", `/Pizza/Delete/${dataId}`);
//        });
//    });
//});

//$.ajax({
//    url: '@Url.Action("Delete", "Pizza")', // Assicurati che l'azione e il controller siano corretti
//    type: 'POST',
//    data: { id: pizzaId },
//    success: function () {
//        $('#deletePizzaModal').modal('hide');
//        location.reload();
//    },
//    error: function () {
//        alert('Errore nella cancellazione.');
//    }

