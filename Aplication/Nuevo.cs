using FluentValidation;
using MediatR;
using Tienda.Microservicios.Autor.Api.Modelo;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplication
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion: AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(p => p.Nombre).NotEmpty();
                RuleFor(p => p.Apellido).NotEmpty();
                RuleFor(p => p.FechaNacimiento).NotEmpty().LessThan(DateTime.Now);
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid()), // Genera un GUID único para el autor
                };
                _contexto.AutorLibros.Add(autorLibro);
                var respuesta = await _contexto.SaveChangesAsync(cancellationToken);
                if (respuesta > 0)
                {
                    return Unit.Value; // Indica que la operación se completó correctamente
                }
                throw new Exception("No se pudo insertar el autor"); // Manejo de error si no se insertó
            }
        }
    }
}
