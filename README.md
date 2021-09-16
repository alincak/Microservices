# Microservices

Run:
docker-compose up

".Net 5 ile Microservices" kursunda inşa ettiğimiz microservice projesi

![114802958-42c15d80-9da7-11eb-8391-ba0abf87a1b1](https://user-images.githubusercontent.com/13946186/126895535-da790d16-7379-46d7-b461-d672f487134b.png)


# Neler yapıyor?
Udemy tarzı bir projedir. Kullanıcı olarak; kurs ekleyebilir, silebilir, güncelleyebilir, sepete ekleyebilir, indirim uygulayabilir ve satın alabilirsiniz.

# Hangi teknolojiler kullanıldı?
Servisler ve veritabanları Docker Container lar aracılığı ile ayağa kaldırılıyor. İşlemler IdentityServer Token teknolojisi ile haberleşmektedir.

- ### Gateway <br>
Client isteklerini ilgili servislere yönlendirir. Bu yönlendirmeler için **Ocelot** kullanılmıştır.

- ### Asp.Net Core MVC Web <br>
Microservice'lerden almış olduğu dataları kullanıcıya gösterecek ve kullanıcı ile etkileşime geçmekten sorumlu olacak UI mikroservisimiz

- ### Course.Shared <br>
Tüm projenin ortak işlemlerinin yürütülmesinden sorumludur.

- ### IdentityServer <br>
Yapılan tüm işlemler için gerekli olan Kimlik Doğrulamalarının yapıldığı proje. Burada **EntityFrameworkCore** ve **SqlServer** kullanılmıştır.

- ### Basket Service <br>
Sepet işlemlerinin yapıldığı projedir. Burada **Redis** ve kuyruk haberleşmesi için **RabbitMQ** kullanılmıştır. 

- ### Catalog Service <br>
Kursların sahip olduğu katalog işlemlerinin yapıldığı projedir. Burada **MongoDB** ve kuyruk haberleşmesi için **RabbitMQ** kullanılmıştır.

- ### Discount Service <br>
Sepete uygulanan indirim işlemlerinin yapıldığı projedir. Burada **Npgsql** ve **Dapper** kullanılmıştır.

- ### FakePayment Service <br>
Sepet ödemesi işlemlerinin yapıldığı projedir. Gerçek bir işlem olmadığından isim olarak "Fake" tercih edildi. Sanki işlem yapılıyormuş gibi yazılmıştır. **RabbitMQ** kullanılarak başarılı olan ödemeler sipariş olarak kuyruğa eklenir.

- ### Order Service <br>
**Domain driven design** (DDD) ile yazılmıştır. Ödemesi başarılı olan işlemleri sipariş olarak eklemiştik. Burada CommandConsumer veya EventConsumer lar aracılığı ile işlemleri yapıyoruz. **RabbitMQ** kulanılmıştır. Burada **EntityFrameworkCore** ve **SqlServer** kullanılmıştır. Ayrıca bu db işlemleri için **CQRS Pattern**, **MediatR** Kütüphanesi kullanılmıştır.

- ### PhotoStock Service <br>
Kursa eklenen resim işlemlerini yürüten projedir. Bu proje özelinde herhangi bir sunucu veya veritabanı işlemi yapılmamıştır. Eklenen resimler local olarak tutulmuştur.

- ### Message Broker <br>
    - Mesaj kuyruk sistemi olarak RabbitMQ kullanıyor.
    - RabbitMQ ile haberleşmek için MassTransit kütüphanesini kullanıyor.
    - RabbitMQ (MassTransit Library)

<br><br>


> **keywords;** Docker, EntityFrameworkCore, SqlServer, Redis, Ocelot, RabbitMQ, MongoDB, Npgsql, Domain driven design, CQRS Pattern, MediatR, Dapper

> iyi kodlamalar... :)
