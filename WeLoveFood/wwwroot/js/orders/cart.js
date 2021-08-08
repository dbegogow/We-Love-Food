(() => {
    const out = (e) => {
        console.log('yes');
    };

    document.querySelector('out')
        .addEventListener('click', out);
})();