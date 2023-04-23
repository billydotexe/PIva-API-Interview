﻿namespace PIva.Api.Endpoints.Internal
{
    public interface IEndpoints
    {
        public static abstract void DefineEndpoints(IEndpointRouteBuilder app);
        public static abstract void AddServices(IServiceCollection services, IConfiguration config);
    }
}
