namespace AirportsDistance.Server.Entities.CustomException
{
	public class UnknownBusinessLogicException : BusinessLogicException
	{
		public UnknownBusinessLogicException(string message) : base(message)
		{

		}

		public UnknownBusinessLogicException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
