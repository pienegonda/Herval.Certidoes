using Herval.DownloadArquivos.Domain.Interfaces;
using Herval.Extensions.ICollections;
using HtmlAgilityPack;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Herval.DownloadArquivos.Domain.Commands.Template.Listar
{
    public class BaixarArquivosCommandHandler : IRequestHandler<BaixarArquivosCommand, Unit>
    {
        private readonly IArquivoDownloaderService _arquivoDownloaderService;

        public async Task<Unit> Handle(BaixarArquivosCommand request, CancellationToken cancellationToken)
        {
            var html = await _arquivoDownloaderService.CarregarPaginaAsync(request.Url);
            if (string.IsNullOrEmpty(html))
                throw new Exception("Não foi possível carregar a página.");

            var corpoHtml = new HtmlDocument();
            corpoHtml.LoadHtml(html);

            var linksArquivos = corpoHtml.DocumentNode.SelectNodes("//table//tr/td[2]/a");
            if (linksArquivos.IsNullOrEmpty())
                return default;

            var linksArquivosZip = _arquivoDownloaderService.ObterLinksDeArquivosZip(request.Url, linksArquivos);

            await _arquivoDownloaderService.BaixarArquivosAsync(linksArquivosZip, request.PastaDestino);

            return default;
        }
    }
}
