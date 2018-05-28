using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Dapper;

namespace PgsqlBulkCopy.Benchmark
{
	internal static class DapperExtensions
	{
		public static (string names, DynamicParameters values) AsDynamicParameter<T>(this IEnumerable<T> parameters,
			string name)
		{
			var values = new DynamicParameters();
			var index = 0;

			foreach (var parameter in parameters)
			{
				values.Add($"{name}{index++}", parameter);
			}

			var names = values.ParameterNames.Select(p => $"@{p}").Join(",");

			return (names, values);
		}

		public static (string batchSql, DynamicParameters values) AsBatch<T>(
			this IEnumerable<T> items,
			string template,
			string[] paramNames)
		{
			var sqls = new StringBuilder();
			var values = new DynamicParameters();
			var index = 0;

			PropertyInfo[] properties = null;

			foreach (var item in items)
			{
				if (properties == null)
				{
					properties = item.GetType().GetCachedProperties();
				}

				sqls.AppendLine(string.Format(template, paramNames.Select(n => $"@{n}{index}").Join(",")));

				foreach (var property in properties)
				{
					values.Add($"{property.Name.ToLower()}{index}", property.GetValue(item));
				}

				index++;
			}

			return (sqls.ToString(), values);
		}
	}

	internal static class TypeExtensions
	{
		private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyInfos;

		static TypeExtensions()
		{
			PropertyInfos = new ConcurrentDictionary<Type, PropertyInfo[]>();
		}

		public static PropertyInfo[] GetCachedProperties(this Type type)
		{
			if (type == null)
			{
				return Array.Empty<PropertyInfo>();
			}

			if (!PropertyInfos.TryGetValue(type, out var properties))
			{
				properties = type.GetProperties();
				PropertyInfos.TryAdd(type, properties);
			}

			return properties;
		}
	}
}
