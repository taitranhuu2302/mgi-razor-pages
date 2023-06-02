import {clearDataClose, clearInputData, closeModalForm, handleDelete, modalEvent, setListError} from "./site.js";
const formCreate = document.querySelector("#create-employee");
const formEdit = document.querySelector("#edit-employee");

const query = {
    count: 0,
    currentPage: 1,
    pageSize: 5
}
const URL = '/api/users'
const fetchData = async (params) => {
    const response = await fetch(`${URL}?pageSize=${params?.pageSize ?? "5"}&currentPage=${params?.currentPage ?? "1"}`).then(res => res.json())
    query.currentPage = response.currentPage;
    query.pageSize = response.pageSize;
    query.count = response.count
    document.querySelector("#employee").innerHTML = response.data.map((item) => {
        return `
            <tr>
                <td>
                    <div class="table-text">${item.id}</div>
                </td>
                <td >
                    <div class="table-text">${item.name}</div>
                </td>
                <td >
                    <div class="table-text">${item.email}</div>
                </td>
                <td >
                    <div class="table-text">
                          <span>${item.month ?? ""} ${item.day ? item.day + ", " : ""}${item.year ?? ""}</span>
                    </div>
                </td>
                <td >
                    <div class="table-text">${item.role === 0 ? "Admin" : "User"}</div>
                </td>
                <td >
                    <div class="flex items-center">
                        <button class="btn" data-obj='${JSON.stringify(item)}' data-modal-target="modal-edit-employee">
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

    const editButtons = document.querySelectorAll(".table [data-modal-target='modal-edit-employee']");
    editButtons.forEach((btn) => {
        btn.addEventListener('click', () => {
            const obj = JSON.parse(btn.getAttribute('data-obj'));
            const modal = document.querySelector('#modal-edit-employee');
            modal.querySelector("[name='Id']").value = obj.id;
            modal.querySelector("[name='Name']").value = obj.name;
            modal.querySelector("[name='Email']").value = obj.email;
            modal.querySelector("[name='Password']").value = obj.password;
        })
    })

    modalEvent(() => {
        clearDataClose(formCreate)
    })
    handleDelete('/api/users', () => {
        fetchData()
    })
}
fetchData().then(() => {
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
    const email = formCreate.querySelector("input[name='Email']");
    const password = formCreate.querySelector("input[name='Password']");
    const day = formCreate.querySelector("select[name='Day']");
    const month = formCreate.querySelector("select[name='Month']");
    const year = formCreate.querySelector("select[name='Year']");
    const request = {
        name: name.value,
        email: email.value,
        password: password.value,
        day: day.value === 'Day' ? "" : day.value,
        month: month.value === 'Month' ? "" : month.value,
        year: year.value === 'Year' ? "" : year.value,
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
        clearInputData([name, email, password])
        await fetchData()
        closeModalForm(e.target)
    }
})


formEdit.addEventListener('submit', async (e) => {
    e.preventDefault();
    const id = formEdit.querySelector("input[name='Id']");
    const name = formEdit.querySelector("input[name='Name']");
    const email = formEdit.querySelector("input[name='Email']");
    const password = formEdit.querySelector("input[name='Password']");
    const day = formEdit.querySelector("select[name='Day']");
    const month = formEdit.querySelector("select[name='Month']");
    const year = formEdit.querySelector("select[name='Year']");
    const request = {
        id: id.value,
        name: name.value,
        email: email.value,
        password: password.value,
        day: day.value === 'Day' ? "" : day.value,
        month: month.value === 'Month' ? "" : month.value,
        year: year.value === 'Year' ? "" : year.value,
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

