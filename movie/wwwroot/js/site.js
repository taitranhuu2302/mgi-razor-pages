const listError = document.querySelectorAll(".list-error");

const removeError = () => {
    listError.forEach((err) => {
        err.innerHTML = ""
    })
}

export const modalEvent = () => {
    document.querySelectorAll('[data-modal-target]').forEach(btn => {
        if (btn.getAttribute('data-listen')) return;
        btn.setAttribute('data-listen', 'listen')
        
        btn.addEventListener('click', (e) => {
            e.preventDefault()
            
            const value = btn.getAttribute('data-modal-target');
            const modal = document.querySelector(`#${value}`);
            modal.classList.toggle('active')
            removeError();

            window.addEventListener('click', (e) => {
                if (e.target === modal) {
                    modal.classList.remove('active');
                    removeError();
                }
            })

            window.addEventListener('keydown', (e) => {
                if (e.key === 'Escape') {
                    modal.classList.remove('active');
                    removeError();
                }
            })
        })
    });
}
export const setListError = (response, form) => {
    const errorArr = Object.values(response.errors).flatMap(value => value.map(v => String(v)));
    console.log(errorArr)
    form.querySelector(".list-error").innerHTML = errorArr.map(error => {
        return `
                <span class="text-error">${error}</span>
            `
    }).join("")
}

export const clearInputData = (arr) => {
    arr.forEach(i => {
        i.value = ""
    })
}

export const handleDelete = (url, callback) => {
    const buttonConfirm = document.querySelectorAll(".btn-confirm");

    buttonConfirm.forEach(btn => {
        btn.addEventListener('click', () => {
            const modal = document.querySelector("#modal-confirm-delete")
            modal.setAttribute("data-obj", btn.getAttribute('data-obj'))
        })
    })

    const modal = document.querySelector("#modal-confirm-delete");
    modal.querySelector(".btn-submit").addEventListener('click', async () => {
        const data = JSON.parse(modal.getAttribute("data-obj"))
        await fetch(`${url}/${data.id}`, {
            method: 'DELETE',
        }).then(res => res.json());
        modal.classList.remove('active')
        callback();
    });

}

export const closeModalForm = (form) => {
    const modal = form.parentNode.parentNode.parentNode;
    modal.classList.remove('active')
}

const selectDays = document.querySelectorAll(".day-custom")
const selectMonths = document.querySelectorAll(".month-custom")
const selectYears = document.querySelectorAll(".year-custom")
const days = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
const months = ['January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December']

function isLeapYear(year) {
    year = parseInt(year);
    if (year % 4 !== 0) {
        return false;
    } else if (year % 400 === 0) {
        return true;
    } else return year % 100 !== 0;
}


const renderYears = () => {
    selectYears.forEach(s => {
        s.innerHTML = ""
        const d = new Date();
        for (let i = 1929; i <= d.getFullYear(); i++) {// years start i
            const option = document.createElement('option')
            if (i === 1929) {
                option.innerText = `Year`
                s.appendChild(option);
                continue;
            }
            const val = i.toString()
            option.value = val;
            option.innerText = val
            s.appendChild(option);
        }

        s.addEventListener('change', () => {
            const month = s.parentNode.querySelector(".month-custom")
            const monthIndex = months.findIndex(m => m === month.value);

            const val = s.value;
            if (isLeapYear(val)) {
                days[1] = 29;
            } else {
                days[1] = 28;
            }
            renderDays(days[monthIndex])
        })
    })
}

const renderMonths = () => {
    selectMonths.forEach(s => {
        s.innerHTML = ""
        for (let i = 0; i <= months.length; i++) {
            const option = document.createElement('option')
            if (i === 0) {
                option.innerText = `Month`
                s.appendChild(option);
                continue;
            }

            const val = months[i - 1]
            option.value = val;
            option.innerText = val
            s.appendChild(option);
        }
        s.addEventListener('change', () => {
            const monthIndex = months.findIndex(m => m === s.value);
            if (monthIndex !== -1) {
                renderDays(days[monthIndex])
            }
        })
    })
}

const renderDays = (days) => {
    selectDays.forEach(s => {
        s.innerHTML = ""
        for (let i = 0; i <= days; i++) {
            const option = document.createElement('option')
            if (i === 0) {
                option.innerText = `Day`
                s.appendChild(option);
                continue;
            }

            const val = `${i}`
            option.value = val;
            option.innerText = val
            s.appendChild(option);
        }
    })
}

renderYears()
renderMonths()
renderDays(days[0])


// const confirmButtons = document.querySelectorAll(".btn-confirm");
//
// confirmButtons.forEach(btn => {
//     const parent = btn.parentNode;
//     const modal = parent.querySelector(".modal");
//
//     btn.addEventListener("click", () => {
//         modal.classList.add('active')
//     })
//
//     const buttonClose = parent.querySelector(".btn-confirm-close");
//
//     buttonClose.addEventListener('click', () => {
//         modal.classList.remove('active')
//     })
// })