using System.Drawing;
using System.Windows.Forms;
using Aquarium.Aquariums;
using Aquarium.UI;
using Timer = System.Windows.Forms.Timer;

namespace Aquarium
{
	public sealed class GameForm : Form
	{
		private readonly IAquarium _aquarium;
		private Size _defaultSize;
		public GameForm(IAquarium aquarium)
		{
			DoubleBuffered = true;
			Size = aquarium.GetSize();
			_defaultSize = Size;
			_aquarium = aquarium;
			Init();
		}

		private void Init()
		{
			Render();
			var updates = new Timer() {Interval = 1000 / 60};
			updates.Tick += (sender, args) =>
			{
				_aquarium.Update();
				Invalidate();
			};
			updates.Start();
		}

		private void Render()
		{
			var drawer = new ObjectDrawer();
			var aquariumImage = new ImageSource("aquarium_", 2);
			Paint += (sender, args) =>
			{
				args.Graphics.DrawImage(aquariumImage.GetImage(), 0, 0, _defaultSize.Width, _defaultSize.Height);
				foreach (var gameObject in _aquarium.GetObjects())
				{
					drawer.DrawObject(args.Graphics, gameObject);
				}
			};
		}
	}
}