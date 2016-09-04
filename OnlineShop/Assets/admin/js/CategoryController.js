var Category = {
    init: function () {
        Category.registerEvent();
        Category.loadData();
    },
    registerEvent: function () {
        $('#btnSave').on('click', function () {
            Category.SaveDate();
        });
        Category.loadDetail();

        //$('.btnDelete').off('click').on('click', function () {
        $(document).on('click', '.btnDelete', function () {
            var id = $(this).data('id');
            if (confirm('Are you sure?')) {
                Category.deleteCategory(id);
                Category.resetForm();
            }
        });
    },
    loadDetail: function () {
        $('#tblData tr').click(function () {
            var selected = $(this).hasClass("highlight");
            $("#tblData tr").removeClass("highlight");
            if (!selected) {
                $(this).addClass("highlight");

                var categoryId = $(this).closest("tr").find(".categoryId").text();
                var categoryName = $(this).closest("tr").find(".categoryName").text();
                var categoryOrder = $(this).closest("tr").find(".categoryOrder").text();
                var categoryIsShow = $(this).closest("tr").find(".categoryIsShow").text();

                $('#hdCategoryId').val(categoryId);
                $('#txtName').val(categoryName);
                $('#txtOrder').val(categoryOrder);
                if (categoryIsShow.trim() === 'Show') {
                    $('#cbStatus').prop('checked', true);
                }
                else {
                    $('#cbStatus').prop('checked', false);
                }
            }
            else {
                $('#hdCategoryId').val('0');
                $('#txtName').val('');
                $('#txtOrder').val('');
                $('#cbStatus').prop('checked', true);
            }
        })
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtOrder').val('');
        $('#cbStatus').prop('checked', true);
    },
    loadData: function (changePageSize) {
        $.ajax({
            url: '/Admin/Category/LoadData',
            type: 'GET',
            dataType: 'json',

            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template,
                            {
                                Id: item.ID,
                                Name: item.Name,
                                DisplayOrder: item.DisplayOrder,
                                ShowOnHome: item.ShowOnHome ? "<span class=\"label label-success\">Show</span>"
                                    : "<span class=\"label label-danger\">Hide</span>"
                            });
                    });
                    $('#tblData').html(html);

                    Category.loadDetail();
                }
            },
            errror: function () {

            }
        });
    },
    SaveDate: function () {
        var id = $('#hdCategoryId').val();
        var name = $('#txtName').val();
        var order = $('#txtOrder').val();
        var isShow = $('#cbStatus').prop('checked');

        var category = {
            Id: id,
            Name: name,
            DisplayOrder: order,
            ShowOnHome: isShow
        }

        $.ajax({
            url: '/Admin/Category/AddOrUpdate',
            type: 'POST',
            dataType: 'json',
            data: {
                model: JSON.stringify(category)
            },
            success: function (reponese) {
                alert('success');
                Category.resetForm();
                Category.loadData();
            },
            error: function (err) {
                console.log(err)
            }
        });
    },
    deleteCategory: function (id) {
        $.ajax({
            url: '/Admin/Category/Delete',
            type: 'POST',
            dataType: 'json',
            data: {
                Id:id
            },
            success: function (respones) {
                alert('Success');
                Category.loadData();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}
Category.init();