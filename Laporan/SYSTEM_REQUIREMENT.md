# Laporan Pemenuhan System Requirement

Dokumen ini memeriksa apakah setiap **requirement teknis** yang diwajibkan sudah diimplementasikan di dalam proyek **eCommerce Console**, beserta lokasi (class/method) implementasinya. Requirement yang belum diimplementasikan ditandai **"Belum"** beserta penjelasan singkatnya.

## Ringkasan Status

| No | Requirement Teknis | Status | Lokasi Implementasi |
|----|--------------------|--------|---------------------|
| 1 | Three parallel arrays | ✅ Sudah | `Users`, `Products`, `Cart`, `Transaction` |
| 2 | One jagged array | ❌ Belum | — |
| 3 | One two-dimensional array | ❌ Belum | — |
| 4 | Linear search | ✅ Sudah | `Users.getIndexUserByName`, `Products.getIndexProductByID`, dst. |
| 5 | Binary search | ✅ Sudah | `Products.BinSearch` (`Products.cs:44-66`) |
| 6 | One hand-written sort | ✅ Sudah | `Catalog.menuProduct` (bubble sort) |
| 7 | Two text files | ✅ Sudah | `Constant.cs` (4 file `.txt`) |
| 8 | Twelve or more methods | ✅ Sudah | 37 method (lihat `FUNGSI.md`) |
| 9 | Three exception types | ✅ Sudah | `DataFile.cs`, `Products.cs` (7+ jenis exception) |

**Total: 8 dari 9 requirement sudah terpenuhi.**

---

## 1. Three Parallel Arrays — ✅ Sudah

Array paralel (beberapa array yang indeksnya merepresentasikan record yang sama) diimplementasikan pada setiap entity.

| Class | File | Array Paralel |
|-------|------|---------------|
| `Users` | `Users.cs:5-13` | `userId[]`, `userName[]`, `userPassword[]`, `role[]` |
| `Products` | `Products.cs:6-13` | `productId[]`, `name[]`, `price[]`, `cost[]`, `category[]`, `stock[]` |
| `Cart` | `Cart.cs:5-8` | `userId[]`, `productId[]`, `productQty[]` |
| `Transaction` | `Transaction.cs:4-14` | `date[]`, `transactionId[]`, `userId[]`, `productId[]`, `quantity[]`, `discount[]`, `cost[]`, `retail[]`, `totalPrice[]` |

> Indeks yang sama pada tiap array mewakili satu record. Pola ini juga dieksploitasi oleh `DataFile` (melalui `Array[] kolom*`) untuk membaca/menulis record ke file teks.

---

## 2. One Jagged Array — ❌ Belum

Belum ada deklarasi array **jagged** (`int[][]`, `string[][]`, dsb.) di seluruh kode sumber.

> Catatan: `DataFile.cs:4-37` mendeklarasikan `Array[] kolomUser`, `kolomProduct`, dll. Namun `Array[]` (array satu dimensi bertipe base class `System.Array`) **bukan** jagged array. Jagged array secara teknis adalah array-of-array yang tiap elemennya berupa array (mis. `int[][]`).

---

## 3. One Two-Dimensional Array — ❌ Belum

Belum ada deklarasi array **dua dimensi persegi panjang** (`int[,]`, `string[,]`, dsb.) di seluruh kode sumber.

---

## 4. Linear Search — ✅ Sudah

Pencarian sekuensial (linear) diimplementasikan pada beberapa method berikut:

| Method | File & Baris | Keterangan |
|--------|--------------|------------|
| `Users.getIndexUserByName` | `Users.cs:34-48` | Mencari user berdasarkan nama. |
| `Products.getIndexProductByID` | `Products.cs:43-57` | Mencari produk berdasarkan ID. |
| `Transaction.getIndexTrxById` | `Transaction.cs:32-46` | Mencari transaksi berdasarkan ID. |
| `Cart.AddToCart` | `Cart.cs:9-27` | Menelusuri item keranjang untuk cek produk sudah ada. |
| `Catalog.byCategory` | `Catalog.cs:37-55` | Menelusuri kategori unik secara sekuensial. |
| `Catalog.search` | `Catalog.cs:178-186` | Mencari produk berdasarkan kata kunci. |

---

## 5. Binary Search — ✅ Sudah

Pencarian biner diimplementasikan pada method `Products.BinSearch` dan dipakai oleh `Products.getIndexProductByID`.

| Method | File & Baris | Keterangan |
|--------|--------------|------------|
| `Products.BinSearch` | `Products.cs:44-66` | Binary search pada array terurut `productId`, mengembalikan indeks atau `Constant.KOSONG` (-1). |
| `Products.getIndexProductByID` | `Products.cs:68-83` | Memanggil `BinSearch(productId, id, productCount)` untuk mencari produk berdasarkan ID. |

---

## 6. One Hand-Written Sort — ✅ Sudah

Sorting yang ditulis tangan (bukan memakai `Array.Sort`) berupa **bubble sort** diimplementasikan pada:

| Method | File & Baris | Algoritma |
|--------|--------------|-----------|
| `Catalog.menuProduct` | `Catalog.cs:265-283` | Bubble sort, mengurutkan indeks produk berdasarkan nama. |

---

## 7. Two Text Files — ✅ Sudah

Program membaca dan menulis **4 file teks** (lebih dari cukup memenuhi minimal 2 file). Path file dideklarasikan di `Constant.cs:5-8`.

| No | Konstanta | Path File |
|----|-----------|-----------|
| 1 | `Constant.FILE_USERS` | `./data/users.txt` |
| 2 | `Constant.FILE_PRODUCTS` | `./data/products.txt` |
| 3 | `Constant.FILE_TRANSACTION` | `./data/transaction.txt` |
| 4 | `Constant.FILE_CART` | `./data/cart.txt` |

File-file tersebut dibaca via `DataFile.loadData` dan ditulis via `DataFile.saveData`.

---

## 8. Twelve or More Methods — ✅ Sudah

Terpenuhi. Total terdapat **37 method** di seluruh class (lihat `FUNGSI.md` untuk rincian lengkap).

| Class | Jumlah Method |
|-------|---------------|
| `Program` | 3 |
| `Users` | 7 |
| `Products` | 8 |
| `Catalog` | 4 |
| `Cart` | 5 |
| `Checkout` | 2 |
| `Transaction` | 2 |
| `DataFile` | 6 |
| **Total** | **37** |

---

## 9. Three Exception Types — ✅ Sudah

Penanganan **exception** sudah diimplementasikan menggunakan `try/catch` dengan lebih dari tiga jenis exception yang berbeda, tersebar di `DataFile.cs` dan `Products.cs`.

### Jenis Exception yang Ditangani

| No | Jenis Exception | Lokasi | Keterangan |
|----|-----------------|--------|------------|
| 1 | `DirectoryNotFoundException` | `DataFile.cs:120` | Folder tujuan tidak ditemukan (saveData). |
| 2 | `UnauthorizedAccessException` | `DataFile.cs:126, 187` | Tidak ada izin baca/tulis file atau file read-only. |
| 3 | `IOException` | `DataFile.cs:132, 180` | Gagal baca/tulis file (file sedang dipakai, disk penuh). |
| 4 | `IndexOutOfRangeException` | `DataFile.cs:138, 194` | Nilai count melebihi kapasitas array. |
| 5 | `ArgumentException` | `DataFile.cs:144` | Nama/path file tidak valid. |
| 6 | `FormatException` | `Products.cs:87, 110` | Input ID produk bukan angka bulat. |
| 7 | `OverflowException` | `Products.cs:92, 115` | Angka ID terlalu besar/kecil. |
| 8 | `Exception` | `DataFile.cs:150, 201`, `Products.cs:97, 120` | Catch-all sebagai pengaman terakhir. |

> Requirement "three exception types" **terpenuhi**, bahkan melebihi — total terdapat **7 jenis exception spesifik** (belum termasuk `Exception` sebagai catch-all) yang ditangani secara eksplisit.

---

## Kesimpulan

Dari **9 requirement teknis** yang diwajibkan, **8 requirement sudah terpenuhi** dan **1 requirement belum terpenuhi**: jagged array dan two-dimensional array.

| Status | Requirement |
|--------|-------------|
| ✅ Sudah | Parallel arrays, linear search, binary search, hand-written sort, text files, 12+ methods, exception handling |
| ❌ Belum | Jagged array, two-dimensional array |