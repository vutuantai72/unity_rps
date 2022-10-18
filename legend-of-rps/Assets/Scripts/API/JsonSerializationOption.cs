using Newtonsoft.Json;
using System;
using UnityEngine;

public class JsonSerializationOption : ISerializationOption
{
    public string ContentType => "application/json";

    public T Deserialize<T>(string text)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(text);
            Debug.Log($"[Json Deserialize] Success: {text}");
            return result;
        }
        catch (Exception ex)
        {
            Debug.Log($"[Json Deserialize] Could not parse response {text}. {ex.Message}");
            return default;
        }
    }
}
