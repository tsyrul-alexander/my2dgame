namespace My2DGame.Network.Server.Console {
	class Program {
		static void Main(string[] args) {
			var server = new TCP.Server();
			server.Listen();
		}
	}
}