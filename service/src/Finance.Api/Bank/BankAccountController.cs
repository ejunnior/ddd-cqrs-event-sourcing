namespace Finance.Api.Bank
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Bank.Dto;
    using Application.Bank.Queries;
    using Domain.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v{version:apiVersion}/bankaccount")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Authorize]
    public class BankAccountController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public BankAccountController(
            IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<RegisterBankAccountDto>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Consumes("application/json")]
        //public async Task<IActionResult> Register(
        //    Guid bankId,
        //    [FromBody]RegisterBankAccountDto request)
        //{
        //    var destination = new Uri(_bus.Address, "financeservice");
        //    var sendEndpoint = await _bus.GetSendEndpoint(destination);

        //    await sendEndpoint
        //        .Send(new RegisterBankAccountMessage(
        //            accountNumber: request.AccountNumber,
        //            initialBalanceAmount: request.InitialBalanceAmount,
        //            bankId: bankId));

        //    return FromResult(Result.Success());
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<GetBankAccountDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetBankAccount()
        {
            var query = new GetBankAccountQuery();

            var result = await _dispatcher
                .DispatchAsync<GetBankAccountQuery, IList<GetBankAccountDto>>(query);

            return Ok(result);
        }
    }
}