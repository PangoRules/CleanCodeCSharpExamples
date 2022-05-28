using CleanCode.Comments;
using System;
using System.Collections.Generic;

namespace CleanCode.OutputParameters
{
    public class CustomersResults
    {
        private int _totalCount;
        private IEnumerable<Customer> _customers;

        public CustomersResults(int totalCount, IEnumerable<Customer> customers)
        {
            _totalCount = totalCount;
            _customers = customers;
        }

        public int TotalCount { get => _totalCount; }
        public IEnumerable<Customer> Customers { get => _customers; }
    }

    public class OutputParameters
    {
        private const int PageIndex = 1;

        public void DisplayCustomers()
        {
            var query = GetCustomers(PageIndex);

            Console.WriteLine("Total customers: " + query.TotalCount);
            foreach (var c in query.Customers)
                Console.WriteLine(c);
        }

        public CustomersResults GetCustomers(int pageIndex)
        {
            return new CustomersResults(100, new List<Customer>());
        }
    }
}
