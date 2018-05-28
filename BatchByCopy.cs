using System.Collections.Generic;

using Npgsql;

namespace PgsqlBulkCopy.Benchmark
{
    public class BatchByCopy
    {
	    private const string BatchCopySql =
		    @"COPY public.crawlurl(code, url, sort, createtime) FROM STDIN BINARY;";

	    public void BatchCopy(IEnumerable<CrawlUrl> crawlUrls)
	    {
		    using (var conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=sampledb;User Id=postgres;Password=123qwe!;"))
		    {
				conn.Open();

			    using (var binaryCopyWriter = conn.BeginBinaryImport(BatchCopySql))
			    {
				    foreach (var crawlUrl in crawlUrls)
				    {
					    binaryCopyWriter.StartRow();

					    binaryCopyWriter.Write(crawlUrl.Code);
					    binaryCopyWriter.Write(crawlUrl.Url);
					    binaryCopyWriter.Write(crawlUrl.Sort);
					    binaryCopyWriter.Write(crawlUrl.CreateTime);
				    }
			    }
		    }
	    }
    }
}
