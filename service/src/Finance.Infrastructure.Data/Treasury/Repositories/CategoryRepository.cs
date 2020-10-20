namespace Finance.Infrastructure.Data.Treasury.Repositories
{
    using Core;
    using Domain.Treasury.Aggregates.CategoryAggregate;
    using UnitOfWork;

    public class CategoryRepository
        : Repository<Category>, ICategoryRepository
    {
        private readonly IFinanceUnitOfWork _unitOfWork;

        public CategoryRepository(IFinanceUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}