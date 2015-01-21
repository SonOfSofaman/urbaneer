using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.SonOfSofaman.Urbaneer
{
	public class Simulator
	{
		public State State { get; private set; }
		public bool IsPaused { get; private set; }

		public Simulator()
		{
			this.State = null;
			this.IsPaused = true;
		}

		public void Update(double deltaTime)
		{
			if (!this.IsPaused && this.State != null)
			{
				this.State.Update(deltaTime);
			}
		}

		public CommandResult Pause(Match match)
		{
			CommandResult result = new CommandResult();

			this.IsPaused = true;
			result.Success = true;

			return result;
		}

		public CommandResult Resume(Match match)
		{
			CommandResult result = new CommandResult();
			if (this.State == null)
			{
				result.Success = false;
				result.Message = "No simulation is available. Unable to resume.";
			}
			else
			{
				result.Success = true;
				this.IsPaused = false;
			}
			return result;
		}

		public CommandResult New(Match match)
		{
			CommandResult result = new CommandResult();
			if (this.State == null)
			{
				this.State = new State();
				result.Success = true;
			}
			else
			{
				result.Success = false;
				result.Message = "A simulation is already in progress. UNLOAD current simulation before starting another.";
			}
			return result;
		}

		public CommandResult Zone(Match match)
		{
			CommandResult result = new CommandResult();

			if (this.State == null)
			{
				result.Success = false;
				result.Message = "No simulation is available. Unable to zone.";
			}
			else
			{
				int x = Convert.ToInt32(match.Groups["x"].Value);
				int y = Convert.ToInt32(match.Groups["y"].Value);
				int w = Convert.ToInt32(match.Groups["w"].Value);
				int h = Convert.ToInt32(match.Groups["h"].Value);
				Rectangle area = new Rectangle(x, y, w, h);
				ZoneType zoneType = match.Groups["zonetype"].Value.GetEnumFromCommandShortcut<ZoneType>();
				ZoneDensity zoneDensity = match.Groups["zonedensity"].Value.GetEnumFromCommandShortcut<ZoneDensity>();

				if (this.State.Zones.Any(z => z.Area.IntersectsWith(area)))
				{
					result.Success = false;
					result.Message = "Zone overlaps another zone.";
				}
				else
				{
					this.State.Zones.Add(new Zone(area, zoneType, zoneDensity));
					result.Success = true;
				}
			}

			return result;
		}
	}
}
