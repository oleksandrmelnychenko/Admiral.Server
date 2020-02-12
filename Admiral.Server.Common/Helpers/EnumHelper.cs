using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Admiral.Server.Common.Helpers {
    public static class EnumHelper {
        public static string GetDescription<T>(this T e) where T : IConvertible {
            if (e is Enum) {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values) {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture)) {
                        var memInfo = type.GetMember(type.GetEnumName(val));

                        if (memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() is DescriptionAttribute descriptionAttribute) {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return null;
        }
    }
}
