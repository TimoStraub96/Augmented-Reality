using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private int activeMenuIndex = 0;
    private readonly string[] buttonNames = {
        "Quit", "Previous", "Next", "CloseButton", "SkipButton",
        "toTutorialHelp", "toTutorialPets", "toTutorialMap", "toTutorialTrophy", "toTutorialSettings",
        "BirdButton", "Help", "Pets", "Map", "Trophy", "Settings", "ToMain", "toStart",
        "toMammut", "toNashorn", "toHirsch", "toLoewe", "More", "Less"
    };
    private readonly string[] menuNames = {
        "Welcome", "Tutorial-1", "Tutorial-2", "Tutorial-3", "TutorialComplete",
        "TutorialHelp", "TutorialPets", "TutorialMap", "TutorialTrophy", "TutorialSettings",
        "Start", "Main", "HelpMenu", "PetsMenu", "TrophyMenu", "Confirmation",
        "MammutInfo", "HirschInfo", "NashornInfo", "LoeweInfo"
    };
    private Button[] buttons;
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
                case "Pets":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("PetsMenu");
                    });
                    buttons[7] = button;
                    break;
                case "Map":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("Confirmation");
                    });
                    buttons[8] = button;
                    break;
                case "Trophy":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("TrophyMenu");
                    });
                    buttons[9] = button;
                    break;
                case "Settings":
                    break;
                case "ToMain":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("Main");
                    });
                    buttons[11] = button;
                    break;
                case "toStart":
                    Debug.Log("test 1");
                    button.RegisterCallback<ClickEvent>(_e => {
                        Debug.Log("test 2");
                        ChangeActiveMenu("Start");
                    });
                    buttons[12] = button;
                    break;
                case "toTutorialHelp":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("TutorialHelp");
                    });
                    buttons[13] = button;
                    break;
                case "toTutorialPets":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("TutorialPets");
                    });
                    buttons[14] = button;
                    break;
                case "toTutorialMap":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("TutorialMap");
                    });
                    buttons[15] = button;
                    break;
                case "toTutorialTrophy":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("TutorialTrophy");
                    });
                    buttons[16] = button;
                    break;
                case "toTutorialSettings":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("TutorialSettings");
                    });
                    buttons[17] = button;
                    break;
                case "toMammut":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("MammutInfo");
                    });
                    break;
                case "toNashorn":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("NashornInfo");
                    });
                    break;
                case "toHirsch":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("HirschInfo");
                    });
                    break;
                case "toLoewe":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ChangeActiveMenu("LoeweInfo");
                    });
                    break;
                case "More":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ToggleInfo();
                    });
                    break;
                case "Less":
                    button.RegisterCallback<ClickEvent>(_e => {
                        ToggleInfo();
                    });
                    break;
                default:
                    break;
            }
        }
    }

    void ToggleInfo() {
        VisualElement small = menus[activeMenuIndex].Q<VisualElement>("Small");
        VisualElement extended = menus[activeMenuIndex].Q<VisualElement>("Extended");
        small.ToggleInClassList("info-hidden");
        extended.ToggleInClassList("info-hidden");
    }

    void ToggleMenu() {
        VisualElement menuButtons = menus[activeMenuIndex].Q<VisualElement>("MenuButtons");
        if (menuButtons == null) return;
        menuButtons.ToggleInClassList("menu-buttons-visible");
        if (menuNames[activeMenuIndex] == "Main") {
            Button closeButton = menus[activeMenuIndex].Q<Button>("CloseButton");
            if (closeButton == null) return;
            closeButton.ToggleInClassList("main-close-button-visible");
        }
    }
}   
