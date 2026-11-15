# Daftar Fungsi Aplikasi ECommerce Console

Dokumentasi seluruh fungsi (method) yang ada di dalam program, diurutkan per class beserta nomor urut dan penjelasan fungsinya.

| No | Class | Nama Fungsi | Deskripsi |
|----|-------|-------------|-----------|
| 1 | `Program` | `Main` | Titik masuk utama program. Memanggil `seedData` pada `Products` dan `Users`, lalu menjalankan `MenuAwal` secara berulang dalam loop tak terbatas. |
| 2 | `Program` | `MenuAwal` | Menampilkan menu awal (Login, Register, Keluar) dan mengarahkan pilihan pengguna ke fungsi terkait. |
| 3 | `Program` | `MenuUtama` | Menampilkan menu utama setelah login (Lihat Produk, Keranjang, Checkout, Riwayat Transaksi, serta menu Admin jika yang login adalah admin) dan mengarahkan pilihan pengguna. |
| 4 | `Users` | `_saveUser` | Menyimpan data user (ID, nama, password, role) ke dalam array global untuk pendaftaran user baru. |
| 5 | `Users` | `seedData` | Mengisi data user bawaan (admin dan customer) saat program pertama kali dijalankan. |
| 6 | `Users` | `IsEmptyOrNull` | Memeriksa apakah input berupa string kosong atau null. |
| 7 | `Users` | `getIndexUserByName` | Mencari index user berdasarkan nama; mengembalikan -1 jika tidak ditemukan. |
| 8 | `Users` | `getReadLine` | Membaca input dari pengguna dengan validasi tidak boleh kosong, disertai opsi membersihkan layar. |
| 9 | `Users` | `Login` | Memproses login dengan memvalidasi username dan password, lalu menyimpan role & ID user ke session dan mengarahkan ke menu utama. |
| 10 | `Users` | `Register` | Memproses registrasi user baru (username unik, password, dan pilihan role admin/customer). |
| 11 | `Users` | `daftarUser` | Fungsi kosong (belum diimplementasikan), disiapkan untuk menampilkan daftar user. |
| 12 | `Products` | `seedData` | Mengisi 25 data produk awal (via `_addProd`) untuk kebutuhan demo aplikasi. |
| 13 | `Products` | `getIndexProductByID` | Mencari index produk berdasarkan ID; mengembalikan -1 jika tidak ditemukan. |
| 14 | `Products` | `menuProduct` | Menu admin untuk kelola produk: list, tambah, edit, dan hapus produk. |
| 15 | `Products` | `listProduct` | Menampilkan daftar seluruh produk dalam bentuk tabel (No, ID, Nama, Stock, Cost, Harga, Kategori). |
| 16 | `Products` | `_addProd` | Helper internal untuk menambahkan satu produk ke dalam array global (dipakai oleh `seedData` dan `addProduct`). |
| 17 | `Products` | `addProduct` | Menambahkan produk baru dengan validasi ID tidak duplikat dan batas maksimal produk. |
| 18 | `Products` | `modifyProductByID` | Mengubah data produk berdasarkan ID (nama, cost, harga, kategori, stok). |
| 19 | `Products` | `deleteProductByID` | Menghapus produk berdasarkan ID dengan konfirmasi, lalu menggeser elemen array. |
| 20 | `Catalog` | `daftarMenu` | Menampilkan menu katalog (semua produk, per kategori, cari produk) dan mengarahkan pilihan pengguna. |
| 21 | `Catalog` | `byCategory` | Menampilkan produk berdasarkan kategori unik, dengan paging dan opsi menambah produk ke keranjang. |
| 22 | `Catalog` | `search` | Mencari produk berdasarkan kata kunci pada nama/kategori, dengan paging dan opsi menambah ke keranjang. |
| 23 | `Catalog` | `menuProduct` | Menampilkan semua produk yang diurutkan (bubble sort) berdasarkan nama, dengan paging dan fitur tambah ke keranjang. |
| 24 | `Cart` | `AddToCart` | Menambahkan produk (ID dan qty) ke dalam keranjang. |
| 25 | `Cart` | `List` | Menampilkan isi keranjang beserta total belanja dan opsi untuk mengubah qty atau menghapus item. |
| 26 | `Cart` | `OpsiMenu` | Menampilkan opsi aksi untuk sebuah item keranjang (edit qty, hapus, batal). |
| 27 | `Cart` | `DeleteProduct` | Menghapus item dari keranjang lalu menggeser elemen array. |
| 28 | `Cart` | `ChangeQty` | Mengubah jumlah (qty) sebuah item di keranjang. |
| 29 | `Checkout` | `Process` | Memproses checkout: menghitung total, menerapkan voucher, membuat struk, konfirmasi pembayaran, lalu mencatat transaksi. |
| 30 | `Transaction` | `addTransaction` | Menyimpan satu record transaksi (tanggal, ID trx, user, produk, qty, diskon, cost, retail, total) ke array. |
| 31 | `Transaction` | `getIndexTrxById` | Mencari index transaksi berdasarkan ID; mengembalikan -1 jika tidak ditemukan. |

## Ringkasan Jumlah Fungsi per Class

| Class | Jumlah Fungsi |
|-------|---------------|
| `Program` | 3 |
| `Users` | 8 |
| `Products` | 8 |
| `Catalog` | 4 |
| `Cart` | 5 |
| `Checkout` | 1 |
| `Transaction` | 2 |
| `Report` | 0 (belum ada fungsi) |

**Total: 31 fungsi**

> Catatan: Class `Report` saat ini masih kosong, sehingga belum ada fungsi yang terdaftar di dalamnya.