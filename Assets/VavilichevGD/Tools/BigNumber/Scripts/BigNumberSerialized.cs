namespace VavilichevGD.Tools.Numerics {
    public class BigNumberSerialized {
        public string fullBigNumberString;

        public BigNumber value {
            get => new BigNumber(this.fullBigNumberString);
            set => this.fullBigNumberString = value.ToString();
        }
    }
}