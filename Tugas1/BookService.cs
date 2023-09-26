using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tugas1
{
    public class BookService
    {
        private List<Book> books = new List<Book>();

        public void AddBook()
        {
            Console.Clear();
            Console.WriteLine("== Tambah Buku ==\n");

            string isbn = GetValidatedInput("Masukkan ISBN : ", @"^[\d-]{10,18}$", "ISBN tidak valid. Minimal 13 karakter.");


            if (books.Any(b => b.ISBN == isbn))
            {
                Console.WriteLine("ISBN sudah ada dalam daftar buku. Tidak boleh ada input yang sama.");
                return;
            }

            string title = GetValidatedInput("Masukkan Judul: ", @"^.*$", "Judul tidak valid.", "title");
            string author = GetValidatedInput("Masukkan Pengarang: ", @"^[a-zA-Z\s]+$", "Pengarang tidak valid. Pengarang hanya boleh berisi huruf dan spasi.", "author");

            var book = new Book { ISBN = isbn, Title = title, Author = author };
            books.Add(book);

            Console.WriteLine("\nBuku berhasil ditambahkan.");
            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();
        }

        public void EditBook()
        {
            Console.WriteLine("== Edit Buku ==\n");

            string isbn = GetValidatedInput("Masukkan ISBN buku yang akan diubah: ", @"^[\d-]{10,18}$", "ISBN tidak valid.Minimal 13 karakter");

            var book = books.Find(b => b.ISBN == isbn);
            if (book != null)
            {
                string newTitle = GetValidatedInput($"Masukkan Judul baru untuk buku dengan ISBN {book.ISBN}: ", @"^.*$", "Judul tidak valid.");
                string newAuthor = GetValidatedInput($"Masukkan Pengarang baru untuk buku dengan ISBN {book.ISBN}: ", @"^[a-zA-Z\s]+$", "Pengarang tidak valid. Pengarang hanya boleh berisi huruf dan spasi.");

                book.Title = newTitle;
                book.Author = newAuthor;
                Console.WriteLine("\nBuku berhasil diubah.");
            }
            else
            {
                Console.WriteLine("Buku dengan ISBN yang dimaksud tidak ditemukan.");
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();
        }

        public void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("== Hapus Buku ==\n");

            string isbn = GetValidatedInput("Masukkan ISBN buku yang akan diubah: ", @"^[\d-]{10,18}$", "ISBN tidak valid. Minimal 13 karakter");

            var book = books.Find(b => b.ISBN == isbn);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("\nBuku berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Buku dengan ISBN yang dimaksud tidak ditemukan.");
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();
        }

        public void ShowBooks()
        {
            Console.Clear();
            Console.WriteLine("== Daftar Buku ==\n");

            foreach (var book in books)
            {
                Console.WriteLine($"ISBN: {book.ISBN}");
                Console.WriteLine($"Judul: {book.Title}");
                Console.WriteLine($"Pengarang: {book.Author}");
                Console.WriteLine();
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();
        }

        private string GetValidatedInput(string prompt, string regexPattern, string errorMessage, string type = "")
        {
            string inputValue;

            do
            {
                Console.Write(prompt);
                inputValue = Console.ReadLine();

                if (!Regex.IsMatch(inputValue, regexPattern))
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!Regex.IsMatch(inputValue, regexPattern));

            return inputValue;
        }

        public Book GetBook(string bookISBN)
        {
            return books.FirstOrDefault(book => book.ISBN == bookISBN);
        }
    }
}
