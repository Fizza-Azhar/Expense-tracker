using ExpenseTracker.Models;

namespace ExpenseTracker.Data;

public static class ExpenseStore
{
    public static List<Expense> Expenses = new();
    public static int NextId = 1;
}