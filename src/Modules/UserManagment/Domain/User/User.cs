using CXChange.CoreElements.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Domain.User
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        public User(string FirstName=null, string LastName=null, string Email=null,bool PhoneNumberConfirmed=false, bool TwoFactorEnabled = false, string DefaultTwoFactor="SMS", string PhoneNumber=null)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            
            this.PhoneNumberConfirmed = PhoneNumberConfirmed;
            this.TwoFactorEnabled = TwoFactorEnabled;
            this.DefaultTwoFactor = DefaultTwoFactor;
            this.PhoneNumber = PhoneNumber;

        }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Email { get; set; }
        private string SecurityStamp { get; set; }
        private string PhoneNumber { get; set; }
        private string Password { get; set; }
        private bool PhoneNumberConfirmed { get; set; }
        private bool TwoFactorEnabled { get; set; }
        private string DefaultTwoFactor { get; set; }
        private string UserName { get; set; }
        public SnapShot GetSnapShot()
        {
            return new SnapShot
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                SecurityStamp = this.SecurityStamp,
                PhoneNumber = this.PhoneNumber,
                Password = this.Password,
                PhoneNumberConfirmed = this.PhoneNumberConfirmed,
                TwoFactorEnabled = this.TwoFactorEnabled,
                DefaultTwoFactor = this.DefaultTwoFactor,
                UserName = this.UserName

            };

        }
        

    }
}
