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

    // список Actions (screen), который состоит из блоков Action (screen). 
    public List<Action> actions;

    // Блоки могут быть двух видов: 1-Inventory Item, 2-Open Scene (на видео больше, нам достаточно двух). 

    // Inventory Item содержит поле Item с перечислением доступных Inventory Item (список подтягивать из txt).
    // И enum статуса (get, use).

    // Open Scene  имеет список сцен из EditorBuildSettings и кнопку Select для открытия выбранной сцены. 
    // Добавить возможность создавать/удалять блоки. Желательно добавить возможность перемещать блоки. 

    // Add menu named "Settings" to the Window menu

    public Action.ActionType actionType;

    //Parse File
    public static string itemListPath = "ItemList.txt";
    public System.IO.FileStream itemListPath1;
    static string text = File.ReadAllText(itemListPath);

    static char[] separators = { '\n' };
    List<String> itemList = text.Split(separators).ToList();

    void OnGUI()
    {
        GUILayout.Label("Actions: ", EditorStyles.boldLabel);

        for (int i = 0; i < actions.Count; i++)
        {
            //действие
            GUILayout.BeginArea(new Rect(0, 0, 50, 100));
            GUILayout.Label("Action ");

            //лист действий

            //лист итемов
            for (int j = 0; j < itemList.Count; j++)
            {
                itemList[j] = (Bar)EditorGUILayout.ObjectField(itemList[j], typeof(Bar));
            }
            int newCount = Mathf.Max(0, EditorGUILayout.IntField("size", itemList.Count));
            while (newCount < itemList.Count)
                itemList.RemoveAt(itemList.Count - 1);
            while (newCount > itemList.Count)
                itemList.Add(null);

            // удалить действие
            if (GUILayout.Button("X"))
            {
                actions.Remove(actions[i]);
            }
            GUILayout.EndArea();
        }

        //GameObject.DestroyImmediate(actions[0]);
        //Destroy(actions[0]);

        //myString = EditorGUILayout.TextField("Text Field", myString);
        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Add Action"))
        {
            AddAction();
        }
    }

    public void AddAction()
    {
        actions.Add(new Action(actionType));
    }
}
