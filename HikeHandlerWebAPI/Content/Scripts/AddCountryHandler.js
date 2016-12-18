var AddCountryHandler = {

	addCountry: function(countryName, description, onSuccess) {

		$.ajax({
	        type: 'POST',
	        url: '/hikehandler/Data/SaveCountry',
	        data: JSON.stringify({ Name: countryName, Description: description }),
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        
	        success: function(data) {
	        	console.log(data);

	        	onSuccess(data)
	        },
	        
	        failure: function(errMsg) {
	            alert(errMsg);
	        }
	    });


	    console.log('asdasd');

	},

	saveCountry: function() {

		var countryName = $('#nameBox').val();
		var description = $('#descriptionBox').val();

		this.addCountry(countryName, description, function(data) {
			console.log("save completed");
		});
	},


}
