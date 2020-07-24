using System;
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

		public void Process() {//todo refactoring
			try {
				Stream = _client.GetStream();
				while (true) {
					var message = Stream.GetMessageBytes();
					if (message.Length == 0) {
						break;
					}
					message.GetRequestInfo(out var data, out var itemId, out var roomId, out var queryType);
					Console.WriteLine($"{itemId} {queryType}");
					if (queryType == QueryType.GetAll) {
						if (GameRoomManager.GetIfExistsRoom(roomId)) {
							foreach (var roomData in GameRoomManager.GetData(roomId)) {
								_server.Send(this, roomData);
							}
						}
						continue;
					}
					GameRoomManager.Save(roomId, itemId, data, queryType);
					_server.BroadcastMessage(data, Id);
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