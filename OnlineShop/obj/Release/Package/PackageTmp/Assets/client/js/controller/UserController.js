var user = {
    init: function () {
        user.loadProvinces();
        user.registerEvent();
    },
    registerEvent:function()
    {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if(id != '')
            {
                user.loadDistrict(id);
            }
            else {
                $('#ddlDistrict').html('');
                $('#ddlVillage').html('');
            }
        }),
        $('#ddlDistrict').off('change').on('change', function () {
            var id = $(this).val();
            if(id != '')
            {
                user.loadVillage(id);
            }
            else {
                $('#ddlVillage').html('');
            }
        })
    }
    ,
    loadProvinces: function () {
        $.ajax({
            url: 'User/LoadProvinces',
            type: 'POST',
            dataType: 'json',
            success: function(response)
            {
                var html = '<option value= "">--Mời nhập tỉnh/thành---</option>';
                if (response.status == true)
                {
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value = "'+item.Id+'">'+item.Name+'</option>';
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },

    loadDistrict: function (id) {
        $.ajax({
            url: 'User/LoadDistrict',
            data: {provinceId : id},
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                var html = '<option value= "">--Mời nhập quận/huyện---</option>';
                if (response.status == true) {
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value = "' + item.Id + '">' + item.Name + '</option>';
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },

    loadVillage: function (id) {
        $.ajax({
            url: 'User/LoadVillage',
            data: { districtID: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                var html = '<option value= "">--Mời nhập phường/xã---</option>';
                if (response.status == true) {
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value = "' + item.Id + '">' + item.Name + '</option>';
                    });
                    $('#ddlVillage').html(html);
                }
            }
        })
    }
}
user.init();