using System.Drawing;

namespace Aquarium.UI
{
	public interface IObjectDrawer
	{
		void DrawObject(Graphics graphics, GameObject gameObject);
	}
}
