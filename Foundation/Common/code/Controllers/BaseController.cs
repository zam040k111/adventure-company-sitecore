using Adventure.Foundation.Common.Exceptions;
using Sitecore.Mvc.Controllers;
using System;
using System.Web.Mvc;

namespace Adventure.Foundation.Common.Controllers
{
    public abstract class BaseController : SitecoreController
    {
        /// <summary>
        /// Method for using try...catch for safety invocation of your methods
        /// </summary>
        /// <returns>
        /// Returns ActionResult if there is no exceptions or call EmptyView method to show the exception
        /// </returns>
        public ActionResult ExecuteSafe(Func<ActionResult> actionMethodLogic)
        {
            try
            {
                return actionMethodLogic.Invoke();
            }
            catch(CannotDisplayComponentException ex)
            {
                return EmptyView(Sitecore.Globalization.Translate.Text(ex.MessageDictionaryKey));
            }
            catch (Exception ex)
            {
                return EmptyView(ex.Message);
            }
        }

        /// <summary>
        /// Returns view with exception if Content Editor is in Experience Editor and return an EmptyView page for website visitor
        /// </summary>
        /// <returns>
        /// Returns view with exception if Content Editor is in Experience Editor and return an EmptyView page for website visitor
        /// </returns>
        /// <remarks>
        ///  Don't forget to create EmptyView.cshtml in Views folder in your project!
        /// </remarks>
        public ActionResult EmptyView(string emptyViewMessage)
        {
            if (Sitecore.Context.PageMode.IsExperienceEditor)
            {
                return new PartialViewResult { ViewName = "~/Views/Base/EmptyView.cshtml", ViewData = new ViewDataDictionary(emptyViewMessage) };
            }

            return new EmptyResult();
        }
    }
}