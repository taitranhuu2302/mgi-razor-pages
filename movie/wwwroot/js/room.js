const editButtons = document.querySelectorAll(".table [data-modal-target='modal-edit-room']");
editButtons.forEach((btn) => {
    btn.addEventListener('click', () => {
        const obj = JSON.parse(btn.getAttribute('data-obj'));
        const modal = document.querySelector('#modal-edit-room');
        console.log(modal)

        modal.querySelector("[name='Id']").value = obj.id;
        modal.querySelector("[name='Name']").value = obj.name;
        modal.querySelector("[name='Seats']").value = obj.seats;
        modal.querySelector("[name='RoomStatus']").value = obj.roomStatus === 0 ? "Available" : "Busy";
        modal.querySelector("[name='UserId']").value = obj.userModel.id;
        modal.querySelector("[name='StartTime']").value = obj.startTime;
        modal.querySelector("[name='EndTime']").value = obj.endTime;
    })
})