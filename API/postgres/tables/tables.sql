CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Users" (
    "Id" SERIAL CONSTRAINT "PK_Users" PRIMARY KEY,
    "TimeCreated" timestamp without time zone NOT NULL,
    "UserName" TEXT NOT NULL,
    "PasswordHash" bytea NOT NULL,
    "PasswordSalt" bytea NOT NULL
);

CREATE TABLE "Follows" (
    "FollowerUserId" INTEGER NOT NULL,
    "FollowedUserId" INTEGER NOT NULL,
    "Time" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Follows" PRIMARY KEY ("FollowerUserId", "FollowedUserId"),
    CONSTRAINT "FK_Follows_Users_FollowedUserId" FOREIGN KEY ("FollowedUserId") REFERENCES "Users" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Follows_Users_FollowerUserId" FOREIGN KEY ("FollowerUserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Posts" (
    "Id" SERIAL CONSTRAINT "PK_Posts" PRIMARY KEY,
    "Title" TEXT NULL,
    "Content" TEXT NULL,
    "UserId" INTEGER NOT NULL,
    "AppUserId" INTEGER NULL,
    "TimePosted" timestamp without time zone NOT NULL,
    CONSTRAINT "FK_Posts_Users_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Likes" (
    "LikesUserId" INTEGER NOT NULL,
    "LikedPostId" INTEGER NOT NULL,
    "Time" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Likes" PRIMARY KEY ("LikesUserId", "LikedPostId"),
    CONSTRAINT "FK_Likes_Posts_LikedPostId" FOREIGN KEY ("LikedPostId") REFERENCES "Posts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Likes_Users_LikesUserId" FOREIGN KEY ("LikesUserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Follows_FollowedUserId" ON "Follows" ("FollowedUserId");

CREATE INDEX "IX_Likes_LikedPostId" ON "Likes" ("LikedPostId");

CREATE INDEX "IX_Posts_AppUserId" ON "Posts" ("AppUserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210721060135_AddedEntities', '5.0.7');

COMMIT;