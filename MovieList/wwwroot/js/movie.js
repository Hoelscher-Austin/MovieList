var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/admin/movie/getall' },
        "columns": [
            { data: 'title' },
            {
                data: 'director',
                render: function (data, type, row) {
                    var fullName = data.split(' '),
                    firstName = fullName[0],
                    lastName = fullName[fullName.length - 1];
                    return `${lastName}, ${firstName}`;
                }
            },
            { data: 'yearReleased' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn-group w-100" role="group">
                                    <a href="/admin/movie/edit?id=${data}" class="btn btn-secondary mx-2">
                                    <i class="bi bi-pencil-square"></i> Edit</a>
                                <a onClick="confirmDelete('/admin/movie/delete/${data}')" class="btn btn-danger mx-2">
                                    <i class="bi bi-trash3-fill"></i> Delete</a>
                                </div>`
                }
                        
            }
            
        ]
    });   
}


function confirmDelete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {                    
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    dataTable.ajax.reload();
                }
            })
        }
    })
}