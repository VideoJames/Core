
namespace VideoJames.Core.Pooling
{
    public static class PoolManager
    {
        public static void Reserve<T>(T key, int reservedCount) where T : IPooledObject<T> => Pool<T>.Reserve(key, reservedCount);

        public static T GetNextObject<T>(T key) where T : IPooledObject<T> => Pool<T>.GetNextObject(key);

        public static void ReturnObject<T>(T returnedObject) where T : IPooledObject<T> => Pool<T>.ReturnObject(returnedObject);
    }
}

