using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Herval.DownloadArquivos.Domain.Interfaces;

public interface IArquivoDownloaderService
{
    Task BaixarArquivosAsync(List<string> linksArquivosZip, string pastaDestino);
    List<string> ObterLinksDeArquivosZip(string url, HtmlNodeCollection links);
    Task<string> CarregarPaginaAsync(string url);
}
