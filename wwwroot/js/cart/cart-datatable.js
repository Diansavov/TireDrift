

document.addEventListener("DOMContentLoaded", function () {
    LoadTable();
});

function searchSubmit() {
    table.destroy();
    LoadTable();
}

function LoadTable() {

    DataTable.type('num', 'className', '');
    DataTable.type('date', 'className', '');

    table = $('#usersTable').DataTable({
        dom: 'tp',
        pageLength: 6,
        "oLanguage": {
            "sEmptyTable": "Няма продукти"
        },
        ajax: {
            url: '/Cart/GetCartJson',
            dataSrc: '',
        },
        columns: [
            { data: 'id' },
            { data: 'tires.imagePath' },
            { data: 'tires.name' },
            { data: 'tires.price' },
            { data: 'tires.quantity' },
            { data: 'totalPrice' },
        ]
    });
}