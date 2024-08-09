namespace AirportsDistance.Server.Entities.CustomException
{
	public class InvalidRequestException : BusinessLogicException
	{
		public InvalidRequestException(string message) : base(message)
		{
		}

		public InvalidRequestException(string message, Exception inner = null) : base(message, inner)
		{

		}
	}
}
