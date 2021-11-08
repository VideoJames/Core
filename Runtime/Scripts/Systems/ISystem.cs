
namespace VideoJames.Core.Systems
{
    public interface ISystem
    {
        void Init();
        void Tick(float deltaTime);
    }
}
