using System;

namespace CleanCode.NestedConditionals
{
    public class Customer
    {
        public int LoyaltyPoints { get; set; }

        public bool IsGoldCustomer()
        {
            return LoyaltyPoints > 100;
        }
    }

    public class Reservation
    {
        public DateTime From { get; set; }
        public Customer Customer { get; set; }
        public bool IsCanceled { get; set; }

        public Reservation(Customer customer, DateTime dateTime)
        {
            From = dateTime;
            Customer = customer;
        }

        public void Cancel()
        {
            if(DateTime.Now > From || CustomerCanCancel())
                throw new InvalidOperationException("It's too late to cancel.");

            IsCanceled = true;
        }

        private bool LessThanHours(int totalHours)
        {
            return (From - DateTime.Now).TotalHours < totalHours;
        }

        private bool CustomerCanCancel()
        {
            return (Customer.IsGoldCustomer() && LessThanHours(24) || !Customer.IsGoldCustomer() && LessThanHours(48));
        }
    }
}
