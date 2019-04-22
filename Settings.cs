using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using System.Linq;

public class Settings : EditorWindow
{
    // кастомное окно Inspector “Settings”.
    [MenuItem("Window/Settings")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        Settings window = (Settings)EditorWindow.GetWindow(typeof(Settings));
        window.Show();
    }
    Vector2 scrollPosition = new Vector2(0, 0);

    //список Actions, который состоит из блоков Action 
    public List<Action> actions = new List<Action>();

    // Parse File "ItemList.txt"
    public static string itemListPath = "ItemList.txt";
    public System.IO.FileStream itemListPath1;
    static string textItemList = File.ReadAllText(itemListPath);
    static char[] separators = { '\n' };
    List<String> itemList = textItemList.Split(separators).ToList();

    // On GUI
    void OnGUI()
    {
        //лист Actions
        GUILayout.Label("Actions: ", EditorStyles.boldLabel);
        // удалить все блоки
        if (GUILayout.Button("Удалить все блоки"))
        {
            actions.Clear();
        }
        // Добавить n блоков
        int n = 5;
        if (GUILayout.Button("Добавить " + n + " блоков"))
        {
            for (int i = 0; i < n; i++)
            {
                actions.Add(new Action());
            }
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        if (actions.Count > 0)
        {
            for (int iAction = 0; iAction < actions.Count; iAction++)
            {
                //Action
                GUILayout.BeginHorizontal(GUILayout.Height(20));
                GUILayout.Label("(" + (iAction + 1) + ") Action");
                // Блоки могут быть двух видов: 1-Inventory Item, 2-Open Scene (на видео больше, нам достаточно двух). 
                //string[] blockTypes = new string[] { "InventoryItem", "OpenScene" };
                // Inventory Item & Open Scene
                if (GUILayout.Button(actions[iAction].actionType.ToString()))
                {
                    actions[iAction].selectBlockType = !actions[iAction].selectBlockType;
                }
                // удалить блок
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    actions.Remove(actions[iAction]);
                }

                //возможность перемещать блок вверх
                if (iAction > 0)
                {
                    if (GUILayout.Button("↑", GUILayout.Width(20)))
                    {
                        //swap с вехрним блоком
                        Action tmp = actions[iAction];
                        actions[iAction] = actions[iAction - 1];
                        actions[iAction - 1] = tmp;
                    }
                }
                else
                {
                    GUILayout.Space(20);//.Label("-");
                }
                GUILayout.EndHorizontal();

                if (actions[iAction].selectBlockType)
                {
                    if (GUILayout.Button(Action.ActionType.InventoryItem.ToString()))
                    {
                        actions[iAction].actionType = Action.ActionType.InventoryItem;
                        actions[iAction].selectBlockType = false;
                    }
                    else if (GUILayout.Button(Action.ActionType.OpenScene.ToString()))
                    {
                        actions[iAction].actionType = Action.ActionType.OpenScene;
                        actions[iAction].selectBlockType = false;
                    }
                }
                GUILayout.BeginHorizontal(GUILayout.Height(20));
                if (actions[iAction].selectBlockType == false)
                {
                    switch (actions[iAction].actionType)
                    {
                        //Inventory Item
                        case Action.ActionType.InventoryItem:
                            // Inventory Item содержит поле Item с перечислением доступных Inventory Item (список подтягивать из txt).
                            GUILayout.Label(Action.ActionType.InventoryItem.ToString());
                            if (GUILayout.Button(actions[iAction].itemName))
                            {
                                actions[iAction].selectInventoryItem = !actions[iAction].selectInventoryItem;
                            }

                            //Debug.Log("Inventory Item");
                            // И enum статуса (get, use).
                            if (GUILayout.Button(actions[iAction].itemStatus.ToString()))
                            {
                                actions[iAction].selectItemStatus = !actions[iAction].selectItemStatus;
                            }
                            break;
                        //Open Scene
                        case Action.ActionType.OpenScene:
                            // Open Scene  имеет список сцен из EditorBuildSettings и кнопку Select для открытия выбранной сцены.
                            GUILayout.Label(Action.ActionType.OpenScene.ToString());
                            if (GUILayout.Button(actions[iAction].sceneName))
                            {
                                actions[iAction].selectScene = !actions[iAction].selectScene;
                            }
                            break;
                        default:
                            break;
                    }
                }
                //возможность перемещать блок вниз
                if (iAction < actions.Count - 1)
                {
                    if (GUILayout.Button("↓", GUILayout.Width(20)))
                    {
                        //swap с нижним блоком
                        Action tmp = actions[iAction];
                        actions[iAction] = actions[iAction + 1];
                        actions[iAction + 1] = tmp;
                    }
                }
                else
                {
                    GUILayout.Space(20);//.Label("-");
                }
                GUILayout.EndHorizontal();
                if (actions[iAction].selectBlockType == false)
                {
                    if (actions[iAction].selectInventoryItem == true)
                    {
                        //лист итемов
                        if (actions[iAction].selectInventoryItem)
                        {
                            for (int iItemList = 0; iItemList < itemList.Count; iItemList++)
                            {
                                if (GUILayout.Button(itemList[iItemList]))
                                {
                                    actions[iAction].itemName = itemList[iItemList];
                                    actions[iAction].selectInventoryItem = false;
                                }
                            }
                        }
                    }
                    if (actions[iAction].selectItemStatus == true)
                    {
                        //selectItemStatus
                        if (GUILayout.Button(Action.ItemStatus.Get.ToString()))
                        {
                            actions[iAction].itemStatus = Action.ItemStatus.Get;
                            actions[iAction].selectItemStatus = false;
                        }
                        else if (GUILayout.Button(Action.ItemStatus.Set.ToString()))
                        {
                            actions[iAction].itemStatus = Action.ItemStatus.Set;
                            actions[iAction].selectItemStatus = false;
                        }
                    }
                    if (actions[iAction].selectScene == true)
                    {
                        EditorBuildSettingsScene[] sceneList = EditorBuildSettings.scenes;
                        //SceneList
                        if (sceneList.Length > 0)
                        {
                            for (int iScene = 0; iScene < sceneList.Length; iScene++)
                            {
                                if (GUILayout.Button(sceneList[iScene].path))
                                {
                                    actions[iAction].sceneName = sceneList[iScene].path;
                                    actions[iAction].selectScene = false;
                                }
                            }
                        }
                    }
                }
                //br
                GUILayout.Label("----------------------------- ↓ -----------------------------");
            }
        }

        //возможность создавать блоки
        if (GUILayout.Button("Add Action", GUILayout.Height(30)))
        {
            actions.Add(new Action());
        }
        GUILayout.EndScrollView();
    }
}
