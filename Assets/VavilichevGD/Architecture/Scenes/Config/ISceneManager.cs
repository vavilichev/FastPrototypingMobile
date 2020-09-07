﻿using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;
using UnityEngine.Events;

namespace VavilichevGD.Architecture.Scenes {
    public delegate void SceneManagerHandler(ISceneConfig config);
    
    public interface ISceneManager {

        #region DELEGATES

        event SceneManagerHandler OnSceneLoadStartedEvent;
        event SceneManagerHandler OnSceneLoadCompletedEvent;

        #endregion

        IScene sceneActual { get; }
        Dictionary<string, ISceneConfig> scenesConfigMap { get; }

        Coroutine LoadScene(string sceneName, UnityAction<ISceneConfig> sceneLoadedCallback = null);
        Coroutine InitializeCurrentScene(UnityAction<ISceneConfig> sceneLoadedCallback = null);
    }
}