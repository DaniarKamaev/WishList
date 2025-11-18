using FluentValidation;
using System.Xml.Linq;

namespace WishList.Feaches.CreateList
{
    public class CreateValidation : AbstractValidator<CreateListReqest>
    {
        public CreateValidation()
        {
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("Id не может быть пустым");
            RuleFor(x => x.Gift).NotEmpty()
                .WithMessage("Поле gift не может быть пустым");
            RuleFor(x => x.Price).NotEmpty()
                .WithMessage("Поле price не может быть пустым");
            RuleFor(x => x.URL).NotEmpty()
                .WithMessage("Поле URL не может быть пустым");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0)
                .WithMessage("Цена не может быть отрицательной");
        }
    }
}