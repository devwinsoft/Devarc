namespace Devarc
{
	public partial class Table
	{
		public static TableManager<SOUND, string> SOUND = new TableManager<SOUND, string>();
	}

	[System.Serializable]
	public class SOUND_ID
	{
		public string Value = string.Empty;
		public static implicit operator string(SOUND_ID obj)
		{
			if (obj == null) return string.Empty;
			return obj.Value;
		}
		public static implicit operator SOUND_ID(string value)
		{
			SOUND_ID obj = new SOUND_ID();
			obj.Value = value;
			return obj;
		}
	}

	public static class Sound_Extension
	{
		public static bool IsValid(this SOUND_ID obj)
		{
			return obj != null && !string.IsNullOrEmpty(obj.Value);
		}
	}
}
