# 🚀 Listify Kurulum Rehberi

Bu rehber, Listify uygulamasını yeni bir bilgisayara nasıl kuracağınızı adım adım açıklar.

## 📋 Sistem Gereksinimleri

### Minimum Gereksinimler
- **İşletim Sistemi:** Windows 10/11, macOS 10.15+, veya Linux (Ubuntu 18.04+)
- **RAM:** 2 GB ve üzeri
- **Disk Alanı:** 500 MB boş alan

### Yazılım Gereksinimleri
- **.NET 10.0 SDK** (Zorunlu)
- **Git** (Opsiyonel - versiyon kontrolü için)
- **Visual Studio Code** veya **Visual Studio 2022** (Opsiyonel - kod düzenleme için)

---

## 🔧 Adım 1: .NET 10.0 SDK Kurulumu

### Windows için
1. [.NET 10.0 SDK indirme sayfasına](https://dotnet.microsoft.com/download/dotnet/10.0) gidin
2. **SDK** bölümünden **Windows** sürümünü indirin
3. İndirilen dosyayı çalıştırın
4. Kurulum sihirbazını "Next" ile ilerletin ve "Install" yapın
5. Kurulum tamamlandıktan sonra komut satırını açın ve doğrulayın:
```cmd
dotnet --version
```
Çıktı `10.0.x` şeklinde olmalıdır.

### macOS için
1. Terminal'i açın
2. Homebrew kurulu değilse önce Homebrew kurun:
```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```
3. .NET 10.0 SDK'yı kurun:
```bash
brew install dotnet@10
```
4. Doğrulayın:
```bash
dotnet --version
```

### Linux (Ubuntu) için
1. Terminal'i açın
2. Microsoft paket deposunu ekleyin:
```bash
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```
3. .NET 10.0 SDK'yı kurun:
```bash
sudo apt-get update
sudo apt-get install -y dotnet-sdk-10.0
```
4. Doğrulayın:
```bash
dotnet --version
```

---

## 📥 Adım 2: Projeyi İndirme

### Seçenek A: ZIP olarak indirme
1. Projeyi GitHub veya depolandığı yerden ZIP olarak indirin
2. İndirilen ZIP dosyasını çıkarın
3. Çıkarılan klasöre gidin:
   - Windows: `cd C:\Users\KullaniciAdiniz\Downloads\Listify`
   - macOS/Linux: `cd ~/Downloads/Listify`

### Seçenek B: Git ile klonlama
```bash
git clone <repository-url>
cd Listify
```

---

## ⚙️ Adım 3: Projeyi Derleme

1. **Bağımlılıkları geri yüklein:**
```bash
dotnet restore
```
Bu komut, projenin gerektirdiği tüm NuGet paketlerini indirir.

2. **Projeyi build edin:**
```bash
dotnet build
```
Başarılı build sonucunda şunu görmelisiniz:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

⚠️ **Hata alırsanız:**
- .NET SDK sürümünü kontrol edin (`dotnet --version`)
- Tüm paketlerin yüklendiğinden emin olun (`dotnet restore`)

---

## ▶️ Adım 4: Uygulamayı Çalıştırma

### Geliştirme Modunda Çalıştırma
```bash
dotnet run
```
Uygulama başlatıldıktan sonra şöyle bir çıktı göreceksiniz:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

Tarayıcınızda açın:
- **http://localhost:5000**
- veya **https://localhost:5001**

### Release Build olarak Çalıştırma
Daha performanslı çalışması için:

1. Release modunda build edin:
```bash
dotnet build -c Release
```

2. Çalıştırın:
```bash
dotnet run -c Release
```

---

## 🗄️ Veritabanı

Uygulama ilk çalıştığında otomatik olarak:
1. `alisveris.db` SQLite veritabanı dosyasını oluşturur
2. Gerekli tabloları otomatik oluşturur (Entity Framework Migration)
3. Varsayılan olarak veritabanı proje klasöründe bulunur

### Veritabanı Konumu
- **Windows:** `%USERPROFILE%\AppData\Local\` veya proje klasörü
- **macOS/Linux:** `~/.local/share/` veya proje klasörü

---

## 📱 Uygulamaya Erişim

Uygulama çalıştıktan sonra:

| Sayfa | URL |
|-------|-----|
| Ana Sayfa | `/` veya `/Home` |
| Ürünler | `/Alisveris?listeId=1` |
| Kategoriler | `/Kategori` |
| Favoriler | `/Favori` |
| İstatistikler | `/Istatistik` |
| Listeler | `/Liste` |

---

## 🛠️ Yaygın Sorunlar ve Çözümleri

### Sorun 1: "dotnet: command not found"
**Çözüm:** .NET SDK doğru kurulmamış. [Kurulum adımlarını](#adım-1-net-100-sdk-kurulumu) tekrarlayın.

### Sorun 2: Port 5000 kullanımda
**Çözüm:** Farklı bir port belirleyin:
```bash
dotnet run --urls "http://localhost:8080"
```

### Sorun 3: Veritabanı hatası
**Çözüm:** Veritabanı dosyasını silin ve uygulamayı yeniden başlatın:
```bash
rm alisveris.db  # macOS/Linux
del alisveris.db  # Windows
```

### Sorun 4: Paket yükleme hatası
**Çözüm:** NuGet kaynaklarını temizleyin:
```bash
dotnet nuget locals all --clear
dotnet restore
```

---

## 🚀 Prodüksiyon için Dağıtım

### Windows için EXE oluşturma:
```bash
dotnet publish -c Release -r win-x64 --self-contained
```
Çıktı: `bin/Release/net10.0/win-x64/publish/`

### macOS için APP oluşturma:
```bash
dotnet publish -c Release -r osx-x64 --self-contained
```

### Linux için:
```bash
dotnet publish -c Release -r linux-x64 --self-contained
```

---

## 📞 Destek

Sorun yaşarsanız:
1. Bu dosyadaki [Yaygın Sorunlar](#yaygın-sorunlar-ve-çözümleri) bölümünü kontrol edin
2. GitHub Issues bölümünden sorun bildirin
3. Proje geliştiricileriyle iletişime geçin

---

## ✅ Kurulum Testi

Kurulumun başarılı olduğunu test etmek için:

1. Uygulamayı çalıştırın: `dotnet run`
2. Tarayıcıda `http://localhost:5000` açın
3. Şu işlemleri test edin:
   - ✅ Yeni liste oluşturma
   - ✅ Ürün ekleme
   - ✅ Kategori ekleme
   - ✅ Ürünü işaretleme (checkbox)
   - ✅ İstatistikler sayfasını görüntüleme

Her şey çalışıyorsa kurulum başarılı! 🎉

---

**Son Güncelleme:** Şubat 2026
**Versiyon:** 1.0.0
