
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    /// <summary>  
    /// checkboxlist扩展类  
    /// </summary>  
    public static class HtmlHelperCheckBoxListExtensions
    {
        /// <summary>  
        /// checkboxlist扩展  
        /// </summary>  
        /// <param name="htmlHelper"></param>  
        /// <param name="listNameExpr"></param>  
        /// <param name="selectedValuesExpr"></param>  
        /// <param name="sourceDataExpr"></param>  
        /// <param name="valueExpr"></param>  
        /// <param name="textToDisplayExpr"></param>  
        /// <param name="htmlAttributes"></param>  
        /// <returns></returns>  
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty, TItem, TValue, TKey>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> listNameExpr, Expression<Func<TModel, IEnumerable<TValue>>> selectedValuesExpr, IEnumerable<TItem> sourceDataExpr, Expression<Func<TItem, TValue>> valueExpr, Expression<Func<TItem, TKey>> textToDisplayExpr, bool autoChangeLine = true, Dictionary<string, object> htmlAttributes = null)
        {
            string checkbuttonStr = string.Empty;
            int count = 1;
            var name = ExpressionHelper.GetExpressionText(listNameExpr);
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(listNameExpr, htmlHelper.ViewData);
            foreach (var item in sourceDataExpr)
            {
                TagBuilder checkbutton = new TagBuilder("input");
                checkbutton.Attributes.Add("type", "checkbox");
                checkbutton.Attributes.Add("name", name);
                checkbutton.Attributes.Add("value", valueExpr.Compile()(item).ToString());
                if (selectedValuesExpr.Compile()(htmlHelper.ViewData.Model) != null && selectedValuesExpr.Compile()(htmlHelper.ViewData.Model).Count() > 0)
                {
                    if (selectedValuesExpr.Compile()(htmlHelper.ViewData.Model).Contains(valueExpr.Compile()(item)))
                    {
                        checkbutton.Attributes.Add("checked", "checked");
                    }
                }
                if (count == 1)
                {
                    checkbutton.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, modelMetadata));
                }
                TagBuilder label = new TagBuilder("label");
                if (htmlAttributes != null)
                {
                    label.MergeAttributes(htmlAttributes);
                    label.InnerHtml = checkbutton.ToString() + textToDisplayExpr.Compile()(item).ToString();
                }
                if (count < sourceDataExpr.Count() && autoChangeLine)
                {
                    label.InnerHtml += "<br/>";
                }
                count++;
                checkbuttonStr += label.ToString();
            }
            return MvcHtmlString.Create(checkbuttonStr);
        }
    }
}