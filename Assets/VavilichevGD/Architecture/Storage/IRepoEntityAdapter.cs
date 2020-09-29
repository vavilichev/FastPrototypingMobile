namespace VavilichevGD.Architecture.Storage {
    public interface IRepoEntityAdapter {
        IRepoEntity AdaptOldVersionAsNew(IRepoEntity oldEntity);
    }
}