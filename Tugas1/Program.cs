using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Tugas1;

class Program
{
    static BookService bookService = new BookService();
    static MemberService memberService = new MemberService();
    static LoanService loanService = new LoanService(bookService);
    static void Main(string[] args)
    {
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Manajemen Buku");
            Console.WriteLine("2. Manajemen Anggota");
            Console.WriteLine("3. Peminjaman Buku");
            Console.WriteLine("4. Keluar");
            Console.Write("Pilih menu (1/2/3/4): ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageBooks(bookService);
                    break;
                case "2":
                    ManageMembers(memberService);
                    break;
                case "3":
                    ManageLoans();
                    break;
                case "4":
                    Console.WriteLine("Terima kasih!");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }

    }

    static void ManageBooks(BookService bookService)
    {
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu Manajemen Buku:");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Edit Buku");
            Console.WriteLine("3. Hapus Buku");
            Console.WriteLine("4. Lihat Daftar Buku");
            Console.WriteLine("5. Kembali ke Menu Utama");
            Console.Write("Pilih submenu (1/2/3/4/5): ");
            var submenuChoice = Console.ReadLine();

            switch (submenuChoice)
            {
                case "1":
                    bookService.AddBook();
                    break;
                case "2":
                    bookService.EditBook();
                    break;
                case "3":
                    bookService.DeleteBook();
                    break;
                case "4":
                    bookService.ShowBooks();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
            Console.ReadLine();
        }
    }

    static void ManageMembers(MemberService memberService)
    {
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu Manajemen Anggota:");
            Console.WriteLine("1. Tambah Anggota");
            Console.WriteLine("2. Edit Anggota");
            Console.WriteLine("3. Hapus Anggota");
            Console.WriteLine("4. Lihat Daftar Anggota");
            Console.WriteLine("5. Kembali ke Menu Utama");
            Console.Write("Pilih submenu (1/2/3/4/5): ");
            var submenuChoice = Console.ReadLine();

            switch (submenuChoice)
            {
                case "1":

                    memberService.CreateMember();
                    break;
                case "2":
                    memberService.EditMember();
                    break;
                case "3":
                    memberService.DeleteMember();
                    break;

                case "4":
                    memberService.ShowMembers();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
            Console.ReadLine();
        }
    }



    static void ManageLoans()
    {
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manajemen Pinjaman Buku");
            Console.WriteLine("---------------------------");
            Console.WriteLine("1. Pinjam Buku");
            Console.WriteLine("2. Kembalikan Buku");
            Console.WriteLine("3. Edit Status Peminjaman");
            Console.WriteLine("4. Hapus Peminjaman");
            Console.WriteLine("5. Lihat Peminjaman");
            Console.WriteLine("6. Status Peminjaman Buku");
            Console.WriteLine("0. Keluar");
            Console.Write("Pilih 0-4 : ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Member Number : ");
                    string memberNumInput = Console.ReadLine();
                    Console.Write("ISBN Buku: ");
                    string bookISBNInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(memberNumInput) && !string.IsNullOrWhiteSpace(bookISBNInput))
                    {
                        loanService.BorrowBook(memberNumInput, bookISBNInput);
                    }
                    else
                    {
                        Console.WriteLine("Member Number atau ISBN Buku tidak valid.");
                    }
                    break;

                case "2":
                    Console.Write("Member Number ");
                    string returnMemberNumInput = Console.ReadLine();
                    Console.Write("ISBN Buku:: ");
                    string returnBookISBNInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(returnMemberNumInput) && !string.IsNullOrWhiteSpace(returnBookISBNInput))
                    {
                        loanService.ReturnBook(returnMemberNumInput, returnBookISBNInput);
                    }
                    else
                    {
                        Console.WriteLine("Member Number atau ISBN Buku tidak valid.");
                    }
                    break;

                case "3":
                    // Edit status peminjaman
                    Console.Write("Masukkan ID Peminjaman yang akan diedit: ");
                    string editLoanIdInput = Console.ReadLine();
                    Console.Write("Masukkan Tanggal Pengembalian Baru (yyyy-MM-dd): ");
                    string newReturnDateInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(editLoanIdInput) && !string.IsNullOrWhiteSpace(newReturnDateInput) &&
                        int.TryParse(editLoanIdInput, out int editLoanId) &&
                        DateTime.TryParseExact(newReturnDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newReturnDate))
                    {
                        loanService.EditLoanStatus(editLoanId, newReturnDate);
                    }
                    else
                    {
                        Console.WriteLine("ID Peminjaman atau Tanggal tidak valid.");
                    }
                    break;

                case "4":
                    Console.Write("Masukkan ID Peminjaman yang akan dihapus: ");
                    string deleteLoanIdInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(deleteLoanIdInput) && int.TryParse(deleteLoanIdInput, out int deleteLoanId))
                    {
                        loanService.DeleteLoan(deleteLoanId);
                    }
                    else
                    {
                        Console.WriteLine("ID Peminjaman tidak valid.");
                    }
                    break;

                case "5":
                    loanService.ViewAllLoans();
                    break;

                case "6":
                    loanService.ViewLoanStatus();
                    break;

                case "0":
                    break;
            }

            Console.ReadLine();
            break;
        }
    }
}


