$(document).ready(function () {
    $('#autocomplete-prod').on('input', function () {
        var searchText = $(this).val();
        $.ajax({
            url: 'https://prodinfo.everys.com/externe/FindProd.ashx',
            method: 'POST',
            data: {
                nMax: 10,
                useSupp: 0,
                details: 1,
                searchString: searchText
            },
            success: function (data) {
                var produits = data.produits;
                var autocompleteList = [];

                produits.forEach(function (produit) {
                    var code13 = produit.code13;
                    var nomLong = produit.wpharma.nomLong;
                    autocompleteList.push({
                        code13: code13,
                        nomLong: nomLong
                    });
                });

                var maxResults = 5;
                autocompleteList = autocompleteList.slice(0, maxResults);

                var $resultsContainer = $('#autocomplete-res-prod');
                $resultsContainer.empty();
                autocompleteList.forEach(function (item) {
                    $resultsContainer.append('<div class="autocomplete-item">' + item.code13 + ' - ' + item.nomLong + '</div>');
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

    $(document).on('click', '.autocomplete-item', function () {
        var selectedText = $(this).text();
        $('#autocomplete-prod').val(selectedText);

        var code = selectedText.split(' - ')[0];
        var nom = selectedText.split(' - ')[1];
        $('#selected-code').text('Code: ' + code);
        $('#selected-nom').text('Nom: ' + nom);

        $('#result-count').val($('.autocomplete-item').length);

        $('#autocomplete-res-prod').hide();
    });
});