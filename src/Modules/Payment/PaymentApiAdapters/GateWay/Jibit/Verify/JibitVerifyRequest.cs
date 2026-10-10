namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Verify
{
    internal record JibitVerifyRequest
    {
        public JibitVerifyRequest(string purchaseId, string cardHash, string cardNubmer)
        {
            CheckBankCard(cardNubmer, cardHash);
            this.purchaseId = purchaseId;
            this.cardHash = cardHash;
            this.cardNumber = cardNubmer;
        }
        public void CheckBankCard(string cardNumber, string cardHash)
        {
            if (!cardNumber.ComputeSha256Hash().Equals(cardHash, StringComparison.OrdinalIgnoreCase))
            {
                //throw new GatewayException(GatewatPurchaseError.CardDoesNotMatch, "the card paid with is not same as the card choosen", purchaseId);
            }
        }
        public string purchaseId { get; init; }
        public string cardHash { get; init; }
        public string cardNumber { get; init; }

    }
}
