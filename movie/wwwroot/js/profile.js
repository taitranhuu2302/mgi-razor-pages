import {handleDelete, modalEvent} from "./site.js";

const URL = '/api/ticket'
const query = {
    count: 0,
    currentPage: 1,
    pageSize: 5
}

const fetchData = async (params) => {
    const response = await fetch(`${URL}?pageSize=${params?.pageSize ?? "5"}&currentPage=${params?.currentPage ?? "1"}`).then(res => res.json())
    console.log(response)
    document.querySelector("#ticket").innerHTML = response.data.map((item) => {
        return `
             <tr>
                <td>
                    <div class="table-text">${item.id}</div>
                </td>
                <td>
                    <div class="table-text">
                        <img src="${item.movie.image}" alt="" class="h-[100px]" onerror="this.onerror=null; this.src='https://phutungnhapkhauchinhhang.com/wp-content/uploads/2020/06/default-thumbnail.jpg'">
                    </div>
                </td>
                <td>
                    <div class="table-text">${item.movie.name}</div>
                </td>
                <td>
                    <div class="table-text">${item.room.name}</div>
                </td>
                <td>
                    <div class="table-text">$${item.movie.price}</div>
                </td>
                <td>
                    <button type="button" data-obj='${JSON.stringify(item)}' data-modal-target="modal-confirm-delete" class="btn btn-confirm">
                        <i class="fas fa-trash"></i>
                    </button>
                </td>
            </tr>        
        `
    }).join("")
    query.currentPage = response.currentPage;
    query.pageSize = response.pageSize;
    query.count = response.count

    handleDelete(URL, () => {
        fetchData()
    })
    modalEvent()

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
                        await fetchData({currentPage: query.currentPage - 1})
                    }
                    break;
                case 'next':
                    if (query.currentPage < totalPage) {
                        await fetchData({currentPage: query.currentPage + 1})
                    }
                    break;
                case 'first':
                    await fetchData({currentPage: 1})
                    break;
                case 'last':
                    await fetchData({currentPage: totalPage})
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