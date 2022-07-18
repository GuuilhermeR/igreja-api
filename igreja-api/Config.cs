using igrejaextensions.IRepositories;
using igrejaextensions.IServices;
using igrejaextensions.Repositories;
using igrejaextensions.Services;

namespace igreja_api
{
     public static class Config
    {
        public static void RegisterServices(WebApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
