using System.Collections.Generic;
using System.Drawing;

namespace Aquarium
{
    public interface IAquarium
    {
        Size GetSize();
        IEnumerable<IObject> GetObjects();
        IEnumerable<Fish> GetFishes();
    }
}
