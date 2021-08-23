(() => {
    const images = document.getElementsByClassName("slider-image");
    const buttons = document.getElementsByClassName("slider-btn");

    let index = 1;
    let repeatLoop = false;

    const activeClass = 'active';

    setInterval(() => {
        if (repeatLoop) {
            images[4].classList.remove(activeClass);
            buttons[4].classList.remove(activeClass);

            images[0].classList.add(activeClass);
            buttons[0].classList.add(activeClass);

            repeatLoop = false;

            return;
        }

        images[index - 1].classList.remove(activeClass);
        buttons[index - 1].classList.remove(activeClass);

        images[index].classList.add(activeClass);
        buttons[index].classList.add(activeClass);

        index++;

        if (index > 4) {
            index = 1;
            repeatLoop = true;
        }
    }, 2000);
})();