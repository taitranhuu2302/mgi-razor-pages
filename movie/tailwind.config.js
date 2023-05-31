/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Pages/**/*.cshtml"
    ],
    theme: {
        extend: {
            colors: {
                primary: "#FE025C",
                secondary: '#2ab27b',
                link: '#3097d1',
                orange: '#fe7900'
            }
        },
    },
    plugins: [],
}

