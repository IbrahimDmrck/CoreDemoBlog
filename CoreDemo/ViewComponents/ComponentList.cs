using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents
{
    public class ComponentList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var commentValues = new List<UserComment>
            {
                new UserComment    {      ID=1,Name="İbrahim" },
                new UserComment    {      ID=2,Name="Seyfi" },
                new UserComment    {      ID=3,Name="Yusuf" },
                new UserComment    {      ID=4,Name="Mehtap" },
                new UserComment    {      ID=5,Name="Sevim" },
                new UserComment    {      ID=6,Name="Ayşe" }

            };
            return View(commentValues);
        }
    }
}
/*
 MVC’nin önceki sürümlerinde birden fazla alanda kullanmak istediğimiz bileşenleri genellikle Partial View
olarak tasarlar ve [ChildActionOnly] attribute ile birlikte tek başlarına çağrılmalarını engellerdik. 
Bu yapı bizim için büyük kolaylık sağlardı. Asp.Net Core ile birlikte [ChildActionOnly] attribute kullanımı 
kaldırılmış ve yeni ViewComponent yapısı geliştirilmiştir. ViewComponentler dışarıdan Http istek ile doğrudan 
ulaşılamazlar. Ve view components'lar partial view'e  çok benzer, ancak partial view'e kıyasla çok daha güçlüdür.
view components'lar model bağlama kullanmaz. Ancak, yalnızca onu çağırdığımızda sağlanan verilerle çalışır.
partial view  gibi, view componentsi de controller'lara bağlı değildir. Bileşenin modelini ve ustura biçimlendirme
görünüm sayfasını geliştirmek için mantığı uygulamak için kendi sınıfına sahiptir. En önemli şey, 
View Components'ın bağımlılık enjeksiyonunu kullanabilmesidir, bu da onları çok güçlü ve test edilebilir kılar.

-------------------

Kullanım amacı olarak birebir aynı olan bu iki yapının birbirinden ayrılmasını sağlayan özelliği
View Component tarafında server tarafa daha az yük bindirilmesinin amaçlanmış olmasıdır. 
Partial View içerisinde tanımlanan bir method local function olarak kullanılır.Bu methodların dışarıdan
bire bire tetiklenmesi ve kullanılması mümkün değildir.

Örnek:“Customer listemiz var ve her eklenen datadan sonra listenin anlık olarak güncellenmesini ve 
bu güncelleme sırasında zaman kaybı olmadan yapılmasını istiyoruz”.

Çözüm:“Bahsettiğim mevcut durumun üstesinden gelmek için CustomerController içerisinde
PartialViewResult dönen bir method yazılır. Method ajax ile tetiklenir ve dönen Partial append edilir.
Bunun gibi bir çok durumda benzer süreçler işlemektedir.

Buda uygulamanın Controller ile çok fazla haberleşmesini ve bu neticede performans kayıplarını beraberinde getirmektedir.
View Component yapısının tamda bu noktada Partial View yapısındaki bu dezavantajı ortadan kaldırmaktadı
 */
