using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicios.Autor.Api.Modelo;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplication
{
    public class ConsultarFiltro
    {
        public class AutorUnico: IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
            public string Nombre { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                this._contexto = contexto;
                this._mapper = mapper;
            }
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibros
                    .Where(p =>
                        (string.IsNullOrEmpty(request.AutorGuid) || p.AutorLibroGuid == request.AutorGuid) &&
                        (string.IsNullOrEmpty(request.Nombre) || p.Nombre == request.Nombre)
                    ).FirstOrDefaultAsync(cancellationToken);

                if (autor == null)
                {
                    throw new Exception("No se encontró el autor");
                }
                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
