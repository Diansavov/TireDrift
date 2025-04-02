document.addEventListener("DOMContentLoaded", function () {
    LoadTable();
});

function LoadTable() {
    console.log('s')
    DataTable.type('num', 'className', '');
    DataTable.type('date', 'className', '');

    table = $('#ordersTable').DataTable({
        dom: 'tp',
        pageLength: 6,
        "oLanguage": {
            "sEmptyTable": "Нямате фактури"
        },
        ajax: {
            url: '/Order/GetInvoicesJson',
            dataSrc: ''
        },
        columns: [
            { 
                data: 'companyName', 
            },
            { 
                data: 'bulStat', 
            },
            { 
                data: 'invoiceDate',
                render: function (data) {
                    return new Date(data).toLocaleDateString('bg-BG'); // Format date
                }
            },
            { 
                data: 'totalPrice', 
                render: function (data) {
                    return data.toFixed(2) + ' лв';  // Format price
                }
            },
            
            { 
                data: 'orderedTires',
                render: function (data) {
                    return data.length > 0 ? data.join(', ') : "Няма поръчани гуми";
                }
            },
            { 
                data: 'orderedServices',
                render: function (data) {
                    return data.length > 0 ? data.join(', ') : "Няма поръчани услуги";
                }
            }
            
        ]
    });


}