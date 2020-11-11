using AutoMapper;
using Checkout.PaymentGateway.Api.Application;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Extensions;

namespace Checkout.PaymentGateway.Api.Infrastructure
{
    /// <summary>
    /// Provides mapping profiles to handler mapping to/from the <see cref="Payment"/> entity and associated DTOs.
    /// </summary>
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            // Maps from the request DTO, to the payment entity
            CreateMap<PaymentRequestDto, Payment>()
                .ConstructUsing(src => 
                    new Payment(new Card(src.CardNumber, src.ExpiryMonth, src.ExpiryYear, src.Cvv), 
                    new Money(src.Currency, src.Amount),
                    default))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SubmittedSuccessfully, opt => opt.Ignore());

            // Maps from the payment entity, to the response DTO
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Amount))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.Card.Number.Mask(4, '*')))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Amount.Currency))
                .ForMember(dest => dest.Cvv, opt => opt.MapFrom(src => src.Card.Cvv))
                .ForMember(dest => dest.ExpiryMonth, opt => opt.MapFrom(src => src.Card.ExpiryMonth))
                .ForMember(dest => dest.ExpiryYear, opt => opt.MapFrom(src => src.Card.ExpiryYear))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SubmittedSuccessfully, opt => opt.MapFrom(src => src.SubmittedSuccessfully));
        }
    }
}
