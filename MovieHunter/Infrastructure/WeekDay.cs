namespace MovieHunter.Infrastructure
{
    public class WeekDay : IRouteConstraint
    {
        private static string[] Days = new[] { "mon", "tue", "wed", "thu", "fri", "sat", "sun" };

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string currentDay = DateTime.Now.DayOfWeek.ToString().ToLowerInvariant().Substring(0, 3);

            return values[routeKey]?.ToString().ToLowerInvariant() == currentDay;
        }
    }
}
