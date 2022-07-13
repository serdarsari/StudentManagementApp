# Student Management Application WebAPI

**Technical Overview**

.Net 6.0

Sql Server

Rest API

Swagger

EF Core

AutoMapper

FluentValidation

MediatR

CQRS

Generic Repository Pattern

Role Based Authorization

JWT Authentication

----- Geliştirmeye devam ediyorum her gün eklemeler oldukça bu alan güncellenecek.
Henüz bütün CRUD işlemlerini yazmadım. Uygulamayı daha hızlı kullanılabilir hale getirmek için bazı sınıfların sadece Create veya Get işlemlerini yazdım. Update ve Delete gibi işlemlerini henüz yazmadığım sınıflar mevcut.

### Veritabanı kurulumu (Sql Server)

Veritabanını kendi yerel veritabanınızda kullanabilmek için,
```
Tools > NuGet Package Manager > Package Manager Console
```
alanını açıp, **Default Project** kısmını, **StudentManagement.Entity** olarak seçtikten sonra,
```
update-database
```
yazmanız yeterlidir. Veritabanınızda **StudentManagementApp** adında bir veritabanı oluşacaktır.

## Uygulama Hakkında
* Öğrenci, Öğretmen ve Yönetici içeren 3 kullanıcının kullanabileceği bir **bilgilendirme ve yönetim** uygulamasıdır.
* 3 farklı rol bulunmaktadır. (Student, Teacher, Admin)
* Kullanıcı yetkisi olmayan işlemleri yapamamaktadır. Sadece rolünün izin verdiği işlemleri yapabilmektedir.
* Öğrenci giriş yaptığında sadece kendi notlarını ve kendine kayıtlı dersleri görebilmektedir.
* Öğretmen giriş yaptığında sadece kendi verdiği dersi ve öğrencilerini görebilmekte ve not verebilmektedir. Ayrıca kendi öğrencilerinin velilerini de görebilmektedir.
* Yönetici giriş yaptığında tüm öğretmenleri, öğrencileri ve velilerini görebilmektedir. Tüm işlemleri yapabilmektedir.

NOT : API için ajax ile basit bir UI hazırladım, onu da yakın zamanda yükleyeceğim. API'yi ve UI'ı çalıştırıp uygulamayı kendi bilgisayarınızda canlı test edebilirsiniz.