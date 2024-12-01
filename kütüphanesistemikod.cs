using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Kitap
    {
        public string Baslik { get; set; }
        public string Yazar { get; set; }
        public string Yayinci { get; set; }
        public bool Mevcut { get; set; } = true;
        public string Kiralayan { get; set; } = null;
        public int Gun { get; set; } = 0;

        public Kitap(string baslik, string yazar, string yayinci)
        {
            Baslik = baslik;
            Yazar = yazar;
            Yayinci = yayinci;
        }
    }

    static void Main(string[] args)
    {
        List<Kitap> kitaplar = new List<Kitap>();

        while (true)
        {
            Console.WriteLine("\nKütüphane Yönetim Sistemi");
            Console.WriteLine("1. Kitap Ekle");
            Console.WriteLine("2. Kitap Kirala");
            Console.WriteLine("3. Kitap İade Et");
            Console.WriteLine("4. Kitap Ara");
            Console.WriteLine("5. Kitap Raporla");
            Console.WriteLine("0. Çıkış");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "0")
            {
                Console.WriteLine("Çıkış yapılıyor...");
                break;
            }

            switch (choice)
            {
                case "1":
                    KitapEkle(kitaplar);
                    break;
                case "2":
                    Kirala(kitaplar);
                    break;
                case "3":
                    İade(kitaplar);
                    break;
                case "4":
                    KitapArama(kitaplar);
                    break;
                case "5":
                    Raporla(kitaplar);
                    break;
                default:
                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void KitapEkle(List<Kitap> kitaplar)
    {
        Console.Write("Kitap adı: ");
        string baslik = Console.ReadLine();
        Console.Write("Yazar adı: ");
        string yazar = Console.ReadLine();
        Console.Write("Yayıncı adı: ");
        string yayinci = Console.ReadLine();

        kitaplar.Add(new Kitap(baslik, yazar, yayinci));
        Console.WriteLine($"\"{baslik}\" adlı kitap eklendi.");
    }

    static void Kirala(List<Kitap> kitaplar)
    {
        Console.Write("Kiralanacak kitap adı: ");
        string baslik = Console.ReadLine();
        Kitap kitap = kitaplar.FirstOrDefault(b => b.Baslik.Equals(baslik, StringComparison.OrdinalIgnoreCase) && b.Mevcut);

        if (kitap != null)
        {
            Console.Write("Kiracı adı: ");
            string kiralayan = Console.ReadLine();
            Console.Write("Kaç günlüğüne kiralanacak: ");
            int gun = int.Parse(Console.ReadLine());

            kitap.Mevcut = false;
            kitap.Kiralayan = kiralayan;
            kitap.Gun = gun;
            Console.WriteLine($"\"{kitap.Baslik}\" adlı kitap {gun} günlüğüne kiralandı. Ödenecek toplam tutar: {gun * 5:C}.");
        }
        else
        {
            Console.WriteLine($"\"{baslik}\" adlı kitap şu anda uygun değil veya mevcut değil.");
        }
    }

    static void İade(List<Kitap> kitaplar)
    {
        Console.Write("İade edilecek kitap adı: ");
        string baslik = Console.ReadLine();
        Kitap kitap = kitaplar.FirstOrDefault(b => b.Baslik.Equals(baslik, StringComparison.OrdinalIgnoreCase) && !b.Mevcut);

        if (kitap != null)
        {
            Console.WriteLine($"\"{kitap.Baslik}\" adlı kitap başarıyla iade edildi. Ödenmesi gereken toplam ücret: {kitap.Gun * 5:C}.");
            kitap.Mevcut = true;
            kitap.Kiralayan = null;
            kitap.Gun = 0;
        }
        else
        {
            Console.WriteLine($"\"{baslik}\" adlı kitap iade edilemez çünkü kirada değil.");
        }
    }

    static void KitapArama(List<Kitap> kitaplar)
    {
        Console.Write("Aranacak kitap adı: ");
        string baslik = Console.ReadLine();
        List<Kitap> bulunanlar = kitaplar.Where(b => b.Baslik.Contains(baslik, StringComparison.OrdinalIgnoreCase)).ToList();

        if (bulunanlar.Any())
        {
            Console.WriteLine("Bulunan kitaplar:");
            foreach (var kitap in bulunanlar)
            {
                Console.WriteLine($"- {kitap.Baslik} (Yazar: {kitap.Yazar}, Yayıncı: {kitap.Yayinci}, Durum: {(kitap.Mevcut ? "Mevcut" : $"Kirada, Kiracı: {kitap.Kiralayan}, Geri Getirme Süresi: {kitap.Gun} gün" )})");
            }
        }
        else
        {
            Console.WriteLine("Hiçbir kitap bulunamadı.");
        }
    }

    static void Raporla(List<Kitap> Kitaplar)
    {
        Console.WriteLine("Raporlama Seçenekleri:");
        Console.WriteLine("1. Bir yazarın tüm kitaplarını ara");
        Console.WriteLine("2. Bir yayının tüm kitaplarını ara");
        Console.WriteLine("3. Hangi kitapların kimde olduğunu listele");
        Console.WriteLine("4. Tüm kitapları listele");
        Console.Write("Seçiminizi yapın: ");
        string rapor = Console.ReadLine();

        switch (rapor)
        {
            case "1":
                Console.Write("Yazar adı: ");
                string yazar = Console.ReadLine();
                var yazarkitap = Kitaplar.Where(b => b.Yazar.Equals(yazar, StringComparison.OrdinalIgnoreCase)).ToList();
                if (yazarkitap.Any())
                {
                    Console.WriteLine($"{yazar} adlı yazarın kitapları:");
                    foreach (var kitap in yazarkitap)
                    {
                        Console.WriteLine($"- {kitap.Baslik}");
                    }
                }
                else
                {
                    Console.WriteLine($"{yazar} adlı yazara ait kitap bulunamadı.");
                }
                break;

            case "2":
                Console.Write("Yayıncı adı: ");
                string yayinci = Console.ReadLine();
                var yayincikitap = Kitaplar.Where(b => b.Yayinci.Equals(yayinci, StringComparison.OrdinalIgnoreCase)).ToList();
                if (yayincikitap.Any())
                {
                    Console.WriteLine($"{yayinci} yayıncısının kitapları:");
                    foreach (var kitap in yayincikitap)
                    {
                        Console.WriteLine($"- {kitap.Baslik}");
                    }
                }
                else
                {
                    Console.WriteLine($"{yayinci} adlı yayıncıya ait kitap bulunamadı.");
                }
                break;

            case "3":
                var kiralananlar = Kitaplar.Where(b => !b.Mevcut).ToList();
                if (kiralananlar.Any())
                {
                    Console.WriteLine("Kirada olan kitaplar:");
                    foreach (var kitap in kiralananlar)
                    {
                        Console.WriteLine($"- {kitap.Baslik}, Kiracı: {kitap.Kiralayan}, Geri Getirme Süresi: {kitap.Gun} gün");
                    }
                }
                else
                {
                    Console.WriteLine("Kirada olan kitap yok.");
                }
                break;

            case "4":
                if (Kitaplar.Any())
                {
                    Console.WriteLine("Tüm kitaplar:");
                    foreach (var kitap in Kitaplar)
                    {
                        Console.WriteLine($"- {kitap.Baslik} (Yazar: {kitap.Yazar}, Yayıncı: {kitap.Yayinci}, Durum: {(kitap.Mevcut ? "Mevcut" : $"Kirada, Kiracı: {kitap.Kiralayan}, Geri Getirme Süresi: {kitap.Gun} gün")})");
                    }
                }
                else
                {
                    Console.WriteLine("Kütüphanede hiçbir kitap yok.");
                }
                break;

                 default:
                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                break;
        }
    }
}