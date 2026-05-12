CREATE EXTENSION IF NOT EXISTS postgis;

CREATE INDEX IF NOT EXISTS ix_sos_requests_location_gist
ON sos_requests
USING GIST (location);

CREATE INDEX IF NOT EXISTS ix_flood_zones_boundary_gist
ON flood_zones
USING GIST (boundary);

CREATE INDEX IF NOT EXISTS ix_shelters_location_gist
ON shelters
USING GIST (location);

CREATE INDEX IF NOT EXISTS ix_rescue_teams_current_location_gist
ON rescue_teams
USING GIST (current_location);

CREATE INDEX IF NOT EXISTS ix_user_locations_location_gist
ON user_locations
USING GIST (location);

CREATE INDEX IF NOT EXISTS ix_sos_requests_status_priority_created_at
ON sos_requests (status, priority_score DESC, created_at DESC);

CREATE INDEX IF NOT EXISTS ix_sos_requests_created_at
ON sos_requests (created_at DESC);

CREATE INDEX IF NOT EXISTS ix_sos_requests_status_created_at
ON sos_requests (status, created_at DESC);

CREATE INDEX IF NOT EXISTS ix_flood_zones_status_severity
ON flood_zones (status, severity);

CREATE INDEX IF NOT EXISTS ix_rescue_teams_status_last_location
ON rescue_teams (status, last_location_updated_at DESC);

CREATE INDEX IF NOT EXISTS ix_user_locations_user_captured
ON user_locations (user_id, captured_at DESC);

CREATE INDEX IF NOT EXISTS ix_rescue_team_location_histories_team_captured
ON rescue_team_location_histories (rescue_team_id, captured_at DESC);

