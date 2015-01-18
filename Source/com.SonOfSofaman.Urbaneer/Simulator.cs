using System.Diagnostics;
using System.Threading;

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
			this.State = new State();
			this.IsPaused = true;
			this.StopwatchFrequency = (double)Stopwatch.Frequency;
			this.Stopwatch = new Stopwatch();
			this.Stopwatch.Start();
			this.TicksLastSample = this.Stopwatch.ElapsedTicks;
			this.Working = false;
			this.Worker = new Thread(new ThreadStart(this.Work));
			this.Worker.Start();
		}

		public void Exit()
		{
			this.Stopwatch.Stop();
			this.Working = false;
			this.Worker.Join(1000);
			this.Worker = null;
		}

		public void Pause()
		{
			this.IsPaused = true;
		}

		public void Resume()
		{
			this.IsPaused = false;
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
