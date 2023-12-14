using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private int activeMenuIndex = 0;
    private readonly string[] buttonNames = {
        "Quit", "Previous", "Next", "CloseButton", "SkipButton", "BirdButton", "Help", "ToMain", "ToStart"
    };
    private readonly string[] menuNames = {
        "Welcome", "Tutorial-1", "TutorialComplete", "Start", "Main", "HelpMenu", "Confirmation"
    };
    private Button[] buttons;
    // private string[] menuNames = {"Welcome"};
    private VisualElement[] menus;


    // Start is called before the first frame update
    void Start()
    {   
        buttons = new Button[buttonNames.Length];
        menus = new VisualElement[menuNames.Length];
        var root = GetComponent<UIDocument>().rootVisualElement;
        int i = 0;
        foreach (string name in menuNames)
        {
            VisualElement menu = root.Q<VisualElement>(name);
            menus[i] = menu;
            i++;
        }
        ChangeActiveMenu("Welcome");
    }

    void ChangeActiveMenu(string name)
    {
        int index = 0;
        foreach (VisualElement menu in menus)
        {
            menu.style.display = DisplayStyle.None;
            if (menu.name == name)
            {
                menu.style.display = DisplayStyle.Flex;
                if (menu.name == "HelpMenu") {
                    Debug.Log(menu.style);
                }
                activeMenuIndex = index;
            }
            index++;
        }
        SetButtons();
    }

    void SetButtons()
    {
        buttons = new Button[buttonNames.Length];
        foreach (string buttonName in buttonNames)
        {
            Button button = menus[activeMenuIndex].Q<Button>(buttonName);
            if (button == null)
            {
                continue;
            }
            switch (buttonName)
            {
                case "Quit":
                    button.RegisterCallback<ClickEvent>(_e => {
                        Application.Quit();
                    });
                    buttons[0] = button;
                    break;
                case "Previous":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu(menuNames[activeMenuIndex - 1]);
                    });
                    buttons[1] = button;
                    break;
                case "Next":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu(menuNames[activeMenuIndex + 1]);
                    });
                    buttons[2] = button;
                    break;
                case "CloseButton":
                    button.RegisterCallback<ClickEvent>(_e => {
                        if (menuNames[activeMenuIndex] == "Main" || menuNames[activeMenuIndex] == "TutorialComplete") {
                            ToggleMenu();
                        } else {
                            ChangeActiveMenu("Main");
                        }
                    });
                    buttons[3] = button;
                    break;
                case "SkipButton":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("Start");
                    });
                    buttons[4] = button;
                    break;
                case "BirdButton":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ToggleMenu();
                    });
                    buttons[5] = button;
                    break;
                case "Help":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("HelpMenu");
                    });
                    buttons[6] = button;
                    break;
                case "ToMain":
                    button.RegisterCallback<ClickEvent>(_e => {
                        Debug.Log("ToMain");
                        ChangeActiveMenu("Main");
                    });
                    buttons[7] = button;
                    break;
                case "ToStart":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("Start");
                    });
                    buttons[8] = button;
                    break;
                default:
                    break;
            }
        }
    }

    void ToggleMenu() {
        VisualElement menuButtons = menus[activeMenuIndex].Q<VisualElement>("MenuButtons");
        if (menuButtons == null) return;
        if (menuButtons.style.display == DisplayStyle.None) {
            menuButtons.style.display = DisplayStyle.Flex;
        } else {
            menuButtons.style.display = DisplayStyle.None;
        }
        if (menuNames[activeMenuIndex] == "Main") {
            Button closeButton = menus[activeMenuIndex].Q<Button>("CloseButton");
            if (closeButton == null) return;
            if (closeButton.style.display == DisplayStyle.None) {
                closeButton.style.display = DisplayStyle.Flex;
            } else {
                closeButton.style.display = DisplayStyle.None;
            }
        }
    }
}   
