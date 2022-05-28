
using System;

namespace CleanCode.SwitchStatements
{
    public abstract class Customer
    {
        public abstract MonthlyStatement GenerateStatement(MonthlyUsage monthlyUsage);
    }

    public class PayAsYouGoCustomer : Customer
    {
        public override MonthlyStatement GenerateStatement(MonthlyUsage monthlyUsage)
        {
            var statement = new MonthlyStatement();

            statement.CallCost = 0.12f * monthlyUsage.CallMinutes;
            statement.SmsCost = 0.12f * monthlyUsage.SmsCount;
            statement.TotalCost = statement.CallCost + statement.SmsCost;

            return statement;
        }
    }

    public class Unlimited : Customer
    {
        public override MonthlyStatement GenerateStatement(MonthlyUsage monthlyUsage)
        {
            var statement = new MonthlyStatement();

            statement.TotalCost = 54.90f;

            return statement;
        }
    }
}
