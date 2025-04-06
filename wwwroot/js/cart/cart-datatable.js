

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
                document.getElementById("totalPrice").innerHTML = json.totalPrice;
                if (document.getElementById('discounted-price') != null) {
                    document.getElementById("discounted-price").innerHTML = json.totalPrice + (document.getElementById('discount').innerHTML * json.totalPrice) / 100;
                }
                return json.tires.concat(json.services);
            }
        },
        columns: [
            {
                data: 'id',
                render: function (data) {
                    return `<a class="btn btn-danger" href="/Cart/RemoveFromCart/${data}">X</a>`;
                }
            },
            {
                data: 'imagePath',
                render: function (data) {
                    return `<div style="width: 60px; height: 60px;">
                            <img style="width: 100%; height: 100%; object-fit: cover;" src="${data}"></img>
                        </div>`;
                }
            },
            { data: 'name', },
            { 
                data: 'price', 
                render: function (data) {
                    return data.toFixed(2) + ' лв';  // Format price
                }
            },
            { data: 'quantity' },
            {
                data: null,
                render: function (data, type, row) {
                    return data.quantity * data.price + ' лв'
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return row.stock ? 'Гуми' : 'Услуга';
                }
            }
        ]
    });


}