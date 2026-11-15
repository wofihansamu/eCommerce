# Daftar Data (Kolom) Aplikasi ECommerce Console

Dokumentasi seluruh kolom data (field) yang disimpan di dalam program, beserta tipe datanya dan class/entity pemiliknya.

## 1. Entity `Users`

Penyimpanan data user (array paralel dengan kapasitas `MAX_USER_COUNT = 100`).

| No | Nama Kolom | Tipe Data | Keterangan |
|----|-----------|-----------|------------|
| 1 | `userId` | `int[]` | ID unik user. |
| 2 | `userName` | `string[]` | Nama / username user. |
| 3 | `userPassword` | `string[]` | Password user (plain text). |
| 4 | `role` | `string[]` | Peran user (`admin` atau `customer`). |
| 5 | `userCount` | `int` | Jumlah user yang tersimpan saat ini. |

**Tambahan (session):**

| Nama Kolom | Tipe Data | Keterangan |
|-----------|-----------|------------|
| `sessionRole` | `string` | Role user yang sedang login. |
| `sessionUserId` | `int` | ID user yang sedang login (default `Constant.KOSONG`). |

## 2. Entity `Products`

Penyimpanan data produk (array paralel dengan kapasitas `MAX_PRODUCT_COUNT = 100`).

| No | Nama Kolom | Tipe Data | Keterangan |
|----|-----------|-----------|------------|
| 1 | `productId` | `int[]` | ID unik produk (auto-increment). |
| 2 | `name` | `string[]` | Nama produk. |
| 3 | `price` | `double[]` | Harga jual produk. |
| 4 | `cost` | `double[]` | Harga modal / biaya produk. |
| 5 | `category` | `string[]` | Kategori produk. |
| 6 | `stock` | `int[]` | Jumlah stok produk. |
| 7 | `productCount` | `int` | Jumlah produk yang tersimpan saat ini. |

## 3. Entity `Cart`

Penyimpanan data keranjang belanja (array paralel dengan kapasitas `MAX_CART_COUNT = 100`).

| No | Nama Kolom | Tipe Data | Keterangan |
|----|-----------|-----------|------------|
| 1 | `userId` | `int[]` | ID user pemilik item di keranjang. |
| 2 | `productId` | `int[]` | ID produk yang dimasukkan ke keranjang. |
| 3 | `productQty` | `double[]` | Jumlah (qty) produk di keranjang. |
| 4 | `cartCount` | `int` | Jumlah item yang tersimpan di keranjang. |

## 4. Entity `Transaction`

Penyimpanan data transaksi (array paralel dengan kapasitas `MAX_TRX_COUNT = 100`).

| No | Nama Kolom | Tipe Data | Keterangan |
|----|-----------|-----------|------------|
| 1 | `date` | `string[]` | Tanggal & waktu transaksi (format `DateTime.ToString()`). |
| 2 | `transactionId` | `int[]` | ID unik transaksi. |
| 3 | `userId` | `int[]` | ID user yang melakukan transaksi. |
| 4 | `productId` | `int[]` | ID produk yang dibeli. |
| 5 | `quantity` | `int[]` | Jumlah produk yang dibeli. |
| 6 | `discount` | `double[]` | Nilai diskon per produk. |
| 7 | `cost` | `double[]` | Harga modal produk saat transaksi. |
| 8 | `retail` | `double[]` | Harga jual produk saat transaksi. |
| 9 | `totalPrice` | `double[]` | Total harga item (qty × retail). |
| 10 | `transactionCount` | `int` | Jumlah record transaksi yang tersimpan saat ini. |

## 5. Class `Constant`

Konstanta global (bukan data entity, tetapi nilai tetap yang dipakai program).

| No | Nama | Tipe Data | Keterangan |
|----|------|-----------|------------|
| 1 | `KOSONG` | `const int` | Nilai sentinel/petanda kosong (`-1`). |
| 2 | `DEFAULT_MAXIMUN_ROWS_DATA` | `const int` | Batas maksimum baris data default (`100`). |
| 3 | `FILE_USERS` | `const string` | Path file data user (`./data/users.txt`). |
| 4 | `FILE_PRODUCTS` | `const string` | Path file data produk (`./data/products.txt`). |
| 5 | `FILE_TRANSACTION` | `const string` | Path file data transaksi (`./data/transaction.txt`). |
| 6 | `FILE_CART` | `const string` | Path file data keranjang (`./data/cart.txt`). |
| 7 | `DIR_DATA` | `const string` | Direktori data (`./data`). |

## 6. Class `Program` & Lainnya

| Class | Nama Kolom | Tipe Data | Keterangan |
|-------|-----------|-----------|------------|
| `Program` | `lines35` | `string` | String pembatas garis (35 karakter `=`) untuk tampilan menu. |

## Ringkasan Jumlah Kolom per Entity

| Entity / Class | Jumlah Kolom Data |
|----------------|-------------------|
| `Users` | 7 (5 array + 2 session) |
| `Products` | 7 |
| `Cart` | 4 |
| `Transaction` | 10 |
| `Constant` | 7 (konstanta) |
| `Program` | 1 |

> Catatan:
> - Setiap entity (`Users`, `Products`, `Cart`, `Transaction`) menyimpan data dengan pola **array paralel**, di mana index yang sama pada setiap array merepresentasikan satu record/baris data.
> - Field `*Count` (`userCount`, `productCount`, `cartCount`, `transactionCount`) adalah penghitung jumlah record, bukan kolom data per record.
> - Data-data tersebut dipersistensikan ke file teks (`.txt`) di direktori `./data` melalui class `DataFile`, dengan format satu record per baris yang dipisahkan koma.