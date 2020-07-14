using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using My2DGame.Network.Utilities;

namespace My2DGame.Network.Server.Client {
	public class NetworkClient {
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
					var message = Stream.GetMessageBytes();
					var (id, date) = message.GetRequestIdData();
					_server.BroadcastMessage(date.ToArray(), Id);
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
		
	}
}