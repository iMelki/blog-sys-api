CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "UserGroups" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text NULL,
    "Content" text NULL,
    "TimeCreated" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_UserGroups" PRIMARY KEY ("Id")
);

CREATE TABLE Users (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "TimeCreated" timestamp without time zone NOT NULL,
    "UserName" text NULL,
    "PasswordHash" bytea NULL,
    "PasswordSalt" bytea NULL,
    "UserGroupId" integer NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Users_UserGroups_UserGroupId" FOREIGN KEY ("UserGroupId") REFERENCES "UserGroups" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Follows" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Time" timestamp without time zone NOT NULL,
    "FollowerUserId" integer NOT NULL,
    "FollowerAppUserId" integer NULL,
    "FollowedUserId" integer NOT NULL,
    "FollowedAppUserId" integer NULL,
    CONSTRAINT "PK_Follows" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Follows_Users_FollowedAppUserId" FOREIGN KEY ("FollowedAppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Follows_Users_FollowerAppUserId" FOREIGN KEY ("FollowerAppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "GroupMessages" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Message" text NULL,
    "SenderUserId" integer NOT NULL,
    "SenderAppUserId" integer NULL,
    "GroupId" integer NOT NULL,
    "UserGroupId" integer NULL,
    "TimePosted" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_GroupMessages" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_GroupMessages_UserGroups_UserGroupId" FOREIGN KEY ("UserGroupId") REFERENCES "UserGroups" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_GroupMessages_Users_SenderAppUserId" FOREIGN KEY ("SenderAppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Posts" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text NULL,
    "Content" text NULL,
    "UserId" integer NOT NULL,
    "AppUserId" integer NULL,
    "TimePosted" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Posts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Posts_Users_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "PrivateMessages" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Content" text NULL,
    "SenderUserId" integer NOT NULL,
    "SenderAppUserId" integer NULL,
    "RecieverUserId" integer NOT NULL,
    "RecieverAppUserId" integer NULL,
    "TimePosted" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_PrivateMessages" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PrivateMessages_Users_RecieverAppUserId" FOREIGN KEY ("RecieverAppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_PrivateMessages_Users_SenderAppUserId" FOREIGN KEY ("SenderAppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Likes" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Time" timestamp without time zone NOT NULL,
    "LikesUserId" integer NOT NULL,
    "LikesAppUserId" integer NULL,
    "LikedPostId" integer NOT NULL,
    "LikedAppPostId" integer NULL,
    CONSTRAINT "PK_Likes" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Likes_Posts_LikedAppPostId" FOREIGN KEY ("LikedAppPostId") REFERENCES "Posts" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Likes_Users_LikesAppUserId" FOREIGN KEY ("LikesAppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Follows_FollowedAppUserId" ON "Follows" ("FollowedAppUserId");

CREATE INDEX "IX_Follows_FollowerAppUserId" ON "Follows" ("FollowerAppUserId");

CREATE INDEX "IX_GroupMessages_SenderAppUserId" ON "GroupMessages" ("SenderAppUserId");

CREATE INDEX "IX_GroupMessages_UserGroupId" ON "GroupMessages" ("UserGroupId");

CREATE INDEX "IX_Likes_LikedAppPostId" ON "Likes" ("LikedAppPostId");

CREATE INDEX "IX_Likes_LikesAppUserId" ON "Likes" ("LikesAppUserId");

CREATE INDEX "IX_Posts_AppUserId" ON "Posts" ("AppUserId");

CREATE INDEX "IX_PrivateMessages_RecieverAppUserId" ON "PrivateMessages" ("RecieverAppUserId");

CREATE INDEX "IX_PrivateMessages_SenderAppUserId" ON "PrivateMessages" ("SenderAppUserId");

CREATE INDEX "IX_Users_UserGroupId" ON "Users" ("UserGroupId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210720194139_AddedEntities', '5.0.7');

COMMIT;