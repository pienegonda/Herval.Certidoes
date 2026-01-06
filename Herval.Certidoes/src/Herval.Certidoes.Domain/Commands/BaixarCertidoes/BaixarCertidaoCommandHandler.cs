using System.Threading;
using System.Threading.Tasks;
using Herval.Certidoes.Domain.Interfaces.Repositories;
using Herval.Certidoes.Domain.Interfaces.Services;
using Herval.RPA.Sdk.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Herval.Certidoes.Domain.Commands.BaixarCertidoes
{
    public class BaixarCertidaoCommandHandler : IRequestHandler<BaixarCertidaoCommand, Unit>
    {
        private readonly ICertidaoRepository _certidaoRepository;
        private readonly IMediator _mediator;
        private readonly ICertidaoService _certidaoService;
        private readonly ILogger<BaixarCertidaoCommandHandler> _logger;
        private readonly IPdfService _pdfService;

        public BaixarCertidaoCommandHandler(
            ICertidaoRepository certidaoRepository, 
            IMediator mediator, 
            ICertidaoService certidaoService,
            ILogger<BaixarCertidaoCommandHandler> logger,
            IPdfService pdfService)
        {
            _certidaoRepository = certidaoRepository;
            _mediator = mediator;
            _certidaoService = certidaoService;
            _logger = logger;
            _pdfService = pdfService;
        }

        public async Task<Unit> Handle(BaixarCertidaoCommand request, CancellationToken cancellationToken)
        {
            var certidoes = await _certidaoRepository.ListarCetidoesAsync();
            
            if(certidoes is null)
                return default;

            foreach (var certidao in certidoes)
            {
                var certidaoBiaxada = _certidaoService.BaixarFeitosTrabalhistas(certidao);
                if (!certidaoBiaxada)
                {
                    _logger.LogWarning($"Falha ao baixar a certidao referente ao documento: {certidao.Documento}");
                    continue;
                }
                
                certidao.AtribuirNomeArquivo();

                var constaPrecedentes = _pdfService.PdfContemTexto(certidao.NomeArquivo, "NÃO CONSTAM");
                
                await _certidaoRepository.InserirCertidaoAsync(certidao);
            }
            

            return default;
        }
    }
}