using System.Collections.Generic;

using Dapper;

using Npgsql;

namespace PgsqlBulkCopy.Benchmark
{
    public class BatchByInsert
    {
	    private const string BatchInsertSql =
			@"INSERT INTO public.crawlurl(code, url, sort, createtime) VALUES ({0});";

	    public void BatchInsert(IEnumerable<CrawlUrl> crawlUrls, int batchSize = 1000)
	    {
		    var paramNames = new[] {"code", "url", "sort", "createtime"};

		    foreach (var urlGroup in crawlUrls.ChunkBy(batchSize))
		    {
			    var (batchSql, values) = urlGroup.AsBatch(BatchInsertSql, paramNames);

			    using (var conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=sampledb;User Id=postgres;Password=123qwe!;"))
			    {
				    conn.Execute(batchSql, values);
			    }
		    }
	    }
    }
}
