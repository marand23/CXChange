using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.User.DomainEvents;
using CXChange.Modules.Kyc.Domain.User.Enums;
using CXChange.Modules.Kyc.Domain.User.Specifications;
using CXChange.Modules.Kyc.Domain.User.ValueObjects;

namespace CXChange.Modules.Kyc.Domain.User
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        private User(Guid id)
        {
            Id = id;
        }
        private User(UserSnapShot snapShot)
        {
            this.Id = snapShot.Id;
            FullName = new FullName(snapShot.Name, snapShot.SurName);
            NationalId = new NationalId(snapShot.NationalId);
            BirthDate = snapShot.BirthDay;
            LandLineStatus = snapShot.LandLineStatus;
            KycStatus = snapShot.KycStatus;
            CreatedAt = snapShot.CreatedAt;
            Version = snapShot.Version;
            LandLine = snapShot.LandLine;


        }

        private FullName FullName { get; set; }
        private NationalId NationalId { get; set; }
        private DateOnly BirthDate { get; set; }
        private int Version { get; set; }
        private KycStatus KycStatus { get; set; }
        private LandLineStatus LandLineStatus { get; set; }
        private DateTimeOffset CreatedAt { get; set; }
        private string LandLine { get; set; }
        public static User CreateUser(Guid id)
        {
            if (id == null) { throw new ArgumentNullException("id"); }
            return new User(id);

        }
        public static User CreateFrom(UserSnapShot snapShot)
        {

            return new User(snapShot);
        }
        public UserSnapShot GetSnapshot()
        {

            return new UserSnapShot
            {
                Id = Id,
                Name = FullName?.GetSnapshot().Name,
                NationalId = NationalId?.NationalNumber,
                SurName = FullName?.GetSnapshot().SurName,
                LandLineStatus = LandLineStatus,
                KycStatus = KycStatus,
                Version = Version,
                CreatedAt = CreatedAt,
                BirthDay = BirthDate,
                LandLine = LandLine

            };

        }
        public void PreCheckKycInitialization(bool isNationalIdUnique)
        {
            CheckRule(new UsersKycShouldNotBeVerifiedOnKycInitialization(KycStatus));
            CheckRule(new NationalIdShouldBeUnique(isNationalIdUnique));
            CheckRule(new UserKycShouldBePossibleOnKycInitialazation(KycStatus));
            CheckRule(new UserKycShouldNotBeInProgressOnKycInitialization(KycStatus));

        }
        public void ChangeKycStateToInProgress()
        {
            KycStatus = KycStatus.InProgress;
        }
        public void PreCheckKycRequestApproval(bool isNationalIdUnique)
        {
            CheckRule(new KycShouldBeInProgressStateOnKycApprove(KycStatus));
            CheckRule(new NationalIdShouldBeUnique(isNationalIdUnique));
        }
        public void ApproveKyc(string nationalId, string name, string surname, DateOnly birthDate)
        {
            KycStatus = KycStatus.Verified;
            FullName = new FullName(name, surname);
            BirthDate = birthDate;
            NationalId = new NationalId(nationalId);
            AddDomainEvent(new KycApprovedDomainEvent(this));

        }
        public void RejectKyc()
        {
            CheckRule(new KycStatusShouldBeInProgressOnRejection(KycStatus));
            KycStatus = KycStatus.Rejected;
            AddDomainEvent(new KycRejectedDomainEvent(this));
        }
        public void VerifyLandLine(string landLineNum)
        {
            CheckRule(new LandLineShouldNotBeConfirmedOnLandLineConfirm(LandLineStatus));
            CheckRule(new LandLineCannotBeConfirmedBeforeKycConfirmation(KycStatus));
            LandLine = landLineNum;
            LandLineStatus = LandLineStatus.Verified;
            AddDomainEvent(new LandLineConfirmedDomainEvent(this));
        }

    }
}

