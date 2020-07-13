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
		}
		public void RemoveConnection(Guid id) {
			var client = Clients.FirstOrDefault(c => c.Id == id);
			if (client != null)
				Clients.Remove(client);
		}
		public void BroadcastMessage(byte[] data, Guid senderId) {
			if (data.Length == 0) {
				return;
			}
			foreach (var client in Clients) {
				if (client.Id != senderId) {
					client.Stream.Write(data, 0, data.Length);
				}
			}
		}
		public void Listen() {
			try {
				_tcpListener = new TcpListener(IPAddress.Any, 8888);
				_tcpListener.Start();
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