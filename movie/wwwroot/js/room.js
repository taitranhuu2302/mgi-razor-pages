const editButtons = document.querySelectorAll(".table [data-modal-target='modal-edit-room']");
editButtons.forEach((btn) => {
    btn.addEventListener('click', () => {
        const obj = JSON.parse(btn.getAttribute('data-obj'));
        const modal = document.querySelector('#modal-edit-room');
        console.log(modal)

        modal.querySelector("[name='id']").value = obj.id;
        modal.querySelector("[name='name']").value = obj.name;
    })
})