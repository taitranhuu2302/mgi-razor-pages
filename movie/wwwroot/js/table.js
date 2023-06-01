const inputs = document.querySelectorAll(".checkbox-control input");

const hideShowColumnTable = (n, type) => {
    const table = document.querySelector(".table")
    const head = table.querySelectorAll("thead tr th");
    const body = table.querySelectorAll(`tbody tr`);

    if (type === 'hidden') {
        head[n].classList.add('hidden')

        body.forEach(tr => {
            const columns = tr.querySelectorAll("td");

            columns.forEach((c, index) => {
                if (index === n) {
                    c.classList.add('hidden')
                }
            })
        })
    } else {
        head[n].classList.remove('hidden')

        body.forEach(tr => {
            const columns = tr.querySelectorAll("td");

            columns.forEach((c, index) => {
                if (index === n) {
                    c.classList.remove('hidden')
                }
            })
        })
    }

}

inputs.forEach((input, index) => {
    input.addEventListener('change', () => {
        const val = input.checked;
        console.log(index, val)
        if (val) {
            hideShowColumnTable(index, 'hidden')
        } else {
            hideShowColumnTable(index, 'show')
        }
    })
})