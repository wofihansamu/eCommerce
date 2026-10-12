# ECommerce Console Application

Aplikasi Ecommerce Console Application adalah aplikasi berbasis console yang digunakan untuk mengelola toko online. Aplikasi ini memungkinkan pengguna untuk menambahkan produk, mengelola inventaris, memproses pesanan, dan melihat laporan penjualan.

## Fitur Utama
- Login
- Registrasi
- Catalog Produk
- Cart
- Checkout
- Transaction History
- Product Management
- Sales Report

## Tipe User
- Admin: Dapat mengelola produk, melihat laporan penjualan
    * User : admin
    * Password : admin123
- Customer: Dapat melihat katalog produk, menambahkan produk ke keranjang, melakukan checkout
    * User : budi
    * Password : budi123

## Properties Produk
- ID Produk
- Nama Produk
- Deskripsi Produk
- Harga Produk
- Kategori Produk
- Stok Produk

## Voucher
- `HEMAT10` : Diskon 10% (potongan harga 10% dari total harga)
- `POTONG5K` : Diskon Rp 5.000 (minimum pembelian Rp 50.000)

## Instalasi
1. Clone repository ini ke komputer Anda:
    ```bash
    git clone https://github.com/wofihansamu/eCommerce.git
    ```
2. Masuk ke direktori proyek:
    ```bash
    cd eCommerce
    ```
3. Jalankan aplikasi menggunakan C#:
    ```bash
    dotnet run
    ```

## Contributing
Jika Anda ingin berkontribusi pada proyek ini, silakan fork repository ini dan buat pull request. Pastikan untuk menambahkan fitur baru atau memperbaiki bug yang ada.

### Cara Fork Repository
1. Klik tombol "Fork" di pojok kanan atas halaman repository ini.
2. Pilih akun GitHub Anda untuk menyimpan salinan repository.
3. Setelah berhasil melakukan fork, clone repository hasil fork ke komputer Anda:

    ```bash
    git clone https://github.com/<username>/eCommerce.git
    ```

### Cara Membuat Pull Request
1. Buat branch baru untuk fitur atau perbaikan bug yang ingin Anda tambahkan:
    ```bash
    git checkout -b nama-branch-anda
    ```
2. Lakukan perubahan pada kode sumber sesuai kebutuhan.
3. Commit perubahan Anda:
    ```bash
    git commit -m "Deskripsi perubahan yang Anda buat"
    ```
4. Push branch Anda ke repository hasil fork:
    ```bash
    git push origin nama-branch-anda
    ```
5. Buka repository asli di GitHub dan klik tombol "New Pull Request".
6. Pilih branch yang baru saja Anda buat dan klik "Create Pull Request".


## License
Proyek ini dilisensikan di bawah MIT License.


## Anggota Kelompok
- Angel
- Wofi
