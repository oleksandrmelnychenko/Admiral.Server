using Microsoft.AspNetCore.Mvc;
using System;

namespace Admiral.Server.Common.WebApi {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AssignControllerRouteAttribute : RouteAttribute {
        /// <summary>
        ///     Web Api version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        ///     Dev or release environment.
        /// </summary>
        public string Environment { get; private set; }

        /// <summary>
        ///     ctor().
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="version">The web api version.</param>
        /// <param name="template">The route template.</param>
        public AssignControllerRouteAttribute(string environment, int version, string template) :
            base($"{environment}/{BuildRouteVersion(version)}/{template}") {
            //base($"{environment}/{BuildRouteVersion(version)}/{template}") {

            Version = BuildRouteVersion(version);
            Environment = environment;
        }

        private static string BuildRouteVersion(int number) {
            return $"v{number.ToString()}";
        }
    }
}
