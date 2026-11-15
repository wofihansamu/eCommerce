# Daftar Fungsi Aplikasi ECommerce Console

Dokumentasi seluruh fungsi (method) yang ada di dalam program, diurutkan per class beserta nomor urut dan penjelasan fungsinya.

| No | Class | Nama Fungsi | Visibilitas | Deskripsi |
|----|-------|-------------|-------------|-----------|
| 1 | `Program` | `Main` | `static` | Titik masuk utama program. Membuat direktori data jika belum ada, memanggil `DataFile.LoadAllData()`, lalu menjalankan `MenuAwal` secara berulang dalam loop tak terbatas. |
| 2 | `Program` | `MenuAwal` | `public static` | Menampilkan menu awal (Login, Register, Keluar), menyimpan data via `DataFile.SaveAllData()`, dan mengarahkan pilihan pengguna ke fungsi terkait. |
| 3 | `Program` | `MenuUtama` | `public static` | Menampilkan menu utama setelah login (Lihat Produk, Keranjang, Checkout, Riwayat Transaksi, Kelola Produk & Laporan Penjualan khusus admin) dan mengarahkan pilihan pengguna. |
| 4 | `Users` | `_saveUser` | `public static` | Menyimpan data user (ID, nama, password, role) ke dalam array global untuk pendaftaran user baru. |
| 5 | `Users` | `seedData` | `public static` | Mengisi data user bawaan (admin dan customer) saat file data user tidak ditemukan. |
| 6 | `Users` | `IsEmptyOrNull` | `static` | Memeriksa apakah input berupa string kosong atau null. |
| 7 | `Users` | `getIndexUserByName` | `public static` | Mencari index user berdasarkan nama; mengembalikan `Constant.KOSONG` (-1) jika tidak ditemukan. |
| 8 | `Users` | `getReadLine` | `static` | Membaca input dari pengguna dengan validasi tidak boleh kosong, disertai opsi membersihkan layar. |
| 9 | `Users` | `Login` | `public static` | Memproses login dengan memvalidasi username dan password (maksimal 3x gagal), lalu menyimpan role & ID user ke session dan mengarahkan ke menu utama. |
| 10 | `Users` | `Register` | `public static` | Memproses registrasi user baru (username unik, password, dan pilihan role admin/customer). |
| 11 | `Products` | `seedData` | `public static` | Mengisi 25 data produk awal (via `_addProd`) untuk kebutuhan demo aplikasi. |
| 12 | `Products` | `getIndexProductByID` | `public static` | Mencari index produk berdasarkan ID; mengembalikan `Constant.KOSONG` (-1) jika tidak ditemukan. |
| 13 | `Products` | `menuProduct` | `public static` | Menu admin untuk kelola produk: list, tambah, edit, dan hapus produk. |
| 14 | `Products` | `listProduct` | `public static` | Menampilkan daftar seluruh produk dalam bentuk tabel (No, ID, Nama, Stock, Cost, Harga, Kategori). |
| 15 | `Products` | `_addProd` | `static` | Helper internal untuk menambahkan satu produk ke dalam array global dengan ID auto-increment (dipakai oleh `seedData` dan `addProduct`). |
| 16 | `Products` | `addProduct` | `public static` | Menambahkan produk baru dengan validasi batas maksimal produk. |
| 17 | `Products` | `modifyProductByID` | `public static` | Mengubah data produk berdasarkan ID (nama, cost, harga, kategori, stok) dengan nilai lama sebagai default jika input kosong. |
| 18 | `Products` | `deleteProductByID` | `public static` | Menghapus produk berdasarkan ID dengan konfirmasi, lalu menggeser elemen array. |
| 19 | `Catalog` | `daftarMenu` | `public static` | Menampilkan menu katalog (semua produk, per kategori, cari produk) dan mengarahkan pilihan pengguna. |
| 20 | `Catalog` | `byCategory` | `public static` | Menampilkan produk berdasarkan kategori unik, dengan paging dan opsi menambah produk ke keranjang. |
| 21 | `Catalog` | `search` | `public static` | Mencari produk berdasarkan kata kunci pada nama/kategori, dengan paging dan opsi menambah ke keranjang. |
| 22 | `Catalog` | `menuProduct` | `public static` | Menampilkan semua produk yang diurutkan (bubble sort) berdasarkan nama, dengan paging dan fitur tambah ke keranjang (via `Cart.AddToCart`). |
| 23 | `Cart` | `AddToCart` | `public static` | Menambahkan produk (ID dan qty) ke keranjang; jika produk sudah ada, menambahkan qty ke item yang ada. |
| 24 | `Cart` | `List` | `public static` | Menampilkan isi keranjang milik user yang sedang login beserta total belanja dan opsi untuk mengubah qty atau menghapus item. |
| 25 | `Cart` | `OpsiMenu` | `public static` | Menampilkan opsi aksi untuk sebuah item keranjang (edit qty, hapus, batal). |
| 26 | `Cart` | `DeleteProduct` | `public static` | Menghapus item dari keranjang, mengembalikan stok produk, lalu menggeser elemen array. |
| 27 | `Cart` | `ChangeQty` | `public static` | Mengubah jumlah (qty) sebuah item di keranjang. |
| 28 | `Checkout` | `GetNextTrxId` | `public static` | Menghitung ID transaksi berikutnya berdasarkan ID transaksi terbesar yang sudah ada. |
| 29 | `Checkout` | `Process` | `public static` | Memproses checkout: menghitung total, menerapkan voucher, membuat struk, konfirmasi pembayaran, lalu mencatat transaksi. |
| 30 | `Transaction` | `addTransaction` | `public static` | Menyimpan satu record transaksi (tanggal, ID trx, user, produk, qty, diskon, cost, retail, total) ke array. |
| 31 | `Transaction` | `getIndexTrxById` | `public static` | Mencari index transaksi berdasarkan ID; mengembalikan `Constant.KOSONG` (-1) jika tidak ditemukan. |
| 32 | `DataFile` | `LoadAllData` | `public static` | Memuat seluruh data (users, products, transaction, cart) dari file ke array; memanggil `seedData` untuk user/product jika file belum ada. |
| 33 | `DataFile` | `SaveAllData` | `public static` | Menyimpan seluruh data (users, products, transaction, cart) dari array ke file. |
| 34 | `DataFile` | `pararelToLine` | `public static` | Menggabungkan nilai kolom-kolom pada index tertentu menjadi satu baris string yang dipisahkan koma. |
| 35 | `DataFile` | `lineToPararel` | `public static` | Memecah satu baris string (dipisahkan koma) menjadi nilai-nilai kolom pada index tertentu. |
| 36 | `DataFile` | `saveData` | `public static` | Menulis data dari array kolom (sebanyak `count`) ke dalam file dalam bentuk baris terpisah koma. |
| 37 | `DataFile` | `loadData` | `public static` | Membaca baris-baris dari file dan mengisinya ke array kolom; mengembalikan jumlah baris yang berhasil dimuat. |

## Ringkasan Jumlah Fungsi per Class

| Class | Jumlah Fungsi |
|-------|---------------|
| `Program` | 3 |
| `Users` | 7 |
| `Products` | 8 |
| `Catalog` | 4 |
| `Cart` | 5 |
| `Checkout` | 2 |
| `Transaction` | 2 |
| `DataFile` | 6 |
| `Constant` | 0 (hanya konstanta) |
| `Report` | 0 (belum ada fungsi) |

**Total: 37 fungsi**

> Catatan:
> - Class `Report` masih kosong, sehingga belum ada fungsi yang terdaftar di dalamnya.
> - Class `Constant` hanya berisi konstanta (mis. `KOSONG`, path file, direktori data), bukan fungsi.
> - Fungsi `Users.daftarUser` yang terdokumentasi sebelumnya sudah tidak ada di dalam kode.