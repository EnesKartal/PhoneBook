# Rehber Projesi

Bu proje, .NET 6 ile mikroservis mimarisi kullanılarak oluşturulmuş bir rehber uygulamasıdır. Proje, PhoneBook.ApiGateway, PhoneBook.Common, PhoneBook.Contact.API ve PhoneBook.Report.API olmak üzere dört ayrı proje içerir.

## Proje Yapısı

- PhoneBook.ApiGateway: API Gateway projesidir. Tüm mikroservislerin tek bir noktadan yönetilmesini sağlar. RabbitMQ ile Contact API ve Report API arasında iletişim kurar.
- PhoneBook.Common: Ortak kodları içeren bir kütüphanedir. Diğer projelerdeki tekrar eden kodları buraya çıkartmak için kullanılır.
- PhoneBook.Common.Tests: Ortak kodların testlerini içeren bir test projesidir.
- PhoneBook.Contact.API: Rehberin kişi bilgilerini yöneten API'dir. RabbitMQ üzerinden Report API ile iletişim kurar.
- PhoneBook.Contact.API.Tests: Contact API'nin testlerini içeren bir test projesidir.
- PhoneBook.Report.API: Raporlama ile ilgili işlemleri yöneten API'dir. RabbitMQ üzerinden Contact API ile iletişim kurar.
- PhoneBook.Report.API.Tests: Report API'nin testlerini içeren bir test projesidir.

## Kurulum ve Kullanım

1. Bu proje için .NET 6 SDK yüklü olmalıdır.

2. dotnet restore ve dotnet build komutlarıyla nuget paketlerini yükleyin ve projeyi derleyin.

3. Veritabanınlarını migrate etmek için CMD ya da PowerShell yardımıyla kodları sırasıyla çalıştırın.
    -cd komutu ile Projelerin bulunduğu konuma gidin.
    -Eğer dotnet-ef yüklü değil ise: dotnet tool install --global dotnet-ef
    -Sırasıyla iki API projesine de CD komutu yardımı ile giderek aşağıdaki kodu çalıştırın
    -dotnet ef migrations add InitialCreate
    -dotnet ef database update

4. PhoneBook.ApiGateway projesini çalıştırın. Proje, localhost:5550 üzerinde çalışacaktır.

5. PhoneBook.Contact.API ve PhoneBook.Report.API projelerini ayrı ayrı çalıştırın. Contact API, localhost:5551 üzerinde çalışacaktır, Report API ise localhost:5552 üzerinde çalışacaktır.

6. Proje Swagger UI arayüzüne erişmek için aşağıdaki adresleri kullanabilirsiniz:
   - API Gateway: http://localhost:5000
   - Contact API: http://localhost:5550/swagger
   - Report API: http://localhost:5551/swagger

7. Contact API ile Report API arasında RabbitMQ ile iletişim sağlandığını unutmayın. Bu sayede her iki API arasında veri alışverişi mümkün olacaktır.


## İletişim

Proje hakkında herhangi bir sorunuz veya geri bildiriminiz varsa lütfen iletişime geçmekten çekinmeyin:

- **Enes Kartal**
- E-posta: eneskartal117@gmail.com
