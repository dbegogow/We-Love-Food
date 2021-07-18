(() => {
    const addMealToOrders = (e) => {
        const targetClass = e.target.className;

        if (targetClass === 'meals-container') { return; }
    };

    document
        .getElementsByClassName('meals-container')[0]
        .addEventListener('click', addMealToOrders);
})();