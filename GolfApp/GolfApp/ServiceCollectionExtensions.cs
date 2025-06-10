namespace GolfApp
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddCustomHttpClient<TInterface, TImplementation>(
            this IServiceCollection services, string baseAddress)
            where TInterface : class
            where TImplementation : class, TInterface
        {
#if DEBUG
            return services.AddHttpClient<TInterface, TImplementation>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        if (cert != null && cert.Issuer.Equals("CN=localhost"))
                            return true;

                        return errors == System.Net.Security.SslPolicyErrors.None;
                    }
                };
                return handler;
            });
#else
        return services.AddHttpClient<TInterface, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        });
#endif
        }
    }
}
