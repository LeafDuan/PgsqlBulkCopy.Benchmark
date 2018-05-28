using System;
using System.Collections.Generic;
using System.IO;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

using Newtonsoft.Json;

namespace PgsqlBulkCopy.Benchmark
{
	[Config(typeof(FastRunConfig))]
	public class InsertVsCopy
	{
		private readonly BatchByInsert _insert = new BatchByInsert();
		private readonly BatchByCopy _copy = new BatchByCopy();

		private List<CrawlUrl> CrawlUrls { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			//var file = Path.Combine(Directory.GetCurrentDirectory(), "crawl-urls.txt");
			var lines = File.ReadAllText(@"X:\github\PgsqlBulkCopy.Benchmark\crawl-urls.txt");

			CrawlUrls = JsonConvert.DeserializeObject<List<CrawlUrl>>(lines);
		}

		[Benchmark]
		public void Copy() => _copy.BatchCopy(CrawlUrls);

		[Benchmark]
		public void Insert() => _insert.BatchInsert(CrawlUrls);
	}

	public class FastRunConfig: ManualConfig
	{
		public FastRunConfig()
		{
			Add(Job.DryCore
				.WithLaunchCount(1)
				.WithWarmupCount(1)
				.WithTargetCount(1));
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<InsertVsCopy>();

			Console.Read();
		}
	}
}
