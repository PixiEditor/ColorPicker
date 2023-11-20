[![Discord Server](https://badgen.net/badge/discord/join%20chat/7289DA?icon=discord)](https://discord.gg/qSRMYmq)
[![Download](https://img.shields.io/badge/nuget-download-blue)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)
[![Downloads](https://img.shields.io/nuget/dt/PixiEditor.ColorPicker)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)

# WPF version of ColorPicker

![demo project](https://i.imgur.com/wZkkykY.png)

# Example Usage

See [ColorPickerDemo](https://github.com/PixiEditor/ColorPicker/tree/master/ColorPickerDemo) for an example project.

**Basic usage:**

Install the NuGet package, insert a reference to the ColorPicker namespace

```xml
<Window ...
xmlns:colorpicker="clr-namespace:ColorPicker;assembly=ColorPicker"
...>
```

Add the controls

```xml
<colorpicker:StandardColorPicker x:Name="main" Width="200" Height="380"/>
<colorpicker:PortableColorPicker ColorState="{Binding ElementName=main, Path=ColorState, Mode=TwoWay}" Width="40" Height="40"/>
```

Note: in some configurations such as using the package in .NET Framework 4.7 the XAML designer tends to break and not
show the control.

# Styling

Out of the box, the color picker uses the default WPF look:

![Default ColorPicker look](https://i.imgur.com/AyweTmS.png)

You may use the included PixiEditor's dark theme by loading a resource dictionary in XAML:

```xml
<Window.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/ColorPicker;component/Styles/DefaultColorPickerStyle.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Window.Resources>
```

and referencing DefaultColorPickerStyle in the style attribute of a control:

```xml
<colorpicker:StandardColorPicker Style="{StaticResource DefaultColorPickerStyle}" />
```

As an alternative, the same can be achieved programmatically:

```csharp
var resourceDictionary = new ResourceDictionary();
resourceDictionary.Source = new System.Uri(
    "pack://application:,,,/ColorPicker;component/Styles/DefaultColorPickerStyle.xaml",
    System.UriKind.RelativeOrAbsolute);

StandardColorPicker picker = new StandardColorPicker()
{
    Style = (Style)resourceDictionary["DefaultColorPickerStyle"]
};
```

You may define your own styles, see
[DefaultColorPickerStyle](https://github.com/PixiEditor/ColorPicker/blob/master/src/ColorPicker/Styles/DefaultColorPickerStyle.xaml)
for reference.
