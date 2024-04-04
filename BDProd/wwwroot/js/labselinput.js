$(function () {
    var selectedImagesUrlParam = localStorage.getItem('carouselImagesArray');
    if (selectedImagesUrlParam) {
        var selectedImages = JSON.parse(selectedImagesUrlParam);
        if (selectedImages.length > 0) {
            var firstImagePath = selectedImages[0];
            var folderPath = firstImagePath.substring(0, firstImagePath.lastIndexOf('/'));
            var folderName = folderPath.substring(folderPath.lastIndexOf('/')+1);
            $('#autocomplete-lab').val(folderName);
        }
    }
});