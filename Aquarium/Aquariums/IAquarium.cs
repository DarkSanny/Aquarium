using System.Collections.Generic;
using System.Drawing;
using Aquarium.Fishes;

namespace Aquarium.Aquariums
{
    public interface IAquarium
    {
        Size GetSize();
        IEnumerable<GameObject> GetObjects();
        IEnumerable<Fish> GetFishes();
	    void Update();
    }
}
