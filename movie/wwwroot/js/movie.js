import {clearDataClose, clearInputData, closeModalForm, handleDelete, modalEvent, setListError} from "./site.js";

const formCreate = document.querySelector("#create-movie");
const formEdit = document.querySelector("#edit-movie");

const URL = '/api/movies'
const query = {
    count: 0,
    currentPage: 1,
    pageSize: 5
}

const fetchData = async (params) => {
    const response = await fetch(`${URL}?pageSize=${params?.pageSize ?? "5"}&currentPage=${params?.currentPage ?? "1"}`).then(res => res.json())
    document.querySelector("#movies").innerHTML = response.data.map((item) => {
        return `
             <tr>
                <td>
                    <div class="table-text">${item.id}</div>
                </td>
                <td>
                    <div class="table-text">
                        <img src="${item.image}" alt="" style="height: 100px" onerror="this.onerror=null; this.src='https://phutungnhapkhauchinhhang.com/wp-content/uploads/2020/06/default-thumbnail.jpg'">
                    </div>
                </td>
                <td>
                    <div class="table-text">${item.name}</div>
                </td>
                <td>
                    <div class="table-text">$${item.price}</div>
                </td>
                <td>
                    <div class="flex items-center">
                        <button class="btn" data-obj='${JSON.stringify(item)}' data-modal-target="modal-edit-movie">
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
    query.currentPage = response.currentPage;
    query.pageSize = response.pageSize;
    query.count = response.count

    const editButtons = document.querySelectorAll(".table [data-modal-target='modal-edit-movie']");
    editButtons.forEach((btn) => {
        btn.addEventListener('click', () => {
            const obj = JSON.parse(btn.getAttribute('data-obj'));
            const modal = document.querySelector('#modal-edit-movie');

            modal.querySelector("[name='Id']").value = obj.id;
            modal.querySelector("[name='Name']").value = obj.name;
            modal.querySelector("[name='Image']").value = obj.image;
            modal.querySelector("[name='Price']").value = obj.price;
        })
    })

    handleDelete(URL, () => {
        fetchData()
    })
    modalEvent(() => {
        clearDataClose(formCreate)
    })
}
fetchData().then(r => {
    paginateHandler()
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


formCreate.addEventListener('submit', async (e) => {
    e.preventDefault();
    const name = formCreate.querySelector("input[name='Name']");
    const image = formCreate.querySelector("input[name='Image']");
    const price = formCreate.querySelector("input[name='Price']");
    const ticketCount = formCreate.querySelector("input[name='TicketCount']");
    const roomId = formCreate.querySelector("select[name='RoomId']");
    const request = {
        name: name.value,
        image: image.value,
        price: price.value || 0,
        ticketCount: ticketCount.value || 0,
        roomId: roomId.value,
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
        clearInputData([name, image, price, ticketCount])
        await fetchData()
        closeModalForm(e.target)
    }
})


formEdit.addEventListener('submit', async (e) => {
    e.preventDefault();
    const id = formEdit.querySelector("input[name='Id']");
    const name = formEdit.querySelector("input[name='Name']");
    const image = formEdit.querySelector("input[name='Image']");
    const price = formEdit.querySelector("input[name='Price']");

    const request = {
        id: id.value,
        name: name.value,
        image: image.value,
        price: price.value || 0,
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
