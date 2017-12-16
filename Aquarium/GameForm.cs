using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aquarium.Aquariums;

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
			var updates = new Timer() { Interval = 1000 / 30 };
			updates.Tick += (sender, args) =>
			{
				_aquarium.Update();
			};
			var render = new Timer() { Interval = 1 };
			render.Tick += (sender, args) => { Invalidate(); };
			updates.Start();
			render.Start();
		}

		private void Render()
		{
			Paint += (sender, args) =>
			{
				foreach (var gameObject in _aquarium.GetObjects())
				{
					args.Graphics.FillRectangle(Brushes.BlueViolet, gameObject.Rectangle());
				}
			};
		}
	}
}
