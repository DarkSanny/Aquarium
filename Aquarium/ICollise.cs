using Aquarium.Fishes;

namespace Aquarium
{
    public interface ICollise
    {
        void Collision(ObjectType objectType, GameObject obj);
        ObjectType GetCollisionType();
        bool IsShouldCollise(ObjectType objectType);
    }
}
