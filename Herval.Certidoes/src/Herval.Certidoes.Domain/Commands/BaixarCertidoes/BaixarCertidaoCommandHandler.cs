using System.Threading;
using System.Threading.Tasks;
using Herval.Certidoes.Domain.Interfaces.Repositories;
using Herval.Certidoes.Domain.Interfaces.Services;
using MediatR;

namespace Herval.Certidoes.Domain.Commands.BaixarCertidoes
{
    public class BaixarCertidaoCommandHandler : IRequestHandler<BaixarCertidaoCommand, Unit>
    {
        private readonly ICertidaoRepository _certidaoRepository;
        private readonly IMediator _mediator;
        private readonly ICertidaoService _certidaoService;

        public BaixarCertidaoCommandHandler(
            ICertidaoRepository certidaoRepository, 
            IMediator mediator, 
            ICertidaoService certidaoService)
        {
            _certidaoRepository = certidaoRepository;
            _mediator = mediator;
            _certidaoService = certidaoService;
        }

        public async Task<Unit> Handle(BaixarCertidaoCommand request, CancellationToken cancellationToken)
        {
            var certidoes = await _certidaoRepository.ListarCetidoesAsync();
            
            if(certidoes is null)
                return default;

            foreach (var certidao in certidoes)
            {
                var certidaoBiaxada = await _certidaoService.BaixarFeitosTrabalhistas(certidao);
                
            }
            

            return default;
        }
    }
}