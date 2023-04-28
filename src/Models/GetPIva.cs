using DotNext;
using MediatR;
using PIva.Api.Services;

namespace PIva.Api.Models
{
    public record GetPIvaQuery(string vat) : IRequest<IResult>;

    public class GetPIvaHandler : IRequestHandler<GetPIvaQuery, IResult>
    {

        private readonly IVatRequester _requester;

        public GetPIvaHandler(IVatRequester vatRequester)
        {
            _requester = vatRequester;
        }

        async Task<IResult> IRequestHandler<GetPIvaQuery, IResult>.Handle(GetPIvaQuery request, CancellationToken cancellationToken)
        {
            var iva = await _requester.GetIva(request.vat);
            if (iva.IsSuccessful)
                return Results.Ok(iva.Value);
            else
                return Results.BadRequest(iva.Error.Message);
        }
    }
}
