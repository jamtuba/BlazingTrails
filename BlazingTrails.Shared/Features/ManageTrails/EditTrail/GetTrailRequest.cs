﻿namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail;

public record GetTrailRequest(int TrailId) : IRequest<GetTrailRequest.Response?>
{
    public const string RouteTemplate = "/api/trails/{trailId}";

    public record Trail(int Id, string Name, string Location, string? Image, int TimeInMinutes, int Length, string Description, string Owner, IEnumerable<Waypoint> Waypoints);
    public record Response(Trail Trail);
    public record Waypoint(decimal Latitude, decimal Longitude);
}