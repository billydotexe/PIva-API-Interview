using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PIva.Api.Models;

namespace PIva.Api.Test
{
    public class PIvaGetInfoEndPointTest : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly WebApplicationFactory<IApiMarker> _factory;

        public PIvaGetInfoEndPointTest(WebApplicationFactory<IApiMarker> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetIvaAsync_ReturnsIva_IfExists()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            string vat = "GB731331179";
            Iva iva = new()
            {
                CompanyName = "CHARITY RETAIL ASSOCIATION",
                Address = "356 HOLLOWAY ROAD",
                City = "LONDON",
                Zip = "N7 6PA",
                CountryName = "United Kingdom",
                CountryCode = "GB"
            };

            // Act
            var result = await httpClient.GetAsync($"/iva/{vat}");
            var ivaData = await result.Content.ReadFromJsonAsync<Iva>();

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            ivaData.Should().BeEquivalentTo(iva);

        }

        [Fact]
        public async Task GetIvaAsync_ReturnsBadRequest_IfInvalid()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            string vat = "GB731331345279";

            // Act
            var result = await httpClient.GetAsync($"/iva/{vat}");
            var error = await result.Content.ReadFromJsonAsync<string>();

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            error.Should().Be($"VAT Number {vat} is not valid.");

        }

        [Fact]
        public async Task GetIvaAsync_ReturnsBadRequest_IfCountryCodeNotSupported()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            string vat = "G1331179";

            // Act
            var result = await httpClient.GetAsync($"/iva/{vat}");
            var error = await result.Content.ReadFromJsonAsync<string>();

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            error.Should().Be("Country code not supported.");

        }
    }
}
