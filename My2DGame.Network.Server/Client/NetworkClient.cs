using System;
using System.IO;
using System.Net.Sockets;

namespace My2DGame.Network.Server.Client
{
	public class NetworkClient
	{
		public Guid Id { get; }
		protected internal NetworkStream Stream { get; private set; }
		private readonly TcpClient _client;
		private readonly IServer _server;
		public NetworkClient(TcpClient tcpClient, IServer server, Guid id) {
			_client = tcpClient;
			_server = server;
			Id = id;
		}

		public void Process() {
			try {
				Stream = _client.GetStream();
				while (true) {
					var message = GetMessage(Stream);
					_server.BroadcastMessage(message, Id);
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			} finally {
				_server.RemoveConnection(Id);
				Close();
			}
		}
		protected internal void Close() {
			Stream?.Close();
			_client?.Close();
		}
		protected virtual byte[] GetMessage(NetworkStream stream) {
			var data = new byte[1024];
			using (var ms = new MemoryStream()) {
				do {
					var bytes = Stream.Read(data, 0, data.Length);
					ms.Write(data, 0, bytes);
				}
				while (Stream.DataAvailable);
				return ms.ToArray();
			}
		}
	}
}
