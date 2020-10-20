namespace Finance.Api.Treasury
{
    using Application.Treasury.Dtos;
    using Application.Treasury.Queries;
    using CSharpFunctionalExtensions;
    using Domain.Core;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using MassTransit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/v{version:apiVersion}/bankposting/")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [Consumes("application/json")]
    public class BankPostingController : BaseController
    {
        private readonly IBusControl _bus;
        private readonly IDispatcher _dispatcher;

        public BankPostingController(
            IBusControl bus,
            IDispatcher dispatcher)
        {
            _bus = bus;
            _dispatcher = dispatcher;
        }

        private static string ServiceName => "finance.service";

        //TODO : need to split into a read api
        [HttpDelete("{bankPostingId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid bankPostingId)
        {
            var destination = new Uri(_bus.Address, ServiceName); //TODO: need to be improved
            var sendEndpoint = await _bus.GetSendEndpoint(destination);

            await sendEndpoint
                .Send<DeleteBankPostingCommand>(new
                {
                    BankPostingId = bankPostingId
                });

            return Accepted();
        }

        [HttpPut("{bankPostingId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Envelope<EditPostingBankDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> Edit(
            Guid bankPostingId,
            [FromBody] EditPostingBankDto request)
        {
            var destination = new Uri(_bus.Address, ServiceName);
            var sendEndpoint = await _bus.GetSendEndpoint(destination);

            await sendEndpoint
                .Send<EditBankPostingCommand>(new
                {
                    BankPostingId = bankPostingId,
                    Amount = request.Amount,
                    DueDate = request.DueDate,
                    DocumentDate = request.DocumentDate,
                    DocumentNumber = request.DocumentNumber,
                    CreditorId = request.CreditorId,
                    Description = request.Description,
                    BankAccountId = request.BankAccountId,
                    CategoryId = request.CategoryId,
                    PaymentDate = request.PaymentDate,
                    Type = request.Type
                });

            return Accepted();
        }

        [HttpPatch("{bankPostingId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Envelope<EditPostingBankDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> Edit(
            Guid bankPostingId,
            [FromBody] JsonPatchDocument<EditPostingBankDto> request)
        {
            var persisted = await _dispatcher
                .DispatchAsync<GetBankPostingByIdQuery, GetBankPostingByIdDto>(
                    new GetBankPostingByIdQuery(bankPostingId));

            if (persisted == null) //TODO: need to be improved
            {
                return NotFound();
            }

            var dto = new EditPostingBankDto();

            request.ApplyTo(dto);

            var destination = new Uri(_bus.Address, ServiceName);
            var sendEndpoint = await _bus.GetSendEndpoint(destination);

            await sendEndpoint
                .Send<EditBankPostingCommand>(new
                {
                    BankPostingId = bankPostingId,
                    Amount = dto.Amount,
                    DueDate = dto.DueDate,
                    DocumentDate = dto.DocumentDate,
                    DocumentNumber = dto.DocumentNumber,
                    CreditorId = dto.CreditorId,
                    Description = dto.Description,
                    BankAccountId = dto.BankAccountId,
                    CategoryId = dto.CategoryId,
                    PaymentDate = dto.PaymentDate,
                    Type = dto.Type
                });

            return Accepted();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<IList<GetBankPostingDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBankPosting()
        {
            var query = new GetBankPostingQuery();

            var result = await _dispatcher
                .DispatchAsync<GetBankPostingQuery, IList<GetBankPostingDto>>(query);

            return Ok(result);
        }

        [HttpGet("{bankPostingId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<GetBankPostingByIdDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBankPosting(Guid bankPostingId)
        {
            var query = new GetBankPostingByIdQuery(bankPostingId);

            var result = await _dispatcher
                .DispatchAsync<GetBankPostingByIdQuery, GetBankPostingByIdDto>(query);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Envelope<RegisterPostingBankDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterPostingBankDto request)
        {
            var destination = new Uri(_bus.Address, ServiceName); //TODO: need to be improved
            var sendEndpoint = await _bus.GetSendEndpoint(destination);

            await sendEndpoint
                .Send<RegisterBankPostingCommand>(new
                {
                    Amount = request.Amount,
                    DueDate = request.DueDate,
                    DocumentDate = request.DocumentDate,
                    DocumentNumber = request.DocumentNumber,
                    CreditorId = request.CreditorId,
                    Description = request.Description,
                    BankAccountId = request.BankAccountId,
                    CategoryId = request.CategoryId,
                    PaymentDate = request.PaymentDate,
                    Type = request.Type
                });

            return Accepted();
        }
    }
}