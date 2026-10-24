using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Mvc.Extensions
{
    public static class HtmlExtensions
    {
        #region CheckboxList 

        //public static System.Web.Mvc.MvcHtmlString TextBoxWithValue(this System.Web.Mvc.HtmlHelper htmlHelper, string name, string value, object htmlAttributes)
        //{
        //    var builder = new System.Web.Mvc.TagBuilder("input");
        //    builder.MergeAttribute("type", "text");
        //    builder.MergeAttribute("name", name);
        //    builder.MergeAttribute("value", value);
        //    builder.MergeAttribute(new System.Web.Routing.RouteValueDictionary(htmlAttributes), value );
        //    return System.Web.Mvc.MvcHtmlString.Create(builder.ToString(System.Web.Mvc.TagRenderMode.SelfClosing));
        //}

        /// <summary>
        /// Create a list of checkboxes via custom class
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IHtmlString CheckBoxList(this System.Web.Mvc.HtmlHelper htmlHelper, List<CheckBoxListItem> list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<div class='checkbox_container'>");

            //System.Web.Routing.RouteValueDictionary(htmlAttributes);
            string check = string.Empty;
            foreach (var item in list)
            {
                if (item.Text.Length > 0)
                {
                    if (item.Checked) { check = "checked='checked'"; }
                    sb.Append("<input name='" + item.Text + "' id='" + item.Text + "' type='checkbox' value='true' " + check + " class='Checkbox' /> ");
                    sb.Append("<input name='" + item.Text + "' type='hidden' value='" + item.Checked + "'>");
                    sb.Append(" <label for='" + item.Text + "'> " + item.Text + "</label><br />");
                    check = string.Empty;
                }
            }

            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Create a list of checkboxes via custom class
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IHtmlString CheckBoxList(this System.Web.Mvc.HtmlHelper htmlHelper, string GroupName, List<CheckBoxListItem> list, object htmlAttributes)
        {
            System.Web.Routing.RouteValueDictionary r = new System.Web.Routing.RouteValueDictionary(htmlAttributes);
            string Attr = "";
            foreach (KeyValuePair<string, object> k in r)
            {
                Attr += k.Key.ToString() + "='" + k.Value.ToString() + "'";
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<div class='checkbox_container'>");

            // Include 'Group Name' as a hidden field to help with Form POST
            // when using more than one CheckBoxList
            if (GroupName != null)
            {
                GroupName = GroupName.Replace(" ", "_");
                sb.Append("<input name='" + GroupName + "' type='hidden' value='" + GroupName + "'>");
            }
            else
                sb.Append("<input name='Group1' type='hidden' value='Group1'>");


            string check = string.Empty;
            foreach (var item in list)
            {
                if (item.Value == null)
                    item.Value = "";

                if (item.Text.Length > 0)
                {
                    string name = item.Text.Replace(" ", "_");
                    if (item.Checked) { check = "checked='checked'"; }
                    sb.Append("<input name='" + name + "' id='" + name + "' type='checkbox' value='true' " + check + " " + Attr + " data-value='" + item.Value + "' data-group=" + GroupName + " /> ");
                    sb.Append("<input name='" + name + "' type='hidden' value='" + item.Checked + "'>");
                    sb.Append(" <label for='" + name + "'> " + item.Text + "</label><br />");
                    check = string.Empty;
                }
            }

            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Create a list of checkboxes
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static IHtmlString CheckBoxList(this System.Web.Mvc.HtmlHelper htmlHelper, IEnumerable<System.Web.Mvc.SelectListItem> selectList)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<div class='checkbox_container'>");

            string check = string.Empty;
            foreach (var item in selectList)
            {
                if (item.Text.Length > 0)
                {
                    if (item.Selected) { check = "checked='checked'"; }
                    sb.Append("<input name='" + item.Text + "' id='" + item.Text + "' type='checkbox' value='true' " + check + " class='Checkbox' /> ");
                    sb.Append("<input name='" + item.Text + "' type='hidden' value='" + item.Selected + "'>");
                    sb.Append(" <label for='" + item.Text + "'> " + item.Text + "</label><br />");
                    check = string.Empty;
                }
            }
            sb.AppendLine("</div>");
            return new HtmlString(sb.ToString());
        }

        //public static System.Web.Mvc.MvcHtmlString CheckBoxList(this System.Web.Mvc.HtmlHelper htmlHelper, string listName, IEnumerable<System.Web.Mvc.SelectListItem> items, object htmlAttributes)
        //{
        //    var container = new System.Web.Mvc.TagBuilder("div");
        //    foreach (var item in items)
        //    {
        //        var label = new System.Web.Mvc.TagBuilder("label");
        //        label.MergeAttribute("class", "checkbox"); // default class
        //        label.MergeAttributes(new System.Web.Routing.RouteValueDictionary(htmlAttributes), true);

        //        var cb = new System.Web.Mvc.TagBuilder("input");
        //        cb.MergeAttribute("type", "checkbox");
        //        cb.MergeAttribute("name", listName);
        //        cb.MergeAttribute("value", item.Value ?? item.Text);
        //        if (item.Selected)
        //            cb.MergeAttribute("checked", "checked");

        //        label.InnerHtml = cb.ToString(System.Web.Mvc.TagRenderMode.SelfClosing) + item.Text;

        //        container.InnerHtml += label.ToString();
        //    }

        //    return new System.Web.Mvc.MvcHtmlString(container.ToString());
        //}

        public static IHtmlString CheckBoxList(this System.Web.Mvc.HtmlHelper htmlHelper, string listName, IEnumerable<System.Web.Mvc.SelectListItem> selectList, object htmlAttributes)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<div id='div-" + listName + "' class='checkbox_container'>");
            //used this hidden input to determine with CheckBoxlist was submitted.
            // search for _list to find start of list or list heading.
            sb.Append("<input name='" + listName + "_list' type='hidden' value=''>");

            System.Web.Routing.RouteValueDictionary r = new System.Web.Routing.RouteValueDictionary(htmlAttributes);

            string Attr = "";
            foreach (KeyValuePair<string, object> k in r)
            {
                Attr += k.Key.ToString() + "='" + k.Value.ToString() + "'";
            }

            string check = string.Empty;
            foreach (var item in selectList)
            {
                if (item.Text.Length > 0)
                {
                    if (item.Selected) { check = "checked='checked'"; }
                    sb.Append("<input name='" + item.Text + "' id='" + item.Text + "' type='checkbox' value='true' " + check + " " + Attr + " /> ");
                    sb.Append("<input name='" + item.Text + "' type='hidden' value='" + item.Selected + "'>");
                    sb.Append(" <label for='" + item.Text + "'> " + item.Text + "</label><br />");
                    check = string.Empty;
                }
            }
            sb.AppendLine("</div>");
            return new HtmlString(sb.ToString());
        }

        #endregion

        #region ChartJs

        /// <summary>
        /// The create chart.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="canvasId">
        /// The canvas id.
        /// </param>
        /// <param name="chartOptions">
        /// The chart options.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static System.Web.Mvc.MvcHtmlString ChartJS<TGlobalChartOptions>(this System.Web.Mvc.HtmlHelper htmlHelper, string canvasId, Utilities.ChartJS.ChartBase<TGlobalChartOptions> chartOptions) where TGlobalChartOptions : Utilities.ChartJS.GlobalChartOptions
        {
            if (chartOptions != null)
            {
                return CreateChart(canvasId, chartOptions.ChartType.ToString(), chartOptions.DataSource.ToJson(), chartOptions.GlobalConfiguration.ToJson());
            }
            return new System.Web.Mvc.MvcHtmlString("Error: no chart options.");
        }

        private static System.Web.Mvc.MvcHtmlString CreateChart(string canvasId, string chartType, string jsonData, string jsonOptions)
        {
            var canvas = new System.Web.Mvc.TagBuilder("canvas");
            canvas.Attributes.Add("id", canvasId);

            var tag = new System.Web.Mvc.TagBuilder("script");
            tag.Attributes.Add("type", "text/javascript");
            var stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendFormat("var ctx = document.getElementById(\"{0}\").getContext(\"2d\");", canvasId);
            stringBuilder.AppendFormat("var data = JSON.parse('{0}');", jsonData);
            stringBuilder.AppendFormat("var options = JSON.parse('{0}');", jsonOptions);
            stringBuilder.Append("var " + canvasId + "_chart = new Chart(ctx, { type: '" + chartType + "', data: data, options: options });");

            tag.InnerHtml = stringBuilder.ToString();

            canvas.InnerHtml += tag.ToString();

            return new System.Web.Mvc.MvcHtmlString(canvas.ToString());
        }

        #endregion

    }

    public class CheckBoxListItem
    {
        public bool Checked { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}