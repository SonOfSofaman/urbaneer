using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;

namespace com.SonOfSofaman.Urbaneer
{
	public class Simulator
	{
		public bool IsAcceptingCommands { get { return this.Working; } }
		public State State { get; private set; }
		public bool IsPaused { get; private set; }

		private double StopwatchFrequency;
		private Stopwatch Stopwatch;
		private long TicksLastSample;
		private volatile bool Working;
		private Thread Worker;

		public Simulator()
		{
			this.State = null;
			this.IsPaused = true;
			this.StopwatchFrequency = (double)Stopwatch.Frequency;
			this.Stopwatch = new Stopwatch();
			this.Stopwatch.Start();
			this.TicksLastSample = this.Stopwatch.ElapsedTicks;
			this.Working = false;
			this.Worker = new Thread(new ThreadStart(this.Work));
			this.Worker.Start();
		}

		public CommandResult Exit(Match match)
		{
			CommandResult result = new CommandResult();

			this.Stopwatch.Stop();
			this.Working = false;
			this.Worker.Join(1000);
			this.Worker = null;
			result.Success = true;

			return result;
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

		private void Work()
		{
			this.Working = true;
			do
			{
				long ticksThisSample = this.Stopwatch.ElapsedTicks;
				long ticksDelta = ticksThisSample - this.TicksLastSample;
				double deltaSeconds = (double)ticksDelta / this.StopwatchFrequency;
				this.TicksLastSample = ticksThisSample;

				if (!this.IsPaused && this.State != null)
				{
					this.State.Update(deltaSeconds);
				}

				Thread.Sleep(10);
			} while (this.Working);
		}
	}
}
