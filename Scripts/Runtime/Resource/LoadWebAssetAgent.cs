/*
 * @Author: xiang huan
 * @Date: 2022-06-13 15:38:25
 * @LastEditTime: 2022-07-08 11:18:58
 * @LastEditors: Please set LastEditors
 * @Description: 网络资产请求代理
 * @FilePath: /meland-unity/Assets/Thirdparty/UnityGameFramework/Scripts/Runtime/Resource/LoadWebAssetAgent.cs
 * 
 */
using GameFramework;
using GameFramework.Resource;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Utility = GameFramework.Utility;
namespace UnityGameFramework.Runtime
{
    public class LoadWebAssetAgent : IDisposable
    {
        private UnityWebRequest _unityWebRequest = null;
        private float _lastProgress = 0f;
        private string _unityWebAssetUrl;
        private eLoadWebAssetType _webAssetType;

        public Action<string, float> LoadWebAssetUpdate;
        public Action<string, object> LoadWebAssetComplete;
        public Action<string, string> LoadWebAssetError;
        public void Update()
        {
            if (_unityWebRequest != null)
            {
                if (_unityWebRequest.isDone)
                {
                    if (string.IsNullOrEmpty(_unityWebRequest.error))
                    {
                        UnityWebAssetRequestComplete();
                        Reset();
                    }
                    else
                    {
                        bool isError = _unityWebRequest.result != UnityWebRequest.Result.Success;
                        string errorStr = Utility.Text.Format("Can not load web asset '{0}' with error message '{1}'.", _unityWebAssetUrl, isError ? _unityWebRequest.error : null);
                        LoadWebAssetError?.Invoke(_unityWebAssetUrl, errorStr);
                        Reset();
                    }
                }
                else if (_unityWebRequest.downloadProgress != _lastProgress)
                {
                    _lastProgress = _unityWebRequest.downloadProgress;
                    LoadWebAssetUpdate?.Invoke(_unityWebAssetUrl, _lastProgress);
                }

            }
        }

        public void LoadAsset(string fullPath)
        {
            string fileName = Path.GetFileName(fullPath);
            string ext = Path.GetExtension(fileName);
            _webAssetType = ResourceUtil.GetWebAssetType(ext);
            _unityWebAssetUrl = fullPath;

            switch (_webAssetType)
            {
                case eLoadWebAssetType.Texture:
                    _unityWebRequest = UnityWebRequestTexture.GetTexture(fullPath);
                    break;
                case eLoadWebAssetType.Audio:
                    AudioType audioType = ResourceUtil.GetAudioType(ext);
                    _unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(fullPath, audioType);
                    break;
                case eLoadWebAssetType.Text:
                case eLoadWebAssetType.Byte:
                default:
                    _unityWebRequest = UnityWebRequest.Get(fullPath);
                    break;
            }
            _ = _unityWebRequest.SendWebRequest();
        }

        private void UnityWebAssetRequestComplete()
        {
            object asset;
            switch (_webAssetType)
            {
                case eLoadWebAssetType.Texture:
                    asset = DownloadHandlerTexture.GetContent(_unityWebRequest);
                    break;
                case eLoadWebAssetType.Audio:
                    asset = DownloadHandlerAudioClip.GetContent(_unityWebRequest);
                    break;
                case eLoadWebAssetType.Text:
                    asset = _unityWebRequest.downloadHandler.text;
                    break;
                case eLoadWebAssetType.Byte:
                default:
                    asset = _unityWebRequest.downloadHandler.data;
                    break;
            }
            LoadWebAssetComplete?.Invoke(_unityWebAssetUrl, asset);
        }

        public void Reset()
        {
            if (_unityWebRequest != null)
            {
                _unityWebRequest.Dispose();
                _unityWebRequest = null;
            }
            _unityWebAssetUrl = null;
            _lastProgress = 0f;
        }

        public void Dispose()
        {
            Reset();
            LoadWebAssetUpdate = null;
            LoadWebAssetComplete = null;
            LoadWebAssetError = null;
        }

        public bool isFree()
        {
            return _unityWebRequest == null;
        }
    }
}