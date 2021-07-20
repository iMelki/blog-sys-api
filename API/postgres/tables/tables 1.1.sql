CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL,
    "UserName" TEXT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210420185625_InitialCreate', '5.0.7');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ADD "PasswordHash" bytea NULL;

ALTER TABLE "Users" ADD "PasswordSalt" bytea NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210603142311_UserPasswordAdded', '5.0.7');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ALTER COLUMN "UserName" TYPE text;

--ALTER TABLE "Users" ALTER COLUMN "PasswordSalt" TYPE bytea;

--ALTER TABLE "Users" ALTER COLUMN "PasswordHash" TYPE bytea;

ALTER TABLE "Users" ALTER COLUMN "Id" TYPE integer;
ALTER TABLE "Users" ALTER COLUMN "Id" DROP DEFAULT;
ALTER TABLE "Users" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY;

CREATE TABLE "Posts" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text NULL,
    "Content" text NULL,
    "UserId" integer NOT NULL,
    "AppUserId" integer NULL,
    CONSTRAINT "PK_Posts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Posts_Users_AppUserId" FOREIGN KEY ("AppUserId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Posts_AppUserId" ON "Posts" ("AppUserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210719013022_AddedPostEntity', '5.0.7');

COMMIT;