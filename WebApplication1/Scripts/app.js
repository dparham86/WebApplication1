$(document).ready(function () {

    $('#animeTable').hide();
    $('#dramaTable').hide();
});


function showThisTable(tableName) {
    $("#table").each(function () {
        if ($(this).id == "#" + tableName + "Table")
    {
        $(this).show();
    }
    else
    {
        $(this).hide();
    }
    })
    //$('#' + tableName + 'table').show();
};