(() => {
    const handleError = (res) => {
        if (!res.ok) {
            const message = 'Възникна грешка.';

            throw Error(message);
        }
        return res;
    };

    const removePortion = (e) => {
        const portionId = document.querySelector('.meal')
            .getAttribute('portion-id');

        const token = document.querySelector("input[name='__RequestVerificationToken']").value;

        fetch('/api/removePortion',
            {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': token
                },
                body: JSON.stringify({ id: portionId })
            })
            .then(res => handleError(res))
            .then(res => res.json())
            .then(quantity => {
                document.querySelector('.number')
                    .textContent = quantity;

                if (quantity === 1) {
                    e.target.classList.add('disable');
                }
            })
            .catch(err => alert(err.message));
    };

    var removePortionButtons = document.getElementsByClassName('remove');

    for (var removePortionButton = 0; removePortionButton < removePortionButtons.length; removePortionButton++) {
        removePortionButtons[removePortionButton].addEventListener("click", removePortion);
    }
})();