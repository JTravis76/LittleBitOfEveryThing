using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Utilities.ChartJS;

namespace WebApp.Mvc.Services
{
    public class ReportSvc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static BarChart Report1(System.Web.Mvc.FormCollection fc = null) 
        {
            //build custom Linq WHERE clause
            //NOTE: Use PredicateBuilder.True for AND() function, 
            //          PredicateBuilder.False for Or() function
            var predicate = LinqKit.PredicateBuilder.New<DataContext.SalesOrderDetail>();

            if (fc != null)
            {
                //f       <-gets form control Name
                //fc[f]   <-gets form control Value
                foreach (string f in fc.AllKeys)
                {
                    System.Diagnostics.Debug.Print(f + ": " + fc[f].ToString());

                    if (fc[f].ToString() != string.Empty)
                    {
                        if (fc[f] != "False")
                        {
                            int year = System.Convert.ToInt32(f);
                            predicate = predicate.Or(x => x.ModifiedDate.Year == year);
                        }                        
                    }
                }
            }



            //** Query Report **//
            using (var db = new DataContext.AdventureWorksEntities())
            {
                IQueryable<Models.SaleOrderDetail> q;

                if(predicate.Body.NodeType == System.Linq.Expressions.ExpressionType.Constant)
                {
                    q = db.SalesOrderDetails
                        .GroupBy(x => x.ModifiedDate.Year)
                        .Select(s => new Models.SaleOrderDetail
                        {
                            Year = s.Key,
                            SumOfLineTotal = s.Sum(o => o.LineTotal)
                        }).OrderBy(x => x.Year);
                } else
                {
                    q = db.SalesOrderDetails
                        .Where(predicate)
                        .GroupBy(x => x.ModifiedDate.Year)                        
                        .Select(s => new Models.SaleOrderDetail
                        {
                            Year = s.Key,
                            SumOfLineTotal = s.Sum(o => o.LineTotal)
                        }).OrderBy(x => x.Year);
                }


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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PieChart Report2()
        {
            using (var db = new DataContext.AdventureWorksEntities())
            {
                var q = db.SalesOrderDetails.GroupBy(x => x.ModifiedDate.Year)
                    .Select(s => new
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

                //add data and values to chart
                List<ChartDataset> dsChart1 = new List<ChartDataset>();
                dsChart1.Add(
                    new ChartDataset
                    {
                        Data = Data,
                        Label = "Yearly Sales",
                        backgroundColor = new[] { "#FF7F7F", "rgba(255,127,182,0.5)" },
                        hoverBackgroundColor = new[] { "#FF0000", "#0026FF", "#FFD800", "#C0C0C0" },
                        hoverBorderWidth = 4,
                        hoverBorderColor = "#000000"                    
                    }
                );


                var chart = new PieChart();
                chart.DataSource.Labels = Series;
                chart.DataSource.Datasets = dsChart1;

                chart.GlobalConfiguration.Animation.animateRotate = false;

                return chart;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PolarAreaChart Report3()
        {
            using (var db = new DataContext.AdventureWorksEntities())
            {
                var q = db.SalesOrderDetails.GroupBy(x => x.ModifiedDate.Year)
                    .Select(s => new
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

                //add data and values to chart
                List<ChartDataset> dsChart1 = new List<ChartDataset>();
                dsChart1.Add(
                    new ChartDataset
                    {
                        Data = Data,
                        Label = "Yearly Sales",
                        backgroundColor = new[] { "#FF6384","#36A2EB","#FFCE56" },
                        hoverBackgroundColor = new[] { "#FF0000", "#0026FF", "#FFD800", "#C0C0C0" },
                        hoverBorderWidth = 2,
                        hoverBorderColor = "#000000"   
                    }
                );

                var bc = new PolarAreaChart();
                bc.DataSource.Labels = Series;
                bc.DataSource.Datasets = dsChart1;
                
                bc.GlobalConfiguration.Title.Display = false;
                return bc;
            }
        }


        public static LineChart Report4()
        {
            //ChartJS collections
            List<string> Series = new List<string>();
            List<double> Data = new List<double>();

            using(var db = new DataContext.AdventureWorksEntities())
            {
                //1st: query some data
                var q = db.SalesOrderDetails
                    .GroupBy(x => x.ModifiedDate.Year)
                    .Select(s => new
                    {
                        Year = s.Key,
                        SumOfLineTotal = s.Sum(o => o.LineTotal)
                    }).OrderBy(x => x.Year);
                
                //Fill ChartJS List
                foreach (var item in q)
                {
                    Series.Add(item.Year.ToString());
                    Data.Add(Convert.ToDouble(item.SumOfLineTotal));
                }
            }


            //add data and values to chart
            List<ChartDataset> dsChart1 = new List<ChartDataset>();
            dsChart1.Add(
                new ChartDataset
                {
                    Data = Data,
                    Label = "Yearly Sales",
                    backgroundColor = "rgba(100,100,100,0.5)",
                    borderColor = "rgba(0,0,0,0.8)",
                    borderwidth = 1,
                    hoverBackgroundColor = "rgba(100,100,100,0.75)",
                    hoverBorderColor = "rgba(0,0,0,0.1)"
                }
            );


            var chart = new LineChart();
            chart.DataSource.Labels = Series;
            chart.DataSource.Datasets = dsChart1;

            chart.GlobalConfiguration.responsive = true;
            chart.GlobalConfiguration.Title.Display = true;
            chart.GlobalConfiguration.Title.Text = "Total Line Cost by Year";

            return chart;
        }

    }
}