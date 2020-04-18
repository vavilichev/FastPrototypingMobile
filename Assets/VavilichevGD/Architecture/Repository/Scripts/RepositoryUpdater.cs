namespace VavilichevGD.Architecture {
    public abstract class RepositoryUpdater<T> where T : Repository {
        private Repository repository;

        public RepositoryUpdater(T repository) {
            this.repository = repository;
        }

        public abstract Repository GetUpdatedRepository();
    }
}