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
    [SerializeField] public GameObject ContentPanel;
    [SerializeField] public Button BaseButton;
    [SerializeField] public Button CancelButton;
    [SerializeField] public Text NoElementsLabel;
    [SerializeField] public Button CreateNewButton;

    public void PromptCreateList()
    {
        renameListPopupComponent.gameObject.SetActive(true);
        renameListPopupComponent.gameObject.transform.SetAsLastSibling();
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
        customListList.gameObject.SetActive(true);
		customListList.GetComponent<RectTransform>().sizeDelta = this.GetComponent<RectTransform>().sizeDelta;

        customListList.CancelButton.onClick.RemoveAllListeners();
        customListList.CancelButton.onClick.AddListener(() => {
            Destroy(customListList.gameObject);
        });

        Button[] buttons = customListList.ContentPanel.GetComponentsInChildren<Button>();

        for(int i = 0; i < lists.Count; i++) {
            buttons[i].onClick.RemoveAllListeners();
            ListManager listManager = lists[i];
            buttons[i].onClick.AddListener(() => {
                sceneUIManager.AddLevelToCustomList(listManager, levelButtonListItem);
                Destroy(customListList.gameObject);
            });
        }
    }

    public void CreateList(int id, string name, ListManager lM)
    {
        ListManager listManager = lM.gameObject.AddComponent<ListManager>();

        listManager.Name = name;
        listManager.Id = id;
        listManager.ContentPanel = lM.ContentPanel;
        listManager.ListUI = lM.ListUI;
        listManager.SceneManager = lM.SceneManager;
        listManager.InformationPopup = lM.InformationPopup;
        listManager.deleteButton = lM.deleteButton;
        listManager.editButton = lM.editButton;
        listManager.saveButton = lM.saveButton;
        listManager.editNameInputField = lM.editNameInputField;

        lists.Add(listManager);

        CreateButton(listManager);

        NoElementsLabel.gameObject.SetActive(false);
    }

    private void CreateButton(ListManager listManager) {
        Button listButton = Instantiate(BaseButton);
        listButton.transform.SetParent(ContentPanel.transform);
        listButton.gameObject.SetActive(true);
		listButton.transform.localScale = 1.66F * Vector3.one;
        listButton.GetComponentInChildren<Text>().text = listManager.Name;
        listButton.onClick.AddListener(() => {
            sceneUIManager.SetCustomListActive(listManager);
            sceneUIManager.ShowUI(sceneUIManager.m_ListaPersonalizadaUI);
            gameObject.SetActive(false);
        });
    }

    public void RemoveList(ListManager listManager) {
        listManager.RemoveAll();
        lists.Remove(listManager);

        ReorderButtons();
    }

    public void ReorderButtons() {
        Button[] buttons = ContentPanel.GetComponentsInChildren<Button>();

        foreach(Button button in buttons) {
            Destroy(button.gameObject);
        }

        foreach(ListManager listManager in lists) {
            CreateButton(listManager);
        }

        if (lists.Count == 0) {
            NoElementsLabel.gameObject.SetActive(true);
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

        Button[] buttons = ContentPanel.GetComponentsInChildren<Button>();

        foreach(Button button in buttons) {
            Destroy(button.gameObject);
        }
        
    }
}
