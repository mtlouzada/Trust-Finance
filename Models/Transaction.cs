namespace TF.Models;

public class Transaction
{
	public int Id { get; set; }
	public string Description { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }

	public int CategoryId { get; set; }
    public Category Category { get; set; } // Navigation property

    public int UserId { get; set; }
	public User User { get; set; } // Navigation property
}