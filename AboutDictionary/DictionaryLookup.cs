public class DictionaryLookup
{
    readonly Dictionary<string, string> MyDictionary = new();
   
    public DictionaryLookup()
    {
        for(var i =0; i < 100; i++)
        {
            MyDictionary[i.ToString()] = $"test_{i}";
        }
    }
    
    public string GetValueByKey(string key)
    {
        if (MyDictionary.ContainsKey(key))
        {
            return MyDictionary[key];
        }
        return null;
    }
    
    public string GetValueByKeyWithLinq(string key)
    {
        return MyDictionary.FirstOrDefault(a => a.Key == key).Value;
    }

    public string GetValueByKeyWithTryGet(string key)
    {
        MyDictionary.TryGetValue(key, out string value);
        return value;
    }

    public string GetValueByKeyManual(string key)
    {
        foreach (var item in MyDictionary)
        {
            if(item.Key == key)
            {
                return item.Value;
            }
        }
        return null;
    }
}