var cart = {
    init: function () {
        cart.regEvents();
    },
    callAlrt: function()
    {
    	alert('a');
    }
	,
    regEvents: function()
    {
        $('#btnContinue').off("click").on("click", function () {
            window.location.href = "/";
        });

        $('#btnUpdate').off("click").on("click", function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product:{
                        ID: $(item).data('id') 
                        }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'Json',
                type: 'POST',
                success:function(res)
                {
                    if(res.status == true)
                    {
                        window.location.href='/gio-hang'
                    }
                }
            })
        });

        //Delete All cart
        $('#btnDelAll').off("click").on("click", function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang'
                    }
                }
            })
        });

        //Delete item
        $('.btn-delete').off("click").on("click", function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Cart/DeleteItem',
                data: {productId: $(this).data('id')},
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang'
                    }
                }
            })
        });

        $('#btnPayment').off("click").on("click", function () {
            window.location.href = "thanh-toan";
        });
    }
}
cart.init();