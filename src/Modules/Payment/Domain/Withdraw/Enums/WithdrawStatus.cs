namespace CXChange.Modules.Payment.Domain.Withdraw.Enums
{
    public enum WithdrawStatus
    {
        ConfirmedByUser,//after user has confirmed withdraw with two factor 
        NeedsManualInterversion,// depending on some settings it should wait for admin approval after successfull submission
        Auditing,//in the futrue we may need to autdit withdraw before sending
        Canceled,//this is for cancelling the withdraw if the user decides to cancel
        SendFailed,//when sending withdraw it failed
        Sent,//when the withdraw has been sent
        InquiryFailed,//when inquiring withraw to make it complete ,the withdraw was failed
        Completed,//after sending withdraw we should inquriy it and if it is reached to the destincation we will make status as completed
        Failed//in rare cases we should have this and only admin has the right to change the status to failed
    }
}
