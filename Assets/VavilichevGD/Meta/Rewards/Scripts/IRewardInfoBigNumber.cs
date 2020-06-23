using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Meta {
    public interface IRewardInfoBigNumber {
        BigNumber GetValue();
        string GetValueToString();
    }
}