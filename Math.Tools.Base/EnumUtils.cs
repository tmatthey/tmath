using System;
using System.Globalization;

namespace Math.Tools.Base
{
	public static class EnumUtils
	{
		public static T ParseEnum<T>(string value, T defaultValue) where T : struct, IConvertible
		{
/*
#if NET_CORE_APP_1_1
			if (!typeof(T).GetTypeInfo().IsEnum)
#else
            if (!typeof(T).IsEnum)
#endif
				throw new ArgumentException("T must be an enumerated type");
*/
			if (string.IsNullOrEmpty(value)) return defaultValue;

			foreach (T item in Enum.GetValues(typeof(T)))
			{
				if (item.ToString(CultureInfo.InvariantCulture).ToLower().Equals(value.Trim().ToLower())) return item;
			}

			return defaultValue;
		}
	}
}