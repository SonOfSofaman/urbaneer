using System;
using System.Drawing;

namespace com.SonOfSofaman.Urbaneer
{
	public class Zone
	{
		public Rectangle Area { get; private set; }
		public ZoneType ZoneType { get; private set; }
		public ZoneDensity ZoneDensity { get; private set; }

		public Zone(Rectangle area, ZoneType zoneType, ZoneDensity zoneDensity)
		{
			this.Area = area;
			this.ZoneType = zoneType;
			this.ZoneDensity = zoneDensity;
		}
	}
}
