using System.Collections.Generic;
using MessagePack;

namespace Devarc
{
	public enum ErrorType
	{
		SUCCESS              = 0,
		UNKNOWN              = 1,
		SERVER_ERROR         = 2,
		SESSION_EXPIRED      = 3,
		SESSION_REMAIN       = 4,
		INVALID_PASSWORD     = 5,
		INVALID_SECRET       = 6,
	}

	public enum GenderType
	{
		None                 = 0,
		Male                 = 1,
		Female               = 2,
	}

	[MessagePackObject]
	public class Account
	{
		[Key(0)]
		public string               nickName;
		[Key(1)]
		public int                  level;
	}

};
