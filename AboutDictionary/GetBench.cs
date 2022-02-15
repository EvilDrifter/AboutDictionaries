using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class GetBench
{
    private readonly Dictionary<Guid, int> _dictionary;
    private readonly ConcurrentDictionary<Guid, int> _concurrentDictionary;
    private readonly ImmutableDictionary<Guid, int> _immutableDictionary;
    private readonly Hashtable _hashtable;
    
    [Params(100, 1000, 10000)] 
    public int Target { get; set; }
    
    public GetBench()
    {
        _dictionary = new Dictionary<Guid, int>();
        _concurrentDictionary = new ConcurrentDictionary<Guid, int>();
        _hashtable = new Hashtable();
        _immutableDictionary = ImmutableDictionary.Create<Guid, int>();
        for (var i = 0; i < 1000; i++)
        {
            _dictionary.Add(Guid.NewGuid(), 5);
            _concurrentDictionary.TryAdd(Guid.NewGuid(), 5);
            _hashtable.Add(Guid.NewGuid(), 5);
            _immutableDictionary = _immutableDictionary.Add(Guid.NewGuid(), 5);
        }
    }
    
    [Benchmark]
    public int? Dictionary_Get()
    {
        int? result = null;
        for (int i = 0; i < Target; i++)
        {
            var key = Guid.NewGuid();
            if (_dictionary.ContainsKey(key))
            {
                result = _dictionary[key];
            }
        }

        return result;
    }
    
    [Benchmark]
    public int? ConcurrentDictionary_Get()
    {
        int? result = null;
        for (int i = 0; i < Target; i++)
        {
            var key = Guid.NewGuid();
            if (_concurrentDictionary.ContainsKey(key))
            {
                result = _concurrentDictionary[key];
            }
        }

        return result;
    }
    
    [Benchmark]
    public int? Hashtable_Get()
    {
        int? result = null;
        for (int i = 0; i < Target; i++)
        {
            var key = Guid.NewGuid();
            if (_hashtable.ContainsKey(key))
            {
                result = (int)_hashtable[key]!;
            }
        }

        return result;
    }
    
    [Benchmark]
    public int? ImmutableDictionary_Get()
    {
        int? result = null;
        for (int i = 0; i < Target; i++)
        {
            var key = Guid.NewGuid();
            if (_immutableDictionary.ContainsKey(key))
            {
                result = _immutableDictionary[key];
            }
        }

        return result;
    }
}