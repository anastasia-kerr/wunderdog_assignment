﻿using System.Text;
using Newtonsoft.Json;

namespace RPS.IntegrationTests.Helpers;

public class JsonContent : StringContent
{
    public JsonContent(object obj) : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json") { }
}
