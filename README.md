[![Discord Server](https://badgen.net/badge/discord/join%20chat/7289DA?icon=discord)](https://discord.gg/qSRMYmq)
[![Download](https://img.shields.io/badge/nuget-download-blue)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)
[![Downloads](https://img.shields.io/nuget/dt/PixiEditor.ColorPicker)](https://www.nuget.org/packages/PixiEditor.ColorPicker/)

# About

A collection of various WPF controls used to select colors. 
Supports .NET Framework 4.5.1+, .NET Core 3.1+, .NET 5 and .NET 6. 
Originally developed for [PixiEditor](https://github.com/PixiEditor/PixiEditor).

![screenshot](https://i.imgur.com/4ysN4Fe.png)

1. [Included Controls](#controls)
1. [Example Usage](#example)
1. [Properties](#properties)
1. [Styling](#styling)
1. [Other](#other)

# Included Controls<a name="controls">

- `SquarePicker`: A HSV/HSL Color Picker, consists of a circular hue slider and HV/HL square.
- `ColorSliders`: A set of HSV/RGB + Alpha sliders
- `HexColorTextBox`: An RGBA Hex text field
- `ColorDisplay`: A Primary/Secondary Color display with a swap button
- `StandardColorPicker`: Combines everything listed above in one control
- `PortableColorPicker`: A collapsible version of StandardColorPicker
- `AlphaSlider`: A separate alpha slider control

![demo project](https://i.imgur.com/wZkkykY.png)
# Example Usage<a name="example">

See [ColorPickerDemo](https://github.com/PixiEditor/ColorPicker/tree/master/ColorPickerDemo) for an example project.

**Basic usage:**

Install the NuGet package, insert a reference to the ColorPicker namespace
```
<Window ...
xmlns:colorpicker="clr-namespace:ColorPicker;assembly=ColorPicker"
...>
```
Add the controls
```
<colorpicker:StandardColorPicker x:Name="main" Width="200" Height="380"/>
<colorpicker:PortableColorPicker ColorState="{Binding ElementName=main, Path=ColorState, Mode=TwoWay}" Width="40" Height="40"/>
```
Note: in some configurations such as using the package in .NET Framework 4.7 the XAML designer tends to break and not show the control.

# Properties<a name="properties">

All controls share these properties:

- `SelectedColor` dependency property stores the current color as System.Windows.Media.Color
- `ColorChanged`: An event that fires on SelectedColor change.
- `ColorState` dependency property contains all info about the current state of the control. Use this property to bind controls together.
- `Color` property contains nested properties you may bind to or use to retrieve the color in code-behind:
    - `Color.A`: Current Alpha, a double ranging from 0 to 255
    - `Color.RGB_R`, `Color.RGB_G`, `Color.RGB_B`: Dimensions of the RGB color space, each is a 0-255 double
    - `Color.HSV_H`: Hue in the HSV color space, a 0-360 double 
    - `Color.HSV_S`: Saturation in the HSV color space, a 0-100 double
    - `Color.HSV_V`: Value in the HSV color space, a 0-100 double
    - `Color.HSL_H`: Hue in the HSL color space, a 0-360 double 
    - `Color.HSL_S`: Saturation in the HSL color space, a 0-100 double
    - `Color.HSL_L`: Lightness in the HSL color space, a 0-100 double


Apart from those, some controls have unique properties:

- `SecondColorState`, `SecondColor`, and `SecondaryColor` are functionally identical to `ColorState`, `Color`, and `SelectedColor` respectively. 
These are present on controls that have a secondary color.
- `HintColorState`, `HintNotifyableColor`, and `HintColor` are functionally identical to `ColorState`, `Color`, and `SelectedColor` respectively. 
These are present on controls that have a hint color. The hint color is a color field that can be used to obtain the primary color from an external source when the user clicks a button.
- `UseHintColor` enables the hint color or disables it (disabled by default).
- `SmallChange` lets you change `SmallChange` of sliders, which is used as sensitivity for when the user
turns the scroll wheel with the cursor over the sliders. Present on controls with sliders.
- `ShowAlpha` lets you hide the alpha channel on various controls. 
Present on all controls containing either an alpha slider (apart from the `AlphaSlider` control itself) or a hex color textbox.
- `PickerType`: HSV or HSL, present on `SquarePicker` or controls that contain `SquarePicker`.

# Styling<a name="styling">

Out of the box, the color picker uses the default WPF look:

![Default ColorPicker look](https://i.imgur.com/AyweTmS.png)

You may use the included PixiEditor's dark theme by loading a resource dictionary in XAML:
```
<Window.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/ColorPicker;component/Styles/DefaultColorPickerStyle.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Window.Resources>
```
and referencing DefaultColorPickerStyle in the style attribute of a control:
```
<colorpicker:StandardColorPicker Style="{StaticResource DefaultColorPickerStyle}" />
```

As an alternative, the same can be achieved programmatically:
```
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

# Other<a name="other">

Read flabbet's article on the theory behind the first version of this project on [dev.to](https://dev.to/flabbet/how-does-color-pickers-work-1275)
