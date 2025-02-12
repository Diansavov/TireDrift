

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
            "sEmptyTable": "Няма кошница"
        },
        ajax: {
            url: '/Cart/GetCartJson',
            dataSrc: 'tires'
        },
        columns: [
            { data: 'id' },
            { data: 'imagePath' },
            { data: 'name' },
            { data: 'price' },
            { data: 'stock' },
            { data: 'description' },
        ]
    });
}