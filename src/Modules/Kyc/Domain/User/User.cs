using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.User.DomainEvents;
using CXChange.Modules.Kyc.Domain.User.Enums;
using CXChange.Modules.Kyc.Domain.User.Specifications;
using CXChange.Modules.Kyc.Domain.User.ValueObjects;

namespace CXChange.Modules.Kyc.Domain.User
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        private User() { }
        private User(Guid id)
        {
            Id = id;
        }


        private FullName FullName;
        private NationalId NationalId;
        private DateOnly BirthDate;
        private int Version;
        private KycStatus KycStatus;
        private LandLineStatus LandLineStatus;
        private DateTimeOffset CreatedAt;
        private string? LandLine;
        private KycRequest UserKycRequest;

        public static User CreateUser(Guid id)
        {
            if (id == Guid.Empty) { throw new ArgumentNullException("id"); }
            return new User(id);

        }

        private void PreCheckKycInitialization(bool isNationalIdUnique)
        {
            CheckRule(new UsersKycShouldNotBeVerifiedOnKycInitialization(KycStatus));
            CheckRule(new NationalIdShouldBeUnique(isNationalIdUnique));
            CheckRule(new UserKycShouldBePossibleOnKycInitialazation(KycStatus));
            CheckRule(new UserKycShouldNotBeInProgressOnKycInitialization(KycStatus));

        }
        private void ChangeKycStateToInProgress()
        {
            KycStatus = KycStatus.InProgress;
        }
        private void PreCheckKycRequestApproval()
        {
            CheckRule(new KycShouldBeInProgressStateOnKycApprove(KycStatus));
            CheckRule(new NationalIdShouldBeUnique(true));
        }
        public void ApproveKyc()
        {
            PreCheckKycRequestApproval();
            KycStatus = KycStatus.Verified;
            FullName = UserKycRequest.FullName;
            BirthDate = UserKycRequest.BirthDate;
            NationalId = UserKycRequest.NationalId;
            UserKycRequest = UserKycRequest with { Status = KycRequestStatus.Stale };
            AddDomainEvent(new KycApprovedDomainEvent(this));

        }
        public void RejectKyc()
        {
            CheckRule(new KycStatusShouldBeInProgressOnRejection(KycStatus));
            KycStatus = KycStatus.Rejected;
            UserKycRequest = UserKycRequest with { Status = KycRequestStatus.Stale };
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
        public void CreateKycRequest(string name, string surName, string nationalId, DateOnly birthday)
        {
            PreCheckKycInitialization(true);
            UserKycRequest = ValueObjects.KycRequest.CreateKycRequest(name, surName, nationalId, birthday);
            ChangeKycStateToInProgress();

        }
        public void SetKycRequestPhotoIdAndStore(string id, StorageTypeEnum storageType)
        {
            this.UserKycRequest = UserKycRequest with { KycPhotoId = id, StorageType = storageType };
        }
        public string GetKycPhotoId()
        {
            return UserKycRequest.KycPhotoId;
        }
        public string GetKycRequestNationalId()
        {
            return UserKycRequest.NationalId.NationalNumber;
        }


    }
}

