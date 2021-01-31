using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TVSeriesAPI.Migrations
{
    public partial class initialmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EpisodeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterEpisode",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    EpisodesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEpisode", x => new { x.CharactersId, x.EpisodesId });
                    table.ForeignKey(
                        name: "FK_CharacterEpisode_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEpisode_Episodes_EpisodesId",
                        column: x => x.EpisodesId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(type: "nvarchar(249)", maxLength: 249, nullable: false),
                    IpAddressLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEpisode_EpisodesId",
                table: "CharacterEpisode",
                column: "EpisodesId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EpisodeId",
                table: "Comments",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CharacterId",
                table: "Locations",
                column: "CharacterId",
                unique: true);


            //POPULATE/SEED THE DB
            migrationBuilder.Sql($@"--CREATE EPISODES
                                        INSERT INTO Episodes
                                        (
                                            --Id - column value is auto-generated
                                            Name,
                                            ReleaseDate,
                                            EpisodeCode,
                                            Created,
                                            Modified
                                        )
                                        VALUES
                                        (
                                            -- Id - int
                                            N'Pilot', -- Name - nvarchar
                                            '2007-09-27', -- ReleaseDate - datetime2
                                            N'276023', -- EpisodeCode - nvarchar
                                            '2021-01-01 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-01 01:27:40.9733333' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'The Big Bran Hypothesis', -- Name - nvarchar
                                            '2007-10-01', -- ReleaseDate - datetime2
                                            N'3T6601', -- EpisodeCode - nvarchar
                                            '2021-01-03 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-03 01:27:40.9733333' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'The Fuzzy Boots Corollary', -- Name - nvarchar
                                            '2007-10-08', -- ReleaseDate - datetime2
                                            N'3T6602', -- EpisodeCode - nvarchar
                                            '2021-01-08 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-08 01:27:40.9733333' -- Modified - datetime2
                                        )");

            migrationBuilder.Sql($@"--CREATE COMMENTS
                                        INSERT INTO Comments
                                        (
                                            --Id - column value is auto-generated
                                            CommentText,
                                            IpAddressLocation,
                                            Created,
                                            Modified,
                                            EpisodeId
                                        )
                                        VALUES
                                        (
                                            -- Id - int
                                            N'After an unsuccessful visit to the high-IQ sperm bank, Dr. Leonard Hofstadter and Dr. Sheldon Cooper return home to find aspiring actress Penny is their new neighbor across the hall from their apartment.', -- CommentText - nvarchar
                                            N'58.36.188.74 - Shanghai', -- IpAddressLocation - nvarchar
                                            '2021-01-01 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-01 01:27:40.9733333', -- Modified - datetime2
                                            1 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'Sheldon thinks Leonard, who is immediately interested in her, is chasing a dream he will never catch.', -- CommentText - nvarchar
                                            N'106.9.182.39 - Gaocheng', -- IpAddressLocation - nvarchar
                                            '2021-01-03 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-03 01:27:40.9733333', -- Modified - datetime2
                                            1 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'Leonard invites Penny to his and Sheldon''s apartment for Indian food, where she asks to use their shower since hers is broken.', -- CommentText - nvarchar
                                            N'197.252.235.43 - Khartoum', -- IpAddressLocation - nvarchar
                                            '2021-01-08 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-08 01:27:40.9733333', -- Modified - datetime2
                                            1 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'While wrapped in a towel, she gets to meet their visiting friends Howard Wolowitz, a wannabe ladies'' man who tries to hit on her', -- CommentText - nvarchar
                                            N'23.65.169.113 - Hermosillo', -- IpAddressLocation - nvarchar
                                            '2021-01-12 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-12 01:27:40.9733333', -- Modified - datetime2
                                            1 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'and Rajesh Koothrappali, who is unable to speak to her as he suffers from selective mutism in the presence of women', -- CommentText - nvarchar
                                            N'138.225.116.176 - Zurich (Kreis 11)', -- IpAddressLocation - nvarchar
                                            '2021-01-13 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-13 01:27:40.9733333', -- Modified - datetime2
                                            1 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'Leonard is so infatuated with Penny that, after helping her use their shower, he agrees to retrieve her TV from her ex-boyfriend Kurt.', -- CommentText - nvarchar
                                            N'40.170.77.106 - Indianapolis', -- IpAddressLocation - nvarchar
                                            '2021-01-16 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-16 01:27:40.9733333', -- Modified - datetime2
                                            1 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'When Sheldon and Leonard drop off a box of flat pack furniture that came for Penny, Sheldon is deeply disturbed at how messy and disorganized her apartment is.', -- CommentText - nvarchar
                                            N'92.15.157.252 - Irlam', -- IpAddressLocation - nvarchar
                                            '2021-01-18 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-18 01:27:40.9733333', -- Modified - datetime2
                                            2 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'Later that night, while Penny sleeps, the obsessive-compulsive Sheldon, unable to sleep, sneaks into her apartment to organize and clean it.', -- CommentText - nvarchar
                                            N'7.196.198.230 - Whitehall', -- IpAddressLocation - nvarchar
                                            '2021-01-23 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-23 01:27:40.9733333', -- Modified - datetime2
                                            2 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'Leonard finds out and reluctantly helps him. The next morning, Penny is furious to discover they had been in her apartment.', -- CommentText - nvarchar
                                            N'20.145.72.158', -- IpAddressLocation - nvarchar
                                            '2021-01-26 01:27:40.9733333', -- Created - datetime2
                                            '2021-01-26 01:27:40.9733333', -- Modified - datetime2
                                            2 -- EpisodeId - int
                                        ),
                                        (
                                            -- Id - int
                                            N'Sheldon tries to apologize to Penny but fails by remarking that Leonard is a ''gentle and thorough lover''.', -- CommentText - nvarchar
                                            N'169.203.9.117 - Washington D.C. (Northwest Washington)', --IpAddressLocation - nvarchar
                                            '2021-01-27 01:27:40.9733333', --Created - datetime2
                                            '2021-01-27 01:27:40.9733333', --Modified - datetime2
                                            2-- EpisodeId - int
                                        ),
                                        (
                                            --Id - int
                                            N'When he sees Penny kissing a man in front of her apartment door, Leonard is devastated that she has ''rejected'' him.', --CommentText - nvarchar
                                            N'99.253.213.189 - Toronto (Old Toronto)', --IpAddressLocation - nvarchar
                                            '2021-01-29 01:27:40.9733333', --Created - datetime2
                                            '2021-01-29 01:27:40.9733333', --Modified - datetime2
                                            3-- EpisodeId - int
                                        ),
                                        (
                                            --Id - int
                                            N'The guys persuade him to date someone at work, so he approaches fellow scientist Leslie Winkle.', --CommentText - nvarchar
                                            N'55.162.117.251 - Sierra Vista (Fort Huachuca)', --IpAddressLocation - nvarchar
                                            '2021-01-30 01:27:40.9733333', --Created - datetime2
                                            '2021-01-30 01:27:40.9733333', --Modified - datetime2
                                            3-- EpisodeId - int
                                        )
                                        ");

            migrationBuilder.Sql($@"--CREATE CHARACTERS
                                        INSERT INTO Characters
                                        (
                                            --Id - column value is auto-generated
                                            FirstName,
                                            LastName,
                                            Status,
                                            StateOfOrigin,
                                            Gender,
                                            Created,
                                            Modified
                                        )
                                        VALUES
                                        (
                                            -- Id - int
                                            N'Leonard', -- FirstName - nvarchar
                                            N'Hofstadter', -- LastName - nvarchar
                                            N'ACTIVE', -- Status - nvarchar
                                            N'New Jersey', -- StateOfOrigin - nvarchar
                                            N'MALE', -- Gender - nvarchar
                                            '2021-01-01 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-01 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Sheldon', -- FirstName - nvarchar
                                            N'Cooper', -- LastName - nvarchar
                                            N'ACTIVE', -- Status - nvarchar
                                            N'Texas', -- StateOfOrigin - nvarchar
                                            N'MALE', -- Gender - nvarchar
                                            '2021-01-05 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-05 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Penny', -- FirstName - nvarchar
                                            N'Wyatt', -- LastName - nvarchar
                                            N'ACTIVE', -- Status - nvarchar
                                            N'Nebraska', -- StateOfOrigin - nvarchar
                                            N'FEMALE', -- Gender - nvarchar
                                            '2021-01-07 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-07 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Howard', -- FirstName - nvarchar
                                            N'Wolowitz', -- LastName - nvarchar
                                            N'DEAD', -- Status - nvarchar
                                            N'Ohio', -- StateOfOrigin - nvarchar
                                            N'MALE', -- Gender - nvarchar
                                            '2021-01-10 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-10 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Rajesh', -- FirstName - nvarchar
                                            N'Koothrappali', -- LastName - nvarchar
                                            N'UNKNOWN', -- Status - nvarchar
                                            N'Kerala', -- StateOfOrigin - nvarchar
                                            N'MALE', -- Gender - nvarchar
                                            '2021-01-11 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-11 01:55:32.2600000' -- Modified - datetime2
                                        )");


            migrationBuilder.Sql($@"--CREATE LOCATIONS FOR EACH CHARACTER
                                        INSERT INTO Locations
                                        (
                                            --Id - column value is auto-generated
                                            Name,
                                            Latitude,
                                            Longitude,
                                            CharacterId,
                                            Created,
                                            Modified
                                        )
                                        VALUES
                                        (
                                            -- Id - int
                                            N'Leonard''s Place', -- Name - nvarchar
                                            41.804078, -- Latitude - float
                                            -109.179121, -- Longitude - float
                                            1, -- CharacterId - int
                                            '2021-01-01 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-01 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Sheldon''s Place', -- Name - nvarchar
                                            35.675147, -- Latitude - float
                                            -105.441276, -- Longitude - float
                                            2, -- CharacterId - int
                                            '2021-01-05 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-05 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Penny''s Place', -- Name - nvarchar
                                            36.809285, -- Latitude - float
                                            -86.664098, -- Longitude - float
                                            3, -- CharacterId - int
                                            '2021-01-07 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-07 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Howard''s Place', -- Name - nvarchar
                                            32.805745, -- Latitude - float
                                            -97.393914, -- Longitude - float
                                            4, -- CharacterId - int
                                            '2021-01-10 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-10 01:55:32.2600000' -- Modified - datetime2
                                        ),
                                        (
                                            -- Id - int
                                            N'Rajesh''s Place', -- Name - nvarchar
                                            33.541395, -- Latitude - float
                                            -80.735911, -- Longitude - float
                                            5, -- CharacterId - int
                                            '2021-01-11 01:55:32.2600000', -- Created - datetime2
                                            '2021-01-11 01:55:32.2600000' -- Modified - datetime2
                                        )");


            migrationBuilder.Sql($@"--ADD CHARACTERS TO EPISODES
                                        INSERT INTO CharacterEpisode
                                        (
                                            CharactersId,
                                            EpisodesId
                                        )
                                        VALUES
                                        (
                                            1, -- CharactersId - int
                                            1 -- EpisodesId - int
                                        ),
                                        (
                                            2, -- CharactersId - int
                                            1 -- EpisodesId - int
                                        ),
                                        (
                                            3, -- CharactersId - int
                                            1 -- EpisodesId - int
                                        ),
                                        (
                                            1, -- CharactersId - int
                                            2 -- EpisodesId - int
                                        ),
                                        (
                                            2, -- CharactersId - int
                                            2 -- EpisodesId - int
                                        ),
                                        (
                                            3, -- CharactersId - int
                                            2 -- EpisodesId - int
                                        ),
                                        (
                                            4, -- CharactersId - int
                                            2 -- EpisodesId - int
                                        ),
                                        (
                                            5, -- CharactersId - int
                                            2 -- EpisodesId - int
                                        ),
                                        (
                                            3, -- CharactersId - int
                                            3 -- EpisodesId - int
                                        ),
                                        (
                                            5, -- CharactersId - int
                                            3 -- EpisodesId - int
                                        )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEpisode");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
