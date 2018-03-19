using ServiceStack.Redis;

namespace A.WebApi
{
    public class RedisService
    {
        private static IRedisClient _redisClient;

        public static void Register()
        {
            var manager = new RedisManagerPool("pubsub");
            _redisClient = manager.GetClient();
        }

        public static void Publish(string @event)
        {
            _redisClient.PublishMessage("monitor-reports", @event);
        }
    }
}