using MediatR;
using PIva.Api.Endpoints.Internal;
using PIva.Api.Models;
using PIva.Api.Services;

namespace PIva.Api.Endpoints
{
    public class IvaEndpoints : IEndpoints
    {
        
        public static void AddServices(IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IbanService>();
            services.AddSingleton<IVatRequester, VatRequester>();
        }

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("iva/{vat}", GetIvaAsync)
                .Produces<Iva>(StatusCodes.Status200OK)
                .Produces<string>(StatusCodes.Status400BadRequest);
        }

        internal static async Task<IResult> GetIvaAsync(string vat, IMediator mediator)
        {
            var query = new GetPIvaQuery(vat);
            var result = await mediator.Send(query);

            return result;
        }
    }
}
