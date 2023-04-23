using HtmlAgilityPack;
using DotNext;
using PIva.Api.Models;

namespace PIva.Api.Services
{
    public interface IVatRequester
    {
        public Task<Result<Iva>> GetIva(string vat);
        public Iva MapIva(IEnumerable<HtmlNode> trs);
    }
}
