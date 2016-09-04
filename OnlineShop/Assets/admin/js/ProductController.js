var homeConfig = {
    pageSize: 5,
    pageIndex: 1,
}

var ProductController = {
    init: function () {
        ProductController.registerEvent();
        ProductController.loadData();
    },
    registerEvent: function () {
        $(document).on('click', '.btnDelete', function () {
            var id = $(this).data('id');
            if (confirm('Bạn có chắc?')) {
            ProductController.deleteProduct(id);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtSearchName').val();

        $.ajax({
            url: '/Admin/Product/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchString: search,
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            success: function (response) {
                debugger;
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template,
                            {
                                Id: item.ID,
                                Name: item.Name,
                                Price: item.Price,
                                CategoryName: item.CateName,
                                Image: "<img src = '" + item.Image + "' alt='Gia Hung' title ='" + item.Name + "' style='max-width: 200px;height: 100px;'> ",
                            });
                    });
                    $('#tblData').html(html);

                    ProductController.paging(response.total, function () {
                        ProductController.loadData();
                    }, changePageSize);
                }
            },
            errror: function (err) {
                debugger;
                console.log(err)
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
    }
    , deleteProduct: function (id) {
        $.ajax({
            url: '/Admin/Product/Delete/',
            type: 'POST',
            dataType: 'json',
            data: {
                id: id
            },
            success: function (response) {
                if (response.status) {
                    alert('Xóa thành công!');
                    ProductController.loadData(true);
                }
            },
            error: function (err) {
                console.log(err)
            }
        });
    }
}

ProductController.init();