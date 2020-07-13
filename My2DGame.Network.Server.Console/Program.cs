using System;

namespace My2DGame.Network.Server.Console {
	class Program {
		static void Main(string[] args) {
			var server = new TCP.Server();
			try {
				server.Listen();
			} catch (Exception e) {
				server.Disconnect();
				System.Console.WriteLine(e);
				throw;
			}
		}
	}
}