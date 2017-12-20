using System;
using System.Drawing;
using System.Windows.Forms;
using Aquarium.Aquariums;
using Aquarium.Fishes;

namespace Aquarium
{
	public static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var aquarium = new SimpleAquarium(new Size(1000, 800));
			var provider = new ObjectRandomizer(aquarium)
				.AddObject(ObjectType.BlueNeon, 10)
				.WithObjects(new[]
				{
					new Flock(aquarium, new Point(500, 500), Math.PI / 18, new Size()),
					new Flock(aquarium, new Point(500, 500), Math.PI / 2 + Math.PI / 18, new Size()),
					new Flock(aquarium, new Point(500, 500), 2 * Math.PI / 2 + Math.PI / 18, new Size()),
					new Flock(aquarium, new Point(500, 500), 3 * Math.PI / 2 + Math.PI / 18, new Size()),
				})
				.AddObject(ObjectType.Piranha, 1)
				.AddObject(ObjectType.Catfish, 2)
				.AddObject(ObjectType.Swordfish, 3);
			aquarium.Start(provider);
			Application.Run(new GameForm(aquarium));
		}
	}
}