function populateCarousel(imagesArray, carousel, thumbCarousel) {

    carousel.innerHTML = '';
    thumbCarousel.innerHTML = '';

    imagesArray.forEach(function (image) {
        var li = document.createElement('li');
        li.classList.add('splide__slide');
        var img = document.createElement('img');
        img.src = image;
        img.alt = 'Image';

        var imageName = document.createElement('div');
        var text = document.createTextNode(image.substring(image.lastIndexOf('/') + 1));
        imageName.appendChild(text);

        var zoomIcon = document.createElement('img');
        zoomIcon.classList.add('zoomIcon');
        zoomIcon.src = "images/glass.svg";

        li.appendChild(img);

        var liCopy = li.cloneNode(true);
        liCopy.appendChild(img.cloneNode(true));

        li.append(zoomIcon);
        li.append(imageName);

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

        var confirmBtn1 = document.createElement('div');
        confirmBtn1.classList.add('pseudo_btn_wrap_in');
        confirmBtn1.style.marginRight = '10px';
        confirmBtn1.innerHTML = '<div class="pseudo_btn my_red">Oui</div>';

        var cancelBtn1 = document.createElement('div');
        cancelBtn1.classList.add('pseudo_btn_wrap_in');
        cancelBtn1.innerHTML = '<div class="pseudo_btn">No</div>';

        confirmBtn1.style.display = 'inline-block';
        cancelBtn1.style.display = 'inline-block';

        this.parentElement.insertBefore(confirmBtn1, this);
        this.parentElement.insertBefore(cancelBtn1, this);

        this.style.display = 'none';

        confirmBtn1.addEventListener('click', function () {
            var currentIndex = main.index;
            carouselImagesArray.splice(currentIndex, 1);
            console.log(carouselImagesArray);

            var carouselImagesArraySetTri = JSON.stringify(carouselImagesArray);
            localStorage.setItem('carouselImagesArray', carouselImagesArraySetTri);

            populateCarousel(carouselImagesArray, carouselImages, carouselImagesThumb);

            initializeSplideCarousels();

            supprimerBtn.style.display = 'block';
            cancelBtn1.remove();
            this.remove();
        });

        cancelBtn1.addEventListener('click', function () {
            supprimerBtn.style.display = 'block';
            confirmBtn1.remove();
            this.remove();
        });
    });

    $('#moveToTrash').on('click', function () {

        var trashBtn = this;

        var confirmBtn2 = document.createElement('div');
        confirmBtn2.classList.add('pseudo_btn_wrap_in');
        confirmBtn2.style.marginRight = '10px';
        confirmBtn2.innerHTML = '<div class="pseudo_btn my_red">Oui</div>';

        var cancelBtn2 = document.createElement('div');
        cancelBtn2.classList.add('pseudo_btn_wrap_in');
        cancelBtn2.innerHTML = '<div class="pseudo_btn">No</div>';

        confirmBtn2.style.display = 'inline-block';
        cancelBtn2.style.display = 'inline-block';

        this.parentElement.insertBefore(confirmBtn2, this);
        this.parentElement.insertBefore(cancelBtn2, this);

        this.style.display = 'none';

        confirmBtn2.addEventListener('click', function () {

            var currentIndex = main.index;
            imageUrl = carouselImagesArray[currentIndex];
            console.log(imageUrl);
            if (imageUrl != 0) {
                var folderPath = imageUrl.substring(0, imageUrl.lastIndexOf('/'));
                $.ajax({
                    url: '/ImageSelection/MoveToTrash',
                    type: 'POST',
                    data: { images: imageUrl, folderPath: folderPath },
                    success: function (data) {
                        carouselImagesArray.splice(currentIndex, 1);
                        console.log(carouselImagesArray);
                        var carouselImagesArraySetTri = JSON.stringify(carouselImagesArray);
                        localStorage.setItem('carouselImagesArray', carouselImagesArraySetTri);
                        populateCarousel(carouselImagesArray, carouselImages, carouselImagesThumb);
                        initializeSplideCarousels();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.error('Erreur lors du déplacement des images:', errorThrown);
                    }
                });
            }

            trashBtn.style.display = 'block';
            cancelBtn2.remove();
            this.remove();
        });

        cancelBtn2.addEventListener('click', function () {
            trashBtn.style.display = 'block';
            confirmBtn2.remove();
            this.remove();
        });

    });

    $(function () {
        var modal = $("#imageModal");
        var modalImg = $("#modalImage");
        var span = $(".close");

        function displayModal(imageSrc) {
            modalImg.attr("src", imageSrc);
            modal.css("display", "block");
            console.log("display");
        }

        span.on("click", function () {
            modal.css("display", "none");
        });

        $(window).on("click", function (event) {
            if (event.target == modal[0]) {
                modal.css("display", "none");
            }
        });

        $('#main-carousel').on('click', '.zoomIcon', function (event) {
            console.log("zOOOM");
            var imageUrl = $(this).closest('li').find('img').attr('src');
            console.log(imageUrl);
            displayModal(imageUrl);
            event.stopPropagation();
        });
    });
});

