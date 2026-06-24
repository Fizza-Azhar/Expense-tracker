using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Data;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(ExpenseStore.Expenses);
    }

    [HttpPost]
    public IActionResult AddExpense([FromBody] Expense expense)
    {
        if (string.IsNullOrWhiteSpace(expense.Title))
        {
            return BadRequest(new { error = "Title cannot be empty" });
        }

        if (expense.Amount <= 0)
        {
            return BadRequest(new { error = "Amount must be greater than zero" });
        }

        expense.Id = ExpenseStore.NextId++;
        expense.CreatedAt = DateTime.Now;

        ExpenseStore.Expenses.Add(expense);

        return StatusCode(201, expense);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        var expense = ExpenseStore.Expenses.FirstOrDefault(e => e.Id == id);

        if (expense == null)
        {
            return NotFound();
        }

        ExpenseStore.Expenses.Remove(expense);

        return NoContent();
    }
}