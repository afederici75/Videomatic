﻿using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.TestData;

public static class JsonHelper
{
    public static JsonSerializerSettings GetJsonSettings()
    {
        return new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }


}