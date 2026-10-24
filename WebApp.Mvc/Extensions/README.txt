/*************************************************
* PORTS.ChecboxList
* v1.1.0
* 
* v1.0.0 - 1/1/2017 inital release
* v1.1.0 - 4/19/2017 add functionality to create a custom list to populate checkboxes via CheckBoxListItem class.
* v1.1.1 - 4/20/2017 Updated one of the function to better name the list of CheckBoxes via a GroupName. Also remove the Group property from the CheckBoxListItem class.
**************************************************/



@Html.CheckBoxList("cbl-year", (IEnumerable<SelectListItem>)ViewData["Years"], new { @class = "cb" })

ViewData["Years"] = new SelectList(db.Database.SqlQuery<System.Int32>("SELECT DISTINCT YEAR(ModifiedDate) FROM Sales.SalesOrderDetail ORDER BY YEAR(ModifiedDate) ASC;").ToList());

OR C# Razor snytax:

@{ 
    List<CheckBoxListItem> items = new List<CheckBoxListItem> {
        new CheckBoxListItem { Text="some amazing long product", Value = "myVal", Checked = true }
    };

    @Html.CheckBoxList("myGroupName", items, new { @class = "myClass" });
}


Data Slicer Example:

<style type="text/css">
    .temp1 {border:1px solid lightgray; border-radius:0px 0px 4px 4px; width:150px; height:auto; }
    .temp2 {width:100%; border-bottom:1px solid lightgray; font-weight:800; background-color:#f1eeee; padding-left:4px;}
    .temp3 {padding:0px 4px 0px 4px;}
    .temp4 {color:lightgray; cursor:pointer;}
    .temp4:hover {color:black; cursor:pointer;}
    .checkbox_container {color:silver;}
</style>

<div class="temp1">
    <div class="temp2">
        Year
        <div style="text-align:right; float:right;">
            <span id="clear-year" title="Clear all" class="glyphicon glyphicon-erase temp4"></span>
        </div>
    </div>
    <div class="temp3" style="height:200px;overflow-x:auto;">
        @Html.CheckBoxList("cbl-year", (IEnumerable<SelectListItem>)ViewData["Years"], new { @class = "cb" })
    </div>
</div>


<script>
    $(document).ready(function () {

        /*== upon click, set checkbox label color to default
        * if selected, change label color */
        $('.cb').click(function () {
            //set to silver as default
            $('label[for="' + this.id + '"]').css("color", "silver");
            $.each($("input:checked"), function () {
                $('label[for="' + this.id + '"]').css("color", "black");
            });
            UpdateChart();
        });

        //change color of all selected checkbox label
        //remove selected checkbox
        //refresh chart
        $('#clear-year').click(function () {
            $.each($("input:checked"), function () {
                $('label[for="' + this.id + '"]').css("color", "silver");
            });
            $('#div-cbl-year').children().prop("checked", false);
            UpdateChart();
        });

        //change color of all selected checkbox label
        //remove selected checkbox
        //refresh chart
        $('#clear-products').click(function () {
            $.each($("input:checked"), function () {
                $('label[for="' + this.id + '"]').css("color", "silver");
            });
            $('#div-cbl-products').children().prop("checked", false);

            UpdateChart();
        });

        function UpdateChart() {
            //this will post entire form
            var data = $('form').serialize();

            $.ajax({
                url: '@Url.Action("UpdateDashboard", "Home")',
                type: "POST",
                cache: false,
                async: true,
                data: data,
                beforeSend: function () { },
                complete: function () { },
                success: function (data) {
                    $('#divCharts').html(data);
                },
                error: function (e) {
                    alert("An error has occured!!!");
                }
            });
        }

    });//end document ready
</script>