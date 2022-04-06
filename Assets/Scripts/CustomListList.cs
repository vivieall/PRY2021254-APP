using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomListList : MonoBehaviour
{
	private List<ListManager> lists = new List<ListManager>();
	[SerializeField] private RenameListPopupComponent renameListPopupComponent;
	[SerializeField] private SceneUIManager sceneUIManager;
	[SerializeField] public Button List1Button;
	[SerializeField] public Button List2Button;
	[SerializeField] public Button List3Button;
    [SerializeField] public Button CreateNewButton;

    public void PromptCreateList()
    {
        renameListPopupComponent.gameObject.SetActive(true);
        renameListPopupComponent.SetSaveButtonAction(() => {
            sceneUIManager.AddCustomList(renameListPopupComponent.nameInput.text);
            renameListPopupComponent.nameInput.text = "";
            renameListPopupComponent.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        });
    }

    public void PromptAddLevelToList(LevelButtonListItem levelButtonListItem)
    {
        CustomListList customListList = Instantiate(this);
        customListList.transform.SetParent(this.transform.parent);
        customListList.transform.localScale = Vector3.one;
        customListList.transform.position = this.transform.position;
        this.gameObject.SetActive(false);
        customListList.CreateNewButton.gameObject.SetActive(false);
        customListList.gameObject.SetActive(true);

        customListList.List1Button.onClick.RemoveAllListeners();
        customListList.List2Button.onClick.RemoveAllListeners();
        customListList.List3Button.onClick.RemoveAllListeners();

        customListList.List1Button.onClick.AddListener(() => {
            sceneUIManager.AddLevelToCustomList(lists[0], levelButtonListItem);
            Destroy(customListList.gameObject);
        });
        customListList.List2Button.onClick.AddListener(() => {
            sceneUIManager.AddLevelToCustomList(lists[1], levelButtonListItem);
            Destroy(customListList.gameObject);
        });
        customListList.List3Button.onClick.AddListener(() => {
            sceneUIManager.AddLevelToCustomList(lists[2], levelButtonListItem);
            Destroy(customListList.gameObject);
        });
    }

    public bool CreateList(string name, ListManager lM)
    {
        if (lists.Count < 3) {
            ListManager listManager = lM.gameObject.AddComponent<ListManager>();

            listManager.Name = name;
            listManager.ContentPanel = lM.ContentPanel;
            listManager.ListUI = lM.ListUI;
            listManager.SceneManager = lM.SceneManager;
            listManager.InformationPopup = lM.InformationPopup;
            listManager.deleteButton = lM.deleteButton;

            lists.Add(listManager);

            Button listButton;
            switch (lists.Count) {
                case 1:
                    listButton = List1Button;
                    break;
                case 2:
                    listButton = List2Button;
                    break;
                default:
                    listButton = List3Button;
                    break;
            }
            listButton.GetComponentInChildren<Text>().text = name;
            listButton.onClick.AddListener(() => {
                sceneUIManager.SetCustomListActive(listManager);
                sceneUIManager.ShowUI(sceneUIManager.m_ListaPersonalizadaUI);
                gameObject.SetActive(false);
            });
            listButton.interactable = true;
            return true;
        }

        return false;
    }

    public void RemoveList(ListManager listManager) {
        listManager.RemoveAll();
        lists.Remove(listManager);

        ReorderButtons();
    }

    private void ReorderButtons() {
        for(int i = 0; i < 3; i++) {
            Button listButton;
            switch (i) {
                case 0:
                    listButton = List1Button;
                    break;
                case 1:
                    listButton = List2Button;
                    break;
                default:
                    listButton = List3Button;
                    break;
            }

            listButton.GetComponentInChildren<Text>().text = "";
            listButton.onClick.RemoveAllListeners();
            listButton.interactable = false;

            if (i < lists.Count) {
                ListManager listManager = lists[i];
                listButton.GetComponentInChildren<Text>().text = listManager.Name;
                listButton.onClick.AddListener(() => {
                    sceneUIManager.SetCustomListActive(listManager);
                    sceneUIManager.ShowUI(sceneUIManager.m_ListaPersonalizadaUI);
                    gameObject.SetActive(false);
                });
                listButton.interactable = true;
            }
        }
    }

    public List<ListManager> getLists()
    {
        return lists;
    }

    public void RemoveAll() {
        foreach(ListManager list in lists) {
            list.RemoveAll();
        }

        lists.Clear();
    }
}
