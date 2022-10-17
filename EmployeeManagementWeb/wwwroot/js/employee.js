google.load("visualization", "1", { packages: ["orgchart"] });
var data; var repo; var selectedId; var btnEdit;

$("#btnOrgChart").on('click', function (e) {

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
				displayButtons();
				
			}
		}

		function displayButtons() {
			var x = document.getElementById('searchEditBtns');
			if (x.style.visibility === 'hidden') {
				x.style.visibility = 'visible';
			} 	
		}

		google.visualization.events.addListener(chart, 'select', selectHandler);
		chart.draw(data, { allowHtml: true });
	}

	function OnErrorCall_getOrgData() {
		console.log("Whoops something went wrong :( ");
	}
	e.preventDefault();

	
});






