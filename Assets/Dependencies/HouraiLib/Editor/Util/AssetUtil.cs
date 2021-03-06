using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HouraiTeahouse.Editor {

    /// <summary>
    /// Utility class for handling Assets in the Unity Editor.
    /// </summary>
    [InitializeOnLoad]
    public static class AssetUtil {

        private static Dictionary<string, string> delayedMoves;

        static AssetUtil() {
            delayedMoves = new Dictionary<string, string>();
            EditorApplication.update += Update;
        }
        
        static void Update() {
            if (delayedMoves.Count <= 0)
                return;

            List<string> toRemove = new List<string>();

            foreach (KeyValuePair<string, string> pair in delayedMoves) {
                string result = AssetDatabase.ValidateMoveAsset(pair.Key, pair.Value);
                if (!string.IsNullOrEmpty(result))
                    continue;
                AssetDatabase.MoveAsset(pair.Key, pair.Value);
                toRemove.Add(pair.Key);
            }

            foreach (string key in toRemove)
                delayedMoves.Remove(key);
        }

        /// <summary>
        /// Create new asset from <see cref="ScriptableObject"/> type with unique name at
        /// selected folder in project window. Asset creation can be cancelled by pressing
        /// escape key when asset is initially being named.
        /// </summary>
        /// <typeparam name="T">Type of scriptable object.</typeparam>
        public static T CreateAssetInProjectWindow<T>(T asset = null) where T : ScriptableObject {
            if(asset == null)
                asset = ScriptableObject.CreateInstance<T>();
            ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + ".asset");
            return asset;
        }

        public static string GetAssetFolderPath(Object asset) {
            if(!asset)
                throw new ArgumentNullException("asset");
            return Regex.Replace(AssetDatabase.GetAssetPath(asset), "(Assets)+/(.*)/.*?\\..*", "$2");
        }
        
        public static bool IsAsset(this Object obj) {
            return obj != null && AssetDatabase.Contains(obj);
        }

        public static string CreateAssetPath(params string[] folderNames) {
            if (folderNames == null)
                throw new ArgumentNullException("folderNames");

            if (folderNames.Length <= 0)
                return "";

            var builder = new StringBuilder();
            builder.Append(folderNames[0]);
            for (var i = 1; i < folderNames.Length; i++) {
                builder.Append('/');
                builder.Append(folderNames[i]);
            }
            return builder.ToString();
        }

        public static bool IsValidFolder(string path) {
            return AssetDatabase.IsValidFolder("Assets/" + path);
        }

        public static void CreateAsset(string folder, Object obj, string suffix = null) {
            if (folder == null)
                throw new ArgumentNullException("folder");
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (suffix == null)
                suffix = "asset";

            if (obj.IsAsset())
                return;

            // Create folder if it doesn't already exist
            CreateFolder(folder);

            AssetDatabase.CreateAsset(obj, "Assets/" + folder + "/" + obj.name + "." + suffix);
        }

        public static void MoveAsset(string targetFolder, Object asset) {
            if (targetFolder == null)
                throw new ArgumentNullException("targetFolder");
            if (asset == null)
                throw new ArgumentNullException("asset");

            if (!asset.IsAsset()) {
                var gameObject = asset as GameObject;
                var component = asset as Component;
                if (component != null)
                    gameObject = component.gameObject;
                if (gameObject != null) {
                    // Assign asset to the object's prefab
                    Debug.Log("Is a GameObject, extracting Prefab...");
                    asset = asset.GetPrefab();
                }
            }

            if (asset.IsAsset()) {
                string assetPath = AssetDatabase.GetAssetPath(asset);

                // Create the folder if it doesn't already exist
                CreateFolder(targetFolder);

                string destination = "Assets/" + targetFolder + "/" + Path.GetFileName(assetPath);
                string result = AssetDatabase.ValidateMoveAsset(assetPath, destination);

                if (string.IsNullOrEmpty(result))
                    AssetDatabase.MoveAsset(assetPath, destination);
                else
                    delayedMoves.Add(assetPath, destination);
            }
        }

        public static string[] FindAssetPaths<T>(string nameFilter = null) where T : Object {
            List<string> paths = new List<string>();
            string search = "t:" + typeof (T).Name;
            if (nameFilter != null)
                search += " " + nameFilter;
            foreach (string guid in AssetDatabase.FindAssets(search))
                paths.Add(AssetDatabase.GUIDToAssetPath(guid));
            return paths.ToArray();
        }

        public static bool IsResource(Object asset) {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            return !string.IsNullOrEmpty(assetPath) && assetPath.Contains("Resources/");
        }

        public static string GetResourcePath(Object asset) {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(assetPath) || !assetPath.Contains("Resources/"))
                return string.Empty;
            else
                return Regex.Replace(assetPath, ".*/Resources/(.*?)\\..*", "$1");
        }

        public static void CreateFolder(string path) {
            if (path == null)
                throw new ArgumentNullException("path");

            if (IsValidFolder(path))
                return;

            string[] folders = path.Split('/');
            var currentPath = "Assets";
            for (var i = 0; i < folders.Length; i++) {
                if (string.IsNullOrEmpty(folders[i]))
                    continue;

                string newPath = currentPath + "/" + folders[i];

                if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                    AssetDatabase.CreateFolder(currentPath, folders[i]);
                currentPath = newPath;
            }
        }

    }

}
