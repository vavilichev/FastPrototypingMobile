namespace VavilichevGD.Architecture.Storage {
    public interface IRepoDataAdapter {
        RepoData Adapt(RepoData oldRepoData);
    }
}