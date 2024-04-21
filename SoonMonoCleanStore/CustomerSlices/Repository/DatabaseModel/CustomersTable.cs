﻿using Core.Domain.ValueObject;

namespace CustomerSlices.Repository.DatabaseModel
{
    public class CustomersTable
    {
        public const string TableName = "Customer";
        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public Address? BillingAddress { get; private set; }
    }
}
