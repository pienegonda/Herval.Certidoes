using MediatR;
using System.Collections.Generic;

namespace Herval.DownloadArquivos.Domain.Commands.Template.Listar
{
    public class BaixarArquivosCommand : IRequest<Unit>
    {
        public string Url { get; set; }
        public string PastaDestino { get; set; }

        public BaixarArquivosCommand(string url, string pastaDestino)
        {
            Url = url;
            PastaDestino = pastaDestino;
        }
    }
}
