using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tugas1
{
    public class MemberService
    {
        private List<Member> members = new List<Member>();

        public void CreateMember()
        {
            Console.WriteLine("== Tambah Anggota ==\n");

            string name = GetValidatedInput("Masukkan Nama: ", @"^[a-zA-Z\s]+$", "Nama tidak valid. Nama hanya boleh berisi huruf dan spasi.", "name");
            string memberNumber = GetValidatedInput("Masukkan Nomor Keanggotaan: ", @"^\d{8,15}$", "Nomor Keanggotaan tidak valid. Hanya angka dengan panjang 8-15 karakter diterima.", "memberNumber");
            string email = GetValidatedInput("Masukkan Email: ", @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$", "Email tidak valid. Pastikan format email yang benar.", "email");

            var member = new Member(name, memberNumber, email);
            members.Add(member);
            Console.WriteLine("\nAnggota berhasil ditambahkan.");
            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();

        }

        public void EditMember()
        {
            Console.WriteLine("== Edit Anggota ==\n");

            string memberNumber = GetValidatedInput("Masukkan Nomor Keanggotaan anggota yang akan diubah: ", @"^\d{4,8}$", "Nomor Keanggotaan tidak valid. Hanya angka dengan panjang 8-15 karakter diterima.");

            var member = members.Find(m => m.MemberNumber == memberNumber);
            if (member != null)
            {
                string newName = GetValidatedInput($"Masukkan Nama baru untuk anggota {member.Name}: ", @"^[a-zA-Z\s]+$", "Nama tidak valid. Nama hanya boleh berisi huruf dan spasi.");
                string newEmail = GetValidatedInput($"Masukkan Email baru untuk anggota {member.Name}: ", @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$", "Email tidak valid. Pastikan format email yang benar.");

                member.Name = newName;
                member.Email = newEmail;
                Console.WriteLine("\nAnggota berhasil diubah.");
            }
            else
            {
                Console.WriteLine("Anggota dengan Nomor Keanggotaan yang dimaksud tidak ditemukan.");
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();
        }

        public void DeleteMember()
        {
            Console.WriteLine("== Hapus Anggota ==\n");

            string memberNumber = GetValidatedInput("Masukkan Nomor Keanggotaan anggota yang akan dihapus: ", @"^\d{8,15}$", "Nomor Keanggotaan tidak valid. Hanya angka dengan panjang 8-15 karakter diterima.");

            var member = members.Find(m => m.MemberNumber == memberNumber);
            if (member != null)
            {
                members.Remove(member);
                Console.WriteLine("\nAnggota berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Anggota dengan Nomor Keanggotaan yang dimaksud tidak ditemukan.");
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();
        }

        public void ShowMembers()
        {
            Console.WriteLine("== Daftar Anggota ==\n");

            foreach (var member in members)
            {
                Console.WriteLine($"Nama: {member.Name}");
                Console.WriteLine($"Nomor Keanggotaan: {member.MemberNumber}");
                Console.WriteLine($"Email: {member.Email}");
                Console.WriteLine();
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu.");
            Console.ReadLine();
        }

        private bool IsDuplicateContact(string name = "", string memberNumber = "", string email = "", string ignoreName = "")
        {
            foreach (var membersi in members)
            {
                if ((membersi.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !membersi.Name.Equals(ignoreName, StringComparison.OrdinalIgnoreCase)) ||
                    membersi.MemberNumber.ToString().Equals(memberNumber, StringComparison.OrdinalIgnoreCase) ||
                    membersi.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private string GetValidatedInput(string prompt, string regexPattern, string invalidMessage, string type = "", string ignoreName = "")
        {
            Console.Write(prompt);
            var inputValue = Console.ReadLine();

            while (!Regex.IsMatch(inputValue, regexPattern) || IsDuplicateContact(name: type == "name" ? inputValue : "",
                                                                                  memberNumber: type == "memberNumber" ? inputValue : "",
                                                                                  email: type == "email" ? inputValue : "",
                                                                                  ignoreName: ignoreName))
            {
                string errorMessage = Regex.IsMatch(inputValue, regexPattern) ? $"{type} telah digunakan!" : invalidMessage;
                Console.WriteLine(errorMessage);
                Console.Write(prompt);
                inputValue = Console.ReadLine();
            }

            return inputValue;
        }
    }


}
