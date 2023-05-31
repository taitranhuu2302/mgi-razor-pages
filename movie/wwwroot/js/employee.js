const editButtons = document.querySelectorAll(".table [data-modal-target='modal-edit-employee']");
editButtons.forEach((btn) => {
    btn.addEventListener('click', () => {
        const obj = JSON.parse(btn.getAttribute('data-obj'));
        const modal = document.querySelector('#modal-edit-employee');

        modal.querySelector("[name='id']").value = obj.id;
        modal.querySelector("[name='name']").value = obj.name;
        modal.querySelector("[name='email']").value = obj.email;
        modal.querySelector("[name='password']").value = obj.password;
    })
})