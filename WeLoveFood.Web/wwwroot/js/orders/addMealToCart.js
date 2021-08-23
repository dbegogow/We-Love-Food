(() => {
    const handleError = (res) => {
        if (!res.ok) {
            const message = 'Възникна грешка. Само клиент може да поръчва и трябва това да бъде само от един и същи ресторант.';

            throw Error(message);
        }
        return res;
    };

    const addMealToOrders = (e) => {
        const currentTarget = e.currentTarget;

        const isUserAuthenticated = currentTarget.getAttribute('is-user-authenticated');

        if (isUserAuthenticated === "false") {
            window.location = "/Identity/Account/Login";
            return;
        }

        const mealId = currentTarget.getAttribute('meal-id');
        const restaurantId = currentTarget.getAttribute('restaurant-id');

        const token = document.querySelector("input[name='__RequestVerificationToken']").value;

        fetch('/api/addMealToCart',
            {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': token
                },
                body: JSON.stringify({ id: mealId, restaurantId })
            })
            .then(res => handleError(res))
            .then(() => {
                currentTarget.classList.add('added');
            })
            .catch(err => alert(err.message));
    };

    var meals = document.getElementsByClassName('meal');

    for (var meal = 0; meal < meals.length; meal++) {
        meals[meal].addEventListener("click", addMealToOrders);
    }
})();