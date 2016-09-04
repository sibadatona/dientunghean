var homeConfig = {
    pageSize: 5,
    pageIndex: 1,
}

var user = {
    init: function () {
        user.RegisterEvent();
        user.loadData();
    },
    RegisterEvent: function () {
        $('#btnSave').off('click').on('click', function () {
            user.saveEmployee();
        });

        $(document).on('click', '.btnUpdate', function () {
            var id = $(this).data('id');
            $('#modalAddOrUpdate').modal('show');
            user.loadDetailUser(id);
        });

        $(document).on('click', '.btnDelete', function () {
            var id = $(this).data('id');
            if (confirm('Are you sure?')) {
                user.deleteEmployee(id);
            }
        });

        $('#btnSearch').on('click', function () {
            user.loadData(true);
        })

        $('#btnRest').on('click', function () {
            $('#ddlStatus').val('');
            $('#txtSearchName').val('');
            user.loadData(true);
        })
    },
    loadData: function (changePageSize) {
        var staus = $('#ddlStatus').val();
        var search = $('#txtSearchName').val();

        $.ajax({
            url: '/Admin/User/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchString: search,
                staus: staus,
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template,
                            {
                                Id: item.ID,
                                UserName: item.UserName,
                                Name: item.Name,
                                Address: item.Address,
                                Email: item.Email,
                                Status: item.Status ? "<span class=\"label label-success\"> Active </span>"
                                    : "<span class=\"label label-danger\"> Locked </span>"
                            });
                    });

                    $('#tblData').html(html);

                    user.paging(response.total, function () {
                        user.loadData();
                    }, changePageSize);
                }
            },
            errror: function () {

            }
        });
    },
    paging: function (totalRow, callback, changePageSize) {

        if ($('#pagination-demo a').length === 0 || changePageSize) {
            $('#pagination-demo').empty();
            $('#pagination-demo').removeData('twbs-pagination');
            $('#pagination-demo').unbind('page');
        }

        var totalPages = Math.ceil(totalRow / homeConfig.pageSize);
        $('#pagination-demo').twbsPagination({
            totalPages: totalPages,
            visiblePages: 5,
            onPageClick: function (event, page) {
                homeConfig.pageIndex = page;
                setTimeout(callback, 200)
            }
        });
    },
    saveEmployee: function () {
        var name = $('#txtName').val();
        var password = $('#txtPassword').val();
        var address = $('#txtAddress').val();
        var email = $('#txtEMail').val();
        var status = $('#cbStatus').prop('checked');
        var id = parseInt($('#hidId').val());

        var us =
            {
                Id: id,
                Name: name,
                Password: password,
                Address: address,
                Email: email,
                Status: status
            };

        $.ajax({
            url: '/Admin/User/Update/',
            type: 'POST',
            dataType: 'json',
            data: {
                model: JSON.stringify(us)
            },
            success: function (response) {
                if (response.status) {
                    alert('Save Success.');
                    $('#modalAddOrUpdate').modal('hide');
                    user.loadData();
                }
            },
            error: function (err) {
                console.log(err);
            }

        });
    },
    loadDetailUser: function (id) {
        $.ajax({
            url: '/Admin/User/Detail/',
            type: 'GET',
            dataType: 'json',
            data: {
                Id: id
            },
            success: function (respone) {
                if (respone.status) {
                    var data = respone.data;
                    $('#hidId').val(id);
                    $('#txtName').val(data.Name);
                    $('#txtPassword').val(data.Password);
                    $('#txtAddress').val(data.Address);
                    $('#txtEMail').val(data.Email);
                    $('#cbStatus').prop('checked', data.Status);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    deleteEmployee: function (id) {
        $.ajax({
            url: '/Admin/User/Delete/',
            type: 'POST',
            dataType: 'json',
            data: {
                Id: id
            },
            success: function (response) {
                if (response.status) {
                    user.loadData(true);
                }
            },
            error: function (err) {
                console.log(err);
            }

        });
    }
}
user.init();