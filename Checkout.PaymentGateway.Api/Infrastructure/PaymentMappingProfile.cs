using AutoMapper;
using Checkout.PaymentGateway.Api.Payments;
using Checkout.PaymentGateway.Domain;

namespace Checkout.PaymentGateway.Api.Infrastructure
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<PaymentRequestDto, Payment>()
                .ConstructUsing(src => 
                    new Payment(new Card(src.CardNumber, src.ExpiryMonth, src.ExpiryYear, src.Cvv), 
                    new Money(src.CurrencySymbol, src.Amount)))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Amount))
                .ForMember(dest => dest.CurrencySymbol, opt => opt.MapFrom(src => src.Amount.CurrencySymbol))
                .ForMember(dest => dest.Cvv, opt => opt.MapFrom(src => src.Card.Cvv))
                .ForMember(dest => dest.ExpiryMonth, opt => opt.MapFrom(src => src.Card.ExpiryMonth))
                .ForMember(dest => dest.ExpiryYear, opt => opt.MapFrom(src => src.Card.ExpiryYear))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
