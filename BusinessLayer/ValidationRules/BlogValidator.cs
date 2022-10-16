using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Blog başlığını boş geçemezsiniz");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("Blog içeriğini boş geçemezsiniz");
            RuleFor(x => x.BlogContent).MinimumLength(160).WithMessage("En az 160 karakter girmelisiniz");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Blog görseli eklemelisiniz");
            RuleFor(x => x.BlogTitle).MinimumLength(5).WithMessage("Bu alana en az 5 karakter girebilirsiniz");
            RuleFor(x => x.BlogTitle).MaximumLength(150).WithMessage("Bu alana en fazla 150 karakter girebilirsiniz");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("İçeriğiniz ile ilgili bir kategori seçin");
        }


    }
}
