using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "app_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flood_zones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    severity = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    boundary = table.Column<Polygon>(type: "geometry(Polygon,4326)", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    effective_from = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    effective_to = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flood_zones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rescue_teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    leader_phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    current_location = table.Column<Point>(type: "geometry(Point,4326)", nullable: true),
                    last_location_updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rescue_teams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shelters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    location = table.Column<Point>(type: "geometry(Point,4326)", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    current_occupancy = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    contact_phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    has_medical_support = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shelters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sos_requests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    citizen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location = table.Column<Point>(type: "geometry(Point,4326)", nullable: false),
                    address_text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    people_count = table.Column<int>(type: "integer", nullable: false),
                    has_injured_people = table.Column<bool>(type: "boolean", nullable: false),
                    has_children = table.Column<bool>(type: "boolean", nullable: false),
                    has_elderly = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    priority_score = table.Column<int>(type: "integer", nullable: false),
                    assigned_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    resolved_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sos_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_sos_requests_app_users_citizen_id",
                        column: x => x.citizen_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location = table.Column<Point>(type: "geometry(Point,4326)", nullable: false),
                    accuracy_meters = table.Column<double>(type: "double precision", nullable: false),
                    captured_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_locations", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_locations_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alert_notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flood_zone_id = table.Column<Guid>(type: "uuid", nullable: true),
                    severity = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false),
                    read_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alert_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_alert_notifications_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alert_notifications_flood_zones_flood_zone_id",
                        column: x => x.flood_zone_id,
                        principalTable: "flood_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "rescue_team_location_histories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    rescue_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location = table.Column<Point>(type: "geometry(Point,4326)", nullable: false),
                    captured_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rescue_team_location_histories", x => x.id);
                    table.ForeignKey(
                        name: "FK_rescue_team_location_histories_rescue_teams_rescue_team_id",
                        column: x => x.rescue_team_id,
                        principalTable: "rescue_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rescue_assignments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sos_request_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rescue_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    assigned_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    accepted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    arrived_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    completed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rescue_assignments", x => x.id);
                    table.ForeignKey(
                        name: "FK_rescue_assignments_rescue_teams_rescue_team_id",
                        column: x => x.rescue_team_id,
                        principalTable: "rescue_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rescue_assignments_sos_requests_sos_request_id",
                        column: x => x.sos_request_id,
                        principalTable: "sos_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alert_notifications_created_at",
                table: "alert_notifications",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_alert_notifications_flood_zone_id",
                table: "alert_notifications",
                column: "flood_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_alert_notifications_is_read",
                table: "alert_notifications",
                column: "is_read");

            migrationBuilder.CreateIndex(
                name: "IX_alert_notifications_user_id",
                table: "alert_notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_alert_notifications_user_id_is_read_created_at",
                table: "alert_notifications",
                columns: new[] { "user_id", "is_read", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_app_users_deleted_at",
                table: "app_users",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_app_users_phone_number",
                table: "app_users",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_app_users_role",
                table: "app_users",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "ix_flood_zones_boundary_gist",
                table: "flood_zones",
                column: "boundary")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_flood_zones_created_at",
                table: "flood_zones",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_flood_zones_severity",
                table: "flood_zones",
                column: "severity");

            migrationBuilder.CreateIndex(
                name: "IX_flood_zones_status",
                table: "flood_zones",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_flood_zones_status_severity",
                table: "flood_zones",
                columns: new[] { "status", "severity" });

            migrationBuilder.CreateIndex(
                name: "IX_rescue_assignments_assigned_at",
                table: "rescue_assignments",
                column: "assigned_at");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_assignments_rescue_team_id",
                table: "rescue_assignments",
                column: "rescue_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_assignments_rescue_team_id_status_assigned_at",
                table: "rescue_assignments",
                columns: new[] { "rescue_team_id", "status", "assigned_at" });

            migrationBuilder.CreateIndex(
                name: "IX_rescue_assignments_sos_request_id",
                table: "rescue_assignments",
                column: "sos_request_id");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_assignments_status",
                table: "rescue_assignments",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_team_location_histories_captured_at",
                table: "rescue_team_location_histories",
                column: "captured_at");

            migrationBuilder.CreateIndex(
                name: "ix_rescue_team_location_histories_location_gist",
                table: "rescue_team_location_histories",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_team_location_histories_rescue_team_id",
                table: "rescue_team_location_histories",
                column: "rescue_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_team_location_histories_rescue_team_id_captured_at",
                table: "rescue_team_location_histories",
                columns: new[] { "rescue_team_id", "captured_at" });

            migrationBuilder.CreateIndex(
                name: "ix_rescue_teams_current_location_gist",
                table: "rescue_teams",
                column: "current_location")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_teams_last_location_updated_at",
                table: "rescue_teams",
                column: "last_location_updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_teams_status",
                table: "rescue_teams",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_rescue_teams_status_last_location_updated_at",
                table: "rescue_teams",
                columns: new[] { "status", "last_location_updated_at" });

            migrationBuilder.CreateIndex(
                name: "IX_shelters_created_at",
                table: "shelters",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_shelters_location_gist",
                table: "shelters",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_shelters_status",
                table: "shelters",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_shelters_status_current_occupancy",
                table: "shelters",
                columns: new[] { "status", "current_occupancy" });

            migrationBuilder.CreateIndex(
                name: "IX_sos_requests_citizen_id",
                table: "sos_requests",
                column: "citizen_id");

            migrationBuilder.CreateIndex(
                name: "IX_sos_requests_created_at",
                table: "sos_requests",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_sos_requests_location_gist",
                table: "sos_requests",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_sos_requests_priority_score",
                table: "sos_requests",
                column: "priority_score");

            migrationBuilder.CreateIndex(
                name: "IX_sos_requests_status",
                table: "sos_requests",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_sos_requests_status_priority_created_at",
                table: "sos_requests",
                columns: new[] { "status", "priority_score", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_user_locations_captured_at",
                table: "user_locations",
                column: "captured_at");

            migrationBuilder.CreateIndex(
                name: "ix_user_locations_location_gist",
                table: "user_locations",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_user_locations_user_id",
                table: "user_locations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_locations_user_id_captured_at",
                table: "user_locations",
                columns: new[] { "user_id", "captured_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alert_notifications");

            migrationBuilder.DropTable(
                name: "rescue_assignments");

            migrationBuilder.DropTable(
                name: "rescue_team_location_histories");

            migrationBuilder.DropTable(
                name: "shelters");

            migrationBuilder.DropTable(
                name: "user_locations");

            migrationBuilder.DropTable(
                name: "flood_zones");

            migrationBuilder.DropTable(
                name: "sos_requests");

            migrationBuilder.DropTable(
                name: "rescue_teams");

            migrationBuilder.DropTable(
                name: "app_users");
        }
    }
}
