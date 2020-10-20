namespace Finance.Api.Creditor
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Creditor.Dtos;
    using Application.Creditor.Queries;
    using Domain.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v{version:apiVersion}/creditor/")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CreditorController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public CreditorController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<GetCreditorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetCreditor()
        {
            var query = new GetCreditorQuery();

            var result = await _dispatcher
                .DispatchAsync<GetCreditorQuery, IList<GetCreditorDto>>(query);

            return Ok(result);
        }

        //private readonly IBusControl _bus;

        //public VendorController(
        //    IBusControl bus)
        //{
        //    _bus = bus;
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<RegisterVendorDto>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Consumes("application/json")]
        //public async Task<IActionResult> Register(
        //    [FromBody]RegisterVendorDto request)
        //{
        //    var destination = new Uri(_bus.Address, "financeservice");
        //    var sendEndpoint = await _bus.GetSendEndpoint(destination);

        //    await sendEndpoint
        //        .Send(new RegisterVendorMessage(
        //            name: request.Name,
        //            mobilePhone: request.MobilePhone,
        //            email: request.Email));

        //    return FromResult(Result.Success());
        //}
    }
}