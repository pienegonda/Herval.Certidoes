using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Herval.DownloadArquivos.Domain.Services;

public class ArquivoDownloaderService
{
    private readonly HttpClient _httpClient;

    public ArquivoDownloaderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CarregarPaginaAsync(string url)
    {
        _httpClient.Timeout = TimeSpan.FromHours(5);

        var html = await _httpClient.GetStringAsync(url);

        return html;
    }

    public static List<string> ObterLinksDeArquivosZip(string url, HtmlNodeCollection links)
    {
        var urlsArquivosDownload = new List<string>();
        foreach (var link in links)
        {
            var textoLink = link.InnerText;
            if (textoLink.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                var linkDownload = $"{url}{textoLink}";
                urlsArquivosDownload.Add(linkDownload);
            }
        }
        return urlsArquivosDownload;
    }

    public async Task BaixarArquivosAsync(List<string> linksArquivosZip, string pastaDestino)
    {
        Directory.CreateDirectory(pastaDestino);

        foreach (var linkArquivo in linksArquivosZip)
        {
            var nomeArquivo = Path.GetFileName(linkArquivo);
            var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

            Console.WriteLine($"Baixando {nomeArquivo}...");

            using var resposta = await _httpClient.GetAsync(linkArquivo);
            resposta.EnsureSuccessStatusCode();
            await using var fluxoArquivo = await resposta.Content.ReadAsStreamAsync();
            await using var fluxoDestino = File.Create(caminhoCompleto);
            await fluxoArquivo.CopyToAsync(fluxoDestino);

            Console.WriteLine($"{nomeArquivo} baixado com sucesso.");
        }
    }
}
