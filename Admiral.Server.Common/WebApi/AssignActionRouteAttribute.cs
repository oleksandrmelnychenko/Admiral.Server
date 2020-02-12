using Microsoft.AspNetCore.Mvc;
using System;

namespace Admiral.Server.Common.WebApi {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AssignActionRouteAttribute : RouteAttribute {

        public AssignActionRouteAttribute(string template) : base(template) {
        }
    }
}
