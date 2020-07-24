using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using My2DGame.Network.Server.Client;

namespace My2DGame.Network.Server.TCP {
	public class Server : IServer {
		static TcpListener _tcpListener;
		private List<NetworkClient> Clients { get; } = new List<NetworkClient>();
		public void AddConnection(NetworkClient clientItem) {
			Clients.Add(clientItem);
			Console.WriteLine("connect " + clientItem.Id);
		}
		public void RemoveConnection(Guid id) {
			var client = Clients.FirstOrDefault(c => c.Id == id);
			if (client != null)
				Clients.Remove(client);
			Console.WriteLine("remove " + id);
		}
		public void BroadcastMessage(byte[] data, Guid senderId) {
			if (data.Length == 0) {
				return;
			}
			foreach (var client in Clients) {
				if (client.Id != senderId) {
					Send(client, data);
				}
			}
		}
		public void Send(NetworkClient client, byte[] data) {
			client.Stream.Write(data, 0, data.Length);
		}
		public void Listen(IPAddress address, int port) {
			try {
				_tcpListener = new TcpListener(address, port);
				_tcpListener.Start();
				Console.WriteLine("Start");
				while (true) {
					var tcpClient = _tcpListener.AcceptTcpClient();
					var clientObject = new NetworkClient(tcpClient, this, Guid.NewGuid());
					AddConnection(clientObject);
					Task.Run(clientObject.Process);
				}
			} catch (Exception ex) {
				Disconnect();
			}
		}
		public void Disconnect() {
			_tcpListener?.Stop();
			foreach (var t in Clients) {
				t.Close();
			}
			Clients.Clear();
		}
	}
}