using Microsoft.EntityFrameworkCore;
using Tienda.Microservicios.Autor.Api.Persistencia;
using MediatR;
using FluentValidation.AspNetCore;
using Tienda.Microservicios.Autor.Api.Aplication;

namespace Tienda.Microservicios.Autor.Api.extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(cfg =>
                cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<ContextoAutor>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Agregamos mediatR como servicio
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));

            return services; // Ensure the method returns the IServiceCollection instance
        }
    }
}
