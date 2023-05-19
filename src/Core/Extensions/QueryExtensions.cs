using System.Linq;


namespace Jh.Core.Extensions
{
    public static class SomeExtensions
    {
        public static void SetValues(this object from, object to)
        {
            var fromType = from.GetType();
            var toType = to.GetType();
            foreach (var property in toType.GetProperties())
            {
                var exist = fromType.GetProperties().FirstOrDefault(m => string.Equals(m.Name, property.Name, System.StringComparison.OrdinalIgnoreCase));
                if (exist != null)
                {
                   var existValue= exist.GetValue(to);
                    property.SetValue(to, existValue);
                }
            }
        }
    }
}
