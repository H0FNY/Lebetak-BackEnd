namespace Lebetak.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; } = 0M;

        // Navigation Properties
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
