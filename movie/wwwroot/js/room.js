import {clearDataClose, clearInputData, closeModalForm, handleDelete, modalEvent, setListError} from "./site.js";

const URL = "/api/rooms"
const formCreate = document.querySelector("#create-room");

const query = {
    count: 0,
    currentPage: 1,
    pageSize: 5
}

const fetchData = async (params) => {
    const response = await fetch(`${URL}?pageSize=${params?.pageSize ?? "5"}&currentPage=${params?.currentPage ?? "1"}`).then(res => res.json())
    query.currentPage = response.currentPage;
    query.pageSize = response.pageSize;
    query.count = response.count
    document.querySelector("#rooms").innerHTML = response.data.map((item) => {
        return `
             <tr>
                <td>
                    <div class="table-text">${item.id}</div>
                </td>
                <td>
                    <div class="table-text">${item.name}</div>
                </td>
                <td>
                    <div class="table-text">${item.seats}</div>
                </td>
                <td>
                    <div class="table-text">${item.startTime ?? ""}</div>
                </td>
                <td>
                    <div class="table-text">${item.endTime ?? ""}</div>
                </td>
                <td>
                    <div class="table-text">${item.userModel ? item.userModel.name : ""}</div>
                </td>
                <td>
                    <div class="table-text">${item.roomStatus === 0 ? 'Available' : 'Busy'}</div>
                </td>
                <td>
                    <div class="flex items-center">
                        <button class="btn" data-obj='${JSON.stringify(item)}' data-modal-target="modal-edit-room">
                            <i class="fas fa-edit"></i>
                        </button>
                        <button type="button" data-obj='${JSON.stringify(item)}' data-modal-target="modal-confirm-delete" class="btn btn-confirm">
                            <i class="fas fa-trash"></i>
                        </button>
                    </div>
                </td>
            </tr>
        `
    }).join("")

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
            modal.querySelector("[name='UserId']").value = obj.userModel?.id;
            modal.querySelector("[name='StartTime']").value = obj.startTime;
            modal.querySelector("[name='EndTime']").value = obj.endTime;
        })
    })

    modalEvent(() => {
        clearDataClose(formCreate)
    })
    handleDelete(URL, () => {
        fetchData()
    })
}
fetchData().then(() => {
    paginateHandler()
})


formCreate.addEventListener('submit', async (e) => {
    e.preventDefault();
    const name = formCreate.querySelector("input[name='Name']");
    const seats = formCreate.querySelector("input[name='Seats']");
    const request = {
        name: name.value,
        seats: seats.value || 0,
    }
    const response = await fetch(URL, {
        method: "post",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(request)
    }).then(res => res.json())
    if (response.status === 400) {
        setListError(response, formCreate)
    } else {
        clearInputData([name, seats])
        await fetchData()
        closeModalForm(e.target)
    }
})

const formEdit = document.querySelector("#edit-room");

formEdit.addEventListener('submit', async (e) => {
    e.preventDefault();
    const id = formEdit.querySelector("input[name='Id']");
    const name = formEdit.querySelector("input[name='Name']");
    const seats = formEdit.querySelector("input[name='Seats']");
    const startTime = formEdit.querySelector("input[name='StartTime']");
    const endTime = formEdit.querySelector("input[name='EndTime']");
    const userId = formEdit.querySelector("select[name='UserId']");
    const roomStatus = formEdit.querySelector("select[name='RoomStatus']");
    const request = {
        id: id.value,
        name: name.value,
        seats: seats.value ||  0,
        startTime: startTime.value || null,
        endTime: endTime.value || null,
        userId: userId.value || null,
        roomStatus: roomStatus.value === 'Available' ? 0 : 1,
    }

    const response = await fetch(URL, {
        method: "put",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(request)
    }).then(res => res.json())
    if (response.status === 400) {
        setListError(response, formEdit)
    } else {
        await fetchData();
        closeModalForm(e.target)
    }
})


function paginateHandler() {
    const btns = document.querySelectorAll(".btn-pagi");


    btns.forEach(btn => {
        if (btn.getAttribute('data-listen')) return;
        btn.setAttribute('data-listen', 'listen')

        btn.addEventListener('click', async (e) => {
            e.preventDefault()
            const type = btn.getAttribute('data-type')
            const totalPage = Math.ceil(query.count / query.pageSize);
            switch (type) {
                case 'prev':
                    if (query.currentPage > 1) {
                        await fetchData({...query, currentPage: query.currentPage - 1})
                    }
                    break;
                case 'next':
                    if (query.currentPage < totalPage) {
                        await fetchData({...query, currentPage: query.currentPage + 1})
                    }
                    break;
                case 'first':
                    await fetchData({...query, currentPage: 1})
                    break;
                case 'last':
                    await fetchData({...query, currentPage: totalPage})
                    break;
            }
            document.querySelector("#current-page").innerText = query.currentPage
        })
    })

    document.querySelector(".select-per-page").addEventListener('change', async (e) => {
        query.pageSize = e.target.value;
        await fetchData({currentPage: query.currentPage, pageSize: query.pageSize})
    })
}