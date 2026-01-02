using Herval.Web.Conversors;
using Microsoft.Extensions.DependencyInjection;

namespace Herval.DownloadArquivos.Infra.CrossCutting.Ioc.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static void AddValueObjectJsonConverterOptions(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new CepJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new CnpjJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new CpfJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DataNascimentoJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DocumentoJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new EmailJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new LatitudeJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new LongitudeJsonConverter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new TelefoneJsonConverter()));
        }
    }
}
