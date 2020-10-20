namespace Finance.Tests.Fixtures
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoFixture;
    using Domain.Treasury.Aggregates.CategoryAggregate;

    public class CategoryDtoFixture
    {
        private readonly CategoryDto _dto;

        public CategoryDtoFixture()
        {
            var fixture = new Fixture();
            _dto = fixture.Create<CategoryDto>();
        }

        public CategoryDto Build()
        {
            return _dto;
        }

        public CategoryDtoFixture WithId(Guid id)
        {
            _dto.Id = id;
            return this;
        }

        public CategoryDtoFixture WithName(string name)
        {
            _dto.Name = name;
            return this;
        }

        public CategoryDtoFixture WithType(CategoryType type)
        {
            _dto.Type = type;
            return this;
        }
    }
}