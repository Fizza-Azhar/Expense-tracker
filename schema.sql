-- Bonus: Create an index on Category
CREATE INDEX IX_Expenses_Category
ON Expenses(Category);

-- This index improves the speed of searching,
-- filtering, and grouping expenses by category.