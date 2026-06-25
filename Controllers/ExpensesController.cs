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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var expense = ExpenseStore.Expenses.FirstOrDefault(e => e.Id == id);

        if (expense == null)
        {
            return NotFound();
        }

        return Ok(expense);
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

    [HttpPut("{id}")]
    public IActionResult UpdateExpense(int id, [FromBody] Expense updatedExpense)
    {
        var expense = ExpenseStore.Expenses.FirstOrDefault(e => e.Id == id);

        if (expense == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(updatedExpense.Title))
        {
            return BadRequest(new { error = "Title cannot be empty" });
        }

        if (updatedExpense.Amount <= 0)
        {
            return BadRequest(new { error = "Amount must be greater than zero" });
        }

        expense.Title = updatedExpense.Title;
        expense.Amount = updatedExpense.Amount;

        return Ok(expense);
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