namespace Infrastructure.PostGIS;

public static class PostGisSql
{
    public const string PointInFloodZone = """
        SELECT
            id AS "FloodZoneId",
            name AS "FloodZoneName",
            severity AS "Severity",
            concat('Bạn đang ở trong vùng ngập: ', name) AS "Message"
        FROM flood_zones
        WHERE deleted_at IS NULL
          AND status = 2
          AND boundary && ST_SetSRID(ST_MakePoint(@p0, @p1), 4326)
          AND ST_Contains(boundary, ST_SetSRID(ST_MakePoint(@p0, @p1), 4326))
        ORDER BY severity DESC
        """;

    public const string NearestShelter = """
        SELECT
            id AS "Id",
            name AS "Name",
            address AS "Address",
            ST_X(location) AS "Longitude",
            ST_Y(location) AS "Latitude",
            greatest(capacity - current_occupancy, 0) AS "AvailableSlots",
            ST_Distance(location::geography, ST_SetSRID(ST_MakePoint(@p0, @p1), 4326)::geography) AS "DistanceMeters"
        FROM shelters
        WHERE deleted_at IS NULL
          AND status = 1
          AND current_occupancy < capacity
          AND ST_DWithin(location::geography, ST_SetSRID(ST_MakePoint(@p0, @p1), 4326)::geography, @p2)
        ORDER BY location <-> ST_SetSRID(ST_MakePoint(@p0, @p1), 4326)
        LIMIT 1
        """;

    public const string NearestRescueTeam = """
        SELECT
            id AS "Id",
            name AS "Name",
            status AS "Status",
            ST_X(current_location) AS "Longitude",
            ST_Y(current_location) AS "Latitude",
            ST_Distance(current_location::geography, ST_SetSRID(ST_MakePoint(@p0, @p1), 4326)::geography) AS "DistanceMeters"
        FROM rescue_teams
        WHERE deleted_at IS NULL
          AND status = 1
          AND current_location IS NOT NULL
          AND ST_DWithin(current_location::geography, ST_SetSRID(ST_MakePoint(@p0, @p1), 4326)::geography, @p2)
        ORDER BY current_location <-> ST_SetSRID(ST_MakePoint(@p0, @p1), 4326)
        LIMIT 1
        """;

    public const string UsersInsideFloodZone = """
        WITH latest_location AS (
            SELECT DISTINCT ON (user_id)
                user_id,
                location,
                captured_at
            FROM user_locations
            ORDER BY user_id, captured_at DESC
        )
        SELECT l.user_id
        FROM latest_location l
        JOIN flood_zones f ON f.id = @p0
        WHERE f.deleted_at IS NULL
          AND f.status = 2
          AND f.boundary && l.location
          AND ST_Contains(f.boundary, l.location)
        """;

    public const string SosCountByFloodZone = """
        SELECT
            f.id AS "FloodZoneId",
            f.name AS "FloodZoneName",
            COUNT(s.id) AS "SosCount"
        FROM flood_zones f
        LEFT JOIN sos_requests s
          ON s.deleted_at IS NULL
         AND f.boundary && s.location
         AND ST_Contains(f.boundary, s.location)
         AND (@p0 IS NULL OR s.created_at >= @p0)
         AND (@p1 IS NULL OR s.created_at <= @p1)
        WHERE f.deleted_at IS NULL
        GROUP BY f.id, f.name
        ORDER BY COUNT(s.id) DESC
        """;
}