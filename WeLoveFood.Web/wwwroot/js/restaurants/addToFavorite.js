(() => {
    const handleError = (res) => {
        if (!res.ok) {
            const message = 'Възникна грешка.';

            throw Error(message);
        }
        return res;
    };

    const addToFavorite = (e) => {
        e.preventDefault();

        const currentTarget = e.currentTarget;
        const id = currentTarget.getAttribute('restaurant-id');

        const token = document.querySelector("input[name='__RequestVerificationToken']").value;

        fetch('/api/restaurants',
            {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': token
                },
                body: JSON.stringify({ id })
            })
            .then(res => handleError(res))
            .then(() => {
                currentTarget.classList.add('added');

                const successMessage = document.querySelector('.add-to-favorite-success-message');
                successMessage.classList.add('present');

                setTimeout(() => {
                    successMessage.classList.remove('present');
                }, 1500);
            })
            .catch(err => alert(err.message));
    }

    const addToFavoriteBtn = document.getElementById('add-to-favorite');

    if (addToFavoriteBtn !== null) {
        addToFavoriteBtn.addEventListener('click', addToFavorite);
    }
})();