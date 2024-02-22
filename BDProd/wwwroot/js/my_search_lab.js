$(document).ready(function () {
    $('#autocomplete-lab').on('input', function () {
        var searchText = $(this).val();

        $.ajax({
            url: '/ImageForm/LabSearch',
            method: 'GET',
            data: { term: searchText },
            success: function (data) {
                var autocompleteList = data.map(function (lab) {
                    return lab.label;
                });

                var $resultsContainer = $('#autocomplete-res-lab');
                $resultsContainer.empty();
                autocompleteList.forEach(function (label) {
                    $resultsContainer.append('<div class="autocomplete-item-lab">' + label + '</div>');
                });

                if (autocompleteList.length > 0) {
                    $resultsContainer.show();
                } else {
                    $resultsContainer.hide();
                }
            },
            error: function (error) {
                console.log('Erreur lors de la réception des recommandations: ' + error);
            }
        });
    });

    $(document).on('click', '.autocomplete-item-lab', function () {
        var selectedText = $(this).text();
        $('#autocomplete-lab').val(selectedText);
        $('#autocomplete-res-lab').hide();
    });
});