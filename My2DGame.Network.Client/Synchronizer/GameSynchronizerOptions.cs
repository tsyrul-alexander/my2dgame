using System;

namespace My2DGame.Network.Client.Synchronizer
{
	public class GameSynchronizerOptions {
		public string IpAddress{ get;set; }
		public int Port { get;set; }
		public Guid RoomId { get; set; }
		public GameSynchronizerOptions(string ipAddress, int port, Guid roomId) {
			IpAddress = ipAddress;
			Port = port;
			RoomId = roomId;
		}
	}
}
