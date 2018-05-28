using System;
using System.Text.RegularExpressions;

namespace PgsqlBulkCopy.Benchmark
{
	public static class StringExtension
	{
		/// <summary>
		/// 截取字符串的指定长度
		/// </summary>
		public static string NullableSubstring(this string value, int length)
		{
			if (value == null)
				return string.Empty;

			return value.Length > length ? value.Substring(0, length) : value;
		}

		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		/// <summary>
		/// value?.Trim()
		/// </summary>
		public static string NullableTrim(this string value)
		{
			return value?.Trim();
		}

		/// <summary>
		/// 替换字符串中的单引号
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ReplaceQuota(this string value)
		{
			return Regex.Replace(value ?? string.Empty, "\'", "''");
		}

		/// <summary>
		/// 按条件分割字符串
		/// </summary>
		/// <param name="source"></param>
		/// <param name="separator"></param>
		/// <returns></returns>
		public static string[] SplitWithoutEmpty(this string source, char separator)
		{
			return source?.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
		}

		#region TryParse

		/// <summary>
		/// Enum.TryParse
		/// </summary>
		public static T AsEnum<T>(this string value) where T : struct
		{
			return value.AsEnum(default(T));
		}

		public static T AsEnum<T>(this string value, T defaultValue) where T : struct
		{
			return Enum.TryParse(value, true, out T t) ? t : defaultValue;
		}

		public static bool AsBool(this string value)
		{
			return value.AsBool(false);
		}

		public static bool AsBool(this string value, bool defaultValue)
		{
			return bool.TryParse(value, out bool flag) ? flag : defaultValue;
		}

		public static DateTime AsDateTime(this string value)
		{
			return value.AsDateTime(new DateTime());
		}

		public static DateTime AsDateTime(this string value, DateTime defaultValue)
		{
			return DateTime.TryParse(value, out DateTime time) ? time : defaultValue;
		}

		public static DateTime? AsNullableDateTime(this string value)
		{
			return value.AsNullableDateTime(null);
		}

		public static DateTime? AsNullableDateTime(this string value, DateTime? defaultValue)
		{
			return DateTime.TryParse(value, out DateTime time) ? time : defaultValue;
		}

		public static decimal AsDecimal(this string value)
		{
			return value.AsDecimal(0m);
		}

		public static decimal AsDecimal(this string value, decimal defaultValue)
		{
			return decimal.TryParse(value, out decimal num) ? num : defaultValue;
		}

		public static double AsDouble(this string value)
		{
			return value.AsDouble(0d);
		}

		public static double AsDouble(this string value, double defaultValue)
		{
			return double.TryParse(value, out double num) ? num : defaultValue;
		}

		public static float AsFloat(this string value)
		{
			return value.AsFloat(0f);
		}

		public static float AsFloat(this string value, float defaultValue)
		{
			return float.TryParse(value, out float num) ? num : defaultValue;
		}

		public static long AsLong(this string value)
		{
			return value.AsLong(0L);
		}

		public static long AsLong(this string value, long defaultValue)
		{
			return long.TryParse(value, out long num) ? num : defaultValue;
		}

		public static int AsInt(this string value)
		{
			return value.AsInt(0);
		}

		public static int AsInt(this string value, int defaultValue)
		{
			return int.TryParse(value, out int num) ? num : defaultValue;
		}

		public static short AsShort(this string value)
		{
			return value.AsShort(0);
		}

		public static short AsShort(this string value, short defaultValue)
		{
			return short.TryParse(value, out short num) ? num : defaultValue;
		}

		public static Guid AsGuid(this string value)
		{
			return value.AsGuid(default(Guid));
		}

		public static Guid AsGuid(this string value, Guid defaultValue)
		{
			return Guid.TryParse(value, out Guid guid) ? guid : defaultValue;
		}

		#endregion
	}
}
