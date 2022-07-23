using PsychoShop.Framework.Domain;

namespace PsychoShop.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public int RefId { get; set; }
        public string UserAccountId { get; set; }
        public string IssueTrackingNo { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order(string userAccountId, double totalAmount, double discountAmount, double payAmount,
            string postalCode, string phoneNumber, string address)
        {
            UserAccountId = userAccountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Address = address;
            RefId = 0;
            IsPaid = false;
            IsCanceled = false;
            OrderItems = new List<OrderItem>();
        }

        public void PaymentSuccess(int refId)
        {
            IsPaid = true;
            if (refId != 0)
                RefId = refId;
        }

        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void Add(OrderItem item)
        {
            OrderItems.Add(item);
        }
    }
}
