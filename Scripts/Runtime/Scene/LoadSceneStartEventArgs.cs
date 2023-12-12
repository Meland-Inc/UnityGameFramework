//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
using GameFramework;
using GameFramework.Event;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 加载场景开始事件。
    /// </summary>
    public sealed class LoadSceneStartEventArgs : GameEventArgs
    {
        /// <summary>
        /// 加载场景开始事件编号。
        /// </summary>
        public static readonly int EventId = typeof(LoadSceneStartEventArgs).GetHashCode();

        /// <summary>
        /// 初始化加载场景开始事件的新实例。
        /// </summary>
        public LoadSceneStartEventArgs()
        {
            SceneAssetName = null;
            UserData = null;
        }

        /// <summary>
        /// 获取加载场景开始事件编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        /// <summary>
        /// 获取场景资源名称。
        /// </summary>
        public string SceneAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建加载场景开始事件。
        /// </summary>
        /// <returns>创建的加载场景开始事件。</returns>
        public static LoadSceneStartEventArgs Create(string sceneAssetName, object userData)
        {
            LoadSceneStartEventArgs LoadSceneStartEventArgs = ReferencePool.Acquire<LoadSceneStartEventArgs>();
            LoadSceneStartEventArgs.SceneAssetName = sceneAssetName;
            LoadSceneStartEventArgs.UserData = userData;
            return LoadSceneStartEventArgs;
        }

        /// <summary>
        /// 清理加载场景开始事件。
        /// </summary>
        public override void Clear()
        {
            SceneAssetName = null;
            UserData = null;
        }
    }
}
