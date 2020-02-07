CREATE TABLE [dbo].[Transactions]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TransactionId] NCHAR(50) NOT NULL, 
    [Amount] DECIMAL NOT NULL, 
    [CurrencyCode] NCHAR(3) NOT NULL, 
    [TransactionDate] DATETIME NOT NULL, 
    [StatusId] INT NOT NULL, 
    CONSTRAINT [FK_Transactions_ToStatuses] FOREIGN KEY ([StatusId]) REFERENCES [Statuses]([Id])
)
