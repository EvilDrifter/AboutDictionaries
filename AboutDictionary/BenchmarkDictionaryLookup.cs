using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BenchmarkDictionaryLookup
{
    DictionaryLookup obj = new DictionaryLookup();
        
    [Benchmark(Baseline = true)] // mark this is as baseline method.
    public void GetValueByKey()
    {
        obj.GetValueByKey("test_99"); // i am doing it for worst case by searching the last item.
    }
    [Benchmark]
    public void GetValueByKeyWithLinq()
    {
        obj.GetValueByKeyWithLinq("test_99");
    }
    [Benchmark]
    public void GetValueByKeyWithTryGet()
    {
        obj.GetValueByKeyWithTryGet("test_99");
    }
    [Benchmark]
    public void GetValueByKeyManual()
    {
        obj.GetValueByKeyManual("test_99");
    }
}