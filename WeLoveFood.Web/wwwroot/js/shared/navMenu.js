(() => {
    const activeClass = 'active';

    const toggle = () => {
        const buttons = document.getElementsByClassName('btns')[0];

        if (buttons.classList.contains(activeClass)) {
            buttons.classList.remove(activeClass);
        } else {
            buttons.classList.add(activeClass);
        }
    };

    document.getElementsByClassName('fa-bars')[0]
        .addEventListener('click', toggle);
})();