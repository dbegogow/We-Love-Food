(() => {
    const handleError = (res) => {
        if (!res.ok) {
            const message = 'Възникна грешка.';

            throw Error(message);
        }
        return res;
    };

    const deletePortion = (e) => {
        e.preventDefault();

        const currentTarget = e.currentTarget;

        const portionId = currentTarget.getAttribute('portion-id');

        const token = document.querySelector("input[name='__RequestVerificationToken']").value;

        fetch('/api/deletePortionFromCart',
            {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': token
                },
                body: JSON.stringify({ id: portionId })
            })
            .then(res => handleError(res))
            .then(() => {
                document.getElementById(`portion-${portionId}`)
                    .remove();
            })
            .catch(err => alert(err.message));
    };

    var deletePortionButtons = document.getElementsByClassName('remove-btn');

    for (var deletePortionButton = 0; deletePortionButton < deletePortionButtons.length; deletePortionButton++) {
        deletePortionButtons[deletePortionButton].addEventListener("click", deletePortion);
    }
})();