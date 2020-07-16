using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace My2DGame.Network.Server.Console {
	class Program {
		static void Main(string[] args) {
			//IConfigurationProvider configuration = new JsonConfigurationProvider(new JsonConfigurationSource {//todo move to new class
			//	Path = Environment.CurrentDirectory + "app.json",
			//	ReloadOnChange = true
			//});
			//configuration.Load();
			IPAddress ipAddress = IPAddress.Any;
			//if (configuration.TryGet("ip_address", out var address)) {
			//	ipAddress = IPAddress.Parse(address);
			//}
			var port = 9976;
			//if (configuration.TryGet("port", out var portStr)) {
			//	port = int.Parse(portStr);
			//}
			var server = new TCP.Server();
			try {
				server.Listen(ipAddress, port);
			} catch (Exception e) {
				server.Disconnect();
				System.Console.WriteLine(e);
				throw;
			}
		}
	}
}