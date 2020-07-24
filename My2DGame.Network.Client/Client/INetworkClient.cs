using System;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Client {
	public interface INetworkClient {
		event Action<INetworkObject> Message;
		void Connect(string ipAddress, int port);
		void Send(INetworkObject obj, Guid roomId, QueryType queryType);
		void Disconnect();
		bool GetIsConnect();
	}
}
