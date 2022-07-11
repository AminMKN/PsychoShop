namespace PsychoShop.Application.Contracts.UserClaim
{
    public class AddOrRemoveClaim
    {
        public string UserAccountId { get; set; }
        public List<UserClaimDetails> UserClaimDetails { get; set; }
    }
}