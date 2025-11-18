using FluentValidation;
namespace WishList.Feaches.Booked
{
    public class BookedValidation : AbstractValidator<BookedReqest>
    {
        public BookedValidation()
        {
            RuleFor(x => x.id).NotEmpty()
                .WithMessage("Не может быть пустым");
        }

    }
}