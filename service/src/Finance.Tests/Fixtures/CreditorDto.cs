namespace Finance.Tests.Fixtures
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreditorDto
    {
        [StringLength(255)]
        public string Email { get; set; }

        public Guid Id { get; set; }

        [StringLength(255)]
        public string MobilePhone { get; set; }

        [StringLength(80)]
        public string Name { get; set; }
    }
}