(() => {
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
                body: JSON.stringify({id})
            })
            .then(res => res.json())
            .then(data => console.log(data));
    }

    document.getElementById('add-to-favorite')
        .addEventListener('click', addToFavorite);
})();