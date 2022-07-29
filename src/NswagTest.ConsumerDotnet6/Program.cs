using System.Diagnostics;
using NswagTest.Client;

var httpClient = new HttpClient();
var client = new ApiClient("https://localhost:7129/", httpClient);
var result = await client.AddAsync(new () { Value1 = 1, Value2 = 2});
Debug.Assert(result.Sum == 3);
Console.WriteLine("Result: " + result.Sum);