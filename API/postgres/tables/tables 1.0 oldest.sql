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

ALTER TABLE "Users" ADD "PasswordHash" BLOB NULL;

ALTER TABLE "Users" ADD "PasswordSalt" BLOB NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210603142311_UserPasswordAdded', '5.0.7');

COMMIT;