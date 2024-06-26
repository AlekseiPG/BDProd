﻿$(function () {
    $('#searchButton').on('click', function () {
        var searchText = $('#autocomplete-prod').val();
        var labSearchText = $('#autocomplete-lab').val();
        if (searchText.trim() !== '') {
            $.ajax({
                url: '/ImageForm/ProdSearch',
                method: 'GET',
                data: {
                    term: searchText,
                    labSearchTerm: labSearchText
                },
                success: function (data) {
                    var autocompleteList = data.map(function (product) {
                        return product.code13 + ' | ' + product.nomLong;
                    });

                    $('#res-prod-count').text(autocompleteList.length + ' ');

                    var maxResults = 25;
                    autocompleteList = autocompleteList.slice(0, maxResults);

                    var $resultsContainer = $('#autocomplete-res-prod');
                    $resultsContainer.empty();
                    autocompleteList.forEach(function (item) {
                        $resultsContainer.append('<div class="autocomplete-item">' + item + '</div>');
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
        }
    });

    $(document).on('click', '.autocomplete-item', function () {
        var selectedText = $(this).text();
        $('#autocomplete-prod').val(selectedText);
        var code = selectedText.split(' | ')[0];
        var nom = selectedText.split(' | ')[1];
        $('#selected-code').text('Code: ' + code);
        $('#selected-nom').text('Nom: ' + nom);
        $('#autocomplete-res-prod').hide();
    });
});
