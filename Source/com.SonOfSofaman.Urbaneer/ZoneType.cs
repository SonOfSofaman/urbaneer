using System.ComponentModel;

namespace com.SonOfSofaman.Urbaneer
{
	public enum ZoneType
	{
		[CommandShortcut("r")]
		Residential,

		[CommandShortcut("c")]
		Commercial,

		[CommandShortcut("i")]
		Industrial,
	}
}
