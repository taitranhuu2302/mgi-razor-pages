const btnOpenModal = document.querySelectorAll('[data-modal-target]');
btnOpenModal.forEach(btn => {

    btn.addEventListener('click', (e) => {
        e.preventDefault();
        const value = btn.getAttribute('data-modal-target');
        const modal = document.querySelector(`#${value}`);
        modal.classList.toggle('active');

        window.addEventListener('click', (e) => {
            if (e.target === modal) {
                modal.classList.remove('active');
            }
        })

        window.addEventListener('keydown', (e) => {
            if (e.key === 'Escape') {
                modal.classList.remove('active');
            }
        })
    })
});

const selectDays = document.querySelectorAll(".day-custom")
const selectMonths = document.querySelectorAll(".month-custom")
const selectYears = document.querySelectorAll(".year-custom")

selectYears.forEach(s => {
    const d = new Date();
    for (let i = 1929; i <= d.getFullYear(); i++) {// years start i
        const option = document.createElement('option')
        if (i === 1929) {
            option.innerText = `Year`
            s.appendChild(option);
            continue;
        }
        const val = i
        option.value = val;
        option.innerText = val
        s.appendChild(option);
    }
})

selectMonths.forEach(s => {
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
})

selectDays.forEach(s => {
    const days = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    for (let i = 0; i <= days[0]; i++) {
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
