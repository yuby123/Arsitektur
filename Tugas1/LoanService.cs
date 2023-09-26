using System;
namespace Tugas1;

public class LoanService
{
    private List<Loan> loans;
    private BookService bookService;
    private int nextLoanId;

    public LoanService(BookService bookService)
    {
        loans = new List<Loan>();
        this.bookService = bookService;
        nextLoanId = 1;
    }

    public void BorrowBook(string memberNum, string bookISBN)
    {

        // Cek apakah buku sudah dipinjam oleh anggota lain
        if (IsBookAvailable(bookISBN))
        {
            // Cek apakah anggota sudah meminjam buku ini sebelumnya
            if (!IsBookAlreadyBorrowed(memberNum, bookISBN))
            {
                Loan newLoan = new Loan
                {
                    Id = nextLoanId,
                    MemberNumber = memberNum,
                    ISBN = bookISBN,
                    BorrowDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(14) // Contoh: batas waktu pengembalian adalah 14 hari dari tanggal peminjaman
                };
                loans.Add(newLoan);
                Console.WriteLine("Buku berhasil dipinjam.");

                // Tingkatkan nilai nextLoanId agar sesuai dengan ID berikutnya
                nextLoanId++;
            }
            else
            {
                Console.WriteLine("Anggota sudah meminjam buku ini sebelumnya.");
            }
        }
        else
        {
            Console.WriteLine("Buku tidak tersedia.");
        }
    }

    public void ReturnBook(string memberNum, string bookISBN)
    {
        // Cek apakah anggota memiliki peminjaman buku ini
        var loan = loans.FirstOrDefault(l => l.MemberNumber == memberNum && l.ISBN == bookISBN);
        if (loan != null)
        {
            // Hapus peminjaman buku dari daftar peminjaman
            loans.Remove(loan);
            Console.WriteLine("Buku berhasil dikembalikan.");
        }
        else
        {
            Console.WriteLine("Anggota tidak memiliki peminjaman buku ini.");
        }
    }

    public List<Loan> GetMemberLoans(string memberNum)
    {
        return loans.FindAll(l => l.MemberNumber == memberNum);
    }

    private bool IsBookAvailable(string bookISBN)
    {
        // Cek apakah buku dengan ID yang diminta tersedia dalam daftar buku
        var book = bookService.GetBook(bookISBN);

        // Lakukan validasi tambahan apakah buku ditemukan atau tidak
        if (book != null)
        {
            // Buku ditemukan, maka kembalikan true
            return true;
        }
        else
        {
            // Buku tidak ditemukan, kembalikan false
            return false;
        }
    }

    private bool IsBookAlreadyBorrowed(string memberNum, string bookISBN)
    {
        // Periksa apakah anggota sudah meminjam buku ini sebelumnya
        return loans.Any(l => l.MemberNumber == memberNum && l.ISBN == bookISBN);
    }

    public void ViewLoanStatus()
    {
        Console.Write("Member Number");
        string memberNum = Console.ReadLine();

        List<Loan> memberLoans = GetMemberLoans(memberNum);

        if (memberLoans.Count > 0)
        {
            Console.WriteLine($"Status Peminjaman untuk Anggota dengan Member Number {memberNum}:");
            foreach (var loan in memberLoans)
            {
                Book book = bookService.GetBook(loan.ISBN);
                Console.WriteLine($"Buku: {book.Title}, Pengarang: {book.Author}, Tanggal Peminjaman: {loan.BorrowDate}, Tanggal Pengembalian: {loan.ReturnDate}");
            }
        }
        else
        {
            Console.WriteLine("Anggota tidak memiliki peminjaman buku.");
        }
    }


    public void EditLoanStatus(int loanId, DateTime newReturnDate)
    {
        // Cari peminjaman berdasarkan ID peminjaman
        var loan = loans.FirstOrDefault(l => l.Id == loanId);

        if (loan != null)
        {
            // Ubah tanggal pengembalian
            loan.ReturnDate = newReturnDate;
            Console.WriteLine("Status peminjaman berhasil diubah.");
        }
        else
        {
            Console.WriteLine("Peminjaman tidak ditemukan.");
        }
    }

    public void DeleteLoan(int loanId)
    {
        // Cari peminjaman berdasarkan ID peminjaman
        var loan = loans.FirstOrDefault(l => l.Id == loanId);

        if (loan != null)
        {
            // Hapus peminjaman buku dari daftar peminjaman
            loans.Remove(loan);
            Console.WriteLine("Peminjaman berhasil dihapus.");
        }
        else
        {
            Console.WriteLine("Peminjaman tidak ditemukan.");
        }
    }

    public void ViewAllLoans()
    {
        Console.WriteLine("Daftar Seluruh Peminjaman:");

        foreach (var loan in loans)
        {
            Book book = bookService.GetBook(loan.ISBN);
            Console.WriteLine($"ID Peminjaman: {loan.Id}" +
                $"\nAnggota: {loan.MemberNumber}" +
                $"\nBuku: {book.Title}" +
                $"\nPengarang: {book.Author}" +
                $"\nTanggal Peminjaman: {loan.BorrowDate}" +
                $"\nTanggal Pengembalian: {loan.ReturnDate}");
        }
    }
}
