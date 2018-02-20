using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aquarium.Aquariums;
using Aquarium.UI;

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
			var rendering = new Task(() =>
			{
				while (true)
				{
					Invalidate();
				}
			});
			var updating = new Task(() =>
			{
				while (true)
				{
					_aquarium.Update();
					Thread.Sleep(1000 / 30);
				}
			});
			rendering.Start();
			updating.Start();
		}

		private void Render()
		{
			var drawer = new ObjectDrawer();
			var aquariumImage = new ImageSource("aquarium_", 1);
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