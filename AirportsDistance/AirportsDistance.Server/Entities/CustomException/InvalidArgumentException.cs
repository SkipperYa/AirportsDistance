namespace AirportsDistance.Server.Entities.CustomException
{
	public class InvalidArgumentException : BusinessLogicException
	{
		public InvalidArgumentException(string paramName) : base($"Invalid {paramName}")
		{

		}

		public InvalidArgumentException(string paramName, Exception inner) : base($"Invalid {paramName}", inner)
		{

		}
	}
}
