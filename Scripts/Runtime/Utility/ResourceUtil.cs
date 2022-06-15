/*
 * @Author: xiang huan
 * @Date: 2022-05-20 14:20:27
 * @LastEditTime: 2022-06-13 19:31:32
 * @LastEditors: xiang huan
 * @Description: 资源工具类
 * @FilePath: /meland-unity/Assets/Thirdparty/UnityGameFramework/Scripts/Runtime/Utility/ResourceUtil.cs
 * 
 */

using System.IO;
using UnityEngine;
namespace UnityGameFramework.Runtime
{
    public static class ResourceUtil
    {
        public static eLoadWebAssetType GetWebAssetType(string ext)
        {
            switch (ext)
            {
                case ".json":
                case ".text":
                    return eLoadWebAssetType.Text;
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".gif":
                    return eLoadWebAssetType.Texture;
                case ".mp3":
                case ".wav":
                case ".ogg":
                    return eLoadWebAssetType.Audio;
                default:
                    return eLoadWebAssetType.Byte;
            }
        }

        public static AudioType GetAudioType(string ext)
        {
            switch (ext)
            {
                case ".mp3":
                    return AudioType.MPEG;
                case ".wav":
                    return AudioType.WAV;
                case ".ogg":
                    return AudioType.OGGVORBIS;
                default:
                    return AudioType.UNKNOWN;
            }

        }
    }
}
