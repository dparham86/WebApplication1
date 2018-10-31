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

$(function() {
    $('input:checkbox').click(function() {
        var $this = $(this);
        $.post(
            '/Home/SaveSettings', 
            {
                name: $this.attr('name'),
                value: $this.val()
            }, 
            function(data) {
                alert('success');
            }
        );
    });
});