using System.Windows.Forms;
using Aquarium.Aquariums;
using Aquarium.UI;
using Timer = System.Windows.Forms.Timer;

namespace Aquarium
{
	public sealed class GameForm : Form
	{
		private readonly IAquarium _aquarium;

		public GameForm(IAquarium aquarium)
		{
			DoubleBuffered = true;
			Size = aquarium.GetSize();
			_aquarium = aquarium;
			Init();
		}

		private void Init()
		{
			Render();
			var updates = new Timer() {Interval = 1000 / 40};
			updates.Tick += (sender, args) =>
			{
				_aquarium.Update();
				Invalidate();
			};
			//var render = new Timer() {Interval = 1000 / 30};
			//render.Tick += (sender, args) => { Invalidate(); };
			updates.Start();
			//render.Start();
		}

		private void Render()
		{
			var drawer = new ObjectDrawer();
			Paint += (sender, args) =>
			{
				foreach (var gameObject in _aquarium.GetObjects())
				{
					drawer.DrawObject(args.Graphics, gameObject);
				}
			};
		}
	}
}