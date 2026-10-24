PORTS.ChartJS


Required JQuery v2.2.3 ONLY!


This ChartJs class is a .Net wrapper for popular ChartJS found here.
http://www.chartjs.org
ChartJs builds wonderful looking charts using jQuery.


PORTS.ChartJs is a MVC HTML Helper to easily draw beautiful charts.
Sample below to create a Bar Chart using AdventureWork database.

Views/Home/Index.cshtml
-------------------------

	@using PORTS.ChartJS

	<div class="row">
		<div class="col-md-6">
			@Html.ChartJS("Chart1", (BarChart)ViewData["Chart1"])
		</div>
		<div class="col-md-6">
			@Html.ChartJS("Chart2", (PieChart)ViewData["Chart2"])
		</div>
	</div>




HomeController.cs
------------------

	ViewData["Chart1"] = Reports.Report1();



Utilities Class
----------------

	public static BarChart Report1() 
	{
		//** Query Report **//
		using (var db = new Models.AdventureWorksEntities())
		{
			IQueryable<Models.vmSalesOrderDetails> q;

			q = db.SalesOrderDetails
				.GroupBy(x => x.ModifiedDate.Year)
				.Select(s => new Models.vmSalesOrderDetails
				{
					Year = s.Key,
					SumOfLineTotal = s.Sum(o => o.LineTotal)
				}).OrderBy(x => x.Year);



			List<string> Series = new List<string>();
			List<double> Data = new List<double>();

			//Fill List
			foreach (var item in q)
			{
				Series.Add(item.Year.ToString());
				Data.Add(Convert.ToDouble(item.SumOfLineTotal));
			}

			//Select random numbers to be used to color chart elements
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			Random rnd = new Random();

			int r = rnd.Next(0, 254);
			int g = rnd.Next(0, 254);
			int b = rnd.Next(0, 254);

			sb.AppendFormat("{0},{1},{2},", r, g, b);

			//add data and values to chart
			List<ChartDataset> dsChart1 = new List<ChartDataset>();
			dsChart1.Add(
				new ChartDataset
				{
					Data = Data,
					Label = "Yearly Sales",
					backgroundColor = "rgba(" + sb.ToString() + "0.5)",
					borderColor = "rgba(" + sb.ToString() + "0.8)",
					borderwidth = 1,
					hoverBackgroundColor = "rgba(" + sb.ToString() + "0.75)",
					hoverBorderColor = "rgba(" + sb.ToString() + "0.1)"
				}
			);

			var bc = new BarChart();
			bc.DataSource.Labels = Series;
			bc.DataSource.Datasets = dsChart1;

			bc.GlobalConfiguration.responsive = true;
			bc.GlobalConfiguration.Title.Display = true;
			bc.GlobalConfiguration.Title.Text = "Total Line Cost by Year";
			return bc;
		}
	}