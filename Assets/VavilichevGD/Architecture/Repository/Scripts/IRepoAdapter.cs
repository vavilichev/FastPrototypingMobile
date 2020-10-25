namespace VavilichevGD.Architecture {
	public interface IRepoAdapter {
		T Adapt<T>(Repository repositoryOld, Repository repositoryNew) where T : Repository;
	}
}