using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AddBench
{
    [Params(100, 1000, 10000)]
    public int Target { get; set; }
    
    [Benchmark]
    public void Dictionary_Add_100()
    {
        var dictionary = new Dictionary<Guid, int>();
        for (var i = 0; i < Target; i++)
        {
            dictionary.Add(Guid.NewGuid(), 5);
        }
    }

    [Benchmark]
    public void ConcurrentDictionary_Add_100()
    {
        var dictionary = new ConcurrentDictionary<Guid, int>();
        for (var i = 0; i < Target; i++)
        {
            dictionary.TryAdd(Guid.NewGuid(), 5);
        }
    }
    
    [Benchmark]
    public void Hashtable_Add_100()
    {
        var dictionary = new Hashtable();
        for (var i = 0; i < Target; i++)
        {
           dictionary.Add(Guid.NewGuid(), 5);
        }
    }
    
    [Benchmark]
    public void ImmutableDictionary_Add_100()
    {
        var dictionary = ImmutableDictionary.Create<Guid, int>();
        for (var i = 0; i < Target; i++)
        {
            dictionary = dictionary.Add(Guid.NewGuid(), 5);
        }
    }
}