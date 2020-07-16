using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using My2DGame.Network.Contract;
using My2DGame.Network.Utilities;

namespace My2DGame.Network.Client.Client.TCP {
	public class ClientTcp : INetworkClient {
		private TcpClient _client;
		private NetworkStream _stream;
		public event Action<INetworkObject> Message;
		public void Connect(string ipAddress, int port) {
			_client = new TcpClient(ipAddress, port);
			_stream = _client.GetStream();
			Task.Run(() => SubscribeMessage(_stream));
		}
		public void Send(INetworkObject obj, Guid roomId) {
			var data = obj.ToBytes().GetRequestData(obj?.RequestItemId ?? Guid.Empty, roomId).ToArray();
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