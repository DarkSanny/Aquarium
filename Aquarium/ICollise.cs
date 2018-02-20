using Aquarium.Fishes;

namespace Aquarium
{
    public interface ICollise
    {
        void Collision(IObject obj);
        ObjectType GetCollisionType();
        bool IsShouldCollise(IObject obj);
    }
}
