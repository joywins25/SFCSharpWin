use [SampleDB]
GO
DROP TABLE dbo.coffee_sales;

CREATE TABLE DBO.COFFEE_SALES (
    id INT PRIMARY KEY IDENTITY (1, 1),
	coffee_kind NVARCHAR (50) NOT NULL,     
    coffee_name NVARCHAR (50) NOT NULL,    
	price INT DEFAULT 0,
    orderTime DATETIME
);

INSERT INTO COFFEE_SALES (coffee_kind, coffee_name, price, orderTime)
VALUES ('Coffee', 'Americano', 200, getdate())

SELECT CAST(id as nvarchar(5)), coffee_kind, coffee_name, price, orderTime FROM COFFEE_SALES;

UPDATE COFFEE_SALES SET price = 200 WHERE coffee_kind = '커피' and coffee_name = '아메리카노'

DELETE FROM COFFEE_SALES WHERE coffee_kind = '커피' and coffee_name = '아메리카노'