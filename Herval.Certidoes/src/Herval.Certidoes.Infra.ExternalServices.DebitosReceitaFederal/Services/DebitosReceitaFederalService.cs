using System;
using System.Threading.Tasks;
using Herval.Certidoes.Domain.Entities;
using Herval.RPA.Sdk.Enums;
using Herval.RPA.Sdk.Interfaces;

namespace Herval.Certidoes.Infra.ExternalServices.DebitosReceitaFederal.Services;

public class DebitosReceitaFederalService
{
    private readonly IWebService _webService;

    public DebitosReceitaFederalService(IWebService webService)
    {
        _webService = webService;
    }

    public Task<bool> BaixarDebitosReceitaFederal(Certidao certidao)
    {
        // Implement the logic to download Debitos Receita Federal here
        throw new NotImplementedException();


    }
}
