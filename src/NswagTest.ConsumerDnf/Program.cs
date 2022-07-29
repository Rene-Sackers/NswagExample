using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using NswagTest.Client;

namespace NswagTest.ConsumerDnf
{
	internal class Program
	{
		public static async Task Main(string[] args)
		{
			var httpClient = new HttpClient();
			var client = new ApiClient("https://localhost:7129/", httpClient);
			var result = await client.AddAsync(new AddRequestData { Value1 = 1, Value2 = 2});
			Debug.Assert(result.Sum == 3);
			Console.WriteLine("Result: " + result.Sum);
		}
	}
}
