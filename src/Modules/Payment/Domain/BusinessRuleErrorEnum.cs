namespace CXChange.Modules.Payment.Domain
{
    internal enum BusinessRuleErrorEnum
    {
        UserCantHaveMoreThanFourCards = -9999,
        IRTConfirmationUsedForCrypto,
        IRTDepositShouldBeInProgressState,
        UserDoesNotHaveDepositPermissions,
        IRTDepositNotInProgressOnRejection,
        WithdrawShouldBeConfirmedByUserOnSend,
        WithdrawShouldBeSentOnComplete,
        UserDoesNotHaveWithdrawPermission
    }
}
