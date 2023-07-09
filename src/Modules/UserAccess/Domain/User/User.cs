
using CXChange.Modules.UserAccess.Domain.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CXChange.BuildingBlocks.Domain;
namespace CXChange.Modules.UserAccess.Domain.User
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        public User(string PhoneNumber,string Password,bool TwoFactorEnabled,string DefaultTwoFactor)
        {
           this.PhoneNumber = PhoneNumber;
           this.Password = Password;
            this.TwoFactorEnabled = TwoFactorEnabled;
            this.DefaultTwoFactor= DefaultTwoFactor;




        }
      
        private string Email { get; set; }
        private string SecurityStamp { get; set; }
        private string PhoneNumber { get; set; }
        private string Password { get; set; }
        private bool PhoneNumberConfirmed { get; set; }
        private bool TwoFactorEnabled { get; set; }
        private string DefaultTwoFactor { get; set; }
        private string UserName { get; set; }
        private bool IsRestricted { get; set; }
        public SnapShot GetSnapShot()
        {
            return new SnapShot
            {

                Email = this.Email,
                SecurityStamp = this.SecurityStamp,
                PhoneNumber = this.PhoneNumber,
                Password = this.Password,
                PhoneNumberConfirmed = this.PhoneNumberConfirmed,
                TwoFactorEnabled = this.TwoFactorEnabled,
                DefaultTwoFactor = this.DefaultTwoFactor,
                UserName = this.UserName,
                

            };

        }
        public static User CreateUserFactory(string Email,string PhoneNumber,bool PhoneNumberConfirmed,bool TwoFactorEnabled,string DefaultTwoFactor,string UserName,bool isRestricted)
        {
            return new User(PhoneNumber,null,TwoFactorEnabled,DefaultTwoFactor)
            {
                Email = Email,
                UserName = UserName,
                PhoneNumberConfirmed = PhoneNumberConfirmed,
                IsRestricted = isRestricted,


            };
        }
     
        public void CheckRegisterPossiblity()
        {
            if (PhoneNumberConfirmed) { throw new UserRegistrationException($"User {PhoneNumber} Already Exist",phoneNumber:PhoneNumber); }
            if(IsRestricted) { throw new UserRegistrationException($"User {PhoneNumber} Is Restricted"); }

        }
        public void ConfirmPhoneNumber()
        {
            PhoneNumberConfirmed = true;
        }
        

    }
}
