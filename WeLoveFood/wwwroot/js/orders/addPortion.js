(() => {
    const handleError = (res) => {
        if (!res.ok) {
            const message = 'Възникна грешка.';

            throw Error(message);
        }
        return res;
    };

    const addPortion = (e) => {
        e.preventDefault();

        const target = e.target;

        const portionId = target
            .getAttribute('portion-id');

        const token = document.querySelector("input[name='__RequestVerificationToken']").value;

        fetch('/api/addPortion',
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

                const portionPrice = Number(document.getElementById(`portion-price-${portionId}`).textContent);

                document.getElementById(`portion-total-price-${portionId}`)
                    .textContent = portionPrice * quantity;

                const orderTotalPrice = document.getElementById('total-price');
                const orderTotalPriceAsNumber = Number(orderTotalPrice.textContent);

                orderTotalPrice.textContent = orderTotalPriceAsNumber + portionPrice;

                if (quantity > 1) {
                    document.getElementById(`remove-${portionId}`)
                        .classList.remove('disable');
                }
            })
            .catch(err => alert(err.message));
    };

    var addPortionButtons = document.getElementsByClassName('add');

    for (var addPortionButton = 0; addPortionButton < addPortionButtons.length; addPortionButton++) {
        addPortionButtons[addPortionButton].addEventListener("click", addPortion);
    }
})();