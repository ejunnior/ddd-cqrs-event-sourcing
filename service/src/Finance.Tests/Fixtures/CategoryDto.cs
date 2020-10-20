namespace Finance.Tests.Fixtures
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Treasury.Aggregates.CategoryAggregate;

    public class CategoryDto
    {
        public Guid Id { get; set; }

        [StringLength(80)]
        public string Name { get; set; }

        public CategoryType Type { get; set; }
    }
}