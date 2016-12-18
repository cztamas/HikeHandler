var BaseHandler = { 

	horizontal : "Search",
	vertical : "Hike",

	hikeClicked : function() {
		$('#verticalNav li').removeClass("active");
		$('#hikeButton').addClass("active");
		this.vertical = 'Hike';
		this.refreshFrame();
	},

	regionClicked : function() {
		$('#verticalNav li').removeClass("active");
		$('#regionButton').addClass("active");
		this.vertical = 'Region';
		this.refreshFrame();
	},

	countryClicked : function() {
		$('#verticalNav li').removeClass("active");
		$('#countryButton').addClass("active");
		this.vertical = 'Country';
		this.refreshFrame();
	},

	cpClicked : function() {
		$('#verticalNav li').removeClass("active");
		$('#cpButton').addClass("active");
		this.vertical = 'CP';
		this.refreshFrame();
	},

	searchClicked : function() {
		$('#horizontalNav li').removeClass("active");
		$('#searchButton').addClass("active");
		this.horizontal = 'Search';
		this.refreshFrame();
	},

	addClicked : function() {
		$('#horizontalNav li').removeClass("active");
		$('#addButton').addClass("active");
		this.horizontal = 'Add';
		this.refreshFrame();
	},

	statClicked : function() {
		$('#horizontalNav li').removeClass("active");
		$('#statButton').addClass("active");
		this.horizontal = 'Stat';
	},

	refreshFrame : function() {
		var source = "/Content/UI/" + this.horizontal + this.vertical + ".html";
		console.log(source);
		$('#frame').attr("src", source);
	}
}