using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using My2DGame.Network.Contract;
using My2DGame.Network.Utilities;

namespace My2DGame.Network.Client.Client.TCP {
	public class ClientTcp : INetworkClient {
		public ILogger<ClientTcp> Logger { get; }
		private TcpClient _client;
		private NetworkStream _stream;
		public event Action<INetworkObject> Message;
		public ClientTcp(ILogger<ClientTcp> logger) {
			Logger = logger;
		}
		public void Connect(string ipAddress, int port) {
			Logger.Log(LogLevel.Debug, "connect");
			_client = new TcpClient(ipAddress, port);
			_stream = _client.GetStream();
			Task.Run(() => SubscribeMessage(_stream));
		}
		public void Send(INetworkObject obj, Guid roomId, QueryType queryType) {
			var data = obj.ToBytes().GetRequestData(obj?.RequestItemId ?? Guid.Empty, roomId, queryType).ToArray();
			_stream.Write(data, 0, data.Length);
		}
		protected virtual void SubscribeMessage(NetworkStream stream) {
			try {
				while (true) {
					if (!stream.DataAvailable)
						continue;
					var message = stream.GetMessageObj();
					OnMessage(message as INetworkObject);
				}
			} catch (Exception ex) {
				Disconnect(); //todo log
				throw;
			}
		}
		public void Disconnect() {
			_stream?.Close();
			_client?.Close();
		}
		public bool GetIsConnect() {
			return _client.Connected;
		}
		protected virtual void OnMessage(INetworkObject obj) {
			Message?.Invoke(obj);
		}
	}
}