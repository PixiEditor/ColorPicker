[![Discord Server](https://badgen.net/badge/discord/join%20chat/7289DA?icon=discord)](https://discord.gg/qSRMYmq)
[![Download](https://img.shields.io/badge/nuget-download-blue)](https://www.nuget.org/packages/PixiEditor.ColorPicker.AvaloniaUI/)
[![Downloads](https://img.shields.io/nuget/dt/PixiEditor.ColorPicker.AvaloniaUI)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)

# AvaloniaUI version of ColorPicker

![demo project](https://i.imgur.com/Jo4J8J7.png)

# Example Usage

See [ColorPickerDemo](https://github.com/PixiEditor/ColorPicker/tree/master/ColorPickerDemo) for an example project.

**Basic usage:**

Install the NuGet package, insert a reference to the ColorPicker namespace

```xml
<Window ...
        xmlns:colorPicker="clr-namespace:ColorPicker;assembly=ColorPicker.AvaloniaUI"
...>
```

Add the controls

```xml
<colorpicker:StandardColorPicker x:Name="main" Width="200" Height="380"/>
<colorpicker:PortableColorPicker ColorState="{Binding #main.ColorState, Mode=TwoWay}" Width="40" Height="40"/>
```

# Styling

Out of the box, the color picker doesn't have a look.
You must select a theme for it. There are 2 bundled themes:

- SimpleColorPickerTheme - Basic theme, as close as possible to Avalonia's SimpleTheme
- PixiPerfectColorPickerTheme - Our custom theme used in PixiEditor

Both themes are available in Dark and Light variants.

![SimpleColorPickerTheme](https://i.imgur.com/SF1F9ba.png)

To use a theme, go to your App.xaml and add the following:

```xml
<Application ...
        xmlns:templates="clr-namespace:ColorPicker.AvaloniaUI.Templates;assembly=ColorPicker.AvaloniaUI">
 <Application.Styles>
        ...
        <templates:SimpleColorPickerTheme /> <!-- or -->
        <templates:PixiPerfectColorPickerTheme />
    </Application.Styles>
   ```

You may define your own themes, AvaloniaUI version of ColorPicker is built with TemplatedControls, so look can be fully customized.

Use [SimpleColorPickerTheme](https://github.com/PixiEditor/ColorPicker/tree/master/src/ColorPicker.AvaloniaUI/Templates) as a reference.
