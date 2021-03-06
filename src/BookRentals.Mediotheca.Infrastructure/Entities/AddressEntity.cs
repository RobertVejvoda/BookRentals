﻿using BookRentals.Core.Infrastructure.Entities;
using System;

namespace BookRentals.Mediotheca.Infrastructure.Entities
{
    public class AddressEntity : AuditableEntity
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }

    public class LibraryAddressEntity : AddressEntity
    {
        public Guid MainAddressId { get; set; }
        public Guid BillingAddressId { get; set; }
        public Guid ShippingAddressId { get; set; }

        public virtual LibraryEntity Library { get; set; }
    }
}
