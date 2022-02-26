using BlazingTrails.Shared.Features.Home.Shared;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.Home.Shared;

public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response?>
{
    private readonly HttpClient _httpClient;

    public GetTrailsHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetTrailsRequest.Response?> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<GetTrailsRequest.Response>(GetTrailsRequest.RouteTemplate);
            return result;
        }
        catch (HttpRequestException)
        {
            return default!;
        }
    }
}
