namespace GegaGamez.WebUI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedis(this IServiceCollection services)
        {
            services.AddRedisOptions();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisOptions = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
                var configurationOptions = redisOptions.ToConfigurationOptions();
                return ConnectionMultiplexer.Connect(configurationOptions);
            });
        }

        public static void AddRedisOptions(this IServiceCollection services)
        {
            services.AddOptions<RedisOptions>()
                .BindConfiguration(RedisOptions.Redis)
                .ValidateDataAnnotations();
        }
    }
}
