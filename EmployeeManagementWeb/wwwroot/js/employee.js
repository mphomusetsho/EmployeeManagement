google.load("visualization", "1", { packages: ["orgchart"] });
var data; var repo; var selectedId; var btnEdit; var url; var id;

$("#btnOrgChart").on('click', function (e) {
	var btnDetails = document.getElementById('tblDetails');
	if (btnDetails.style == 'visible') {
		btnDetails.style.visibility = 'hidden';
	}

	$.ajax({
		type: "POST",
		url: "/Employee/Home/GetData",
		data: '{}',		
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: OnSuccess_getOrgData,
		error: OnErrorCall_getOrgData
	});

	function OnSuccess_getOrgData(repo) {
		data = new google.visualization.DataTable();
		data.addColumn('string', 'Name');
		data.addColumn('string', 'Manager');
		data.addColumn('string', 'ToolTip');

		for (var i = 0; i < repo.length; i++) {
			row = repo[i];
			var empID = repo[i].employeeNumber.toString();
			var empName = repo[i].firstName;
			
			if (repo[i].managerID == null) {
				var mgrID = "";
			} else {
				var mgrID = repo[i].managerID.toString();
			}

			var designation = repo[i].role;

			// For each orgchart box, provide the name, manager, and tooltip to show.
			data.addRows([[{
				v: empID,
				f: empName + '<div style="color:red">' + designation + '</div>'
			}, mgrID, designation]]);
			
		}

		var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));

		function selectHandler() {
			var selectedItem = chart.getSelection()[0];
			if (selectedItem) {
				selectedId = data.getValue(selectedItem.row, 0);
				id = parseInt(selectedId);
				console.log('type: ' + typeof id);
				/*url = '/Admin/Employee/Delete/' + selectedId*/
				displayButtons();
				
			}
		}

		function displayButtons() {
			var x = document.getElementById('searchEditBtns');
			if (x.style.visibility == 'hidden') {
				x.style.visibility = 'visible';
				//document.getElementById('btnEdit').onclick = Delete('/Admin/Employee/Delete/' + selectedId);

			} 	


		}
		//document.getElementById('btnEdit').onclick = Delete('/Admin/Employee/Delete/' + selectedId);


		google.visualization.events.addListener(chart, 'select', selectHandler);
		chart.draw(data, { allowHtml: true });

		var obj = document.getElementById('btnDelete');

		$("#btnDelete").on('click', function () {
			Delete('/Admin/Employee/Delete/?id=' + id);
        })

		//obj.addEventListener('click', function () {
		//	alert('You clicked');
  //      }, false)
	}

	function OnErrorCall_getOrgData() {
		console.log("Whoops something went wrong :( ");
	}
	e.preventDefault();

	// Delete an employee
	function Delete(url) {
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
						
						if (data.success) {
							OnSuccess_getOrgData.ajax.reload();
							toastr.success(data.message);
						} else {
							toastr.error("Cannot delete employee");
						}
					}
				})
			}
		})
	}

	// Edit an employee
	function Edit(url) {
		$.ajax({
			url: url,
			type: 'POST',
			success: function (data) {
				if (data.success) {
					//OnSuccess_getOrgData.ajax.reload();
					toastr.success(data.message);
				} else {
					toastr.error(data.message);
				}
			}
		})
	}

});









