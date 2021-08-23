(() => {
    const change = (e) => {
        e.target.parentElement.classList.add('hidden');

        document.getElementById('save-btn')
            .parentElement
            .classList.remove('hidden');

        document.querySelectorAll('input')
            .forEach(e => e.classList.remove('disable'));
    };

    document.getElementById('change-btn')
        .addEventListener('click', change);
})();