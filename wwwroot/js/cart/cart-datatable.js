

document.addEventListener("DOMContentLoaded", function () {
    LoadTable();
});

function LoadTable() {
    DataTable.type('num', 'className', '');
    DataTable.type('date', 'className', '');

    table = $('#usersTable').DataTable({
        dom: 'tp',
        pageLength: 6,
        "oLanguage": {
            "sEmptyTable": "Няма продукти в кошницата"
        },
        ajax: {
            url: '/Cart/GetCartJson',
            dataSrc: function (json) {
                return json.tires.concat(json.services);
            }
        },
        columns: [
            { data: 'id' },
            { data: 'imagePath' },
            { data: 'name' },
            { data: 'price' },
            { data: 'stock' },
            { data: 'description' },
            {
                data: null,
                render: function (data, type, row) {
                    return row.name ? 'Гуми' : 'Услуги';
                }
            }
        ]
    });
}