var BdProdJs = BdProdJs || {};

BdProdJs.images_selection = function (modelData) {
    $(function () {
        $('#fancytree').fancytree({
            source: modelData,
            extensions: ['filter'],
            filter: {
                mode: 'hide'
            },
            activate: function (event, data) {
                var selectedFolder = data.node.data.path;
                $.ajax({
                    url: '/ImageSelection/GetImages',
                    type: 'GET',
                    data: { folderPath: selectedFolder },
                    success: function (data) {
                        $('#imageGallery').empty();
                        data.forEach(function (image) {
                            var imageElement = $('<div class="imageContainer"></div>');
                            var img = $('<img class="main" src="' + image.path + '" style="max-width: 100%;" />');
                            var imageName = $('<p>' + getImageName(image.path) + '</p>');
                            var zoomIcon = $('<img class="zoomIcon" src="images/glass.svg" />');
                            imageElement.append(img);
                            imageElement.append(zoomIcon);
                            imageElement.append(imageName);
                            $('#imageGallery').append(imageElement);
                        });
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.error('Erreur de chargement des images:', errorThrown);
                    }
                });
            }
        });

        $('#moveToTrash').on('click', function () {
            var selectedImages = [];
            $('.imageContainer.selected img.main').each(function () {
                selectedImages.push($(this).attr('src'));
            });
            if (selectedImages.length > 0) {
                var folderPath = selectedImages[0].substring(0, selectedImages[0].lastIndexOf('/'));
                console.log(folderPath);
                console.log(selectedImages);
                $.ajax({
                    url: '/ImageSelection/MoveToTrash',
                    type: 'POST',
                    data: { images: selectedImages, folderPath: folderPath },
                    success: function (data) {
                        alert('Les images ont été déplacées avec succès vers la corbeille.');
                        $('.imageContainer.selected').remove();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.error('Erreur lors du déplacement des images:', errorThrown);
                    }
                });
            } else {
                alert('Sélectionnez les images à déplacer vers le panier.');
            }
        });

        $('#imageGallery').on('dblclick', '.imageContainer', function () {
            $(this).toggleClass('selected');
        });

        function getImageName(path) {
            return path.substring(path.lastIndexOf('/') + 1);
        };

        $('#treeSearch').on('input', function () {
            var searchString = $(this).val();
            var tree = $.ui.fancytree.getTree("#fancytree");;
            tree.filterNodes(searchString);
        });

        $('#submitImages').on('click', function () {
            var selectedImages = [];
            $('.imageContainer.selected img.main').each(function () {
                selectedImages.push($(this).attr('src'));
            });
            var carouselImagesArray = JSON.stringify(selectedImages);
            localStorage.setItem('carouselImagesArray', carouselImagesArray);
            window.location.href = '/ImageForm';
        });

        $(function () {
            var modal = $("#imageModal");
            var modalImg = $("#modalImage");
            var span = $(".close");

            function displayModal(imageSrc) {
                modalImg.attr("src", imageSrc);
                modal.css("display", "block");
            }

            span.on("click", function () {
                modal.css("display", "none");
            });

            $(window).on("click", function (event) {
                if (event.target == modal[0]) {
                    modal.css("display", "none");
                }
            });

            $('#imageGallery').on('click', '.zoomIcon', function (event) {
                var imageUrl = $(this).siblings('img').attr('src');
                displayModal(imageUrl);
                event.stopPropagation();
            });
        });
    });
};