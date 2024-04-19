using BenchmarkDotNet.Attributes;
using DictionaryBenckmark.Enums;

namespace DictionaryBenckmark
{
  public class DictionaryBenchmarking
  {
    private readonly Dictionary<string, string> dic;

    private readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> innerDictionary;


    private readonly string _workGroup;
    private readonly string _code;
    private readonly string _lang;
    private readonly string _key;

    public DictionaryBenchmarking()
    {
      /* ------------------ First Method ------------------ */
      Dictionary<string, string[]> dictCodes = new Dictionary<string, string[]>
      {
        { Brand.Volkswagen.ToString(), Enum.GetNames(typeof(Volkswagen)) },
        { Brand.Audi.ToString(), Enum.GetNames(typeof(Audi)) },
        { Brand.Skoda.ToString(), Enum.GetNames(typeof(Skoda)) },
        { Brand.Seat.ToString(), Enum.GetNames(typeof(Seat)) },
        { Brand.Mercedes.ToString(), Enum.GetNames(typeof(Mercedes)) },
        { Brand.Bmw.ToString(), Enum.GetNames(typeof(Bmw)) },
      };

      dic = new Dictionary<string, string>();

      Random rnd = new Random(DateTime.Now.Millisecond);

      foreach (var brand in Enum.GetNames(typeof(Brand)))
      {
        foreach (var model in dictCodes[brand])
        {
          string keyTr = $"{brand}_{model}_tr";
          string keyEn = $"{brand}_{model}_en";

          dic.Add(keyTr, keyTr);
          dic.Add(keyEn, keyEn);
        }
      }

      /* ------------------ Second Method ------------------ */
      innerDictionary = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
      foreach (var brand in Enum.GetNames(typeof(Brand)))
      {
        innerDictionary.Add(brand, new Dictionary<string, Dictionary<string, string>>());
        foreach (var model in dictCodes[brand])
        {
          innerDictionary[brand].Add(model, new Dictionary<string, string>());
          innerDictionary[brand][model].Add("tr", $"{brand}_{model}_tr");
          innerDictionary[brand][model].Add("en", $"{brand}_{model}_en");
        }
      }

      int keyNumber = rnd.Next(dic.Keys.Count);
      _key = dic.Keys.ToArray()[keyNumber];
      string[] keys = _key.Split('_');
      _workGroup = keys[0];
      _code = keys[1];
      _lang = keys[2];
    }

    [Benchmark]
    public string GetValue()
    {
      return dic[_key];
    }

    [Benchmark]
    public string GetValueWithInnerDictionaries()
    {
      return innerDictionary[_workGroup][_code][_lang];
    }
  }
}
