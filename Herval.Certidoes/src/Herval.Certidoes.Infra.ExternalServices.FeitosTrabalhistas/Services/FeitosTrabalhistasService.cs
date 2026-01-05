using System;
using Herval.Certidoes.Domain.Entities;
using Herval.Certidoes.Domain.Interfaces.Services;
using Herval.RPA.Sdk.Enums;
using Herval.RPA.Sdk.Interfaces;
using Microsoft.Extensions.Logging;

namespace Herval.Certidoes.Infra.ExternalServices.FeitosTrabalhistas.Services
{
    public class FeitosTrabalhistasService : IFeitosTrabalhistasService
    {
        private readonly IWebService _webService;
        private readonly ICaptchaService _captchaService;
        private readonly ILogger<FeitosTrabalhistasService> _logger;

        public FeitosTrabalhistasService(
            IWebService webService, 
            ICaptchaService captchaService, 
            ILogger<FeitosTrabalhistasService> logger)
        {
            _webService = webService;
            _captchaService = captchaService;
            _logger = logger;
        }

        public bool BaixarFeitosTrabalhistas(Certidao certidao)
        {
            try
            {
                _webService.IniciarBrowser(EBrowser.Chrome, certidao.Link);
                _webService.AguardarPaginaCarregar();

                var elementoExiste =_webService.VerificarElementoExiste(ESeletor.XPath, "/html/body/b/div[2]/div/div[2]/input");
                if (elementoExiste)
                    _webService.ClicarQuandoAparecer(ESeletor.XPath, "/html/body/b/div[2]/div/div[2]/input");

                _webService.PreencherCampoQuandoAparecer(ESeletor.XPath, "/html/body/div[1]/section/div[7]/div/form/input[3]", certidao.Documento);
                _webService.ClicarQuandoAparecer(ESeletor.XPath, "/html/body/div[1]/section/div[7]/div/form/fieldset/b/input[1]");

                var captchaResolvido = _captchaService.ResolverCaptchaPorTipo(ETipoCaptcha.Recaptcha, certidao.Link).Result;
                if (!captchaResolvido)
                {
                    _logger.LogWarning($"Falha ao resolver o captcha para o link: {certidao.Link}");
                    return false;
                }

                _webService.ClicarQuandoAparecer(ESeletor.XPath, "/html/body/div[1]/section/div[7]/div/form/b/input");
                _webService.AguardarPaginaCarregar();

                var botaoApareceu = _webService.VerificarElementoExiste(ESeletor.XPath, "/html/body/div[1]/section/div[7]/div/input[1]");
                if (!botaoApareceu)
                {
                    _logger.LogWarning($"Falha ao carregar a certidão");
                    return false;
                }

                _webService.ClicarQuandoAparecer(ESeletor.XPath, "/html/body/div[1]/section/div[7]/div/input[1]");
                _webService.AguardandoDownloadConcluir(certidao.CaminhoArquivo, 20);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao baixar certidão de feitos trabalhistas para o documento: {certidao.Documento}");
                return false;
            }
        }
    }
}