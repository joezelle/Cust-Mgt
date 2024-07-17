BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Customers].[Gender]', N'PhoneNumber', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240717160646_Edited Entity', N'7.0.20');
GO

COMMIT;
GO

