using System;
using System.Collections.Generic;
using System.Linq;

namespace PgsqlBulkCopy.Benchmark
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// when join the array format each string
		/// </summary>
		public static string Join<T>(this IEnumerable<T> target, string separator, Func<T, string> func = null)
		{
			return func == null ? string.Join(separator, target) : string.Join(separator, target.Select(func));
		}

		/// <summary>
		/// ForEach method for Generic IEnumerable
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			foreach (var item in list)
				action(item);
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
		{
			return new HashSet<T>(source ?? Enumerable.Empty<T>());
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			return !(source?.Any() ?? false);
		}

		public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
		{
			return source
				.Select((x, i) => new { Index = i, Value = x })
				.GroupBy(x => x.Index / chunkSize)
				.Select(x => x.Select(v => v.Value));
		}

		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
				yield break;

			var keys = new HashSet<TKey>();

			foreach (var element in source)
			{
				if (keys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}
	}
}
