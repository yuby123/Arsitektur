using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugas1;
public class Member
{
    public string Name { get; set; }
    public string MemberNumber { get; set; }
    public string Email { get; set; }

    public Member(string name, string memberNumber, string email)
    {
        Name = name;
        MemberNumber = memberNumber;
        Email = email;
    }
}
