﻿using System.Collections.Generic;
using MLAPI;
using UnityEngine;

namespace UnityEditor
{
    [CustomEditor(typeof(NetworkedObject), true)]
    [CanEditMultipleObjects]
    public class NetworkedObjectEditor : Editor
    {
        private bool initialized;
        private NetworkedObject networkedObject;
        private bool showObservers;

        private void Init()
        {
            if (initialized)
                return;
            initialized = true;
            networkedObject = (NetworkedObject)target;
        }

        public override void OnInspectorGUI()
        {
            Init();
            if (NetworkingManager.Singleton == null || (!NetworkingManager.Singleton.IsServer && !NetworkingManager.Singleton.IsClient))
                base.OnInspectorGUI(); //Only run this if we are NOT running server. This is where the ServerOnly box is drawn

            if (!networkedObject.IsSpawned && NetworkingManager.Singleton != null && NetworkingManager.Singleton.IsServer)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Spawn", "Spawns the object across the network"));
                if (GUILayout.Toggle(false, "Spawn", EditorStyles.miniButtonLeft))
                {
                    networkedObject.Spawn();
                    EditorUtility.SetDirty(target);
                }
                EditorGUILayout.EndHorizontal();
            }
            else if (networkedObject.IsSpawned)
            {
                EditorGUILayout.LabelField("PrefabName: ", networkedObject.NetworkedPrefabName, EditorStyles.label);
                EditorGUILayout.LabelField("PrefabHash: ", networkedObject.NetworkedPrefabHash.ToString(), EditorStyles.label);
                EditorGUILayout.LabelField("NetworkId: ", networkedObject.NetworkId.ToString(), EditorStyles.label);
                EditorGUILayout.LabelField("OwnerId: ", networkedObject.OwnerClientId.ToString(), EditorStyles.label);
                EditorGUILayout.LabelField("IsSpawned: ", networkedObject.IsSpawned.ToString(), EditorStyles.label);
                EditorGUILayout.LabelField("IsLocalPlayer: ", networkedObject.IsLocalPlayer.ToString(), EditorStyles.label);
                EditorGUILayout.LabelField("IsOwner: ", networkedObject.IsOwner.ToString(), EditorStyles.label);
				EditorGUILayout.LabelField("IsOwnedByServer: ", networkedObject.IsOwnedByServer.ToString(), EditorStyles.label);
                EditorGUILayout.LabelField("IsPlayerObject: ", networkedObject.IsPlayerObject.ToString(), EditorStyles.label);

                if (NetworkingManager.Singleton != null && NetworkingManager.Singleton.IsServer)
                {
                    showObservers = EditorGUILayout.Foldout(showObservers, "Observers");

                    if (showObservers)
                    {
                        HashSet<uint>.Enumerator observerClientIds = networkedObject.GetObservers();
                    
                        EditorGUI.indentLevel += 1;
                        
                        while (observerClientIds.MoveNext())
                        {
                            if (NetworkingManager.Singleton.ConnectedClients[observerClientIds.Current].PlayerObject != null)
                                EditorGUILayout.ObjectField("ClientId: " + observerClientIds.Current, NetworkingManager.Singleton.ConnectedClients[observerClientIds.Current].PlayerObject, typeof(GameObject), false);
                            else
                                EditorGUILayout.TextField("ClientId: " + observerClientIds.Current, EditorStyles.label);
                        }
                        
                        EditorGUI.indentLevel -= 1;
                    }
                }
            }
        }
    }
}