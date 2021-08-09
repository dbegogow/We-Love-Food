﻿(() => {
    const handleError = (res) => {
        if (!res.ok) {
            const message = 'Възникна грешка.';

            throw Error(message);
        }
        return res;
    };

    const removePortion = (e) => {
        const target = e.target;

        const portionId = target
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
                document.getElementById(`number-${portionId}`)
                    .textContent = quantity;

                if (quantity === 1) {
                    document.getElementById(`remove-${portionId}`)
                        .classList.add('disable');
                }
            })
            .catch(err => alert(err.message));
    };

    var removePortionButtons = document.getElementsByClassName('remove');

    for (var removePortionButton = 0; removePortionButton < removePortionButtons.length; removePortionButton++) {
        removePortionButtons[removePortionButton].addEventListener("click", removePortion);
    }
})();