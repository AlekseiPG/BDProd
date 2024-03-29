function populateCarousel(imagesArray, carousel, thumbCarousel) {

    carousel.innerHTML = '';
    thumbCarousel.innerHTML = '';

    imagesArray.forEach(function (image) {
        var li = document.createElement('li');
        li.classList.add('splide__slide');
        var img = document.createElement('img');
        img.src = image;
        img.alt = 'Image';
        li.appendChild(img);

        var liCopy = li.cloneNode(true);
        liCopy.appendChild(img.cloneNode(true));

        carousel.appendChild(li);
        thumbCarousel.appendChild(liCopy);
    });
}

var main;
var thumbnails;

function initializeSplideCarousels() {

    if (main) {
        main.destroy(); 
    }

    if (thumbnails) {
        thumbnails.destroy(); 
    }

    main = new Splide('#main-carousel', {
        type: 'fade',
        rewind: true,
        pagination: false,
        arrows: true,
    });

    thumbnails = new Splide('#thumbnail-carousel', {
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
}

document.addEventListener('DOMContentLoaded', function () {

    var storedImages = localStorage.getItem('carouselImagesArray');
    var carouselImagesArray = JSON.parse(storedImages);

    console.log(carouselImagesArray);

    var carouselImages = document.getElementById('carousel-images');
    var carouselImagesThumb = document.getElementById('carousel-images-thumbnail');

    populateCarousel(carouselImagesArray, carouselImages, carouselImagesThumb);

    initializeSplideCarousels();

    document.getElementById('tri-button').addEventListener('click', function () {
        var triInput = document.getElementById('tri-input').value;
        var newIndex = parseInt(triInput) - 1;
        if (!isNaN(newIndex) && newIndex >= 0 && newIndex < carouselImagesArray.length) {
            var currentIndex = main.index;
            var movedImage = carouselImagesArray.splice(currentIndex, 1)[0];
            carouselImagesArray.splice(newIndex, 0, movedImage);
            console.log(carouselImagesArray);

            var carouselImagesArraySetTri = JSON.stringify(carouselImagesArray);
            localStorage.setItem('carouselImagesArray', carouselImagesArraySetTri);

            populateCarousel(carouselImagesArray, carouselImages, carouselImagesThumb);

            initializeSplideCarousels();

            main.go(newIndex);
        } else {
            alert('Entrée de tri invalide!');
        }
    });

    document.getElementById('supprimer').addEventListener('click', function () {

        var supprimerBtn = this;

        var confirmBtn = document.createElement('div');
        confirmBtn.classList.add('pseudo_btn_wrap_out');
        confirmBtn.innerHTML = '<div class="pseudo_btn_wrap_in"><div class="pseudo_btn my_red">Oui</div></div>';

        var cancelBtn = document.createElement('div');
        cancelBtn.classList.add('pseudo_btn_wrap_out');
        cancelBtn.innerHTML = '<div class="pseudo_btn_wrap_in"><div class="pseudo_btn">No</div></div>';

        confirmBtn.style.display = 'inline-block';
        cancelBtn.style.display = 'inline-block';

        this.parentElement.insertBefore(confirmBtn, this);
        this.parentElement.insertBefore(cancelBtn, this);

        this.style.display = 'none';

        confirmBtn.addEventListener('click', function () {
            var currentIndex = main.index;
            carouselImagesArray.splice(currentIndex, 1);
            console.log(carouselImagesArray);

            var carouselImagesArraySetTri = JSON.stringify(carouselImagesArray);
            localStorage.setItem('carouselImagesArray', carouselImagesArraySetTri);

            populateCarousel(carouselImagesArray, carouselImages, carouselImagesThumb);

            initializeSplideCarousels();

            supprimerBtn.style.display = 'block';
            cancelBtn.remove();
            this.remove();
        });

        cancelBtn.addEventListener('click', function () {

            supprimerBtn.style.display = 'block';
            confirmBtn.remove();
            this.remove();
        });
    });

});