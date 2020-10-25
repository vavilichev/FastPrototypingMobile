using UnityEngine;

namespace VavilichevGD.Architecture {
	public class DummyRepoAdapter : IRepoAdapter {
		
		public T Adapt<T>(Repository repositoryOld, Repository repositoryNew) where T : Repository {
			var newVersion = repositoryNew.version > repositoryOld.version;
			if (newVersion) {
				Debug.Log($"DUMMY REPO ADAPTER: New version of repository ({repositoryNew.GetType()}) is available. Version {repositoryNew.version}");
			}

			return (T) repositoryNew;
		}
	}
}