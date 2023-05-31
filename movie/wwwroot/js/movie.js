const editButtons = document.querySelectorAll(".table [data-modal-target='modal-edit-movie']");
editButtons.forEach((btn) => {
    btn.addEventListener('click', () => {
        const obj = JSON.parse(btn.getAttribute('data-obj'));
        const modal = document.querySelector('#modal-edit-movie');

        modal.querySelector("[name='id']").value = obj.id;
        modal.querySelector("[name='name']").value = obj.name;
        modal.querySelector("[name='image']").value = obj.image;
        modal.querySelector("[name='price']").value = obj.price;
    })
})