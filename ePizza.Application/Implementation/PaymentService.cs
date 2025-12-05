using AutoMapper;
using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Request;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;


namespace ePizza.Application.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(
            IMapper mapper,
            IPaymentRepository paymentRepository,
            IOrderRepository orderRepository)
        {
            this._mapper = mapper;
            this._paymentRepository = paymentRepository;
            this._orderRepository = orderRepository;
        }

        public async Task<string> CapturePaymentDetailsAsync(MakePaymentRequestDto request)
        {
            // apply validation here as well

            var paymentDomain = _mapper.Map<PaymentDomain>(request);

            if (request.OrderRequest is not null)
            {
                var orderDomain = _mapper.Map<OrderDomain>(request.OrderRequest);

                await _orderRepository.AddAsync(orderDomain);
            }

            await _paymentRepository.AddAsync(paymentDomain);

            await _paymentRepository.CommitAsync();


            return await Task.FromResult("paymentCompleted"); // modify it as per use case
        }
    }
}
