using Herval.Certidoes.Domain.Entities;
using Herval.Certidoes.Domain.Interfaces.Services;
using Herval.RPA.Sdk.Enums;
using Herval.RPA.Sdk.Interfaces;
using System.Threading.Tasks;

namespace Herval.Certidoes.Infra.ExternalServices.FeitosTrabalhistas.Services
{
    public class FeitosTrabalhistasService : IFeitosTrabalhistasService
    {
        private readonly IWebService _webService;

        public FeitosTrabalhistasService(IWebService webService)
        {
            _webService = webService;
        }

        public Task<bool> BaixarFeitosTrabalhistas(Certidao certidao)
        {
            _webService.IniciarBrowser(EBrowser.Chrome, certidao.Link);
            _webService.AguardarPaginaCarregar();

            var elementoExiste =_webService.VerificarElementoExiste(
                ESeletor.XPath, 
                "https://www.tst.jus.br/certidao1");
            
            if (elementoExiste)
                _webService.ClicarQuandoAparecer(
                    ESeletor.XPath, 
                    "//*[@id='someElementId']");
            
            _webService.ClicarQuandoAparecer(
                ESeletor.XPath, 
                "/html/body/form/div/div/div[2]/input[1]");

            _webService.PreencherCampoQuandoAparecer(
                ESeletor.XPath,
                "/html/body/form/div/div/div[2]/input[1]",
                certidao.Documento);
            
            return Task.FromResult(true);

            
        }
    }
}