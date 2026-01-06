using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Herval.Certidoes.Domain.Enums;

namespace Herval.Certidoes.Domain.Entities
{
    public class Certidao
    {
        public string Documento { get; private set; }
        public ECertidaoId SiteId { get; private set; }
        public string Link { get; private set; }
        public string CaminhoArquivo { get; private set; }
        public string NomeArquivo { get; private set; }

        public void AtribuirNomeArquivo()
        {
            NomeArquivo = $"{Documento}_Certidao_{SiteId.ToString()}.pdf";
        }
    }
}