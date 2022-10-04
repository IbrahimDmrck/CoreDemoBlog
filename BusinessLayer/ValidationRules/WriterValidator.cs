using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Ad Alanı Boş Geçilemez");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Ad Alanı En Az 2 Karakter Olabilir");
            RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("Ad Alanı En fFazla 50 Karakter Olabilir");
            

            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Alanı Boş Geçilemez");
  
        }

      
    }
}
