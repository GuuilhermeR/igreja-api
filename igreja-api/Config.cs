using nrmcontrolextension.IRepositories;
using nrmcontrolextension.IServices;
using nrmcontrolextension.Repositories;
using nrmcontrolextension.Services;

namespace igreja_api
{
    public class Config
    {
        public static void RegisterServices(WebApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddScoped<IDespenseTypeRepository, DespenseTypeRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDespenseTypeService, DespenseTypeService>();
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
