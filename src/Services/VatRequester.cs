using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using PIva.Api.Models;
using DotNext;

namespace PIva.Api.Services
{
    public class VatRequester : IVatRequester
    {
        private readonly IbanService _ibanService;

        public VatRequester(IbanService ibanService) 
        {
            _ibanService = ibanService;
        }

        public async Task<Result<Iva>> GetIva(string vat)
        {
            string page = await _ibanService.LoadPage(vat);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(page);

            var alert = htmlDoc.DocumentNode.QuerySelector(".alert strong");
            var table = htmlDoc.DocumentNode.QuerySelector("table");

            if (alert is not null)
                return new Result<Iva>(new Exception(alert.InnerHtml));
            if (table.QuerySelector(".fa-exclamation-triangle") is not null)
                return new Result<Iva>(new Exception($"VAT Number {vat} is not valid."));

            return new Result<Iva>(MapIva(table.QuerySelectorAll("tr").Skip(1)));
        }

        public Iva MapIva(IEnumerable<HtmlNode> trs)
        {
            Iva iva = new();

            foreach (var tr in trs)
            {
                var tds = tr.QuerySelectorAll("td");
                foreach (var prop in iva.GetType().GetProperties())
                {
                    if (tds.First().QuerySelector("strong").InnerHtml.Replace(" ", "") == prop.Name)
                    {
                        if (prop.PropertyType == typeof(int))
                            prop.SetValue(iva, int.Parse(tds.Last().InnerHtml));
                        else
                            prop.SetValue(iva, tds.Last().InnerHtml);
                    }

                }
            }

            return iva;
        }

    }
}
