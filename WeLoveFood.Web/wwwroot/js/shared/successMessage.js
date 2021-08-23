(() => {
    const successMessageEl = document.querySelector('.success-message');

    if (successMessageEl) {
        const expirationTime = 3000;

        setTimeout(() => {
            successMessageEl.classList.add('hidden');
        }, expirationTime);
    }
})();