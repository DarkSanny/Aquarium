namespace Aquarium
{
    public interface ICollise
    {
        void Collision(ObjectType objectType);
        ObjectType GetCollisionType();
        bool IsShouldCollise(ObjectType objectType);
    }
}
