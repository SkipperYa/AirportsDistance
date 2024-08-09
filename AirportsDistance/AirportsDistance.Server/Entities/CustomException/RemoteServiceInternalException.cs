namespace AirportsDistance.Server.Entities.CustomException
{
	public class RemoteServiceInternalException : BusinessLogicException
	{
		public RemoteServiceInternalException(string message) : base(message)
		{

		}

		public RemoteServiceInternalException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
