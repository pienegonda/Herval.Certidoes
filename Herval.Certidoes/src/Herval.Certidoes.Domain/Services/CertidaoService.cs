using System;
using System.Threading.Tasks;
using Herval.Certidoes.Domain.Entities;
using Herval.Certidoes.Domain.Enums;
using Herval.Certidoes.Domain.Interfaces.Services;

namespace Herval.Certidoes.Domain.Services
{
    public class CertidaoService : ICertidaoService
    {
        private readonly IFeitosTrabalhistasService _feitosTrabalhistasService;

        public CertidaoService(IFeitosTrabalhistasService feitosTrabalhistasService)
        {
            _feitosTrabalhistasService = feitosTrabalhistasService;
        }

        public bool BaixarFeitosTrabalhistas(Certidao certidao)
        {
            switch (certidao.SiteId)
            {
                case ECertidaoId.FeitosTrabalhistas:
                    return _feitosTrabalhistasService.BaixarFeitosTrabalhistas(certidao);       

                default:
                    throw new NotImplementedException($"O serviço para o site {certidao.SiteId} não está implementado.");
            }
        }

    }
}