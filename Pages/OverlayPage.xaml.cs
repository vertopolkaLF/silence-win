using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using silence_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Graphics;

namespace silence_.Pages;

public sealed partial class OverlayPage : Page
{
    private List<ScreenInfo> _screens = new();
    private bool _isInitializing = true;

    public OverlayPage()
    {
        InitializeComponent();
        LoadSettings();
        LoadScreens();
        _isInitializing = false;
    }

    private void LoadSettings()
    {
        var settings = App.Instance?.SettingsService.Settings;
        if (settings == null) return;

        OverlayEnabledToggle.IsOn = settings.OverlayEnabled;
        UpdatePanelsEnabled(settings.OverlayEnabled);

        // Set visibility mode
        foreach (var item in VisibilityModeSelector.Items.Cast<RadioButton>())
        {
            if (item.Tag?.ToString() == settings.OverlayVisibilityMode)
            {
                item.IsChecked = true;
                break;
            }
        }

        UpdatePositionText(settings);
    }

    private void LoadScreens()
    {
        _screens.Clear();
        ScreenComboBox.Items.Clear();

        // Add "Primary Screen" option
        _screens.Add(new ScreenInfo 
        { 
            DisplayName = "Primary Screen", 
            DeviceName = "PRIMARY",
            IsPrimary = true
        });

        // Get all display areas
        var displayAreas = GetAllDisplayAreas();
        int screenIndex = 1;
        
        foreach (var area in displayAreas)
        {
            var screenInfo = new ScreenInfo
            {
                DisplayName = $"Screen {screenIndex} ({area.WorkArea.Width}x{area.WorkArea.Height})",
                DeviceName = area.DisplayId.Value.ToString(),
                WorkArea = area.WorkArea,
                IsPrimary = false
            };
            _screens.Add(screenInfo);
            screenIndex++;
        }

        foreach (var screen in _screens)
        {
            ScreenComboBox.Items.Add(screen.DisplayName);
        }

        // Select the saved screen
        var settings = App.Instance?.SettingsService.Settings;
        if (settings != null)
        {
            var savedScreen = settings.OverlayScreenId;
            if (string.IsNullOrEmpty(savedScreen) || savedScreen == "PRIMARY")
            {
                ScreenComboBox.SelectedIndex = 0;
            }
            else
            {
                var index = _screens.FindIndex(s => s.DeviceName == savedScreen);
                ScreenComboBox.SelectedIndex = index >= 0 ? index : 0;
            }
        }
        else
        {
            ScreenComboBox.SelectedIndex = 0;
        }
    }

    private List<Microsoft.UI.Windowing.DisplayArea> GetAllDisplayAreas()
    {
        var areas = new List<Microsoft.UI.Windowing.DisplayArea>();
        
        try
        {
            // Get all display areas
            var displayAreas = Microsoft.UI.Windowing.DisplayArea.FindAll();
            areas.AddRange(displayAreas);
        }
        catch
        {
            // Fallback to primary
        }

        return areas;
    }

    private void UpdatePositionText(AppSettings settings)
    {
        string horizontalPos;
        string verticalPos;

        // Horizontal position
        if (Math.Abs(settings.OverlayPositionX - 50) < 1)
        {
            horizontalPos = "Center";
        }
        else if (settings.OverlayPositionX < 50)
        {
            horizontalPos = $"Left {settings.OverlayPositionX:F0}%";
        }
        else
        {
            horizontalPos = $"Right {100 - settings.OverlayPositionX:F0}%";
        }

        // Vertical position
        if (Math.Abs(settings.OverlayPositionY - 50) < 1)
        {
            verticalPos = "Middle";
        }
        else if (settings.OverlayPositionY < 50)
        {
            verticalPos = $"Top {settings.OverlayPositionY:F0}%";
        }
        else
        {
            verticalPos = $"Bottom {100 - settings.OverlayPositionY:F0}%";
        }

        CurrentPositionText.Text = $"Current: {horizontalPos}, {verticalPos}";
    }

    private void UpdatePanelsEnabled(bool enabled)
    {
        VisibilityModePanel.Opacity = enabled ? 1.0 : 0.5;
        ScreenSelectionPanel.Opacity = enabled ? 1.0 : 0.5;
        PositionPanel.Opacity = enabled ? 1.0 : 0.5;
        VisibilityModePanel.IsHitTestVisible = enabled;
        ScreenSelectionPanel.IsHitTestVisible = enabled;
        PositionPanel.IsHitTestVisible = enabled;
    }

    private void OverlayEnabledToggle_Toggled(object sender, RoutedEventArgs e)
    {
        if (_isInitializing) return;

        var enabled = OverlayEnabledToggle.IsOn;
        App.Instance?.SettingsService.UpdateOverlayEnabled(enabled);
        UpdatePanelsEnabled(enabled);
        
        // Show/hide overlay based on setting
        App.Instance?.UpdateOverlayVisibility();
    }

    private void VisibilityModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing) return;

        if (VisibilityModeSelector.SelectedItem is RadioButton selectedButton)
        {
            var mode = selectedButton.Tag?.ToString() ?? "WhenMuted";
            App.Instance?.SettingsService.UpdateOverlayVisibilityMode(mode);
            App.Instance?.UpdateOverlayVisibility();
        }
    }

    private void ScreenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing || ScreenComboBox.SelectedIndex < 0) return;

        var selectedScreen = _screens[ScreenComboBox.SelectedIndex];
        App.Instance?.SettingsService.UpdateOverlayScreen(selectedScreen.DeviceName);
        App.Instance?.UpdateOverlayPosition();
    }

    private void SetPositionButton_Click(object sender, RoutedEventArgs e)
    {
        // Enter position editing mode - show overlay and make it draggable
        App.Instance?.StartOverlayPositioning();
        SetPositionButton.Visibility = Visibility.Collapsed;
        DonePositionButton.Visibility = Visibility.Visible;
    }

    private void DonePositionButton_Click(object sender, RoutedEventArgs e)
    {
        // Exit position editing mode
        App.Instance?.StopOverlayPositioning();
        SetPositionButton.Visibility = Visibility.Visible;
        DonePositionButton.Visibility = Visibility.Collapsed;
        
        // Refresh position text
        var settings = App.Instance?.SettingsService.Settings;
        if (settings != null)
        {
            UpdatePositionText(settings);
        }
    }

    private void PreviewOverlayButton_Click(object sender, RoutedEventArgs e)
    {
        // Temporarily show the overlay for preview
        App.Instance?.PreviewOverlay();
    }
    
    public void RefreshPositionText()
    {
        var settings = App.Instance?.SettingsService.Settings;
        if (settings != null)
        {
            UpdatePositionText(settings);
        }
        
        // Also reset button visibility
        SetPositionButton.Visibility = Visibility.Visible;
        DonePositionButton.Visibility = Visibility.Collapsed;
    }
}

public class ScreenInfo
{
    public string DisplayName { get; set; } = "";
    public string DeviceName { get; set; } = "";
    public RectInt32 WorkArea { get; set; }
    public bool IsPrimary { get; set; }
}

