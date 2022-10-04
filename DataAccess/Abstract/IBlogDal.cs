using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBlogDal:IGenericDal<Blog>
    {
        /* Blogları Listelerken kategorileri isimleri ile getirebilmek için include metotunu kullnmamız gerekiyor.
         Burada Yazdığımız operasyonu efblogrepository de dolduralım*/
        List<Blog> GetListWithCategory();

        /* bu metot sayesinde yazar sayfasında oturum açan yazarın kendi yayımladığı blogları getireceğiz*/
        List<Blog> GetListWithCategoryByWriter(int id);
    }
}
