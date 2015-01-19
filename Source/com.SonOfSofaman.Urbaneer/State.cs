using System.Collections.Generic;
using System.Linq;

namespace com.SonOfSofaman.Urbaneer
{
	public class State
	{
		public double ElapsedSeconds { get; private set; }
		public List<Zone> Zones { get; private set; }

		public State()
		{
			this.ElapsedSeconds = 0.0;
			this.Zones = new List<Zone>();
		}

		public void Update(double deltaSeconds)
		{
			this.ElapsedSeconds += deltaSeconds;
		}

		public Zone GetZoneAt(int x, int y)
		{
			return this.Zones.FirstOrDefault(z => z.Area.Contains(x, y));
		}
	}
}
