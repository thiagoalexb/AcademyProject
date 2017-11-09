﻿using Academy.WebAPI.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.WebAPI.Filters
{
    public class TokenPutFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var dic = filterContext.ActionArguments.FirstOrDefault();
            var defaultValue = default(KeyValuePair<string, object>);

            if (!dic.Equals(defaultValue))
            {
                dynamic model = dic.Value;
                model = SetPut(filterContext, model);
            }
        }

        private dynamic SetPut(ActionExecutingContext filterContext, dynamic model)
        {
            var path = filterContext.HttpContext.Request.Path.Value;
            var split = path.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (split.LastOrDefault().Contains("update"))
            {
                try
                {
                    model.LastUpdatedUserId = ManageToken.GetToken(filterContext.HttpContext.Request);
                }
                catch { }
                try
                {
                    model.LastUpdateDate = DateTime.Now;
                }
                catch { }
            }
            return model;
        }
    }
}
