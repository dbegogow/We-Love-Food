(() => {
    const handleError = (res) => {
        if (!res.ok) {
            throw Error('Възникна грешка.');
        }
        return response;
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
            .then(res => console.log('yes'))
            .catch(err => console.log(err.message));
    }

    document.getElementById('add-to-favorite')
        .addEventListener('click', addToFavorite);
})();