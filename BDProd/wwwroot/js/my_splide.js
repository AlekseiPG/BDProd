document.addEventListener('DOMContentLoaded', function () {
    var main = new Splide('#main-carousel', {
        type: 'fade',
        rewind: true,
        pagination: true,
        arrows: true,
    });

    var thumbnails = new Splide('#thumbnail-carousel', {
        fixedWidth: 75,
        fixedHeight: 45,
        gap: 5,
        rewind: true,
        pagination: false,
        isNavigation: true,
        breakpoints: {
            600: {
                fixedWidth: 60,
                fixedHeight: 44,
            },
        },
    });

    main.sync(thumbnails);
    main.mount();
    thumbnails.mount();

});