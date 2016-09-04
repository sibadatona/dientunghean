var user =
{
	init: function () {
		user.RegisterEvent();
	},
	RegisterEvent: function () {
		$('.btn-active').off('click').on('click', function (e) {
			e.preventDefault();
			var btn = $(this);
			var id = btn.data('id');
			$.ajax({
				url: "/Admin/User/ChangeStatus",
				data: { id: id },
				dataType: "json",
				type: "POST",
				success: function (response) {
					console.log(response);
					if (response.status == true) {
						btn.text("Active");
					} else {
						btn.text("Block")
					}
				}
			});
		});

		$(".editDialog").off('click').on("click", function (e) {

			var url = $(this).attr('href');
			$("#dialog-confirm").dialog({
				autoOpen: false,
				resizable: false,
				height: 170,
				width: 350,
				show: { effect: 'drop', direction: "up" },
				modal: true,
				draggable: true,
				buttons: {
					"OK": function () {
						$(this).dialog("close");
						window.location = url;

					},
					"Cancel": function () {
						$(this).dialog("close");

					}
				}
			});
			$("#dialog-confirm").dialog('open');
			return false;
		});

		$('.btn-Update').on('click', function () {
		    var location = '/Admin/User/Update';
		    var jsonData = '3';
		    $.ajax({
		        url: location,
		        type: "POST",
		        contentType: "application/jsons; charset=utf-8",
		        data: JSON.stringify(jsonData),
		        success: function (data) {
		            if (IsEmpty(data) == false && data.CommonError) {
		                window.location.href = data.url;
		                return;
		            }

		            if (funcSuccess) funcSuccess(data);
		        },
		        error: function (data) {
		            if (IsEmpty(data) == false && (data.statusText == "error" || data.statusText == "timeout")) {
		                window.location.href = "/Common/Error/SysException";
		                return;
		            }

		            if (funcError) funcError(data);
		        }
		    });

		    //alert('a');
		    //var id = $(this).data('id');
		    //var urlModal = '/Admin/User/Update';
		    //callModal('H2034Modal', urlModal, { autoOpen: true, dataModal: { id: id }, width: "1000" });

			//$("#edit-person-container").dialog({
			//	autoOpen: true,
			//	position: { my: "center", at: "top+350", of: window },
			//	width: 1000,
			//	resizable: false,
			//	title: 'Add User Form',
			//	modal: true,
			//	open: function () {
			//		//$(this).load('@Url.Action("GetList", "User")');
			//		$(this).load('/User/GetList');
			//	},
			//	buttons: {
			//		"Add User": function () {
			//			addUserInfo();
			//		},
			//		Cancel: function () {
			//			$(this).dialog("close");
			//		}
			//	}
			//});

			//$.ajax({
			//	url: '/Admin/User/GetList', // The method name + paramater
			//	success: function (data) {
			//		$('#edit-person-container').html(data); // This should be an empty div where you can inject your new html (the partial view)
			//	}
			//});
		});
	},
}
user.init();