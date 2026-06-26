-- 1. Show all expenses with their category
SELECT Id, Title, Amount, Category
FROM Expenses;

-- 2. Show total amount spent in each category
SELECT Category, SUM(Amount) AS TotalAmount
FROM Expenses
GROUP BY Category;

-- 3. Show the most expensive expense
SELECT TOP 1 *
FROM Expenses
ORDER BY Amount DESC;