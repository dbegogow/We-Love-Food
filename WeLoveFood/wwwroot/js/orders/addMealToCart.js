﻿(() => {
    const handleError = (res) => {
        if (!res.ok) {
            const message = 'Възникна грешка. Може да поръчвате едновременно само от един и същи ресторант.';

            throw Error(message);
        }
        return response;
    };

    const addMealToOrders = (e) => {
        const currentTarget = e.currentTarget;

        const isUserClient = currentTarget.getAttribute('is-user-client');

        if (isUserClient === "false") {
            window.location = "/Identity/Account/Login";

            return;
        }

        const mealId = currentTarget.getAttribute('meal-id');
        const restaurantId = currentTarget.getAttribute('restaurant-id');

        const token = document.querySelector("input[name='__RequestVerificationToken']").value;

        fetch('/api/orders',
            {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    'RequestVerificationToken': token
                },
                body: JSON.stringify({ id: mealId, restaurantId })
            })
            .then(res => handleError(res))
            .catch(err => console.log(err.message));
    };

    var meals = document.getElementsByClassName('meal');

    for (var meal = 0; meal < meals.length; meal++) {
        meals[meal].addEventListener("click", addMealToOrders);
    }
})();