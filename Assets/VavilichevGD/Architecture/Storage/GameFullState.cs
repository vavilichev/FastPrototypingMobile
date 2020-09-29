//using System;
//using System.Collections;
//using UnityEngine;
//using VavilichevGD.Tools;
//
//namespace VavilichevGD.Architecture.Storage {
//    public class GameFullState {
//
//        #region DELEGATES
//
//        public delegate void GameDataBaseHandler();
//        public event GameDataBaseHandler OnGameDataBaseLoadedEvent;
//        public event GameDataBaseHandler OnGameDataBaseSavedEvent;
//
//        #endregion
//
//        private IRepository[] allRepositories;
//
//        public GameFullState(IRepository[] allRepositories) {
//            this.allRepositories = allRepositories;
//        }
//
//        #region LOAD
//
//        public void Load() {
//            foreach (var storage in this.storages)
//                storage.Load();
//            this.OnGameDataBaseLoadedEvent?.Invoke();
//        }
//
//        public Coroutine LoadAsync() {
//            return Coroutines.StartRoutine(this.LoadAsyncRoutine());
//        }
//
//        private IEnumerator LoadAsyncRoutine() {
//            foreach (var storage in this.storages)
//                yield return storage.LoadAsync();
//            this.OnGameDataBaseLoadedEvent?.Invoke();
//        }
//
//        #endregion
//
//
//        #region SAVE
//
//        public void Save() {
//            foreach (var storage in this.storages)
//                storage.Save();
//            this.OnGameDataBaseSavedEvent?.Invoke();
//        }
//
//        public Coroutine SaveAsync() {
//            return Coroutines.StartRoutine(this.SaveAsyncRoutine());
//        }
//
//        private IEnumerator SaveAsyncRoutine() {
//            foreach (var storage in this.storages)
//                yield return storage.SaveAsync();
//            this.OnGameDataBaseSavedEvent?.Invoke();
//        }
//
//        #endregion
//
//        public RepoData GetRepoData(string id) {
//            foreach (var storage in this.storages) {
//                var repoData = storage.GetRepoData(id);
//                if (repoData != null)
//                    return repoData;
//            }
//            
//            throw new Exception($"There is no repoData with id: {id}");
//        }
//
//        public void SetRepoData(string id, RepoData repoData) {
//            foreach (var storage in this.storages)
//                storage.SetRepoData(id, repoData);
//        }
//    }
//}