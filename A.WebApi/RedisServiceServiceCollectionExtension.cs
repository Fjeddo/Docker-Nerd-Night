using Microsoft.Extensions.DependencyInjection;

namespace A.WebApi
{
    public static class RedisServiceServiceCollectionExtension
    {
        public static void AddRedisPubSub(this IServiceCollection services)
        {
            RedisService.Register();
        }
    }
}