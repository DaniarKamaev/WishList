using FluentValidation;
namespace WishList.Feaches.Booked
{
    public class BookedValidation : AbstractValidator<BookedReqest>
    {
        public BookedValidation()
        {
            RuleFor(x => x.id).NotEmpty()
                .WithMessage("Не может быть пустым");
            RuleFor(x => x.booked)
                .Must(value => value == 0 || value == 1)
                .WithMessage("Значение должно быть 0 или 1");
        }

    }
}