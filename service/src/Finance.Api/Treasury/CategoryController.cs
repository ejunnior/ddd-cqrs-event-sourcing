namespace Finance.Api.Treasury
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Treasury.Dtos;
    using Application.Treasury.Queries;
    using Domain.Core;
    using MassTransit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v{version:apiVersion}/category/")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IDispatcher _dispatcher; //TODO : need to split into a read api

        public CategoryController(
            IBusControl bus,
            IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<GetCategoryDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetCategory()
        {
            var query = new GetCategoryQuery();

            var result = await _dispatcher
                .DispatchAsync<GetCategoryQuery, IList<GetCategoryDto>>(query);

            return Ok(result);
        }
    }
}