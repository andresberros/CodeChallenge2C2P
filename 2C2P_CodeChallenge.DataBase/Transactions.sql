CREATE TABLE [dbo].[Transactions]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TransactionId] NCHAR(50) NOT NULL, 
    [Amount] DECIMAL NOT NULL, 
    [CurrencyCode] NCHAR(3) NOT NULL, 
    [TransactionDate] DATETIME NOT NULL, 
    [Status] NCHAR(10) NOT NULL
)
